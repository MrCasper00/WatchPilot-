using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic.DataTransferObjects;
using WatchPilot.Logic.Entities;

namespace WatchPilot.Logic.Interfaces
{
    public interface IUserLogic
    {
        UserDTO ObtainUser(User userToGet);
        UserDTO LoginUser(User userToLogin);

        UserDTO CreateUser(User userToCreate);


    }
}
