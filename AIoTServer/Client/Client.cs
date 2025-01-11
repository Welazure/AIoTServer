using WebSocketSharp;

namespace AIoTServer.Client
{
    public class Client
    {
        public WebSocket Socket { get; }
        public String Url { get; private set; }

        public Client(bool start, String url)
        {
            Url = url;
            Socket = new WebSocket(url);
            if (start)
            {
                Start();
            }
        }

        public virtual void Start()
        {
            HandleMessage();
            Socket.Connect();
        }

        public virtual void Stop()
        {
            Socket.Close();
        }

        protected virtual void HandleMessage()
        {
            Socket.OnMessage += (sender, e) =>
            {
                Console.WriteLine(e.Data);
            };
        }
    }
}