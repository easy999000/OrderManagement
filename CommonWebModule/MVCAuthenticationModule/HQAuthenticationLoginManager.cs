using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CommonWebModule.MVCAuthenticationModule
{
    public class HQAuthenticationLoginManager
    {
        public void Login(HttpContext Context, HQAuthenticationLoginUser User)
        {
            List<Claim> claims = new List<Claim>();

            //属性
            claims.AddRange(new List<Claim>()
                {
                    new Claim("Account", User.Account)
                });

            //证件
            var identity = new ClaimsIdentity(claims);

            //证件夹
            var identityPrincipal = new ClaimsPrincipal(identity);

            //登入
            Context.SignInAsync(identityPrincipal);
        }

        public void LogOut(HttpContext Context)
        {
            Context.SignOutAsync();
        }


    }

    /// <summary>
    /// 
    /// </summary>
    public class HQAuthenticationLoginUser
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

    }
}
