﻿using System.Text.Json;
using AIoTServer.Util;

namespace AIoTServer.Client
{
    public class IotClient : Client
    {
        private readonly Queue<EventData> _queue;

        public IotClient(bool start, string url) : base(start, url)
        {
            _queue = new Queue<EventData>();
            if (!start) Start();
            while (true)
            {
                if (!(_queue.Count > 0))
                {
                    String jsonData = JsonUtil.Serialize(new DataPacket(1, _queue.Peek()));
                    Console.WriteLine("Sending data: " + jsonData);
                    Socket.Send(jsonData);
                }

                Console.WriteLine("Send Data: ");
                var data = Console.ReadLine().Trim();
                if (data == "EXIT")
                {
                    break;
                }

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
                    if (datas[0] == "current")
                    {
                        time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    }
                    else
                    {
                        time = long.Parse(datas[0]);
                    }

                    eventType = byte.Parse(datas[1]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid data format! Please enter in the format: <time> <eventType>");
                    continue;
                }

                var toSend = new EventData(DataStore.GenerateUniqueBase64Id(), time, eventType);
                _queue.Enqueue(toSend);
            }
        }

        protected override void HandleMessage()
        {
            Socket.OnMessage += (sender, e) =>
            {
                Console.WriteLine("Received Data: " + e.Data);

                var packet = JsonUtil.Deserialize<DataPacket>(e.Data);
                switch (packet.Type)
                {
                    case 0:
                        Console.WriteLine("No clients available! Discarding event with id:" +
                                          ((JsonElement)packet.Data).Deserialize<String>());
                        break;
                    case 2:
                        Console.WriteLine("ID Collision! Resending..");
                        var id = ((JsonElement)packet.Data).Deserialize<String>();
                        var toSend = _queue.Peek();
                        if (!(toSend.Id == id))
                        {
                            Console.WriteLine("Fatal Error! Exiting..");
                            Environment.Exit(1);
                        }

                        toSend.Id = DataStore.GenerateUniqueBase64Id();
                        Socket.Send(JsonUtil.Serialize(new DataPacket(1, toSend)));
                        break;
                    case 3:
                        Console.WriteLine("Acknowledged! Removing from queue event with id: " +
                                          ((JsonElement)packet.Data).Deserialize<String>());
                        _queue.Dequeue();
                        break;
                }

                Console.WriteLine("Send data: ");
            };
        }
    }
}