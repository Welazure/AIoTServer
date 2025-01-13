using Google.Cloud.Firestore;

namespace AIoTServer.Firebase;

public class FirebaseConnection
{
    public FirebaseConnection()
    {
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", Program._firebaseJson);
        Connection = FirestoreDb.Create(Program._firebaseId);
    }

    public FirestoreDb Connection { get; private set; }
}