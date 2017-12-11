using learningNetCore.Models;
using learningNetCore.Services;
using learningNetCore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace learningNetCore.Controllers
{
    public class HomeController : Controller
    {
        private IRestuarantData _resturantData;
        private IGreeter _greeter;

        public HomeController(IRestuarantData restuarantData, IGreeter greeter)
        {
            _resturantData = restuarantData;
            _greeter = greeter;
        }
        //Action Result : Doesn't retrn to client straight away
        public IActionResult Index()
        {
            //var model = _resturantData.GetAllRestaurants();
            var model = new HomeIndexViewModel
            {
                Restaurants = _resturantData.GetAllRestaurants(),
                CurrentMessage = _greeter.GetMessageOfTheDay()
            };

            return View(model);
            //return new ObjectResult(model);
        }

        //Having a param on ActionMethod (IActionResult) it'll look for it through the http request, routing and other sources
        public IActionResult Details(int id)
        {
            //Matched in the routing config in startup.cs
            var model = _resturantData.Get(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RestaurantEditModel model)
        {
            if (ModelState.IsValid)
            {
                var newRestaurant = new Restaurant
                {
                    Name = model.Name,
                    Cuisine = model.Cuisine

                };
                newRestaurant = _resturantData.Add(newRestaurant);
                return RedirectToAction(nameof(Details), new {id = newRestaurant.Id});
            }
            return View();
        }
    }
}
