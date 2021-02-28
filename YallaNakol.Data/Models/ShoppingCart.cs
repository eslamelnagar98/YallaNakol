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
    public class ShoppingCart : IShoppingCart
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public string CartId { get; private set; }
        public int ResturantId { get; set; }
        public IEnumerable<ShoppingCartItem> ShoppingCartItems
        {
            get => _applicationDbContext.ShoppingCartItems
                                        .Include(D => D.Dish)
                                        .Where(D => D.ShoppingCartId == CartId)
                                        .ToList();
        }

        public bool IsEmpty => ShoppingCartItems.Count() == 0;

        private ShoppingCart(ApplicationDbContext applicationDbContext, string cartId)
        {
            this.CartId = cartId;
            this._applicationDbContext = applicationDbContext;

            if(applicationDbContext.ShoppingCartItems.Any())
                SetResturantId(_applicationDbContext.ShoppingCartItems
                                                    .Include( sh => sh.Dish )
                                                    .FirstOrDefault(sh => sh.ShoppingCartId == this.CartId)
                                                   ?.Dish);
        }

        public static ShoppingCart GetCart(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor)
        {
            var context = applicationDbContext;
            ISession session = httpContextAccessor.HttpContext.Session;



            string cartId = session.GetString("CartId");

            if (cartId == null)
            {
                cartId = Guid.NewGuid().ToString();
            }
            session.SetString("CartId", cartId);

            #region OldWay,usingNewCookie

            //// retrive CartID from Session
            //string cartId = session.GetString("CartId");

            //// if no cart id  is assigned to session
            //if (cartId is null)
            //{
            //    bool clientHasCookie = httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("CartId");

            //    if (clientHasCookie)
            //    {
            //        // 1: read from cookie 
            //        // 2: save into session
            //        cartId = httpContextAccessor.HttpContext.Request.Cookies["CartId"];
            //        session.SetString("CartId", cartId);
            //    }

            //    else
            //    {
            //        cartId = Guid.NewGuid().ToString();
            //        CookieOptions cookieOptions = new CookieOptions
            //        {
            //            Expires = DateTime.Now.AddMinutes(30),
            //            Secure = true,
            //            HttpOnly = true
            //        };

            //        httpContextAccessor.HttpContext.Response.Cookies.Append("cartId", cartId, cookieOptions);
            //        session.SetString("CartId", cartId);
            //    }
            //}

            #endregion


            return new ShoppingCart(context, cartId);
           
        }

        /// <summary>
        /// Only Sets the ID if the cart is empty ,otherwise does nothing
        /// </summary>
        private void SetResturantId(Dish dish)
        {
            if(dish is not null)
                ResturantId = _applicationDbContext.Restaurants
                                                   .Select(r => r.Id)
                                                   .SingleOrDefault(r => r == dish.MenuId);
        }

        public void AddDish(Dish dish, int amount)
        {
            if( this.IsEmpty )
                SetResturantId(dish);


            var resturantMenuId = _applicationDbContext.Restaurants.Find(ResturantId).MenuId;
            
            // check if dish belongs to current menu, other wise return
            if(resturantMenuId != dish.MenuId)
                return;

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
                                                        .SingleOrDefault(D => D.Dish.Id == dish.Id && D.ShoppingCartId == CartId);
            if (shoppingCartItem is not null)
            {
                if (shoppingCartItem.Amount > 1)
                    shoppingCartItem.Amount--;
                else
                    _applicationDbContext.ShoppingCartItems.Remove(shoppingCartItem);
            }

            if (this.IsEmpty)
                ClearItems();

        }

        public decimal TotalCost() => ShoppingCartItems.Select(D => D.Dish.Price * D.Amount)
                                                       .Sum();
        //    {
        //        First Trial
        //        decimal TotalCoast = ShoppingCartItems.Select(D => D.Dish.Price * D.Amount)
        //                                             .Sum();
        //        /Second Trial(The Worst One By The Way)
        //        foreach (var item in ShoppingCartItems)
        //        {
        //            TotalCoast += item.Dish.Price* item.Amount;
        //        }
        //         second  
        //        return ShoppingCartItems.Select(D => D.Dish.Price* D.Amount)
        //                                             .Sum();
        //  }

        public void SaveChanges() => _applicationDbContext.SaveChanges();

        public void ClearItems()
        {
            var cartItemsToRemove = _applicationDbContext.ShoppingCartItems.Where(sh => sh.ShoppingCartId == this.CartId);
            ResturantId = 0;
            _applicationDbContext.ShoppingCartItems.RemoveRange(cartItemsToRemove);
        }
    }
}
