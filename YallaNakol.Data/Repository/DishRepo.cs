using System;
using System.Collections.Generic;
using System.Text;
using YallaNakol.Data.Models;
using YallaNakol.Data.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace YallaNakol.Data.Repository
{
    public class DishRepo : IDish
    {
        private readonly ApplicationDbContext _applicationDbContext;
 
        public DishRepo(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
        public IEnumerable<Dish> AllDishes => 
            _applicationDbContext.Dishes.AsNoTracking().ToList();

        public Dish GetDishById(int? DishId) =>
            _applicationDbContext.Dishes.FirstOrDefault(I => I.Id == DishId);
        public void AddDish(Dish Dish)
        {
            _applicationDbContext.Dishes.Add(Dish);
            _applicationDbContext.SaveChanges();
        }
        public void UpdateDish(Dish Dish)
        {
            _applicationDbContext.Update(Dish);
            _applicationDbContext.SaveChanges();
        }
        public void DeleteDish(Dish Dish)
        {
            _applicationDbContext.Remove(Dish);
            _applicationDbContext.SaveChanges();
        }
        public bool DishExists(int id)
        {
            return _applicationDbContext.Dishes.Any(e => e.Id == id);
        }



    }
}
