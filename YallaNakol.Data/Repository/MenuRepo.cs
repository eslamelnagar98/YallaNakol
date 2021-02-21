using System;
using System.Collections.Generic;
using System.Text;
using YallaNakol.Data.Models;
using YallaNakol.Data.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace YallaNakol.Data.Repository
{
    public class MenuRepo:IMenu
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public MenuRepo(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }

        public IEnumerable<Menu> AllMenus => 
            _applicationDbContext.Menus
                                 .AsNoTracking()
                                 .ToList();
        public Menu GetMenuById(int? menuId) => 
            _applicationDbContext.Menus.FirstOrDefault(I => I.Id == menuId);

        public void AddMenu(Menu Menu) =>
        
            _applicationDbContext.Menus.Add(Menu);
        
        public void UpdateMenu(Menu Menu) =>
        
            _applicationDbContext.Update(Menu);
        
        public void DeleteMenu(Menu Menu) =>
        
            _applicationDbContext.Remove(Menu);
        
        public bool MenuExists(int id) =>
        
             _applicationDbContext.Menus.Any(e => e.Id == id);
        
        public int SaveChanges() => _applicationDbContext.SaveChanges();
    }
}
