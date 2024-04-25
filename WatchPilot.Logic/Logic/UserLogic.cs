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
using System.Net.Http.Headers;

namespace WatchPilot.Logic.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserDAO userDAO;

        private int maxUsernameLength = 50;
        private int minUsernameLength = 1;
        private int minValidId = 1;
        private int maxPasswordLength = 50;
        private int minPasswordLength = 1;
        public UserLogic(IUserDAO userDAO)
        {
            this.userDAO = userDAO;
        }

        private void ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new UsernameException("username cannot be empty");
            }
            if (username.Length > maxUsernameLength || username.Length < minUsernameLength)
            {
                throw new UsernameException("username cannot be longer than 50 characters or shorter than 1");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new PasswordException("password cannot be empty");
            }
            if (password.Length > maxPasswordLength || password.Length < minPasswordLength)
            {
                throw new PasswordException("password cannot be longer than 50 characters or shorter than 1");
            }

        }


        public UserDTO CreateUser(string username, string password)
        {
            
            ValidateUser(username, password);
            
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, workFactor: 15);

            try
            {
                if (userDAO.Get(username) == null)
                {
                    try
                    {
                        UserDTO newUser = userDAO.Add(username, hashedPassword);
                        Console.WriteLine("User Succesfully created");
                        return newUser;
                    }
                    catch (Exception e)
                    {
                        throw new UnkownErrorException();
                    }
                    
                } else
                {
                    throw new UserAlreadyExistsException();
                }
            } catch (Exception e)
            {
                if (e is UserAlreadyExistsException)
                {
                    throw new UserAlreadyExistsException();
                }
                throw new UnkownErrorException();
            }
            
        }


        public UserDTO LoginUser(string username, string password)
        {
            ValidateUser(username, password);

            try
            {
                UserDTO userDTO = userDAO.Get(username);
                if (userDTO == null)
                {
                    throw new UserInfoNoMatchException();
                }
                if (!BCrypt.Net.BCrypt.Verify(password, userDTO.Password))
                {
                    throw new UserInfoNoMatchException();
                }
                return userDTO;
            }
            catch (Exception e)
            {
                if (e is UserInfoNoMatchException)
                {
                    throw new UserInfoNoMatchException();
                }
                throw new UnkownErrorException();
            }
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
