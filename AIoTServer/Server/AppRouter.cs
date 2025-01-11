
using WebSocketSharp.Server;

namespace AIoTServer.Server
{
    public class AppRouter
    {
        private static readonly Lazy<AppRouter> instance = new(() => new AppRouter());
        public static AppRouter Instance => instance.Value;

        private WebSocketSessionManager _appSessions;

        public void RegisterAppSessions(WebSocketSessionManager sessions)
        {
            _appSessions = sessions;
        }

        public void SendToApp(string message)
        {
            _appSessions?.Broadcast(message);
        }

        public bool hasConnections()
        {
            if (_appSessions == null)
            {
                return false;
            }

            return _appSessions.Count > 0 && _appSessions.ActiveIDs.Any();
        }
    }
}
