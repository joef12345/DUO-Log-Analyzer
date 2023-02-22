using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Duo_Log_Analyzer
{
    internal class iCloudPrivateRelay
    {
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
                    Console.WriteLine("Error downloading Icloud Private Relay CSV: " + ex.Message);

                }
            }
        }



    }
}
