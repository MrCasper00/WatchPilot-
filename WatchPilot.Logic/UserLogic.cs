using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Data;

namespace WatchPilot.Logic
{
    public class UserLogic
    {
        public void ObtainUser(int id)
        {
            User user = new User();
            UserDAO userDAO = new UserDAO();
            UserDTO userDTO = new UserDTO();
            userDTO = userDAO.Get(id);

            user.UserID = userDTO.UserID;
            user.Username = userDTO.Username;

        }
    }
}
