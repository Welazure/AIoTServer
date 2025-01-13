using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIoTServer.Auth
{
    public interface IAuthProvider
    {
        bool createAccount(string name, string password);
        string verifyAccount(string name, string password);
        string verifyToken(string token);
    }
}
