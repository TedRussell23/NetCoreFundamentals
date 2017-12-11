using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learningNetCore.Models;

namespace learningNetCore.Services
{
    public class InMemoryRestaurantData : IRestuarantData
    {
        public InMemoryRestaurantData()
        {
            _restaurants = new List<Restaurant>
            {
                new Restaurant{Id = 1, Name = "Ted's Pizzeria"},
                new Restaurant{Id = 2, Name = "Lara's Pizzeria"},
                new Restaurant{Id = 3, Name = "Jeremey's Pizzeria"}
            };
        }

        //Signle instance with multiple requests could lead to threading issues...
        private List<Restaurant> _restaurants;

        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            return _restaurants.OrderBy(r => r.Name);
        }

        public Restaurant Get(int id)
        {
            return _restaurants.FirstOrDefault(r => r.Id == id);
        }

        public Restaurant Add(Restaurant restaurant)
        {
            restaurant.Id = _restaurants.Max(r => r.Id) + 1;
            _restaurants.Add(restaurant);
            return restaurant;
        }
    }
}
