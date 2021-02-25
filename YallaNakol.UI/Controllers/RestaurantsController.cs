using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YallaNakol.Data.Models;
using YallaNakol.Data.Services;
using YallaNakol.Data.ViewModel;

namespace YallaNakol.UI.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly IRestaurant _repo; 
        private readonly IDish dish;
        private readonly ICategory cat;



        public RestaurantsController(IRestaurant context,IDish dish, ICategory cat)
        {
            _repo = context;
            this.dish = dish;
            this.cat = cat;

        }

        public IActionResult Index()
        {
            string y = null;
            var x = y.Length;
            return View(_repo.AllRestaurants);
        }
        
        // GET: Restaurants/Details/5
        public IActionResult CustomerDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var restaurant = _repo.GetRestaurantById(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            var restaurantMenu = new RestaurantMenu
            {
                Dishes = dish.AllDishes.Where(m => m.MenuId == restaurant.MenuId),
                Category = cat.AllCategories,
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
            var restaurant = _repo.GetRestaurantById(id);
            if (restaurant == null)
            {
                return NotFound();
            }

          
            return View(restaurant);
        }
        // GET: Restaurants/Create
        public IActionResult Create()
        {
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
                _repo.AddRestaurant(restaurant);
                _repo.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }
        
        // GET: Restaurants/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = _repo.GetRestaurantById(id);
            if (restaurant == null)
            {
                return NotFound();
            }
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
                    _repo.UpdateRestaurant(restaurant);
                    _repo.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_repo.RestaurantExists(restaurant.Id))
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
            return View(restaurant);
        }
        
        // GET: Restaurants/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = _repo.GetRestaurantById(id);
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
            var restaurant = _repo.GetRestaurantById(id);
             _repo.DeleteRestaurant(restaurant);
            _repo.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
