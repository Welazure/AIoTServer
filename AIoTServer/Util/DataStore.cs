using System.Security.Cryptography;

namespace AIoTServer.Util
{
    public class DataStore
    {

        private readonly HashSet<EventData> _data;

        private static DataStore _instance = null;
        private static readonly object Padlock = new object();

        private DataStore()
        {
            _data = new HashSet<EventData>();
        }
        public static DataStore Instance
        {
            get
            {
                lock (Padlock)
                {
                    return _instance ??= new DataStore();
                }
            }
        }
        public bool Add(EventData _data)
        {
            return this._data.Add(_data);
        }

        public HashSet<EventData> Get()
        {
            return _data;
        }

        public static string GenerateUniqueBase64Id()
        {
            byte[] randomBytes = new byte[6];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            string base64String = Convert.ToBase64String(randomBytes)
                .TrimEnd('=');

            return base64String.Substring(0, 8);
        }
    }
}