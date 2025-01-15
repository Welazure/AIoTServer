using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIoTServer.Auth
{
    public interface IUser
    {
        public string Username { get; }
        public string Password { get; }
    }
}
