using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YallaNakol.Data.Services;

namespace YallaNakol.UI.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCart _shoppingCart;
        private readonly IDish _dish;

        public ShoppingCartController(IShoppingCart shoppingCart,
                                      IDish dish)
        {
            this._shoppingCart = shoppingCart;
            _dish = dish;
        }
        // GET: ShoppingCartController
        public ActionResult Index()
        {
            
            return View(_shoppingCart);
        }

        [Route("ShoppingCart/AddToCart/dishId")]
        public ActionResult AddToCart(int dishId)
        {

            var addedDish = _dish.GetDishById(dishId);
            if (addedDish != null)
            {

                _shoppingCart.AddDish(addedDish, 1);
                _shoppingCart.SaveChanges();

            }
            return RedirectToAction("Index");
        }

        [Route("ShoppingCart/RemoveFromCart/dishId")]
        public ActionResult RemoveFromCart(int dishId )
        {
            var removedDish = _dish.GetDishById(dishId);
            if (removedDish != null)
            {
                _shoppingCart.RemoveDish(removedDish);
                _shoppingCart.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
