using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wa.Pizza.Core.Model.AuthenticateController
{
    public class AuthResponse
    {
        public string Status { get; set; }

        public string Message { get; set; }
    }
}
