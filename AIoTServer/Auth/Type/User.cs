namespace AIoTServer.Auth.Type;

public class User : IUser
{
    public User()
    {
    }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public string Username { get; }
    public string Password { get; }
}