using System.Security.Cryptography;

namespace AIoTServer.Util;

public class UidGenerator
{
    public static string GenerateUniqueBase64Id()
    {
        var randomBytes = new byte[6];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        var base64String = Convert.ToBase64String(randomBytes)
            .Replace('+', '-')
            .Replace('/', '-')
            .TrimEnd('=');

        return base64String[..8];
    }
}