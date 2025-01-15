using AIoTServer.Data.Type;

namespace AIoTServer.Data;

public interface IDataProvider
{
    public bool Add(EventData data);
    public List<EventData> Get();
    public List<EventData> Get(int days);
}