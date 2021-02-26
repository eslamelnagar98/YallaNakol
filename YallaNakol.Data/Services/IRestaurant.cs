using System;
using System.Collections.Generic;
using System.Text;
using YallaNakol.Data.Models;

namespace YallaNakol.Data.Services
{
    public interface IRestaurant
    {
        IEnumerable<Restaurant> AllRestaurants { get; }
        Restaurant GetRestaurantById(int? restaurantId);
        void AddRestaurant(Restaurant restaurant);
        void UpdateRestaurant(Restaurant restaurant);
        void DeleteRestaurant(Restaurant restaurant);
        bool RestaurantExists(int id);

        public DeliveryAreas GetDeliveryAreasByResturantId(int resturantId);
        int SaveChanges();
    }
}
