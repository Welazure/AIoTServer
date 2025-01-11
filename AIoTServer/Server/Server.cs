using System.Text.Json;
using AIoTServer.Server.EndPoint;
using AIoTServer.Util;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace AIoTServer.Server
{
    public class Server
    {
        public WebSocketServer Socket { get; }
        public String Url { get; }

        public Server(bool start, String url)
        {
            Url = url;
            Socket = new WebSocketServer(Url);
            if (start)
            {
                Start();
            }
        }

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
        }
    }
}