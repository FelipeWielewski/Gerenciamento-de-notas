using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NeoMode.Core.Domain;
using NeoMode.Services;
using NeoMode.Services.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NeoMode.Model
{
    public class UserProviderMiddleware
    {
        private readonly RequestDelegate _next;

        private IUserService _userService { get; set; }
        private IEncryptionService encryptionService { get; set; }
        public UserProviderMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public Task Invoke(HttpContext context)
        {
            #region instanceServices
            var service = context.RequestServices.GetService(typeof(IEncryptionService));
            encryptionService = (IEncryptionService)service;

            var userService = context.RequestServices.GetService(typeof(IUserService));
            _userService = (IUserService)userService;

            var accessor = context.RequestServices.GetService(typeof(IHttpContextAccessor));
            AuthenticationHelp.SetHttpContextAccessor((IHttpContextAccessor)accessor);
            #endregion

            if (context.Request.Cookies[AuthenticationHelp.CookieUserName] != null && AuthenticationHelp.isLogged() == false)
            {
                var userCrypt = context.Request.Cookies[AuthenticationHelp.CookieUserName];
                var userString = encryptionService.DecryptText(userCrypt);
                var userModel = JsonConvert.DeserializeObject<User>(userString);

                var userDB = _userService.GetByUsername(userModel.Username);
                if (userDB != null)
                {
                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.SerialNumber, userDB.Username, ClaimValueTypes.String),
                        new Claim(ClaimTypes.Name, userDB.FullName, ClaimValueTypes.String),
                        new Claim(ClaimTypes.NameIdentifier, userDB.Id.ToString(), ClaimValueTypes.Integer32)
                    };

                    var userIdentity = new ClaimsIdentity(claims, "UserSystem");

                    var userPrincipal = new ClaimsPrincipal(userIdentity);
                    context.User = userPrincipal;
                }
            }

            return _next.Invoke(context);
            //// If the request path doesn't match, skip
            //if (!context.Request.Path.Equals(_options.Path, StringComparison.Ordinal))
            //{
            //    return _next(context);
            //}

            //// Request must be POST with Content-Type: application/x-www-form-urlencoded
            //if (!context.Request.Method.Equals("POST")
            //   || !context.Request.HasFormContentType)
            //{
            //    context.Response.StatusCode = 400;
            //    return context.Response.WriteAsync("Bad request.");
            //}

            //return GenerateToken(context);
        }

    }
}
