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

namespace YallaNakol.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DishesController : Controller
    {
        private readonly IDish _dishRepo;
        private readonly IRestaurant _restRepo;
        private readonly ICategory _catRepo;
        private readonly IMenu _menuRepo;

        public DishesController(IDish dish, IRestaurant rest, ICategory cat, IMenu menu)
        {
            _dishRepo = dish;
            this._restRepo = rest;
            this._catRepo = cat;
            this._menuRepo = menu;
        }
     
        public IActionResult Index(int? id)
        {
            TempData["RestID"] = id;
            return View(_dishRepo.AllDishes.Where(D=>D.MenuId == _restRepo.GetRestaurantById(id).MenuId).ToList());
        }

        // GET: Dishes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dish = _dishRepo.GetDishById(id);
            if (dish == null)
            {
                return NotFound();
            }
            ViewBag.RestID = TempData["RestID"];
            TempData.Keep("RestID");
            return View(dish);
        }

        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_catRepo.AllCategories, "Id", "Name");
            ViewData["MenuId"] = new SelectList(_menuRepo.AllMenus, "Id", "Id");
            ViewBag.RestID = TempData["RestID"];
            TempData.Keep("RestID");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Dish dish)
        {
            if (ModelState.IsValid)
            {
                _dishRepo.AddDish(dish);
                _dishRepo.SaveChanges();
                return RedirectToAction(nameof(Index), new { id = TempData["RestID"] });
            }
            ViewData["CategoryId"] = new SelectList(_catRepo.AllCategories, "Id", "Name");
            ViewData["MenuId"] = new SelectList(_menuRepo.AllMenus, "Id", "Id");
            ViewBag.RestID = TempData["RestID"];
            return View(dish);
        }

        // GET: Dishes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = _dishRepo.GetDishById(id);
            if (dish == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_catRepo.AllCategories, "Id", "Name",dish.CategoryId);
            ViewData["MenuId"] = new SelectList(_menuRepo.AllMenus, "Id", "Id",dish.MenuId);
            ViewBag.RestID = TempData["RestID"];
            TempData.Keep("RestID");
            return View(dish);
        }

        // POST: Dishes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Dish dish)
        {
            if (id != dish.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _dishRepo.UpdateDish(dish);
                    _dishRepo.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_dishRepo.DishExists(dish.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = TempData["RestID"] });
            }
            ViewData["CategoryId"] = new SelectList(_catRepo.AllCategories, "Id", "Name",dish.CategoryId);
            ViewData["MenuId"] = new SelectList(_menuRepo.AllMenus, "Id", "Id",dish.MenuId);
            ViewBag.RestID = TempData["RestID"];
            TempData.Keep("RestID");
            return View(dish);
        }

        // GET: Dishes/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = _dishRepo.GetDishById(id);
            if (dish == null)
            {
                return NotFound();
            }
            ViewBag.RestID = TempData["RestID"];
            TempData.Keep("RestID");
            return View(dish);
        }

        // POST: Dishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var dish = _dishRepo.GetDishById(id);
            _dishRepo.DeleteDish(dish);
            _dishRepo.SaveChanges();
            return RedirectToAction(nameof(Index),new { id = TempData["RestID"] });
        }
    }
}
