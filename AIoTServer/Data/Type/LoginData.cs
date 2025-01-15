namespace AIoTServer.Data.Type;

public class LoginData(string userName, string password)
{
    public string Username { get; set; } = userName;
    public string Password { get; set; } = password;
}