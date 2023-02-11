using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Amazon.Runtime;

namespace Duo_Log_Analyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Contains("-setup"))
            {
                var form = new FormSetup();
                form.Show();
                System.Windows.Forms.Application.Run();
                return;
            }



            Console.WriteLine("Duo Log Analyzer {0} https://github.com/joef12345/DUO-Log-Analyzer", Assembly.GetExecutingAssembly().GetName().Version);
            if (!args.Contains("-run"))
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
            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
            DateTime LastCheck = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(LastAccess)).UtcDateTime;
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(LastCheck, localTimeZone);

            Console.WriteLine("Getting logs from {0}", localTime.ToString());

            if (args.Contains("-back7days"))
            {
                DateTime dateTime = DateTimeOffset.FromUnixTimeMilliseconds(unixTimestamp).UtcDateTime;
                DateTime oneDayEarlier = dateTime.AddDays(-7);
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
                    Console.WriteLine("Processing logon event at: {0}", item.isotimestamp);
                    if (IgnoreUserList.Contains(item.user.name)) { continue; }
                    
                    if (!IgnoreIPList.Contains(item.access_device.ip))
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
                                SendSNSMessage(String.Format("Alert: User {0} logged in from a suspicious location.\n\n{1}\n\nDuo Info:\n{2}", item.user.name, IPInfo, DuoEvent));
                                continue;
                            }
                        }
                        

                        if (IP.country_code != "US" && Properties.Settings.Default.OutsideUS)
                        {
                            SendSNSMessage(String.Format("Alert: User {0} logged in from outside the US.\n\n{1}\n\nDuo Info:\n{2}", item.user.name, IPInfo, DuoEvent));
                            continue;
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
                            SendSNSMessage(String.Format("Notice: User {0} appears to be a staff member that is currently unenrolled in DUO. They logged in from IP: {1}", item.user.name, item.access_device.ip));
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

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
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

