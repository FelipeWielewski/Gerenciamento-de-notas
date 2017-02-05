using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NeoMode.Services
{
    public class AuthenticationHelp
    {
        private static IHttpContextAccessor httpContextAccessor;
        public static void SetHttpContextAccessor(IHttpContextAccessor accessor)
        {
            httpContextAccessor = accessor;
        }
        public static void ResetUser()
        {
            if (httpContextAccessor.HttpContext != null)
                httpContextAccessor.HttpContext.User = null;
        }

        public static string CookieUserName = "NeoModeUser";
        public static bool isLogged()
        {
            try
            {
                return httpContextAccessor.HttpContext.User != null && httpContextAccessor.HttpContext.User.Claims.FirstOrDefault().Type == ClaimTypes.SerialNumber;
            }
            catch
            {
                return false;
            }
        }
        public static string getFullName()
        {
            try
            {
                return httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().Value as string;
            }
            catch
            {
                return "";
            }
        }
    }
}
