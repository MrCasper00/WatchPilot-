using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic;

namespace WatchPilot.Test
{
    public class MockUserDAO : IUserDAO
    {
        public UserDTO Get(int id)
        {
            if (id != 1)
            {
                return null;
            }
            else
            {
                return new UserDTO
                {
                    UserID = 1,
                    Username = "TestUser"
                };
            }
            
        }
    }
}
