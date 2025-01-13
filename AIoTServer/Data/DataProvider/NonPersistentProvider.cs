using AIoTServer.Data.Type;
using AIoTServer.Util.Collections;

namespace AIoTServer.Data.DataProvider;

public class NonPersistentProvider : IDataProvider
{
    private readonly OrderedSet<EventData> _data = [];

    public bool Add(EventData data)
    {
        return _data.Add(data);
    }

    public List<EventData> Get()
    {
        return _data.ToList();
    }
}