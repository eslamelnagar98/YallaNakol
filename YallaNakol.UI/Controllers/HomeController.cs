using Microsoft.AspNetCore.Authorization;
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
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
