using AIoTServer.Data.DataProvider;
using AIoTServer.Data.Type;

namespace AIoTServer.Data.Services;

public class DataStore : IDataProvider
{
    private static DataStore _instance;
    private static readonly object Padlock = new();

    private readonly IDataProvider _data;

    private DataStore(IDataProvider provider)
    {
        _data = provider;
    }

    public static DataStore Instance
    {
        get
        {
            if (_instance != null) return _instance;
            lock (Padlock)
            {
                _instance ??= new DataStore(new FirebaseProvider());
            }

            return _instance;
        }
    }

    public bool Add(EventData data)
    {
        return _data.Add(data);
    }

    public List<EventData> Get()
    {
        return _data.Get();
    }

    public List<EventData> Get(int days)
    {
        return _data.Get(days);
    }
}