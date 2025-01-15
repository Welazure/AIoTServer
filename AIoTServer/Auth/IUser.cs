namespace AIoTServer.Auth;

public interface IUser
{
    public string Username { get; }
    public string Password { get; }
}