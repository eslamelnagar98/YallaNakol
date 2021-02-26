using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using YallaNakol.Data.Models;
using YallaNakol.Data.Services;
using YallaNakol.Data;


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


        public async Task<IActionResult> Checkout( )
        {
            var user = await userManager.GetUserAsync(User);
            var order = new YallaNakol.Data.Models.Order()
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
        public IActionResult Checkout(YallaNakol.Data.Models.Order order)
        {
            if (shoppingCart.IsEmpty)
            {
                ModelState.AddModelError("CartItems", "Shopping Cart Empty..");
            }

            if (ModelState.IsValid)
            {
                // save for upcoming requests
                TempData["Order"] = JsonSerializer.Serialize(order); //order;
                TempData["LocalRedirect"] = true;
                return RedirectToAction("Pay");
            }

            return View();
        }


        //TODO: Stop user from directly accessing this page.
        public async Task<IActionResult> Pay()
        {
             int centToDollar = 100;
            //TODO: show payment form here
            bool isLocalRedirect = TempData.ContainsKey("LocalRedirect");

            TempData.Keep("Order");
            if (!isLocalRedirect)
                return LocalRedirect("~/Home");

            var user = await userManager.GetUserAsync(User);
            ViewBag.PaymentAmount = shoppingCart.TotalCost() * centToDollar;
            ViewBag.UserMail = user.Email;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Processing(string stripeToken, string stripeTokenType, string stripeEmail)
        {
            Dictionary<string, string> Metadata = new Dictionary<string, string>();
            //Metadata.Add("CustomerName", "Ahmed");
            //Metadata.Add("CartId", "10sads20");
            //Metadata.Add("OrderNo", "1000");
            //Metadata.Add("Product", "RubberDuck");
            //Metadata.Add("Quantity", "10000");
            //Metadata.Add("stripeTokenType", stripeTokenType);
            //Metadata.Add("stripeToken", stripeToken);

            var options = new ChargeCreateOptions
            {
                StatementDescriptorSuffix = "YallaNakol Website",
                Amount = (long)(shoppingCart.TotalCost()),
                Currency = "US",
                Description = "YallaNakol Checkout Cart",
                Source = "tok_mastercard",
                ReceiptEmail = stripeEmail,
                Metadata = Metadata
            };

            var service = new ChargeService();
            Charge charge = service.Create(options);
            #region Logging
            //charge.Created//charge.Currency//charge.Amount//charge.AmountCaptured//charge.AmountRedunded//
            //charge.BalanceTransactionId//charge.Captured//charge.Description//charge.Id//charge.Metadata//charge.PaymentMethod//charge.ReceiptEmail                                  
            //charge.ReceiptUrl//charge.StatementDescriptorSuffix//charge.Status
            #endregion
            switch (charge.Status)
            {
                case "succeeded":
                    //This is an example of what to do after a charge is successful
                    return RedirectToAction("PaymentComplete");
                    //Database.ReduceStock(Product, Quantity);
                    break;
                case "failed":
                    //Code to execute on a failed charge
                    return View();
                    break;
                default:
                    return View();
                    break;
            }
        }
        //4242424242424242
//test card https://stripe.com/docs/testing



        public IActionResult PaymentComplete()
        {
            //Retrieve object from temp data
            var orderToPlace = JsonSerializer.Deserialize<YallaNakol.Data.Models.Order>((string)TempData["Order"]);

            // if Payment Successfull
            orderRepo.CreateOrder(orderToPlace); // place order
            shoppingCart.ClearItems(); // clear cart items
            orderRepo.SaveChanges(); // save order changes
            shoppingCart.SaveChanges(); // save cart changes

            return View();
        }

    }
}
