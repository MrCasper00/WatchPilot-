using WatchPilot.Logic.DataTransferObjects;

namespace WatchPilot.Models
{
    public class UserViewModel
    {
        public int UserID { get; set; }
        public string Username { get; set; }




        public UserDTO ToDTO()
        {
            return new UserDTO
            {
                UserID = this.UserID,
                Username = this.Username
            };
        }

        public UserViewModel FromDTO(UserDTO userDTO)
        {
            return new UserViewModel
            {
                UserID = (int)userDTO.UserID,
                Username = userDTO.Username
            };
        }


    }

}
