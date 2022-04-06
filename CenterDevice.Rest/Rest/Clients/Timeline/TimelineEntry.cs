using log4net;
using System;
using System.Collections.Generic;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Rest.Clients.Timeline
{
    public class TimelineEntry
    {
        private static ILog logger = LogManager.GetLogger(typeof(TimelineEntry));

        public string Id { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; }
        public DateTime Timestamp { get; set; }
        public string Creator { get; set; }
        public Dictionary<string, object> Details { get; set; }

        public string GetStringDetail(string key)
        {
            if (Details != null)
            {
                if (Details.ContainsKey(key))
                {
                    return Details[key] as string;
                }
                else
                {
                    throw new InvalidOperationException(string.Format("Key {0} not found for timeline event {1} in given details [{2}].", key, Id, string.Join(", ", Details)));
                }
            }
            else
            {
                throw new InvalidOperationException("Unable to read " + key + " because Details are null!");
            }
        }

        public override string ToString()
        {
            return "[TimelineEntry Id='" + Id + "', Type='" + Type + "']";
        }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element