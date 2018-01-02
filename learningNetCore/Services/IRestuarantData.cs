using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learningNetCore.Models;

namespace learningNetCore.Services
{
    public interface IRestuarantData
    {
        IEnumerable<Restaurant> GetAllRestaurants();
        Restaurant Get(int id);
        Restaurant Add(Restaurant restaurant);
        Restaurant Update(Restaurant restaurant);
    }
}
