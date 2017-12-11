using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace learningNetCore.Services
{
    public interface IGreeter
    {
        string GetMessageOfTheDay();
    }

    public class Greeter : IGreeter
    {

        private readonly IConfiguration _configuration;
        public Greeter(IConfiguration configuration)
        {
            //Constructs a Greeter with antoher service using the ASP.NET Container (The depenecy injection sysyem) and figure all this out.
            //Already registered by our default web host builder

            _configuration = configuration;
        }

        public string GetMessageOfTheDay()
        {
            return _configuration["Greeting"];
        }
    }
}
