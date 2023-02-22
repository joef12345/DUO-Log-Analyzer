using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Duo_Log_Analyzer.Program;

namespace Duo_Log_Analyzer
{
    internal class IpWhoisIo
    {
        public static string GetFormattedIOWHOINFO(string IPAddr, ref Boolean SecurityEvent, ref IpWhoisIo.IPWhoIS IPWHOISInfo)
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
                string IPInfo = string.Format("IP: {0}\n" +
                    "Country: {1}\n" +
                    "Region: {2}\n" +
                    "City: {3}\n" +
                    "Security Flags: {4}\n" +
                    "ISP: {5}\n" +
                    "Latitude: {6}\n" +
                    "Longitude: {7}",
                    IP.ip, IP.country, IP.region, IP.city, SecurityFlags, IP.connection.isp, IP.latitude, IP.longitude);
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
    }
}
