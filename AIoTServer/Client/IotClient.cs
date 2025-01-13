using System.Text.Json;
using AIoTServer.Data.Type;
using AIoTServer.Util;

namespace AIoTServer.Client;

public class IotClient : Client
{
    private bool _isWaitingForAck; // Flag to indicate if we're waiting for an ack
    private readonly Queue<EventData> _queue;

    public IotClient(bool start, string url) : base(start, url)
    {
        _queue = new Queue<EventData>();
        if (!start) Start();

        while (true)
        {
            if (_queue.Count > 0 && !_isWaitingForAck)
            {
                var jsonData = JsonUtil.Serialize(new DataPacket(1, _queue.Peek()));
                Console.WriteLine("Sending data: " + jsonData);
                Socket.Send(jsonData);
                _isWaitingForAck = true; // Mark that we're waiting for an ack
            }

            Console.WriteLine("Send Data: ");
            var data = Console.ReadLine()?.Trim();
            if (data == "EXIT") break;

            var datas = data.Split(" ");
            if (datas.Length != 2)
            {
                Console.WriteLine("Invalid data format! Please enter in the format: <time> <eventType>");
                continue;
            }

            long time;
            byte eventType;
            try
            {
                time = datas[0] == "current" ? DateTimeOffset.Now.ToUnixTimeMilliseconds() : long.Parse(datas[0]);
                eventType = byte.Parse(datas[1]);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid data format! Please enter in the format: <time> <eventType>");
                continue;
            }

            var toSend = new EventData(UidGenerator.GenerateUniqueBase64Id(), time, eventType);
            _queue.Enqueue(toSend);
        }
    }

    protected override void HandleMessage()
    {
        Socket.OnMessage += (sender, e) =>
        {
            Console.WriteLine("Received Data: " + e.Data);

            var packet = JsonUtil.Deserialize<DataPacket>(e.Data);
            var id = ((JsonElement)packet.Data).Deserialize<string>();
            switch (packet.Type)
            {
                case 0:
                    Console.WriteLine("No clients available! Discarding event with id:" + id);
                    _queue.Dequeue();
                    _isWaitingForAck = false;
                    Console.WriteLine("Send data: ");
                    break;

                case 2:
                    Console.WriteLine("ID Collision! Resending..");
                    var toSend = _queue.Peek();
                    if (toSend.Id != id)
                    {
                        Console.WriteLine("Fatal Error! Exiting..");
                        Environment.Exit(1);
                    }

                    toSend.Id = UidGenerator.GenerateUniqueBase64Id();
                    Socket.Send(JsonUtil.Serialize(new DataPacket(1, toSend)));
                    _isWaitingForAck = true;
                    Console.WriteLine("Send data: ");
                    break;

                case 3:
                    Console.WriteLine("Acknowledged! Removing from queue event with id: " + id);
                    toSend = _queue.Peek();
                    if (toSend.Id != id)
                    {
                        Console.WriteLine("Fatal Error! Exiting..");
                        Environment.Exit(1);
                    }

                    _queue.Dequeue();
                    _isWaitingForAck = false;
                    break;
            }

            if (_queue.Count <= 0 || _isWaitingForAck) return;
            var jsonData = JsonUtil.Serialize(new DataPacket(1, _queue.Peek()));
            Console.WriteLine("Sending data: " + jsonData);
            Socket.Send(jsonData);
            _isWaitingForAck = true;
        };
    }
}

