namespace AIoTServer.Client;

public class AppClient : Client
{
    public AppClient(bool start, string url) : base(start, url)
    {
        if (!start) Start();
        while (true)
        {
            Console.WriteLine("Send Data: ");
            var data = Console.ReadLine()?.Trim();
            if (data == "EXIT") break;

            Socket.Send(data);
        }

        Stop();
    }

    protected override void HandleMessage()
    {
        Socket.OnMessage += (sender, e) =>
        {
            Console.WriteLine("Received Data: " + e.Data);
            Console.WriteLine("Send Data: ");
        };
    }
}