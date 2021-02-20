using System;
using System.Collections.Generic;
using System.Text;
using YallaNakol.Data.Models;

namespace YallaNakol.Data.Services
{
    public interface IDish
    {
        IEnumerable<Dish> AllDishes { get; }
        Dish GetDishById(int? DishId);
        void AddDish(Dish dish);
        void UpdateDish(Dish dish);
        void DeleteDish(Dish dish);
        bool DishExists(int id);
    }
}
