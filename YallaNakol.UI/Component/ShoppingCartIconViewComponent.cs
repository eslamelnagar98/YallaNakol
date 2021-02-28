using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNakol.Data.Models;
using YallaNakol.Data.Services;

namespace YallaNakol.UI.Component
{
    public class ShoppingCartIconViewComponent:ViewComponent
    {
        private readonly IShoppingCart _shoppingCart;

        public ShoppingCartIconViewComponent(IShoppingCart shoppingCart)
        {
            this._shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            return View(_shoppingCart.ShoppingCartItems.Count());
        }
    }
}
