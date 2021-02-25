using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using YallaNakol.Data.Models;
using YallaNakol.Data.Services;
using YallaNakol.Data.ViewModel;
using YallaNakol.UI.Models;

namespace YallaNakol.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRestaurant _restaurant;

        public HomeController(ILogger<HomeController> logger,IRestaurant restaurant)
        {
            _logger = logger;
            this._restaurant = restaurant;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                Restaurants = _restaurant.AllRestaurants
            };

            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error([FromServices]UserManager<ApplicationUser> userManager)
        {
            //_logger.LogError();
            var user = await userManager.GetUserAsync(this.User);
            var exeptionHandlerFeature=HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _logger.LogError($"UserID:{user.Id}|User Mail:{user.Email} | " +
                             $"ErrorPath:{exeptionHandlerFeature.Path} | Exeption:{exeptionHandlerFeature.Error.Message}");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
