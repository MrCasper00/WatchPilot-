using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Data;

namespace WatchPilot.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserDAO userDAO;
        
        public UserLogic(IUserDAO userDAO)
        {
            this.userDAO = userDAO;
        }

        private User UserDTOToUser(UserDTO userDTO)
        {
            User user = new User
            {
                UserID = userDTO.UserID,
                Username = userDTO.Username
            };

            return user;
        }



        public User ObtainUser(int id)
        {
            
            UserDTO userDTO = userDAO.Get(id);
            return UserDTOToUser(userDTO);
            
        }


    }
}
