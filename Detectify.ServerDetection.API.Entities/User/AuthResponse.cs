using System;
using System.Collections.Generic;
using System.Text;

namespace Detectify.ServerDetection.API.Entities
{
    public class AuthResponse
    {
        public User UserInfo { get; set; }

        public string Token { get; set; }

        public string TokenType { get; set; }
    }
}
