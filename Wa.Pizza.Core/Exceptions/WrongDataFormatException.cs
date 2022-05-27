﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wa.Pizza.Core.Exceptions
{
    public class WrongDataFormatException : Exception
    {

        public WrongDataFormatException()
        {

        }

        public WrongDataFormatException(string message) : base(message)
        {

        }

    }
}
