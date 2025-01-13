using WebSocketSharp.Server;

namespace AIoTServer.Server.Router;

public class AppRouter
{
    private static readonly Lazy<AppRouter> instance = new(() => new AppRouter());

    private WebSocketSessionManager _appSessions;
    public static AppRouter Instance => instance.Value;

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