using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchPilot.Logic.Exceptions
{
    public class ShowException : Exception
    {
        public ShowException(string message) : base(message)
        {
        }
    }
}
