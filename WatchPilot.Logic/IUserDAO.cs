using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchPilot.Logic
{
    public interface IUserDAO
    {
        UserDTO Get(int id);
    }
}
