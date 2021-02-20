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

        public IEnumerable<Dish> AllDishes => 
            _applicationDbContext.Dishes
                                 .AsNoTracking()
                                 .ToList();
        public Dish GetDishById(int dishId) =>
            _applicationDbContext.Dishes.FirstOrDefault(I => I.Id == dishId); 
    }
}
