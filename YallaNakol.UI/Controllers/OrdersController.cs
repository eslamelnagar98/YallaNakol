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
    public class OrdersController : Controller
    {
        private readonly IOrder orderRepo;
        private readonly IShoppingCart shoppingCart;

        public OrdersController(IOrder orderRepo, IShoppingCart shoppingCart)
        {
            this.orderRepo = orderRepo;
            this.shoppingCart = shoppingCart;
        }



        public IActionResult Checkout()
        {
            return View();
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
            // Payment succesfull

            orderRepo.CreateOrder(orderToPlace);

            return View();

        }

    }
}
