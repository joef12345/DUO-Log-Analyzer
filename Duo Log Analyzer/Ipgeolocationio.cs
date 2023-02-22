using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Duo_Log_Analyzer.IpWhoisIo;

namespace Duo_Log_Analyzer
{
    internal class Ipgeolocationio
    {
        public static string GetFormatedIPGeolocationInformation(string IPaddr, ref IPGeolocationInfo IP)
        {
            try
            {
                IP = IPGeolocationLookup(IPaddr);
                string IPinfo = string.Format("" +
                                        "Country: {0}\n" +
                    "State/Providance: {1}\n" +
                    "City: {2}\n" +
                    "Organization: {3}\n" +
                    "ISP: {4}\n" +
                    "Latitude: {5}\n" +
                    "Longitude: {6}"
                    , IP.country_name, IP.state_prov, IP.city, IP.organization, IP.isp, IP.latitude, IP.longitude);
                return IPinfo;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static IPGeolocationInfo IPGeolocationLookup(string IPaddr)
        {
            try
            {
                var webRequest = WebRequest.Create(string.Format("https://api.ipgeolocation.io/ipgeo?apiKey={1}&ip={0}", IPaddr, Properties.Settings.Default.GeolocationioAPIKey)) as HttpWebRequest;
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
                        IPGeolocationInfo IPInfo = JsonConvert.DeserializeObject<IPGeolocationInfo>(IPIOInfo);
                        return IPInfo;
                    }
                }
            }
            catch
            {

                throw;
            }

        }
        public class Currency
        {
            public string code { get; set; }
            public string name { get; set; }
            public string symbol { get; set; }
        }

        public class IPGeolocationInfo
        {
            public string ip { get; set; }
            public string hostname { get; set; }
            public string continent_code { get; set; }
            public string continent_name { get; set; }
            public string country_code2 { get; set; }
            public string country_code3 { get; set; }
            public string country_name { get; set; }
            public string country_capital { get; set; }
            public string state_prov { get; set; }
            public string district { get; set; }
            public string city { get; set; }
            public string zipcode { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
            public bool is_eu { get; set; }
            public string calling_code { get; set; }
            public string country_tld { get; set; }
            public string languages { get; set; }
            public string country_flag { get; set; }
            public string geoname_id { get; set; }
            public string isp { get; set; }
            public string connection_type { get; set; }
            public string organization { get; set; }
            public string asn { get; set; }
            public Currency currency { get; set; }
            public TimeZone time_zone { get; set; }
        }

        public class TimeZone
        {
            public string name { get; set; }
            public int offset { get; set; }
            public string current_time { get; set; }
            public double current_time_unix { get; set; }
            public bool is_dst { get; set; }
            public int dst_savings { get; set; }
        }

    }

}
