using System.Collections.Generic;
using System.Linq;
using learningNetCore.Data;
using learningNetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace learningNetCore.Services
{
    public class SqlRestaurantData : IRestuarantData
    {
        private readonly learningNetCoreDbContext _context;

        public SqlRestaurantData(learningNetCoreDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            //If Restaurants was a massive table could cause performance issues through IEmmurable, better of with IQuearyable
            return _context.Restaurants.OrderBy(r => r.Name);
        }

        public Restaurant Get(int id)
        {
            return _context.Restaurants.FirstOrDefault(r => r.Id == id);
        }

        public Restaurant Add(Restaurant restaurant)
        {
            //Auto-Generated Entity Framework ID
            _context.Restaurants.Add(restaurant);
            //Issues SQL commands like the one above to the database? 
            _context.SaveChanges();
            return restaurant;
        }

        public Restaurant Update(Restaurant restaurant)
        {
            _context.Attach(restaurant).State = EntityState.Modified;
            _context.SaveChanges();
            return restaurant;
        }
    }
}
