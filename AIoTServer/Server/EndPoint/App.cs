using System.Text.Json;
using AIoTServer.Data;
using AIoTServer.Data.Type;
using AIoTServer.Server.Router;
using AIoTServer.Util;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace AIoTServer.Server.EndPoint;

public class App : WebSocketBehavior
{
    protected override void OnOpen()
    {
        AppRouter.Instance.RegisterAppSessions(Sessions);
        Console.WriteLine("DEBUG: New App Client Connected!");
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine("Received App Message: " + e.Data);
        try
        {
            var dataPacket = JsonSerializer.Deserialize<DataPacket>(e.Data);
            switch (dataPacket.Type)
            {
                case 4:
                    var events = DataStore.Instance.Get();
                    Send(JsonUtil.Serialize(new DataPacket(5, events)));
                    return;
                default:
                    return;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}