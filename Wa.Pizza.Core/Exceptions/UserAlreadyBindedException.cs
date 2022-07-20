using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wa.Pizza.Core.Exceptions
{
    public class UserAlreadyBindedException : Exception
    {

        public UserAlreadyBindedException()
        {

        }

        public UserAlreadyBindedException(string message) : base(message)
        {

        }

    }
}
