using WebSocketSharp;

namespace AIoTServer.Client;

public class Client
{
    public Client(bool start, string url)
    {
        Url = url;
        Socket = new WebSocket(url);
        if (start) Start();
    }

    public WebSocket Socket { get; }
    public string Url { get; private set; }

    public virtual void Start()
    {
        HandleMessage();
        Socket.Connect();
    }

    public virtual void Stop()
    {
        Socket.Close();
    }

    protected virtual void HandleMessage()
    {
        Socket.OnMessage += (sender, e) => { Console.WriteLine(e.Data); };
    }
}