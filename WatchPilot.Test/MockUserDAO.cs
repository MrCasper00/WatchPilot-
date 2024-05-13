using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic.DataTransferObjects;
using WatchPilot.Logic.Interfaces;

namespace WatchPilot.Test
{
    public class MockUserDAO : IUserDAO
    {
        List<UserDTO> users = new List<UserDTO>();

        public UserDTO Get(int id)
        {
            foreach (UserDTO user in users)
            {
                if (user.UserID == id)
                {
                    return user;
                }
            }

            return null;
        }
        public UserDTO Get(string username)
        {
            foreach (UserDTO user in users)
            {
                if (user.Username == username)
                {
                    return user;
                }
            }
            return null;
        }

        public UserDTO Add(string username, string password)
        {
           
            UserDTO user =  new UserDTO { Username = username, Password = password, UserID = 1 };
            users.Add(user);
            return user;
        }
    }
}
