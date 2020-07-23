using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonWebModule.MVCAuthenticationModule
{
    public class HQAuthenticationHandlerOptions: CookieAuthenticationOptions
    {
        public HQAuthenticationHandlerOptions()
        {
            this.Cookie.Name = "HQAuthentication";
        }

        public Action<string> Login { get; set; }

        public Action<string> LogOut { get; set; }






    }
}
