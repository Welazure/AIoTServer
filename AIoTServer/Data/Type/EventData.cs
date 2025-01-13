using Google.Cloud.Firestore;

namespace AIoTServer.Data.Type;

[FirestoreData]
public class EventData
{
    public EventData()
    {
    }

    public EventData(string id, long time, byte eventType)
    {
        Id = id[..8];
        EventType = eventType;
        Time = time;
    }

    public string Id { get; set; }
    [FirestoreProperty] public byte EventType { get; set; }
    [FirestoreProperty] public long Time { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != typeof(EventData)) return false;

        var data = (EventData)obj;
        return data.Id == Id && data.Time == Time && data.EventType == EventType;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }
}