using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIoTServer.Auth
{
    public interface ITokenProvider
    {
        public string GenerateToken(IUser user);
        public bool ValidateToken(string token);
    }
}
