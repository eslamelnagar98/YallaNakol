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
using YallaNakol.UI.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace YallaNakol.UI.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrder orderRepo;
        private readonly IShoppingCart shoppingCart;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRestaurant restaurant;

        public OrdersController(IOrder orderRepo, IShoppingCart shoppingCart, UserManager<ApplicationUser> userManager, IRestaurant restaurant)
        {
            this.orderRepo = orderRepo;
            this.shoppingCart = shoppingCart;
            this.userManager = userManager;
            this.restaurant = restaurant;
        }
        public async Task<IActionResult> Checkout()
        {
            //if (TempData.ContainsKey("ViewData")) ViewData = JsonSerializer.Deserialize<ViewDataDictionary>(TempData["ViewData"].ToString());
            var user = await userManager.Users.Include(U => U.Addresses).SingleOrDefaultAsync(U => U.UserName == User.Identity.Name);
            var orderToPlace = new YallaNakol.Data.Models.Order()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
            var checkoutVM = new CheckoutViewModel()
            {
                Order = orderToPlace,
                Addresses = user.Addresses,
                Lat = 30,
                Lng = 31,
                showMap = 0
            };
            return View(checkoutVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckoutAsync(YallaNakol.Data.Models.Order order, double Lat, double Lng)
        {
            
            if (shoppingCart.IsEmpty)
            {
                ModelState.AddModelError("CartItems", "Shopping Cart Empty..");
            }
            var deliveryAreas = restaurant.GetDeliveryAreasByResturantId(shoppingCart.ResturantId);
            var list = new List<string>();
            foreach (var area in Enum.GetValues<DeliveryAreas>())
            {
                if (deliveryAreas.HasFlag(area))
                {
                    list.Add(area.ToString());
                }
            }
            order.Address.Area = Regex.Replace(order.Address.Area, @"\s+", "");
            if (!list.Contains(order.Address.Area))
            {
                ModelState.AddModelError("Area", "Area out of coverage");
            }
            //Check if will address or not
            //order.Address.ID
            var user = await userManager.Users.Include(U => U.Addresses).SingleOrDefaultAsync(U => U.UserName == User.Identity.Name);
            
            if (ModelState.IsValid)
            {
                if (order.Address.ID == 0) //Save new address
                {
                    user.Addresses.Add(order.Address);
                    orderRepo.SaveChanges();
                }
                // save for upcoming requests
                TempData["Order"] = JsonSerializer.Serialize(order); //order;
                TempData["LocalRedirect"] = true;
                return RedirectToAction("Pay");
            }
            /*TempData["ViewData"] = JsonSerializer.Serialize(ViewData); //to keep invalid model state
            TempData["Lat"] = JsonSerializer.Serialize(Lat);
            TempData["Lng"] = JsonSerializer.Serialize(Lng);
            TempData["showMap"] = 1;*/

            var orderToPlace = new YallaNakol.Data.Models.Order()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
            var checkoutVM = new CheckoutViewModel()
            {
                Order = orderToPlace,
                Addresses = user.Addresses,
                Lat = Lat, //(TempData.ContainsKey("Lat")) ? JsonSerializer.Deserialize<double>((string)TempData["Lat"]) : 30.0,
                Lng = Lng, //(TempData.ContainsKey("Lng")) ? JsonSerializer.Deserialize<double>((string)TempData["Lng"]) : 31.0,
                showMap = 1 //(TempData.ContainsKey("showMap")) ? (int)TempData["showMap"] : 0
            };
            //return RedirectToAction("Checkout");
            return View(checkoutVM);
        }


        //TODO: Stop user from directly accessing this page.
        public async Task<IActionResult> Pay()
        {
            var orderToPlace = JsonSerializer.Deserialize<YallaNakol.Data.Models.Order>((string)TempData["Order"]);
            orderToPlace.PaymentType = PaymentType.Visa;
            TempData["Order"] = JsonSerializer.Serialize(orderToPlace); //order;

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
            int centToDollar = 100;
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
                Amount = (long)(shoppingCart.TotalCost() * centToDollar),
                Currency = "EGP",
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
                    return RedirectToAction("PaymentComplete");
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

            orderRepo.CreateOrder(orderToPlace); // place order
            shoppingCart.ClearItems(); // clear cart items
            orderRepo.SaveChanges(); // save order changes
            shoppingCart.SaveChanges(); // save cart changes

            var paymentCompleteVM = new PaymentCompleteViewModel()
            {
                TotalOrderCost = orderToPlace.OrderTotal,
                TrackingId = orderToPlace.TrackingID,
                PaymentType = orderToPlace.PaymentType
            };


            return View(paymentCompleteVM);
        }

    }
}
