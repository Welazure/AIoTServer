namespace AIoTServer.Data;

public class LoginData(string userName, string password)
{
    public string UserName { get; set; } = userName;
    public string Password { get; set; } = password;
}