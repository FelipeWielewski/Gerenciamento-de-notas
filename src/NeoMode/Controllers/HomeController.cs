using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NeoMode.Service.Services;

namespace NeoMode.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICityService _cityService;
        public HomeController(ICityService cityService)
        {
            this._cityService = cityService;
        }
        public IActionResult Index()
        {


            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
