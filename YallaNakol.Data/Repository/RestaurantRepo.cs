using System;
using System.Collections.Generic;
using System.Text;
using YallaNakol.Data.Models;
using YallaNakol.Data.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace YallaNakol.Data.Repository
{
    public class RestaurantRepo : IRestaurant
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RestaurantRepo(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
        public IEnumerable<Restaurant> AllRestaurants =>
            _applicationDbContext.Restaurants
                                 .Include(r => r.Categories)
                                 .AsNoTracking()
                                 .ToList();

        public Restaurant GetRestaurantById(int? restaurantId) =>
            _applicationDbContext.Restaurants.Include(r=>r.Categories).FirstOrDefault(I => I.Id == restaurantId);
        public void AddRestaurant(Restaurant restaurant) =>
            _applicationDbContext.Restaurants.Add(restaurant);

        public void UpdateRestaurant(Restaurant restaurant) =>
            _applicationDbContext.Update(restaurant);

        public void DeleteRestaurant(Restaurant restaurant) =>
            _applicationDbContext.Remove(restaurant);

        public bool RestaurantExists(int id) =>
             _applicationDbContext.Restaurants.Any(e => e.Id == id);

        public int SaveChanges() =>
            _applicationDbContext.SaveChanges();

        public DeliveryAreas GetDeliveryAreasByResturantId(int resturantId)
        {
           return _applicationDbContext.Restaurants
                                       .Where(r => r.Id == resturantId)
                                       .Select(r => r.DeliveryAreas)
                                       .FirstOrDefault();
        }
    }

}

