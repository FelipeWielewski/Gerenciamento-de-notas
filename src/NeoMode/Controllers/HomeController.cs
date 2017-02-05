using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NeoMode.Service.Services;
using Microsoft.AspNetCore.Authorization;
using NeoMode.Services;
using NeoMode.Model;
using NeoMode.Services.Services;

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
        
    }
}
