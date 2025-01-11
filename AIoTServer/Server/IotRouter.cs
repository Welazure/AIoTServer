using WebSocketSharp.Server;

namespace AIoTServer.Server
{
    public class IotRouter
    {
        private static readonly Lazy<IotRouter> instance = new(() => new IotRouter());
        public static IotRouter Instance => instance.Value;
    
        private WebSocketSessionManager _appSessions;
    
        public void RegisterAppSessions(WebSocketSessionManager sessions)
        {
            _appSessions = sessions;
        }
    
        public void SendToApp(string message)
        {
            _appSessions?.Broadcast(message);
        }
    }
}
