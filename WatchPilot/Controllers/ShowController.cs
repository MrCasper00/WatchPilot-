using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WatchPilot.Data.Entities;
using WatchPilot.Logic.DataTransferObjects;
using WatchPilot.Logic.Exceptions;
using WatchPilot.Logic.Interfaces;
using WatchPilot.Logic.Logic;
using WatchPilot.Models;

namespace WatchPilot.Controllers
{
    public class ShowController : Controller
    {
        private readonly IImageLogic _ImageLogic;
        private readonly IShowLogic _ShowLogic;
        
        public ShowController(IImageLogic imageLogic, IShowLogic showLogic)
        {
            _ImageLogic = imageLogic;
            _ShowLogic = showLogic;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditShowLoad(int showID, int showOverViewID)
        {
            EditShowViewModel viewModel = new EditShowViewModel();
            viewModel.ShowID = showID;
            viewModel.ShowOverviewID = showOverViewID;
            return PartialView("_EditShow", viewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult DeleteShowLoad(int showID, int showOverviewID)
        {
            DeleteShowViewModel viewModel = new DeleteShowViewModel();
            viewModel.ShowID = showID;
            viewModel.ShowOverviewID = showOverviewID;
            return PartialView("_DeleteShow", viewModel);
        }


        public IActionResult DeleteShow(DeleteShowViewModel show)
        {
            string userIdstring = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId = int.Parse(userIdstring);
            if (ModelState.IsValid)
            {
                try
                {
                    _ShowLogic.DeleteShow(show.ShowID, show.ShowOverviewID, userId);
                    return RedirectToAction("GetShowOverviews", "ShowOverview");
                }
                catch (UnauthorizedAccessException)
                {
                    return RedirectToAction("Unauthorized", "Home");
                }
                catch (UnkownErrorException)
                {
                    return RedirectToAction("UnknownError", "Home");
                }
            }


            return RedirectToAction("GetShowDetails", "Show", new { showID = show.ShowID, showOverviewID = show.ShowOverviewID });

        }


        [HttpPost]
        [Authorize]
        public IActionResult EditShow(EditShowViewModel show)
        {
            string userIdstring = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId = int.Parse(userIdstring);
            if (ModelState.IsValid)
            {
                ShowDTO showDTO = new ShowDTO();
                showDTO.ShowID = show.ShowID;
                showDTO.Title = show.Title;
                showDTO.Description = show.Description;
                showDTO.TotalEpisodes = show.TotalEpisodes;
                showDTO.CurrentEpisode = show.CurrentEpisode;
                showDTO.LastEdited = DateTime.UtcNow;
                showDTO.ShowOverViewID = show.ShowOverviewID;
                
                try
                {
                    _ShowLogic.UpdateShow(showDTO, userId);
                } catch (UnauthorizedAccessException)
                {
                    return RedirectToAction("Unauthorized", "Home");
                } catch (UnkownErrorException)
                {
                    return RedirectToAction("UnknownError", "Home");
                }
               
            }


            return RedirectToAction("GetShowDetails", "Show", new {showID = show.ShowID, showOverviewID = show.ShowOverviewID});
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> NewShow(IFormFile picture, int totalEpisodes, string title, string description, int showOverviewID)
        {
            ShowViewModel NewShow = new ShowViewModel
            {
                Title = title,
                Description = description,
                TotalEpisodes = totalEpisodes,
                CurrentEpisode = 0,
                LastEdited = DateTime.UtcNow,
                ShowOverViewID = showOverviewID
            };

            //Dit later verplaatsen naar anderen plek
            if (picture == null)
            {
                NewShow.Picture = "";
            } else
            {
                using (var memoryStream = new MemoryStream())
                {
                    await picture.CopyToAsync(memoryStream);
                    var bytes = memoryStream.ToArray();
                    string path = await _ImageLogic.UploadImage(bytes, picture.FileName);
                    NewShow.Picture = path;
                }
            }

            ShowDTO showDTO = NewShow.ToDTO();

            string userIdstring = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId = int.Parse(userIdstring);

            try
            {
                _ShowLogic.AddShow(showDTO, userId);
            } catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Unauthorized", "Home");
            } catch (UnkownErrorException)
            {
                return RedirectToAction("UnknownError", "Home");
            } catch (ShowException e)
            {
                return RedirectToAction("GetShowOverviews", "ShowOverview");
            }
            


            return RedirectToAction("GetShowOverviews", "ShowOverview");
        }


        [HttpPost]
        [Authorize]
        public IActionResult GetAllInOverview(int showOverviewID)
        {
            string userIdstring = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId = int.Parse(userIdstring);
            try
            {
                List<ShowDTO> showsDTO = _ShowLogic.GetAll(showOverviewID, userId);
                List<ShowViewModel> showsViewModel = new List<ShowViewModel>();
                foreach (ShowDTO show in showsDTO)
                {
                    showsViewModel.Add(new ShowViewModel().FromDTO(show));
                }


                return PartialView("~/Views/Home/_Shows.cshtml", showsViewModel);
            } catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Unauthorized", "Home");
            } catch (UnkownErrorException)
            {
                return RedirectToAction("UnknownError", "Home");
            }
            
        }

        [Authorize]
        public IActionResult GetShowDetails(int showID, int showOverviewID)
        {
            string userIdstring = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId = int.Parse(userIdstring);

            try
            {
                ShowDTO show = _ShowLogic.GetShowDetails(showID, showOverviewID, userId);
                ShowDetailsViewModel viewModel = new ShowDetailsViewModel();
                viewModel.FromDTO(show);

                return View("~/Views/Show/ShowDetails.cshtml", viewModel);
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Unauthorized", "Home");
            } catch (UnkownErrorException)
            {
                return RedirectToAction("UnknownError", "Home");
            }
            
        }
    }
}
