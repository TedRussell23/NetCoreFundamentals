using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learningNetCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace learningNetCore.Pages
{
    public class GreetingModel : PageModel
    {
        //When this Razor page recieves an HTTP Get Request the MVC Framework will instansiate this model, inject servces and then invoke OnGet()
        private IGreeter _greeter;

        public string CurrentGreeting { get; set; }

        public GreetingModel(IGreeter greeter)
        {
            _greeter = greeter;
        }

        //Params in the Get Request of the URL i.e ~Home/Greeting/{name}
        public void OnGet(string name)
        {
            CurrentGreeting = $"{name}: {_greeter.GetMessageOfTheDay()}";
        }
    }
}