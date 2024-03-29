﻿using System;
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
            _applicationDbContext.Dishes.Include(c=>c.Category).AsNoTracking().ToList();

        public Dish GetDishById(int? DishId) =>
            _applicationDbContext.Dishes.FirstOrDefault(I => I.Id == DishId);
        public void AddDish(Dish Dish) =>
        
            _applicationDbContext.Dishes.Add(Dish);
        
        public void UpdateDish(Dish Dish) =>
        
            _applicationDbContext.Update(Dish);
        
        public void DeleteDish(Dish Dish) =>
        
            _applicationDbContext.Remove(Dish);
        
        public bool DishExists(int id) =>
        
             _applicationDbContext.Dishes.Any(e => e.Id == id);
        
        public int SaveChanges() => _applicationDbContext.SaveChanges();



    }
}
