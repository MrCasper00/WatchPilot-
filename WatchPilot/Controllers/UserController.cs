using Microsoft.AspNetCore.Mvc;
using WatchPilot.Logic.DataTransferObjects;
using WatchPilot.Logic.Exceptions;
using WatchPilot.Logic.Interfaces;
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
                UserViewModel ViewModel = new UserViewModel().FromDTO(user);



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
