using System;
using System.Collections.Generic;
using System.Text;
using YallaNakol.Data.Models;

namespace YallaNakol.Data.Services
{
    public interface IMenu
    {
        IEnumerable<Menu> AllMenus { get; }
        Menu GetMenuById(int? menuId);
        void AddMenu(Menu menu);
        void UpdateMenu(Menu menu);
        void DeleteMenu(Menu menu);
        bool MenuExists(int id);
        int SaveChanges();
    }
}
