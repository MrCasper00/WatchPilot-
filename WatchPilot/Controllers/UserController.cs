using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WatchPilot.Logic.DataTransferObjects;
using WatchPilot.Logic.Exceptions;
using WatchPilot.Logic.Interfaces;
using WatchPilot.Models;
using Microsoft.AspNetCore.Authorization;
using WatchPilot.Data.Entities;

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
        public IActionResult Register(string username, string password)
        {

            try
            {
                UserDTO user = _UserLogic.CreateUser(username, password);
            } catch (Exception ex)
            {
                return RedirectToAction("Register", "Home");
            }
            

            

            return RedirectToAction("Login", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {   
            try
            {
                UserDTO user = _UserLogic.LoginUser(username, password);

                var claims = new List<Claim>
                {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Name, user.Username)

                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            } catch (Exception ex)
            {
                
            }


            return RedirectToAction("GetShowOverviews", "ShowOverview");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Home");
        }
        

        [HttpPost]
        [Authorize]
        public IActionResult ObtainUserInfo()
        {
            string userIdstring = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId = int.Parse(userIdstring);
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
