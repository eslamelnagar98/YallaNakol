using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YallaNakol.Data.Models;
using YallaNakol.Data.Services;
using static YallaNakol.Data.Models.Order;

namespace YallaNakol.Data.Repository
{
    public class OrderRepo : IOrder
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IShoppingCart shoppingCart;

        public OrderRepo(ApplicationDbContext dbContext, IShoppingCart shoppingCart)
        {
            this.dbContext = dbContext;
            this.shoppingCart = shoppingCart;
        }


        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            order.OrderTotal = shoppingCart.TotalCost();
            order.TrackingID = $"{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Millisecond}";
            dbContext.Orders.Add(order);
            order.OrderDetails = new List<OrderDetail>();


            //Add All dishes in the shopping cart to the order
            foreach (var shoppingCartItem in shoppingCart.ShoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    DishId = shoppingCartItem.DishId,
                    Amount = shoppingCartItem.Amount,
                    Price = shoppingCartItem.Dish.Price,
                };

                order.OrderDetails.Add(orderDetail);
            }

        }

        public void SaveChanges() => dbContext.SaveChanges();

    }
}
