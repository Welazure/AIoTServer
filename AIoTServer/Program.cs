using System.ComponentModel;
using System.Text.Json;

namespace AIoTServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("server/client? ");
            String input = Console.ReadLine();
            if (input == "server")
            {
                Console.Write("Url: ");
                var server = new Server.Server(true, Console.ReadLine().Trim());
                Console.ReadKey(true);
                server.Stop();
            }
            else if (input == "client")
            {
                Console.Write("iot/app? ");
                var connectionType = Console.ReadLine().Trim();
                if (!(connectionType == "iot" || connectionType == "app"))
                {
                    return;
                }
                Console.Write("Url: ");
                var connectionString = Console.ReadLine().Trim();
                if (connectionType == "iot")
                {
                    var client = new Client.IotClient(true, connectionString);
                }
                else
                {
                    var client = new Client.AppClient(true, connectionString);
                }
            }
        }
    }
}