using AIoTServer.Data.DataProvider;
using AIoTServer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIoTServer.Auth
{
    public class AuthHandler
    {
        private static AuthHandler _instance;
        private static readonly object Padlock = new();

        private readonly IAuthProvider _data;

        private AuthHandler(IAuthProvider provider)
        {
            _data = provider;
        }

        public static AuthHandler Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (Padlock)
                {
                    _instance ??= new AuthHandler(new DefaultAuthProvider());
                }

                return _instance;
            }
        }
    }
}
