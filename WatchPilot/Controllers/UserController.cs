using Microsoft.AspNetCore.Mvc;
using WatchPilot.Logic;
using WatchPilot.Logic.Exceptions;
using WatchPilot.Models;

namespace WatchPilot.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserLogic _UserLogic;

        public UserController(IUserLogic userLogic)
        {
            _UserLogic = userLogic;
        }



        [HttpPost]
        public IActionResult ObtainUserInfo(int userId)
        {
            try
            {
                UserDTO user = _UserLogic.ObtainUser(userId);
                UserViewModel ViewModel = new UserViewModel
                {
                    UserID = user.UserID,
                    Username = user.Username
                };

                return View("~/Views/Home/Index.cshtml", ViewModel);
            }
            catch (UserNotFoundException ex)
            {
                return View("~/Views/Home/Index.cshtml", new UserViewModel 
                {  
                    UserID = -1,
                    Username = "User not found"
                });
            }
        }
    }
}
