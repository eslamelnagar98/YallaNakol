using System;
using System.Collections.Generic;
using System.Text;
using YallaNakol.Data.Models;

namespace YallaNakol.Data.Services
{
    public interface IRestaurant
    {
        IEnumerable<Restaurant> AllRestaurant { get; }
        Restaurant GetRestaurantById(int restaurantId);
    }
}
