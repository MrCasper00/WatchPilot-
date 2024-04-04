using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchPilot.Data
{
    public interface IUserDAO
    {
        UserDTO Get(int id);

    }
}
