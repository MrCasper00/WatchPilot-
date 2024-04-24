using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic.DataTransferObjects;

namespace WatchPilot.Logic.Interfaces
{
    public interface IUserLogic
    {
        UserDTO ObtainUser(int id);
        UserDTO LoginUser(string username, string password);

        UserDTO CreateUser(string username, string password);


    }
}
