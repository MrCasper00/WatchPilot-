using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic.Exceptions;

namespace WatchPilot.Logic.Entities
{
    public class User
    {
        public string? Username { get; private set; }
        public string? Password { get; private set; }
        public int? UserID { get; private set; }


        public User(string? username, string? password, int? userID)
        {
            if (username != null)
            {
                ValidateUsername(username);
                this.Username = username;
            }

            if (password != null)
            {
                ValidatePassword(password);
                this.Password = password;
            }

            if (userID != null)
            {
                ValidateUserID((int)userID);
                this.UserID = (int)userID;
            }
        }
        private void ValidateUsername(string username)
        {
            if (username.Length < 1 || username.Length > 50)
            {
                throw new UsernameException("Username cannot be longer than 50 characters or shorter than 1");
            }
        }

        private void ValidatePassword(string password)
        {
            if (password.Length < 1 || password.Length > 50)
            {
                throw new PasswordException("Password cannot be longer than 50 characters or shorter than 1");
            }
        }

        private void ValidateUserID(int userID)
        {
            if (userID < 0)
            {
                throw new ArgumentException("UserID cannot be lower than 0");
            }
        }

        public void UpdateUserID(int id)
        {
            ValidateUserID(id);
            this.UserID = id;
        }

        public void UpdateUsername(string username)
        {
            ValidateUsername(username);
            this.Username = username;
        }

        public void UpdatePassword(string password)
        {
            ValidatePassword(password);
            this.Password = password;
        }
    }
}
