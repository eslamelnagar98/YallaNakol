using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text;

namespace YallaNakol.Data.Models
{
    public class Dish
    {
        [Key]

        public int Id { get; set; }

        [Required, Display(Name = "Dish Name")]
        [MaxLength(40), MinLength(5)]
        public string Name { get; set; }

        [MinLength(5)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public bool InStock { get; set; }
        public virtual Category Category { get; set; }

    }
}
