using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonWebModule.MVCAuthenticationModule
{
    public static class HQAuthenticationDefaults
    {

        /// <summary>
        /// The default value used for CookieAuthenticationOptions.AuthenticationScheme
        /// </summary>
        public const string AuthenticationScheme = "HQAuthenticationCookies";

        /// <summary>
        /// The prefix used to provide a default CookieAuthenticationOptions.CookieName
        /// </summary>
        public static readonly string CookiePrefix = ".AspNetCore.";

        /// <summary>
        /// The default value used by CookieAuthenticationMiddleware for the
        /// CookieAuthenticationOptions.LoginPath
        /// </summary>
        public static readonly PathString LoginPath = new PathString("/Account/Login");

        /// <summary>
        /// The default value used by CookieAuthenticationMiddleware for the
        /// CookieAuthenticationOptions.LogoutPath
        /// </summary>
        public static readonly PathString LogoutPath = new PathString("/Account/Logout");

        /// <summary>
        /// The default value used by CookieAuthenticationMiddleware for the
        /// CookieAuthenticationOptions.AccessDeniedPath
        /// </summary>
        public static readonly PathString AccessDeniedPath = new PathString("/Account/AccessDenied");

        /// <summary>
        /// The default value of the CookieAuthenticationOptions.ReturnUrlParameter
        /// </summary>
        public static readonly string ReturnUrlParameter = "ReturnUrl";
    }
}
