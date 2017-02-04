using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NeoMode.Service.Services;
using Microsoft.AspNetCore.Authorization;

namespace NeoMode.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ICityService _cityService;
        public HomeController(ICityService cityService)
        {
            this._cityService = cityService;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {

            var result = _cityService.GetById(1);

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        [Authorize]
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
