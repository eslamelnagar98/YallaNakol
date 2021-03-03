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
    [Authorize(Roles = UserRoles.Admin)]
    public class CategoriesController : Controller
    {
        private readonly ICategory _repo;

        public CategoriesController(ICategory context)
        {
            _repo = context;
        }

        // GET: Categories
        public IActionResult Index()
        {
            return View(_repo.AllCategories);
        }

        // GET: Categories/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Category = _repo.GetCategoryById(id);
            if (Category == null)
            {
                return NotFound();
            }
            return View(Category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create([Bind("Id,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                _repo.AddCategory(category);
                _repo.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Category = _repo.GetCategoryById(id);
            if (Category == null)
            {
                return NotFound();
            }
            return View(Category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Description")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repo.UpdateCategory(category);
                    _repo.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_repo.CategoryExists(category.Id))
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

            return View(category);
        }

        // GET: Categories/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _repo.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _repo.GetCategoryById(id);
            _repo.DeleteCategory(category);
            _repo.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
