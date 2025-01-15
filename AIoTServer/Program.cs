using AIoTServer.Client;

namespace AIoTServer;

public class Program
{
    public static string _firebaseId;
    public static string _firebaseJson;

    public static void Main(string[] args)
    {
        Console.Write("server/client? ");
        var input = Console.ReadLine();
        if (input == "server")
        {
            Console.Write("Url: ");
            var server = new Server.Server(true, Console.ReadLine()?.Trim());
            Console.Write("Firebase database id: ");
            _firebaseId = Console.ReadLine()?.Trim();
            Console.Write("Firebase json path: ");
            _firebaseJson = Console.ReadLine()?.Trim();
            Console.ReadKey(true);
            server.Stop();
        }
        else if (input == "client")
        {
            Console.Write("iot/app? ");
            var connectionType = Console.ReadLine()?.Trim();
            if (!(connectionType == "iot" || connectionType == "app")) return;
            Console.Write("Url: ");
            var connectionString = Console.ReadLine()?.Trim();
            if (connectionType == "iot")
            {
                var client = new IotClient(true, connectionString);
            }
            else
            {
                var client = new AppClient(true, connectionString);
            }
        }
    }
}