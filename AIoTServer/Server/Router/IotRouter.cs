using WebSocketSharp.Server;

namespace AIoTServer.Server.Router;

// Unused Class
public class IotRouter
{
    private static readonly Lazy<IotRouter> instance = new(() => new IotRouter());

    private WebSocketSessionManager _appSessions;
    public static IotRouter Instance => instance.Value;

    public void RegisterAppSessions(WebSocketSessionManager sessions)
    {
        _appSessions = sessions;
    }

    public void SendToApp(string message)
    {
        _appSessions?.Broadcast(message);
    }

    public bool HasConnections()
    {
        if (_appSessions == null) return false;

        return _appSessions.Count > 0 && _appSessions.ActiveIDs.Any();
    }
}