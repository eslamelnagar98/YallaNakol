using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YallaNakol.Data.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }

        public int DishId { get; set; }
        public virtual Dish Dish { get; set; }

        [Range(0,int.MaxValue,ErrorMessage ="Amount Cant not be Less Than one")]
        public int Amount { get; set; }

        [Required]
        public string ShoppingCartId { get; set; }


    }
}
