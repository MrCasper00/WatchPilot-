using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchPilot.Logic.Exceptions
{
    public class UserInfoNoMatchException : Exception
    {
        public  UserInfoNoMatchException() : base($"User info doesn't match known user-data.")
        {
        }
    }
}
