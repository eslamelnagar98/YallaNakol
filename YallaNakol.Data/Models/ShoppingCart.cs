using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using YallaNakol.Data.Services;

namespace YallaNakol.Data.Models
{
    public class ShoppingCart :  IShoppingCart
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public IEnumerable<ShoppingCartItem> ShoppingCartItems
        {
            get => _applicationDbContext.shoppingCartItems
                                        .Include(D => D.Dish)
                                        .Where(D => D.ShoppingCartId == CartId)
                                        .ToList();
        }
        public string CartId { get; set; }
        private ShoppingCart(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }

        public static ShoppingCart GetCart(ApplicationDbContext applicationDbContext,IHttpContextAccessor httpContextAccessor)
        {
            ISession session = httpContextAccessor.HttpContext.Session;
            var context = applicationDbContext;
            string cartId = session.GetString("CartId");
            if (cartId is null)
            {
                Guid.NewGuid().ToString();
                session.SetString("CartId", cartId);
            }
            return new ShoppingCart(context) { CartId = cartId };

        }

        public void AddDish(Dish dish, int amount)
        {
            var shoppingCartItem = _applicationDbContext.shoppingCartItems
                                                        .SingleOrDefault(D => D.Dish.Id == dish.Id && D.ShoppingCartId == CartId);
            if (shoppingCartItem != null)
            {
                shoppingCartItem.Amount += amount;
            }
            else
            {
                _applicationDbContext.shoppingCartItems.Add(
                    new ShoppingCartItem
                    {
                        Dish = dish,
                        Amount = amount,
                        ShoppingCartId = CartId
                    });
            }

        }

        public void RemoveDish(Dish dish)
        {
            var shoppingCartItem = _applicationDbContext.shoppingCartItems
                                                        .SingleOrDefault(D => D.Id == dish.Id && D.ShoppingCartId == CartId);
            if (shoppingCartItem is not null)
            {
                if (shoppingCartItem.Amount > 1)
                    shoppingCartItem.Amount--;
                else
                    _applicationDbContext.shoppingCartItems.Remove(shoppingCartItem);
            }

        }

        public decimal TotalCost()
        {
            //First Trial
            //decimal TotalCoast= ShoppingCartItems.Select(D => D.Dish.Price * D.Amount)
            //                                     .Sum();
            ///Second Trial (The Worst One By The Way)
            //foreach (var item in ShoppingCartItems)
            //{
            //    TotalCoast += item.Dish.Price*item.Amount;
            //}

            return ShoppingCartItems.Select(D => D.Dish.Price * D.Amount)
                                                 .Sum();
        }

        public void SaveChanges() => _applicationDbContext.SaveChanges();

    }
}
