using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic.Entities;
using WatchPilot.Logic.DataTransferObjects;
using WatchPilot.Logic.Interfaces;

namespace WatchPilot.Data.DataAccessObjects
{
    public class UserDAO : IUserDAO
    {
        private readonly DatabaseConnection databaseConnection;

        public UserDAO(DatabaseConnection db)
        {
            databaseConnection = db;
        }

        public UserDTO Add(UserDTO userToAdd)
        {
            if (userToAdd.Username == null || userToAdd.Password == null)
            {
                throw new Exception("Username or password is null");
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@username", userToAdd.Username),
                new SqlParameter("@password", userToAdd.Password),

            };

            
            int userId = databaseConnection.ExecuteScalar($@"
                INSERT INTO dbo.[user](username, password)
                VALUES (@username, @password); SELECT SCOPE_IDENTITY();", parameters);

            UserDTO user = new UserDTO
            {
                UserID = userId,
                Username = userToAdd.Username,
                Password = userToAdd.Password
            };

            return GetByID(user);
        }

        public void Update(User user)
        {

        }

        public void Delete(User user)
        {

        }

        public UserDTO GetByUsername(UserDTO userToGet)
        {
            if (userToGet.Username == null)
            {
                throw new Exception("Username is null");
            }

            User user = databaseConnection.ExecuteQuery(
              $"SELECT * FROM dbo.[user] where username = '{userToGet.Username}'",
              reader =>
              {
                  int userID = int.Parse(reader["UserID"].ToString());
                  string username = reader["Username"].ToString();
                  string password = reader["Password"].ToString();
                  return new User(username, password, userID);
              }
                );

            return ToDTO(user);
        }

        public UserDTO GetByID(UserDTO userToGet)
        {
            if (userToGet.UserID == null)
            {
                throw new Exception("UserID is null");
            }

            User user = databaseConnection.ExecuteQuery(
              $"SELECT * FROM dbo.[user] WHERE UserID = {userToGet.UserID}",
              reader =>
              {
                  int userID = int.Parse(reader["UserID"].ToString());
                  string username = reader["Username"].ToString();
                  string password = reader["Password"].ToString();
                  return new User(username, password, userID);
                }
                );

            return ToDTO(user);
        }

        private UserDTO ToDTO(User user)
        {
            return user == null ? null : new UserDTO
            {
                UserID = (int)user.UserID,
                Username = user.Username,
                Password = user.Password
            };
        }

    }
}
