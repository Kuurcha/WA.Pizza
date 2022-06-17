using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wa.Pizza.Core.Exceptions
{
    public class WrongDataFormatException : Exception
    {
        private static string _defaultMessage = "Invalid data format: " + System.Environment.NewLine;
        public WrongDataFormatException()
        {
            
        }

        public WrongDataFormatException(string message) : base(_defaultMessage + message)
        {
            
        }

    }
}
