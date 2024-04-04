using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WatchPilot.Data
{
    public class UserDAO : IUserDAO
    {
        private DatabaseConnection databaseConnection;

        public UserDAO()
        {
            databaseConnection = new DatabaseConnection();
        }

        public void Add(string username, string password)
        {
            

        }

        public void Update(User user)
        {

        }

        public void Delete(User user)
        {

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
            return new UserDTO
            {
                UserID = user.userID,
                Username = user.username
            };
        }

    }
}
