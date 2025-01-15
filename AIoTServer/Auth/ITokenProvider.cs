namespace AIoTServer.Auth;

public interface ITokenProvider
{
    public string GenerateToken(IUser user);
    public bool ValidateToken(string token);
}