using AIoTServer.Data.Type;

namespace AIoTServer.Util;

// Unused class
public class Scheduler
{
    private static Scheduler _instance;
    private static readonly object Padlock = new();
    private readonly Queue<EventData> _data;

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