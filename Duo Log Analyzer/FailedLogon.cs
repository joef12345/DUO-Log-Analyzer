using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Duo_Log_Analyzer
{
    internal class FailedLogon
    {
        public class FailedLogonEvent
        {
            public string UserName { get; set; }
            public DateTime EventDateTime { get; set; }
            public string Message { get; set; }
        }
        private const string _filename = @".\failedlogon.db";

        public static void ReportValidLogonEvent(string Username)
        {
            DeleteRecord(Username.ToLower());
        }

        public static void ReportInvalidLogon(string Username, string Message, DateTime EventDateTime)
        {
            FailedLogonEvent failedLogonEvent = new FailedLogonEvent()
            {
                UserName = Username.ToLower(),
                Message = Message,
                EventDateTime = EventDateTime
            };
            AddRecord(failedLogonEvent);
        }
        public static List<FailedLogonEvent> FindOlderThan(int Minutes)
        {
            List<FailedLogonEvent> TaggedEvents = new List<FailedLogonEvent> { };
            List<FailedLogonEvent> Events = GetAllRecords();
            foreach (var item in Events)
            {
                if (item.EventDateTime.AddMinutes(Minutes) <= DateTime.Now)
                {
                    TaggedEvents.Add(item);
                }
            }
            return TaggedEvents;
        }

        public static List<FailedLogonEvent> GetFailedEventsByUsername(string Username)
        {
            List<FailedLogonEvent> TaggedEvents = new List<FailedLogonEvent> { };
            List<FailedLogonEvent> Events = GetAllRecords();
            foreach (var item in Events)
            {
                if (item.UserName == Username.ToLower())
                {
                    TaggedEvents.Add(item);
                }
            }
            return TaggedEvents;
        }

        private static List<FailedLogonEvent> GetAllRecords()
        {
            List<FailedLogonEvent> records = new List<FailedLogonEvent>();
            if (!System.IO.File.Exists(_filename))
            {
                return records;
            }
            using (StreamReader reader = new StreamReader(_filename))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');

                    FailedLogonEvent record = new FailedLogonEvent();
                    record.UserName = fields[0];
                    record.Message = fields[1];
                    DateTime EventDateTime;
                    bool ConvertSuccess = DateTime.TryParse(fields[2], out EventDateTime);
                    if (!ConvertSuccess)
                    {
                        continue;
                    }
                    record.EventDateTime = EventDateTime;

                    records.Add(record);
                }
            }

            return records;
        }

        public static void AddRecord(FailedLogonEvent record)
        {
            using (StreamWriter writer = new StreamWriter(_filename, true))
            {
                writer.WriteLine(record.UserName + "," + record.Message + "," + record.EventDateTime.ToString());
                writer.Close();
            }
        }

        public static void DeleteRecord(string name)
        {
            List<FailedLogonEvent> records = GetAllRecords();
            records.RemoveAll(record => record.UserName == name);

            using (StreamWriter writer = new StreamWriter(_filename))
            {
                foreach (FailedLogonEvent record in records)
                {
                    writer.WriteLine(record.UserName + "," + record.Message + "," + record.EventDateTime);
                }
            }
        }
    }
}

