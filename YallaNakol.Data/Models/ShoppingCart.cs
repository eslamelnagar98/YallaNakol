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
            get => _applicationDbContext.ShoppingCartItems
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
            var context = applicationDbContext;
            ISession session = httpContextAccessor.HttpContext.Session;

            // retrive CartID from Session
            string cartId = session.GetString("CartId");

            // if no cart id  is assigned to session
            if (cartId is null)
            {
                bool clientHasCookie = httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("CartId");

                if (clientHasCookie)
                {
                    // 1: read from cookie 
                    // 2: save into session
                    cartId = httpContextAccessor.HttpContext.Request.Cookies["CartId"];
                    session.SetString("CartId", cartId);
                }

                else
                {
                    cartId = Guid.NewGuid().ToString();
                    CookieOptions cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.Now.AddMinutes(30),
                        Secure = true,
                        HttpOnly = true
                    };

                    httpContextAccessor.HttpContext.Response.Cookies.Append("cartId", cartId, cookieOptions);
                    session.SetString("CartId", cartId);
                }
            }
            return new ShoppingCart(context) { CartId = cartId };

        }

        public void AddDish(Dish dish, int amount)
        {
            var shoppingCartItem = _applicationDbContext.ShoppingCartItems
                                                        .SingleOrDefault(D => D.Dish.Id == dish.Id && D.ShoppingCartId == CartId);
            if (shoppingCartItem != null)
            {
                shoppingCartItem.Amount += amount;
            }
            else
            {
                _applicationDbContext.ShoppingCartItems.Add(
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
            var shoppingCartItem = _applicationDbContext.ShoppingCartItems
                                                        .SingleOrDefault(D => D.Id == dish.Id && D.ShoppingCartId == CartId);
            if (shoppingCartItem is not null)
            {
                if (shoppingCartItem.Amount > 1)
                    shoppingCartItem.Amount--;
                else
                    _applicationDbContext.ShoppingCartItems.Remove(shoppingCartItem);
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
