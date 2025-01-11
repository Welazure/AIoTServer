using System.Text.Json.Serialization;
using System.Text.Json;

namespace AIoTServer.Util
{
    internal class JsonUtil
    {
        private static readonly JsonSerializerOptions _options = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public static string Serialize(object obj)
        {
            return JsonSerializer.Serialize(obj, _options);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
