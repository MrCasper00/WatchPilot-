using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Data.Entities;
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

        public UserDTO Add(string username, string password)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@username", username),
                new SqlParameter("@password", password),

            };

            int userId = databaseConnection.ExecuteScalar($@"
                INSERT INTO dbo.[user](username, password)
                VALUES (@username, @password); SELECT SCOPE_IDENTITY();", parameters);

            return Get(userId);
        }

        public void Update(User user)
        {

        }

        public void Delete(User user)
        {

        }

        public UserDTO Get(string username)
        {
            User user = databaseConnection.ExecuteQuery(
                $"SELECT * FROM dbo.[user] where username = '{username}'",
                                reader => new User
                                {
                    userID = int.Parse(reader["UserID"].ToString()),
                    username = reader["Username"].ToString(),
                    password = reader["Password"].ToString()
                }
                                                             );

            return ToDTO(user);
        }

        public UserDTO Get(int id)
        {
            User user = databaseConnection.ExecuteQuery(
                $"SELECT * FROM dbo.[user] WHERE UserID = '{id}'",
                reader => new User
                {
                    userID = int.Parse(reader["UserID"].ToString()),
                    username = reader["Username"].ToString(),
                    password = reader["Password"].ToString()
                }
                );



            return ToDTO(user);
        }

        private UserDTO ToDTO(User user)
        {
            return user == null ? null : new UserDTO
            {
                UserID = user.userID,
                Username = user.username,
                Password = user.password
            };
        }

    }
}
