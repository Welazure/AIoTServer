using System.Text.Json;
using AIoTServer.Data.Services;
using AIoTServer.Data.Type;
using AIoTServer.Server.Router;
using AIoTServer.Util;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace AIoTServer.Server.EndPoint;

public class IoT : WebSocketBehavior
{
    protected override void OnOpen()
    {
        Console.WriteLine("DEBUG: New IoT Client Connected!");
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine("Received IoT Message: " + e.Data);
        try
        {
            var packet = JsonUtil.Deserialize<DataPacket>(e.Data);
            switch (packet.Type)
            {
                case 1:
                    var eventData = ((JsonElement)packet.Data).Deserialize<EventData>();

                    if (!DataStore.Instance.Add(eventData))
                    {
                        Send(JsonUtil.Serialize(new DataPacket(2, eventData.Id)));
                        return;
                    }

                    if (!AppRouter.Instance.HasConnections())
                    {
                        Send(JsonUtil.Serialize(new DataPacket(0, eventData.Id)));
                        return;
                    }

                    AppRouter.Instance.SendToApp(e.Data);
                    Send(JsonUtil.Serialize(new DataPacket(3, eventData.Id)));
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