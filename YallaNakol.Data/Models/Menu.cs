using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YallaNakol.Data.Models
{
    public class Menu
    {
        public Menu() => Dishes = new HashSet<Dish>();
        [Key]
        public int Id { get; set; }
        public Dish DishOfTheWeek { get; set; }
        public virtual ICollection<Dish> Dishes { get; set; }
    }
}
