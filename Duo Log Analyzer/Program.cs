using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using CsvHelper;
using Duo_Log_Analyzer.Properties;
using System.Data;
using CsvHelper.Configuration;
using System.Globalization;
using CsvHelper.Configuration.Attributes;

namespace Duo_Log_Analyzer
{
    class Program
    {
        static void Main(string[] args)
        {


            if (args.Contains("-setup", StringComparer.OrdinalIgnoreCase))
            {
                var form = new FormSetup();
                form.Show();
                System.Windows.Forms.Application.Run();
                return;
            }



            Console.WriteLine("Duo Log Analyzer {0} https://github.com/joef12345/DUO-Log-Analyzer", "1.0.0.3");
            if (!args.Contains("-run", StringComparer.OrdinalIgnoreCase))
            {
                Console.WriteLine("\nUsage: \n-setup Displays the setup GUI.\n-run Scans DUO Logs and sends alerts. (-back7days Request logs from the last 7 days for testing purposes.)\n");
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

            if (Properties.Settings.Default.iCloudPrivateRelayEnabled)
            {
                TimeSpan timeDifference = DateTime.Now - Properties.Settings.Default.ICloudPrivateRelayLastUpdate;
                double totalHours = timeDifference.TotalHours;

                if (totalHours >= 24)
                {
                    UpdatePrivateIPDatabase();
                }

            }

            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
            DateTime LastCheck = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(LastAccess)).UtcDateTime;
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(LastCheck, localTimeZone);

            Console.WriteLine("Getting logs from {0}", localTime.ToString());

            if (args.Contains("-back7days", StringComparer.OrdinalIgnoreCase))
            {
                DateTime dateTime = DateTimeOffset.FromUnixTimeMilliseconds(unixTimestamp).UtcDateTime;
                DateTime oneDayEarlier = dateTime.AddDays(-7);
                LastAccess = new DateTimeOffset(oneDayEarlier).ToUnixTimeMilliseconds().ToString();
            }
            if (args.Contains("-back2hours", StringComparer.OrdinalIgnoreCase))
            {
                DateTime dateTime = DateTimeOffset.FromUnixTimeMilliseconds(unixTimestamp).UtcDateTime;
                DateTime oneDayEarlier = dateTime.AddHours(-2);
                LastAccess = new DateTimeOffset(oneDayEarlier).ToUnixTimeMilliseconds().ToString();
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


                    if (!IgnoreIPList.Contains(item.access_device.ip) && !IsPrivateIpAddress(item.access_device.ip))
                    {
                        IPWhoIS IP = new IPWhoIS();
                        Boolean SecurityEvent = false;

                        string IPInfo = GetFormattedIOWHOINFO(item.access_device.ip, ref SecurityEvent, ref IP);

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

                                    if (IsIPiCloudPrivateIP(item.access_device.ip, ref iCloudCountryCode, ref iCloudLanguage, ref iCloudRegion))
                                    {
                                        if (!Settings.Default.iCloudPrivateRelayIgnore)
                                        {
                                            string iCloudPrivateRelayInfo = string.Format("Country Code: {0}\nRegion: {1}\nLanguage: {2} ",
                                                iCloudCountryCode, iCloudRegion, iCloudLanguage);
                                            SendSNSMessage(String.Format("Warning: User {0} logged in from an iCloud Private Relay.\n\n{1}\n\nDuo Info:\n{2} \n\nIcloud Private Relay Info:\n{3}", item.user.name, IPInfo, DuoEvent, iCloudPrivateRelayInfo));
                                            continue;
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                }
                                SendSNSMessage(String.Format("Alert: User {0} logged in from a suspicious location.\n\n{1}\n\nDuo Info:\n{2}", item.user.name, IPInfo, DuoEvent));
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
                            SendSNSMessage(String.Format("Alert: User {0} logged in from outside {3}.\n\n{1}\n\nDuo Info:\n{2}", item.user.name, IPInfo, DuoEvent, CountryName));
                            continue;
                        }

                        if (Properties.Settings.Default.GeoAlertsEnabled)
                        {
                        double distance =  GetDistanceBetweenCoordinatesInMiles(IP.latitude, IP.longitude, Properties.Settings.Default.GeoAlertsLat, Settings.Default.GeoAlertsLong);
                        if (distance > ((double)Settings.Default.GeoAlertsDistance))
                            {
                                SendSNSMessage(String.Format("Alert: User {0} logged in from {3} miles away. \n\n{1}\n\nDuo Info:\n{2}", 
                                    item.user.name, IPInfo, DuoEvent, ((int)distance)));

                            }
                        }
                    }

                    if (item.reason == "user_marked_fraud")
                    {
                        SendSNSMessage(String.Format("Alert: User {0} reported that the logon attempt was fraud. They attempted to login from IP: {1}", item.user.name, item.access_device.ip));
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
                            SendSNSMessage(String.Format("Notice: User {0} is currently unenrolled in DUO. They logged in from IP: {1}", item.user.name, item.access_device.ip));
                            continue;
                        }

                    }


                    if (item.reason == "bypass_user" && Properties.Settings.Default.UserInBypassMode)
                    {
                        SendSNSMessage(String.Format("Warning: User {0} is in bypass mode in the DUO console. They logged in from IP: {1}", item.user.name, item.access_device.ip));
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

        public static string GetFormattedIOWHOINFO(string IPAddr, ref Boolean SecurityEvent, ref Program.IPWhoIS IPWHOISInfo)
        {
            try
            {
                IPWhoIS IP = IPIOLookup(IPAddr);
                string SecurityFlags = "";
                if (IP.security.anonymous) { SecurityFlags = SecurityFlags + "*Anonymous "; }
                if (IP.security.hosting) { SecurityFlags = SecurityFlags + "*Hosting "; }
                if (IP.security.proxy) { SecurityFlags = SecurityFlags + "*Proxy "; }
                if (IP.security.tor) { SecurityFlags = SecurityFlags + "*TOR "; }
                if (IP.security.vpn) { SecurityFlags = SecurityFlags + "*VPN "; }
                if (SecurityFlags == "") { SecurityFlags = "None"; }
                string IPInfo = string.Format("IP: {0}\nCountry: {1}\nRegion: {2}\nCity: {3}\nSecurity Flags: {4}\nISP: {5}",
                    IP.ip, IP.country, IP.region, IP.city, SecurityFlags, IP.connection.isp);
                if (IP.security.anonymous || IP.security.hosting || IP.security.proxy || IP.security.tor || IP.security.vpn)
                {
                    SecurityEvent = true;
                }
                IPWHOISInfo = IP;
                return IPInfo;
            }
            catch (Exception)
            {
                throw;

            }
        }
        private static IPWhoIS IPIOLookup(string IPaddr)
        {
            try
            {
                var webRequest = WebRequest.Create(string.Format("https://ipwhois.pro/{0}?key={1}&security=1", IPaddr, Properties.Settings.Default.IPWhoisioAPIKey)) as HttpWebRequest;
                if (webRequest == null)
                {
                    return null;
                }

                webRequest.ContentType = "application/json";
                webRequest.UserAgent = "Nothing";

                using (var s = webRequest.GetResponse().GetResponseStream())
                {
                    using (var sr = new StreamReader(s))
                    {
                        var IPIOInfo = sr.ReadToEnd();
                        IPWhoIS IPInfo = JsonConvert.DeserializeObject<IPWhoIS>(IPIOInfo);
                        if (IPInfo.success == false)
                        {
                            throw new Exception(string.Format("Got the following return error from IPWHOIS.IO: {0}", IPIOInfo));
                        }
                        return IPInfo;
                    }
                }
            }
            catch
            {

                throw;
            }

        }
        public static bool IsIPInCDIRRange(string IP, string CDIRRange)
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(IP);
                IPNetwork ipNetwork = IPNetwork.Parse(CDIRRange);
                return ipNetwork.Contains(ipAddress);
            }
            catch (Exception ex)
            {

                Console.WriteLine("IsIPInCDIRRange ERROR: {0} ", ex.Message);
                return false;
            }
        }

        public class CsvRow
        {
            public string Column1 { get; set; }
            public string Column2 { get; set; }
            public string Column3 { get; set; }
            public string Column4 { get; set; }

        }

        public static bool IsIPiCloudPrivateIP(string Ipaddress, ref string CountryCode, ref string Language, ref string Region)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ",", HasHeaderRecord = false };

            using (var reader = new StreamReader("egress-ip-ranges.csv"))
            using (var csv = new CsvReader(reader, config))

            {

                var records = csv.GetRecords<dynamic>();

                foreach (var record in records)
                {
                    if (IsIPInCDIRRange(Ipaddress, record.Field1))
                    {
                        CountryCode = record.Field2;
                        Language = record.Field3;
                        Region = record.Field4;
                        return true;
                    }
                }
                return false;
            }
        }
        public static void UpdatePrivateIPDatabase()
        {
            using (var client = new WebClient())
            {
                string url = "https://mask-api.icloud.com/egress-ip-ranges.csv"; // replace with the URL of the file you want to download
                string filePath = "egress-ip-ranges.csv"; // replace with the path where you want to save the file

                try
                {
                    client.DownloadFile(url, filePath);
                    Console.WriteLine("Updated iCloudPrivate Relay DB Successfully.");
                    Properties.Settings.Default.ICloudPrivateRelayLastUpdate = DateTime.Now;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error downloading file: " + ex.Message);
                }
            }
        }
        public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 3958.8; // Earth's radius in miles
            double latRad1 = lat1 * Math.PI / 180.0;
            double lonRad1 = lon1 * Math.PI / 180.0;
            double latRad2 = lat2 * Math.PI / 180.0;
            double lonRad2 = lon2 * Math.PI / 180.0;
            double dLat = latRad2 - latRad1;
            double dLon = lonRad2 - lonRad1;
            double a = Math.Sin(dLat / 2.0) * Math.Sin(dLat / 2.0) +
                       Math.Cos(latRad1) * Math.Cos(latRad2) *
                       Math.Sin(dLon / 2.0) * Math.Sin(dLon / 2.0);
            double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = R * c;
            return distance;
        }


        public static string SendSNSMessage(string Message)
        {

            string arn = Properties.Settings.Default.SNSTopicARN;
            string[] arnParts = arn.Split(':');
            string region = arnParts[3];
            AmazonSimpleNotificationServiceConfig snsConfig = new AmazonSimpleNotificationServiceConfig();
            snsConfig.Timeout = TimeSpan.FromMinutes(10);

            AmazonSimpleNotificationServiceClient snsClient = new AmazonSimpleNotificationServiceClient(Properties.Settings.Default.SNSAccessKeyID, Properties.Settings.Default.SNSSecretAccessKey, Amazon.RegionEndpoint.GetBySystemName(region));

            PublishRequest request = new PublishRequest
            {
                TopicArn = arn,
                Message = Message
            };
            try
            {
                PublishResponse response = snsClient.Publish(request);
                return response.MessageId;
            }
            catch (Exception)
            {
                throw;
            }
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
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
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
        public class Flag
        {
            public string img { get; set; }
            public string emoji { get; set; }
            public string emoji_unicode { get; set; }
        }

        public class Connection
        {
            public string asn { get; set; }
            public string org { get; set; }
            public string isp { get; set; }
            public string domain { get; set; }
        }

        public class Timezone
        {
            public string id { get; set; }
            public string abbr { get; set; }
            public bool is_dst { get; set; }
            public string offset { get; set; }
            public string utc { get; set; }
            public DateTime current_time { get; set; }
        }

        public class Currency
        {
            public string name { get; set; }
            public string code { get; set; }
            public string symbol { get; set; }
            public string plural { get; set; }
            public string exchange_rate { get; set; }
        }

        public class Security
        {
            public bool anonymous { get; set; }
            public bool proxy { get; set; }
            public bool vpn { get; set; }
            public bool tor { get; set; }
            public bool hosting { get; set; }
        }

        public class IPWhoIS
        {
            public string ip { get; set; }
            public bool success { get; set; }
            public string type { get; set; }
            public string continent { get; set; }
            public string continent_code { get; set; }
            public string country { get; set; }
            public string country_code { get; set; }
            public string region { get; set; }
            public string region_code { get; set; }
            public string city { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
            public bool is_eu { get; set; }
            public string postal { get; set; }
            public string calling_code { get; set; }
            public string capital { get; set; }
            public string borders { get; set; }
            public Flag flag { get; set; }
            public Connection connection { get; set; }
            public Timezone timezone { get; set; }
            public Currency currency { get; set; }
            public Security security { get; set; }
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

