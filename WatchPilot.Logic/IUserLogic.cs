using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchPilot.Logic
{
    public interface IUserLogic
    {
        User ObtainUser(int id);
    }
}
