using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YallaNakol.Data.Models;
using YallaNakol.Data.Services;

namespace YallaNakol.UI.Controllers
{
    public class DishesController : Controller
    {
        private readonly IDish _repo;

        public DishesController(IDish context)
        {
            _repo = context;
        }

        public IActionResult Index()
        {
            return View(_repo.AllDishes);
        }

        // GET: Dishes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dish = _repo.GetDishById(id);
            if (dish == null)
            {
                return NotFound();
            }
            return View(dish);
        }

        // GET: Dishes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dishes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Address,Description,Rate,ImageUrl,PhoneNumber,WorkingHours,DeliveryAreas")] Dish dish)
        {
            if (ModelState.IsValid)
            {
                _repo.AddDish(dish);
                _repo.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(dish);
        }

        // GET: Dishes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = _repo.GetDishById(id);
            if (dish == null)
            {
                return NotFound();
            }
            return View(dish);
        }

        // POST: Dishes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Address,Description,Rate,ImageUrl,PhoneNumber,WorkingHours,DeliveryAreas")] Dish dish)
        {
            if (id != dish.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _repo.UpdateDish(dish);
                    _repo.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_repo.DishExists(dish.Id))
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
            return View(dish);
        }

        // GET: Dishes/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = _repo.GetDishById(id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // POST: Dishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var dish = _repo.GetDishById(id);
            _repo.DeleteDish(dish);
            _repo.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
