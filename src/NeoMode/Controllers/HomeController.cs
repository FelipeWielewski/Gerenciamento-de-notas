using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NeoMode.Service.Services;
using Microsoft.AspNetCore.Authorization;
using NeoMode.Services;

namespace NeoMode.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }
        public IActionResult Index()
        {
            if (!AuthenticationHelp.isLogged())
                return RedirectToAction("Index", "Login");


            return View();
        }

        public IActionResult About()
        {
            if (!AuthenticationHelp.isLogged())
                return RedirectToAction("Index", "Login");

            ViewData["Message"] = "Your application description page.";

            return View();
        }
        public IActionResult Contact()
        {
            if (!AuthenticationHelp.isLogged())
                return RedirectToAction("Index", "Login");


            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            if (!AuthenticationHelp.isLogged())
                return RedirectToAction("Index", "Login");

            return View();
        }
    }
}
