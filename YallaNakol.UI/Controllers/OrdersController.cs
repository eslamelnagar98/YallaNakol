using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using YallaNakol.Data.Models;
using YallaNakol.Data.Services;

namespace YallaNakol.UI.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrder orderRepo;
        private readonly IShoppingCart shoppingCart;
        private readonly UserManager<ApplicationUser> userManager;

        public OrdersController(IOrder orderRepo, IShoppingCart shoppingCart, UserManager<ApplicationUser> userManager)
        {
            this.orderRepo = orderRepo;
            this.shoppingCart = shoppingCart;
            this.userManager = userManager;
        }


        public async Task<IActionResult> Checkout()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;

            var user = await userManager.GetUserAsync(User);

            var order = new Order()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            };

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(Order order)
        {
            if( shoppingCart.IsEmpty)
            {
                ModelState.AddModelError("CartItems","Shopping Cart Empty..");
            }

            if(ModelState.IsValid)
            {
                // save for upcoming requests
                TempData["Order"] = JsonSerializer.Serialize(order); //order;
                TempData["LocalRedirect"] = true;
                return RedirectToAction("Pay");
            }

            return View();
        }
    
            
        //TODO: Stop user from directly accessing this page.
        public IActionResult Pay()
        {
            //TODO: show payment form here
            bool isLocalRedirect = TempData.ContainsKey("LocalRedirect");


            if (!isLocalRedirect)
                return LocalRedirect("~/Home");

            return View();
        }

        [HttpPost]
        [Route("Orders/Pay")]
        [ValidateAntiForgeryToken]
        public IActionResult PaymentComplete(int placeHolder)
        {
            var orderToPlace = JsonSerializer.Deserialize<Order>((string)TempData["Order"]);

            // if Payment succesfull
            orderRepo.CreateOrder(orderToPlace); // place order
            shoppingCart.ClearItems(); // clear cart items
            orderRepo.SaveChanges(); // save order changes
            shoppingCart.SaveChanges(); // save cart changes
            return View();
        }

    }
}
