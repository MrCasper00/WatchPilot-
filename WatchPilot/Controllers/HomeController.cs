using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WatchPilot.Logic;
using WatchPilot.Models;

namespace WatchPilot.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUserLogic _UserLogic;

        public HomeController(ILogger<HomeController> logger, IUserLogic userLogic)
        {
            _logger = logger;
            _UserLogic = userLogic;
        }

        public IActionResult Index()
        {
            UserViewModel ViewModel = new UserViewModel();




            return View(ViewModel);
        }

        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
