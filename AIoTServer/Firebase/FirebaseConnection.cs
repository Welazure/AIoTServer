using Google.Cloud.Firestore;

namespace AIoTServer.Firebase;

public class FirebaseConnection
{
    public FirebaseConnection()
    {
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "D:/aiot.json");
        Connection = FirestoreDb.Create("aiot-df756");
    }

    public FirestoreDb Connection { get; private set; }
}