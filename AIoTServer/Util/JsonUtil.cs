using System.Text.Json;
using System.Text.Json.Serialization;

namespace AIoTServer.Util;

internal class JsonUtil
{
    private static readonly JsonSerializerOptions Options = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public static string Serialize(object obj)
    {
        return JsonSerializer.Serialize(obj, Options);
    }

    public static T Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json);
    }
}