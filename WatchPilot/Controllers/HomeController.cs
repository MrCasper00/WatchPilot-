using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WatchPilot.Logic.Interfaces;
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

        [Authorize]
        public IActionResult Index()
        {
            UserViewModel ViewModel = new UserViewModel();

            return View(ViewModel);
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Unauthorized()
        {
            return View("~/Views/Home/Unauthorized.cshtml");
        }

        public IActionResult UnknownError()
        {
            return View("~/Views/Home/UnknownError.cshtml");
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public IActionResult CreateShowOverview()
        {
            return PartialView("~/Views/Home/_CreateShowOverview.cshtml");
        }

        [Authorize]
        public IActionResult NewShow(int showOverviewID)
        {
            ShowViewModel viewModel = new ShowViewModel();
            viewModel.ShowOverViewID = showOverviewID;
            return PartialView("~/Views/Home/_NewShow.cshtml", viewModel);
        }

        

        [Authorize]
        public IActionResult ShowOverview()
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
