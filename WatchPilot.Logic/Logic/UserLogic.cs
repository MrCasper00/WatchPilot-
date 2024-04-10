﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic.DataTransferObjects;
using WatchPilot.Logic.Exceptions;
using WatchPilot.Logic.Interfaces;


namespace WatchPilot.Logic.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserDAO userDAO;

        public UserLogic(IUserDAO userDAO)
        {
            this.userDAO = userDAO;
        }


        public UserDTO ObtainUser(int id)
        {

            UserDTO userDTO = userDAO.Get(id);

            if (userDTO == null)
            {
                throw new UserNotFoundException();
            }

            return userDTO;

        }


    }
}