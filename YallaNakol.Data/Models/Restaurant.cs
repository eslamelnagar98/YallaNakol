using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YallaNakol.Data.Models
{
    public class Restaurant
    {
        public Restaurant() => Categories = new HashSet<Category>();
        [Key]
        public int Id { get; set; }

        [Required,Display(Name ="Restaurant Name")]
        [MaxLength(20),MinLength(3)]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }

        [MinLength(5)]
        public string Description { get; set; }
        [Required]
        public string Rate { get; set; }
        [Required]
        public string ImageUrl { get; set; }

        [Required,RegularExpression(@"^01[0125]\d{8}$",ErrorMessage ="Invalid Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string WorkingHours { get; set; }
        public Menu Menu { get; set; }
        [Required]
        public DeliveryAreas DeliveryAreas { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
