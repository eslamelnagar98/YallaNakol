using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YallaNakol.Data.Models;

namespace YallaNakol.Data.ViewModel
{
    public class RestaurantMenu
    {
        public Restaurant Restaurant{ get;set;}
        public IEnumerable<Dish> Dishes { get; set; }
        public IEnumerable<Category> Category { get; set; }
    }
}
