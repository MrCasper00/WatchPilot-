using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic.DataTransferObjects;

namespace WatchPilot.Logic.Interfaces
{
    public interface IUserDAO
    {
        UserDTO GetByID(UserDTO userToGet);
        UserDTO GetByUsername(UserDTO userToGet);
        UserDTO Add(UserDTO userToAdd);
    }
}
