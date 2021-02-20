using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YallaNakol.Data.Models
{
    public class Category 
    {
        public Category()
        {
            Restaurants = new HashSet<Restaurant>();
            Dishes = new HashSet<Dish>();
        }
        [Key]
        public int Id { get; set; }

        [Required, Display(Name = "Restaurant Name")]
        [MaxLength(20), MinLength(3)]
        public string Name { get; set; }

        [MinLength(5)]
        public string Description { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; }
        public virtual ICollection<Dish> Dishes { get; set; }

        public int Islam { get; set; }

    }
}
