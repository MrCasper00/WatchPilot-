using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchPilot.Logic.Exceptions
{
    public class UserAlreadyExistsOrPasswordInvalidException : Exception
    {
        public UserAlreadyExistsOrPasswordInvalidException() : base($"An user with this username already exists or the password is invalid")
        {
        }
    }
}
