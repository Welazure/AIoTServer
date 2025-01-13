using AIoTServer.Data.Type;
using AIoTServer.Firebase;

namespace AIoTServer.Data.DataProvider;

internal class FirebaseProvider : IDataProvider
{
    private readonly FirebaseConnection _connection = new();

    public bool Add(EventData data)
    {
        var toReturn = Task.Run(async () => await AddAsync(data)).Result;
        Console.WriteLine("Add event: " + toReturn);
        return toReturn;
    }

    public List<EventData> Get()
    {
        var toReturn = Task.Run(async () => await GetAsync()).Result;
        Console.WriteLine("Done retrieving");
        return toReturn;
    }

    public async Task<List<EventData>> GetAsync()
    {
        var eventsCollection = _connection.Connection.Collection("events");

        // Get all documents in the collection
        var snapshot = await eventsCollection.OrderByDescending("Time").GetSnapshotAsync();

        var eventsList = new List<EventData>();

        foreach (var doc in snapshot.Documents)
        {
            if (!doc.Exists) continue;
            // Convert document data to EventData and populate Id
            var eventData = doc.ConvertTo<EventData>();
            eventData.Id = doc.Id;

            eventsList.Add(eventData);
        }

        return eventsList;
    }

    public async Task<bool> AddAsync(EventData data)
    {
        var eventsCollection = _connection.Connection.Collection("events");

        var snapshot = await eventsCollection.GetSnapshotAsync();
        if (snapshot.Documents.Any(x => x.Id == data.Id)) return false;

        var docRef = eventsCollection.Document(data.Id);
        await docRef.SetAsync(new { data.EventType, data.Time });
        return true;
    }
}