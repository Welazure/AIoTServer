namespace AIoTServer.Util
{
    public class Scheduler
    {
        private readonly Queue<EventData> _data;

        private static Scheduler _instance = null;
        private static readonly object Padlock = new object();

        private Scheduler()
        {
            _data = new Queue<EventData>();
        }

        public static Scheduler Instance
        {
            get
            {
                lock (Padlock)
                {
                    return _instance ??= new Scheduler();
                }
            }
        }

        public bool IsEmpty()
        {
            return _data.Count == 0;
        }

        public EventData Peek()
        {
            return _data.Peek();
        }

        public EventData Dequeue()
        {
            return _data.Dequeue();
        }
        public void Enqueue(EventData data)
        {
            _data.Enqueue(data);
        }
    }
}
