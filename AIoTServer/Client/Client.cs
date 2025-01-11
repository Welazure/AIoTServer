using WebSocketSharp;

namespace AIoTServer.Client
{
    public class Client
    {
        public WebSocket Socket { get; }
        public String Url { get; private set; }

        public Client(bool start, String url)
        {
            this.Url = url;
            Socket = new WebSocket(url);
            if (start)
            {
                this.Start();
            }
        }

        public void Start()
        {
            this.HandleMessage();
            Socket.Connect();
        }

        public void Stop()
        {
            Socket.Close();
        }

        private void HandleMessage()
        {
            Socket.OnMessage += (sender, e) =>
            {
                Console.WriteLine(e.Data);
            };
        }
    }
}