﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchPilot.Logic.Exceptions
{
    public class PasswordException : Exception
    {
        public PasswordException(string message) : base(message)
        {
        }
    }
}
