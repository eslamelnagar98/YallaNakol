using System;
using System.Collections.Generic;
using System.Text;
using YallaNakol.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace YallaNakol.Data.Services
{
    public interface IShoppingCart
    {
        string CartId { get;}
        IEnumerable<ShoppingCartItem> ShoppingCartItems { get; }

        void AddDish(Dish dish, int amount);
        void RemoveDish(Dish dish);
        void SaveChanges();
        decimal TotalCost();
        bool IsEmpty { get; }
        public int ResturantId { get; set; }

        void ClearItems();
    }
}
