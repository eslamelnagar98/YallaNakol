using System;
using System.Collections.Generic;
using System.Text;
using YallaNakol.Data.Models;
using YallaNakol.Data.Services;
using System.Linq;

namespace YallaNakol.Data.Repository
{
    public class ResturantRepo : IRestaurant
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ResturantRepo(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
        public IEnumerable<Restaurant> AllRestaurants => _applicationDbContext.Restaurants.ToList();
        
        public Restaurant GetRestaurantById(int restaurantId)=>
            _applicationDbContext.Restaurants.FirstOrDefault(I => I.Id == restaurantId);
    }
}
