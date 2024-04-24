using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchPilot.Logic.Exceptions
{
    public class UnkownErrorException : Exception
    {
        public UnkownErrorException() : base($"An unknown error occurred")
        {
        }
    }
}
