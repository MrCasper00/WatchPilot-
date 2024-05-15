using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic.DataTransferObjects;
using WatchPilot.Logic.Interfaces;
using WatchPilot.Logic.Entities;
using WatchPilot.Logic.Exceptions;

namespace WatchPilot.Test
{
    public class MockUserDAO : IUserDAO
    {
        List<UserDTO> users = new List<UserDTO>();

        public UserDTO GetByID(UserDTO userToObtain)
        {
            foreach (var user in users)
            {
                if (user.UserID == userToObtain.UserID)
                {
                    return new UserDTO
                    {
                        UserID = user.UserID,
                        Username = user.Username,
                        Password = user.Password
                    };
                }
            }

            return null;
        }

        public UserDTO GetByUsername(UserDTO userToObtain)
        {
            foreach (var user in users)
            {
                if (user.Username == userToObtain.Username)
                {
                    return new UserDTO
                    {
                        UserID = user.UserID,
                        Username = user.Username,
                        Password = user.Password
                    };
                }
            }

            return null;
        }


        public UserDTO Add(UserDTO userToAdd)
        {
            UserDTO user = new UserDTO { UserID = users.Count + 1, Username = userToAdd.Username, Password = userToAdd.Password };
            users.Add(user);
            return user;
        }

    }
}
