using AIoTServer.Server.EndPoint;
using WebSocketSharp.Server;

namespace AIoTServer.Server;

public class Server
{
    public Server(bool start, string url)
    {
        Url = url;
        Socket = new WebSocketServer(url);

        if (start) Start();
    }

    public WebSocketServer Socket { get; }
    public string Url { get; }

    public void Start()
    {
        Init();
        Socket.Start();
    }

    public void Stop()
    {
        Socket.Stop();
    }

    public void Init()
    {
        Socket.AddWebSocketService<IoT>("/iot");
        Socket.AddWebSocketService<App>("/app");
        Socket.AddWebSocketService<Echo>("/echo");
    }
}