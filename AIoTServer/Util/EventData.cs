using System.Text.Json;

namespace AIoTServer.Util
{
    public class EventData
    {
        public EventData(string id, long time, byte eventType)
        {
            Id = id.Length > 8 ? id.Substring(0, 8) : id;
            Time = time;
            EventType = eventType;
        }

        public string Id { get; private set; }
        public long Time { get; private set; }
        public byte EventType { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(EventData))
            {
                return false;
            }

            var data = (EventData)obj;
            return data.Id == Id && data.Time == Time && data.EventType == EventType;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}