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
    public class MenusController : Controller
    {
        private readonly IMenu _repo;

        public MenusController(IMenu context)
        {
            _repo = context;
        }

        // GET: Menus
        public IActionResult Index()
        {
            return View(_repo.AllMenus);
        }

        public IActionResult Create()
        {
            _repo.AddMenu(new Menu());
            _repo.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Menus/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = _repo.GetMenuById(id);
                
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  IActionResult DeleteConfirmed(int id)
        {
            var menu = _repo.GetMenuById(id);

            _repo.DeleteMenu(menu);
            _repo.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    
    }
}
