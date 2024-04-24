using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchPilot.Logic.Exceptions
{
    public class PasswordInvalidException : Exception
    {
        public PasswordInvalidException() : base($"The password is invalid")
        {
        }
    }
}
