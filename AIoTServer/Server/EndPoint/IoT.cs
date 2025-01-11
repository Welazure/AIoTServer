using AIoTServer.Util;
using System.Text.Json;
using WebSocketSharp.Server;
using WebSocketSharp;

namespace AIoTServer.Server.EndPoint
{
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
                            Send(JsonUtil.Serialize(new DataPacket(2, null)));
                            return;
                        }

                        if (!AppRouter.Instance.hasConnections())
                        {
                            Send(JsonUtil.Serialize(new DataPacket(0, null)));
                            return;
                        }

                        AppRouter.Instance.SendToApp(e.Data);
                        Send(JsonUtil.Serialize(new DataPacket(3, null)));
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
}
