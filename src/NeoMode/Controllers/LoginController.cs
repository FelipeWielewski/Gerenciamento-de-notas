using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NeoMode.Model;
using NeoMode.Services.Services;
using NeoMode.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NeoMode.Core.Domain;
using System.Security.Principal;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NeoMode.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEncryptionService _encryptionService;
        public LoginController(IUserService userService, IEncryptionService encryptionService)
        {
            this._userService = userService;
            this._encryptionService = encryptionService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.isIndividualPage = true;
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            Response.Cookies.Delete(AuthenticationHelp.CookieUserName);
            AuthenticationHelp.ResetUser();

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Index(LoginModel model)
        {
            if (!VerifyModel(model))
                return View();

            var user = _userService.GetByUsername(model.username);
            if (user != null)
            {
                var passCrypt = _encryptionService.EncryptText(model.password);
                if (user.Password == passCrypt)
                {
                    //Usuario Autenticado
                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddDays(1);

                    Response.Cookies.Append(AuthenticationHelp.CookieUserName, EncryptUserInfo(user), options);
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["Message"] = "Login ou senha incorreta";
            return RedirectToAction("Index");
        }
        private string EncryptUserInfo(User model)
        {
            string userData = JsonConvert.SerializeObject(new User()
            {
                Username = model.Username,
                Id = model.Id
            });

            return _encryptionService.EncryptText(userData);
        }
        private bool VerifyModel(LoginModel model)
        {
            if (string.IsNullOrEmpty(model.password) || string.IsNullOrEmpty(model.username))
            {
                ViewBag.isIndividualPage = true;
                TempData["Message"] = "Por favor, preencha todos os campos";
                return false;
            }
            return true;
        }
    }
}
