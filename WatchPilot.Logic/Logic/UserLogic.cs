using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic.DataTransferObjects;
using WatchPilot.Logic.Exceptions;
using WatchPilot.Logic.Interfaces;
using BCrypt.Net;

namespace WatchPilot.Logic.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserDAO userDAO;

        private int maxUsernameLength = 50;
        private int minUsernameLength = 1;
        private int minValidId = 1;
        public UserLogic(IUserDAO userDAO)
        {
            this.userDAO = userDAO;
        }

        

        public UserDTO CreateUser(string username, string password)
        {
            //Validatie
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("username or password cannot be null or empty");
            }
            if (username.Length > maxUsernameLength || username.Length < minUsernameLength)
            {
                throw new ArgumentException("username cannot be longer than 50 characters or shorter than 1");
            }
            //Einde validatie

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, workFactor: 15);
            UserDTO newUser = userDAO.Add(username, hashedPassword);

       
            if (newUser == null)
            {
                throw new UserAlreadyExistsOrPasswordInvalidException();
            }
            

            
            
            return newUser;
        }


        public UserDTO LoginUser(string username, string password)
        {
            //Validatie
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("username or password cannot be null or empty");
            }
            if (username.Length > maxUsernameLength || username.Length < minUsernameLength)
            {
                throw new ArgumentException("username cannot be longer than 50 characters or shorter than 1");
            }
            //Einde validatie

            UserDTO userDTO = userDAO.Get(username);

            
            if (userDTO == null)
            {
                throw new UserNotFoundException();
            }
     
            if (!BCrypt.Net.BCrypt.Verify(password, userDTO.Password))
            {
                throw new UserInfoNoMatchException();
            }

            return userDTO;
        }


        public UserDTO ObtainUser(int id)
        {
            if (id < minValidId)
            {
                throw new ArgumentException("id cannot be less than 1");
            }

            UserDTO userDTO = userDAO.Get(id);

            if (userDTO == null)
            {
                throw new UserNotFoundException();
            }

            return userDTO;

        }


    }
}
