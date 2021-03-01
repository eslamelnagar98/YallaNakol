using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YallaNakol.Data.Models;
using YallaNakol.Data.Services;
using YallaNakol.Data.ViewModel;

namespace YallaNakol.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RestaurantsController : Controller
    {
        private readonly IRestaurant _restRepo; 
        private readonly IDish _dishRepo;
        private readonly ICategory _catRepo;
        private readonly IMenu _menuRepo;

        public RestaurantsController(IRestaurant context,IDish dish, ICategory cat, IMenu menu)
        {
            this._restRepo = context;
            this._dishRepo = dish;
            this._catRepo = cat;
            this._menuRepo = menu;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            //string y = null;
            //var x = y.Length;
            return View(_restRepo.AllRestaurants);
        }
        [AllowAnonymous]
        // GET: Restaurants/Details/5
        public IActionResult CustomerDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var restaurant = _restRepo.GetRestaurantById(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            var restaurantMenu = new RestaurantMenu
            {
                Dishes = _dishRepo.AllDishes.Where(m => m.MenuId == restaurant.MenuId),
                //Category = cat.AllCategories,
                Category = restaurant.Categories,
                Restaurant = restaurant
            };
            
            return View(restaurantMenu);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var restaurant = _restRepo.GetRestaurantById(id);
            if (restaurant == null)
            {
                return NotFound();
            }

          
            return View(restaurant);
        }
        // GET: Restaurants/Create
        public IActionResult Create()
        {
            ViewData["MenuId"] = new SelectList(_menuRepo.AllMenus, "Id", "Id");

            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Address,Description,Rate,ImageUrl,PhoneNumber,WorkingHours,DeliveryAreas")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _restRepo.AddRestaurant(restaurant);
                _restRepo.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuId"] = new SelectList(_menuRepo.AllMenus, "Id", "Id");

            return View(restaurant);
        }
        
        // GET: Restaurants/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //ViewBag.MenuId = new SelectList()

            var restaurant = _restRepo.GetRestaurantById(id);

            if (restaurant == null)
            {
                return NotFound();
            }
            ViewData["MenuId"] = new SelectList(_menuRepo.AllMenus, "Id", "Id", restaurant.MenuId);

            return View(restaurant);
        }
        
        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Address,Description,Rate,ImageUrl,PhoneNumber,WorkingHours,DeliveryAreas")] Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _restRepo.UpdateRestaurant(restaurant);
                    _restRepo.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_restRepo.RestaurantExists(restaurant.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuId"] = new SelectList(_menuRepo.AllMenus, "Id", "Id");

            return View(restaurant);
        }
        
        // GET: Restaurants/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = _restRepo.GetRestaurantById(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var restaurant = _restRepo.GetRestaurantById(id);
             _restRepo.DeleteRestaurant(restaurant);
            _restRepo.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
