namespace AIoTServer.Auth;

public interface IAuthProvider
{
    bool createAccount(string name, string password);
    string verifyAccount(string name, string password);
    bool verifyToken(string token);
}