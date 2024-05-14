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
using WatchPilot.Logic.Entities;

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

        public UserDTO CreateUser(User userToCreate)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userToCreate.Password, workFactor: 15);
            UserDTO user = new UserDTO
            {
                Username = userToCreate.Username,
                Password = hashedPassword
            };

            try
            {
                if (userDAO.GetByUsername(user) == null)
                {
                    try
                    {
                        UserDTO newUser = userDAO.Add(user);
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


        public UserDTO LoginUser(User userToLogin)
        {
            UserDTO user = new UserDTO
            {
                Username = userToLogin.Username,
                Password = userToLogin.Password
            };

            try
            {
                UserDTO userDTO = userDAO.GetByUsername(user);
                if (userDTO == null)
                {
                    throw new UserInfoNoMatchException();
                }
                if (!BCrypt.Net.BCrypt.Verify(user.Password, userDTO.Password))
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


        public UserDTO ObtainUser(User userToGet)
        {
            UserDTO user = new UserDTO
            {
                UserID = userToGet.UserID
            };

            if (user.UserID < minValidId)
            {
                throw new ArgumentException("id cannot be less than 1");
            }

            UserDTO userDTO = userDAO.GetByID(user);

            if (userDTO == null)
            {
                throw new UserNotFoundException();
            }

            return userDTO;

        }


    }
}
