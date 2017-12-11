using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace learningNetCore.Controllers
{
    //About
    [Route("company/[controller]/[action]")]
    public class AboutController
    {
        public string Phone()
        {
            return "+44 7927498136";
        }

        public string Address()
        {
            return "United Kingdom (UK)";
        }
    }
}
