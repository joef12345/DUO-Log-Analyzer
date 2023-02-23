using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Duo_Log_Analyzer.Properties;
using System.Globalization;

namespace Duo_Log_Analyzer
{
    class Program
    {
        static void Main(string[] args)
        {

            Version AppVersion = Assembly.GetExecutingAssembly().GetName().Version;
            Version ConfigurationVersion = null;

            if (!Version.TryParse(Settings.Default.AppVersion, out ConfigurationVersion))
            {
                Console.WriteLine("Configurion version cannot be determined upgrading config.");
                ConfigurationVersion = Version.Parse("0.0.0.0");
            }

            if (ConfigurationVersion < AppVersion)
            {
                Console.WriteLine("Upgrading config from previous verion...");
                Properties.Settings.Default.Upgrade();
                Settings.Default.AppVersion = AppVersion.ToString();
                Properties.Settings.Default.Save();
            }


            if (args.Contains("-setup", StringComparer.OrdinalIgnoreCase))
            {
                var form = new FormSetup();
                form.Show();
                System.Windows.Forms.Application.Run();
                return;
            }



            Console.WriteLine("Duo Log Analyzer {0} https://github.com/joef12345/DUO-Log-Analyzer", AppVersion.ToString());
            if (!args.Contains("-run", StringComparer.OrdinalIgnoreCase))
            {
                Console.WriteLine(
                    "\nUsage: \n" +
                    "-setup     Displays the setup GUI.\n" +
                    "-run       Scans DUO Logs and sends alerts. (-back [Hours] Request logs from previous time for testing purposes.)\n");
                return;
            }

            string ikey = Properties.Settings.Default.DUOIKey;
            string skey = Properties.Settings.Default.DuoSKey;
            string host = Properties.Settings.Default.DuoHost;
            string NextOffset = "";
            Int64 unixTimestamp = (Int64)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds);
            string LastAccess = Properties.Settings.Default.LastLogFetch;

            if (LastAccess == "")
            {
                LastAccess = unixTimestamp.ToString();
                Properties.Settings.Default.LastLogFetch = unixTimestamp.ToString();
                Properties.Settings.Default.Save();
                Console.WriteLine("Set TimeStamp offset - Run Again.");
                return;
            }

            if (Settings.Default.iCloudPrivateRelayEnabled)
            {
                TimeSpan timeDifference = DateTime.Now - Properties.Settings.Default.ICloudPrivateRelayLastUpdate;
                double totalHours = timeDifference.TotalHours;

                if (totalHours >= 24)
                {
                    iCloudPrivateRelay.UpdatePrivateIPDatabase();
                }

            }

            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
            DateTime LastCheck = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(LastAccess)).UtcDateTime;
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(LastCheck, localTimeZone);

            Console.WriteLine("Getting logs from {0}", localTime.ToString());

            if (args.Contains("-back", StringComparer.OrdinalIgnoreCase))
            {
                int position = 0;
                foreach (var arg in args)
                {

                    if (arg.ToLower() == "-back")
                    {
                        break;
                    }
                    position++;
                }
                {
                    if (args.Length > position)
                    {
                        int hours = 0;
                        if (int.TryParse(args[position + 1], out hours))
                        {
                            DateTime dateTime = DateTimeOffset.FromUnixTimeMilliseconds(unixTimestamp).UtcDateTime;
                            DateTime oneDayEarlier = dateTime.AddHours(hours * -1);
                            LastAccess = new DateTimeOffset(oneDayEarlier).ToUnixTimeMilliseconds().ToString();
                        }
                    }
                }
            }


            do
            {
                var client = new Duo.DuoApi(ikey, skey, host);
                var parameters = new Dictionary<string, string>();

                parameters.Add("mintime", (Int64.Parse(LastAccess) + 1).ToString());
                parameters.Add("maxtime", unixTimestamp.ToString());
                parameters.Add("limit", "1000");
                if (NextOffset != "")
                {
                    parameters.Add("next_offset", NextOffset);
                }

                var r = client.JSONApiCall<Dictionary<string, object>>(
                    "GET", "/admin/v2/logs/authentication", parameters);

                string duo = JsonConvert.SerializeObject(r);

                DUOAPI_Class DuoApiResponse = JsonConvert.DeserializeObject<DUOAPI_Class>(duo);
                if (DuoApiResponse.authlogs.Count == 0) { break; }
                foreach (var item in DuoApiResponse.authlogs)
                {
                    List<string> IgnoreIPList = Properties.Settings.Default.IgnoreIPList.Split(char.Parse("|")).ToList();
                    List<string> IgnoreUserList = Properties.Settings.Default.IgnoreUsers.Split(char.Parse("|")).ToList();
                    Console.WriteLine("Processing logon event at: {0} for user {1}", item.isotimestamp, item.user.name);
                    if (IgnoreUserList.Contains(item.user.name, StringComparer.OrdinalIgnoreCase)) { continue; }

                    IpWhoisIo.IPWhoIS IP = new IpWhoisIo.IPWhoIS();
                    Boolean SecurityEvent = false;
                    string IPInfo = null;
                    if (!IgnoreIPList.Contains(item.access_device.ip) && !IsPrivateIpAddress(item.access_device.ip))
                    {
                        IPInfo = IpWhoisIo.GetFormattedIOWHOINFO(item.access_device.ip, ref SecurityEvent, ref IP);

                        string DuoEvent = string.Format("Application: {3}\nReason: {0}\nAuth Result: {1}\nFactor: {2}\nAuth Device: {4} ",
                            item.reason, item.result, item.factor, item.application.name, item.auth_device.name);
                        if (Properties.Settings.Default.SuspiciousLocation)
                        {
                            if (IP.security.anonymous || IP.security.hosting || IP.security.proxy || IP.security.tor || IP.security.vpn)
                            {
                                if (Properties.Settings.Default.iCloudPrivateRelayEnabled)
                                {
                                    string iCloudRegion = "";
                                    string iCloudLanguage = "";
                                    string iCloudCountryCode = "";

                                    if (iCloudPrivateRelay.IsIPiCloudPrivateIP(item.access_device.ip, ref iCloudCountryCode, ref iCloudLanguage, ref iCloudRegion))
                                    {
                                        if (!Settings.Default.iCloudPrivateRelayIgnore)
                                        {
                                            string iCloudPrivateRelayInfo = string.Format("Country Code: {0}\nRegion: {1}\nLanguage: {2} ",
                                                iCloudCountryCode, iCloudRegion, iCloudLanguage);
                                            AWS.SendSNSMessage(String.Format("Warning: User {0} logged in from an iCloud Private Relay.\n\n{1}\n\nDuo Info:\n{2} \n\nIcloud Private Relay Info:\n{3}", item.user.name, IPInfo, DuoEvent, iCloudPrivateRelayInfo));
                                            continue;
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                }
                                AWS.SendSNSMessage(String.Format("Alert: User {0} logged in from a suspicious location.\n\n{1}\n\nDuo Info:\n{2}", item.user.name, IPInfo, DuoEvent));
                                continue;
                            }
                        }

                        string CountryCode = Settings.Default.OutsideCountryCode;
                        string CountryName = CountryCode;
                        try
                        {
                            RegionInfo region = new RegionInfo(CountryCode);
                            CountryName = region.DisplayName;
                        }
                        catch (ArgumentException)
                        {
                            Console.Write("Invalid Country Code specified in config file!");
                        }


                        if (IP.country_code != CountryCode && Properties.Settings.Default.OutsideCountryEnabled)
                        {
                            AWS.SendSNSMessage(String.Format("Alert: User {0} logged in from outside {3}.\n\n{1}\n\nDuo Info:\n{2}", item.user.name, IPInfo, DuoEvent, CountryName));
                            continue;
                        }

                        if (Properties.Settings.Default.GeoAlertsEnabled)
                        {
                            double distance = GetDistanceBetweenCoordinatesInMiles(IP.latitude, IP.longitude, Properties.Settings.Default.GeoAlertsLat, Settings.Default.GeoAlertsLong);
                            if (distance > ((double)Settings.Default.GeoAlertsDistance))
                            {
                                if (Properties.Settings.Default.GeoLocationIoCheck)
                                {
                                    Ipgeolocationio.IPGeolocationInfo GeioIPInfo = null;
                                    string GeoIpinfoFormated = Ipgeolocationio.GetFormatedIPGeolocationInformation(item.auth_device.ip, ref GeioIPInfo);
                                    double distance2 = GetDistanceBetweenCoordinatesInMiles(GeioIPInfo.latitude, GeioIPInfo.longitude, Properties.Settings.Default.GeoAlertsLat, Settings.Default.GeoAlertsLong);
                                    if (distance2 > ((double)Settings.Default.GeoAlertsDistance))
                                    {
                                        AWS.SendSNSMessage(String.Format("Alert: User {0} logged in from {3} miles away. Second geo provider has the user logging in {4} miles away. \n\n{1}\n\nGeolocation.io Info:\n{5}\n\nDuo Info:\n{2}",
                                         item.user.name, IPInfo, DuoEvent, ((int)distance), ((int)distance2), GeoIpinfoFormated));
                                        continue;
                                    }
                                    else
                                    {
                                        if (Properties.Settings.Default.GeoLocationIoCheckOverrideAlert)
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            AWS.SendSNSMessage(String.Format("Notice: User {0} logged in from {3} miles away. Second geo provider has the user logging in {4} miles away. \n\n{1}\n\nGeolocation.io Info:\n{5}\n\nDuo Info:\n{2}",
                                        item.user.name, IPInfo, DuoEvent, ((int)distance), ((int)distance2), GeoIpinfoFormated));
                                            continue;
                                        }
                                    }

                                }
                                AWS.SendSNSMessage(String.Format("Alert: User {0} logged in from {3} miles away. \n\n{1}\n\nDuo Info:\n{2}",
                                    item.user.name, IPInfo, DuoEvent, ((int)distance)));

                            }
                        }
                    }

                    if (item.reason == "user_marked_fraud")
                    {
                        AWS.SendSNSMessage(String.Format("Alert: User {0} reported that the logon attempt was fraud. They attempted to login from IP: {1}", item.user.name, item.access_device.ip));
                        continue;
                    }

                    if (Properties.Settings.Default.UnenrolledUser)
                    {
                        bool RegexIgnore = false;
                        if (Properties.Settings.Default.UnenrolledUserRegExEnabled)
                        {
                            string pattern = Properties.Settings.Default.UnenrolledUserRegExExpression;
                            string input = item.user.name;
                            Match m = Regex.Match(input, pattern, RegexOptions.IgnoreCase);
                            if (m.Success)
                            {
                                RegexIgnore = true;
                            }
                        }
                        if (item.reason == "allow_unenrolled_user" && !RegexIgnore)
                        {
                            if (AWS.IsArnValid(Settings.Default.UnenrolledARN))
                            {
                                AWS.SendSNSMessage(String.Format("Notice: User {0} is currently unenrolled in DUO. They logged in from IP: {1}", item.user.name, item.access_device.ip), Settings.Default.UnenrolledARN);
                                if (Properties.Settings.Default.UnenrolledAlertsOnlySendToUnenrolledARN)
                                {
                                    continue;
                                }
                            }
                            AWS.SendSNSMessage(String.Format("Notice: User {0} is currently unenrolled in DUO. They logged in from IP: {1}", item.user.name, item.access_device.ip));
                            continue;
                        }

                    }


                    if (item.reason == "bypass_user" && Properties.Settings.Default.UserInBypassMode)
                    {
                        AWS.SendSNSMessage(String.Format("Warning: User {0} is in bypass mode in the DUO console. They logged in from IP: {1}", item.user.name, item.access_device.ip));
                        continue;
                    }

                }
                if (DuoApiResponse.metadata.next_offset != null)
                {
                    NextOffset = string.Format("{0},{1}", DuoApiResponse.metadata.next_offset[0], DuoApiResponse.metadata.next_offset[1]);

                }
                else
                {
                    break;
                }
            } while (true);

            Properties.Settings.Default.LastLogFetch = unixTimestamp.ToString();
            Properties.Settings.Default.Save();
            return;
        }



        public static double GetDistanceBetweenCoordinatesInMiles(double lat1, double lon1, double lat2, double lon2)
        {
            double lat1Rad = lat1 * Math.PI / 180;
            double lon1Rad = lon1 * Math.PI / 180;
            double lat2Rad = lat2 * Math.PI / 180;
            double lon2Rad = lon2 * Math.PI / 180;


            double dLat = lat2Rad - lat1Rad;
            double dLon = lon2Rad - lon1Rad;
            double a = Math.Pow(Math.Sin(dLat / 2), 2) + Math.Cos(lat1Rad) * Math.Cos(lat2Rad) * Math.Pow(Math.Sin(dLon / 2), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = 3959 * c;

            return d;
        }
        public static bool IsPrivateIpAddress(string ipAddress)
        {
            string privateIpPattern1 = @"^10\.";  // 10.0.0.0 - 10.255.255.255
            string privateIpPattern2 = @"^172\.(1[6-9]|2[0-9]|3[0-1])\.";  // 172.16.0.0 - 172.31.255.255
            string privateIpPattern3 = @"^192\.168\.";  // 192.168.0.0 - 192.168.255.255
            if (ipAddress == "0.0.0.0") { return true; }

            bool isPrivate1 = Regex.IsMatch(ipAddress, privateIpPattern1);
            bool isPrivate2 = Regex.IsMatch(ipAddress, privateIpPattern2);
            bool isPrivate3 = Regex.IsMatch(ipAddress, privateIpPattern3);

            return isPrivate1 || isPrivate2 || isPrivate3;
        }


        public class Location
        {
            public string city { get; set; }
            public string country { get; set; }
            public string state { get; set; }
        }

        public class AccessDevice
        {
            public string epkey { get; set; }
            public object hostname { get; set; }
            public string ip { get; set; }
            public Location location { get; set; }
        }

        public class Application
        {
            public string key { get; set; }
            public string name { get; set; }
        }

        public class AuthDevice
        {
            public string ip { get; set; }
            public string key { get; set; }
            public Location location { get; set; }
            public string name { get; set; }
        }

        public class User
        {
            public IList<string> groups { get; set; }
            public string key { get; set; }
            public string name { get; set; }
        }

        public class Authlog
        {
            public AccessDevice access_device { get; set; }
            public string alias { get; set; }
            public Application application { get; set; }
            public AuthDevice auth_device { get; set; }
            public string email { get; set; }
            public string event_type { get; set; }
            public string factor { get; set; }
            public DateTime isotimestamp { get; set; }
            public object ood_software { get; set; }
            public string reason { get; set; }
            public string result { get; set; }
            public int timestamp { get; set; }
            public string txid { get; set; }
            public User user { get; set; }
        }

        public class Metadata
        {
            public IList<string> next_offset { get; set; }
            public int total_objects { get; set; }
        }

        public class DUOAPI_Class
        {
            public IList<Authlog> authlogs { get; set; }
            public Metadata metadata { get; set; }
        }
    }
}

