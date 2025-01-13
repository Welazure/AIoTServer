using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIoTServer.Auth
{
    public class DefaultAuthProvider : IAuthProvider
    {
        private Dictionary<String, String> _auth;

        public DefaultAuthProvider()
        {
            _auth = new Dictionary<string, string>();
        }
        public bool createAccount(string name, string password)
        {
            if (string.IsNullOrEmpty(name)) return false;
            if (_auth.ContainsKey(name)) return false;
            return true;
        }

        public string verifyAccount(string name, string password)
        {
            throw new NotImplementedException();
        }

        public string verifyToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
