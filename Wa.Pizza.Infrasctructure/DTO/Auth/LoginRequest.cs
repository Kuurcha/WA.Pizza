using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wa.Pizza.Core.Model.AuthenticateController
{
    public class LoginRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
