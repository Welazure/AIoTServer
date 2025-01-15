namespace AIoTServer.Auth.AuthProvider
{
    public class DefaultAuthProvider : IAuthProvider
    {
        private readonly Dictionary<string, string> _auth;

        public DefaultAuthProvider()
        {
            _auth = new Dictionary<string, string>();
        }
        public bool createAccount(string name, string password)
        {
            if (string.IsNullOrEmpty(name)) return false;
            return _auth.TryAdd(name, password);
        }

        public string verifyAccount(string name, string password)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password)) return null;
            return _auth.TryGetValue(name, out var pass) && pass == password ? name : null;
        }

        public bool verifyToken(string token)
        {
            
        }


    }
}
