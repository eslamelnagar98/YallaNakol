using System;
using System.Collections.Generic;
using System.Text;
using YallaNakol.Data.Models;

namespace YallaNakol.Data.Services
{
    public interface IMenu
    {
        IEnumerable<Dish> AllDishes { get; }
        Dish GetDishById(int dishId);
    }
}
