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

        public IActionResult Login()
        {
            LoginViewModel view = new LoginViewModel();
            return View(view);
        }

        public IActionResult Register()
        {
            RegisterViewModel view = new RegisterViewModel();
            return View(view);
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {
            try
            {
                UserDTO user = _UserLogic.CreateUser(register.Username, register.Password);

            } catch (UserAlreadyExistsException ex)
            {
                ModelState.AddModelError(nameof(register.Username), ex.Message);
                return View(register);
            } 
            catch (UsernameException ex)
            {
                ModelState.AddModelError(nameof(register.Username), ex.Message);
                return View(register);
            }
            catch (PasswordException ex)
            {
                ModelState.AddModelError(nameof(register.Password), ex.Message);
                return View(register);
            }
            catch (UnkownErrorException ex)
            {
                ModelState.AddModelError("RegisterError", ex.Message);
                return View(register);
            }
            
            return RedirectToAction("Login", "User");
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {   
            try
            {
                UserDTO user = _UserLogic.LoginUser(login.Username, login.Password);

                var claims = new List<Claim>
                {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Name, user.Username)

                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            } catch (UserInfoNoMatchException)
            {
                ModelState.AddModelError("LoginError", "Username or password is incorrect or user does not exist");
                return View(login);
            } catch (UsernameException ex)
            {
                ModelState.AddModelError(nameof(login.Username), ex.Message);
                return View(login);
            }
            catch (PasswordException ex)
            {
                ModelState.AddModelError(nameof(login.Password), ex.Message);
                return View(login);
            } catch (UnkownErrorException ex)
            {
                ModelState.AddModelError("LoginError", ex.Message);
                return View(login);
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
