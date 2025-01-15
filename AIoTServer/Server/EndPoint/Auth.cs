using System.Text.Json;
using AIoTServer.Data.Type;
using AIoTServer.Util;
using FirebaseAdmin.Auth;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace AIoTServer.Server.EndPoint;

public class Auth : WebSocketBehavior
{
    protected override void OnOpen()
    {
        Console.WriteLine("DEBUG: New Auth Client Connected!");
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine("Received Auth Message: " + e.Data);
        try
        {
            var packet = JsonUtil.Deserialize<DataPacket>(e.Data);
            switch (packet.Type)
            {
                case 6:
                    var loginData = ((JsonElement)packet.Data).Deserialize<LoginData>();
                    var auth = FirebaseAuth.DefaultInstance;
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