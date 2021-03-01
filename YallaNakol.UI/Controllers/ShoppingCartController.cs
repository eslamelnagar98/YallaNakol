using Microsoft.AspNetCore.Authorization;
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
            // TEST DATA ONLY 
            // START OF TEST DATA
            var addedDish1 = _dish.GetDishById(1);
           // var addedDish2 = _dish.GetDishById(2);
           // var addedDish3 = _dish.GetDishById(3);
           //
            _shoppingCart.AddDish(addedDish1, 1);
           // _shoppingCart.AddDish(addedDish2, 3);
           // _shoppingCart.AddDish(addedDish3, 2);
            _shoppingCart.SaveChanges();
            // END OF TEST DATA


            // Redirect To Home If Empty Cart
            if (_shoppingCart.IsEmpty)
                return LocalRedirect("/Home");


            return View(_shoppingCart);
        }

        [Route("ShoppingCart/AddToCart/dishId")]
        public ActionResult AddToCart(int dishId, int amount = 1)
        {
            var addedDish = _dish.GetDishById(dishId);
            if (addedDish != null)
            {

                _shoppingCart.AddDish(addedDish, amount);
                _shoppingCart.SaveChanges();

            }
            return RedirectToAction("Index");
        }

        [Route("ShoppingCart/RemoveFromCart/dishId")]
        public ActionResult RemoveFromCart(int dishId)
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
