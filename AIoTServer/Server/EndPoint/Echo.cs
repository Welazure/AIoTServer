using WebSocketSharp;
using WebSocketSharp.Server;

namespace AIoTServer.Server.EndPoint;

public class Echo : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        Send("Pong: " + e.Data);
    }
}