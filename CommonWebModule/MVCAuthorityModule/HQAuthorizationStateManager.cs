using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using CommonWebModule;

namespace CommonWebModule.MVCAuthorityModule
{
    /// <summary>
    /// 登入管理
    /// </summary>
    public class HQAuthorizationStateManager
    {
        IHttpContextAccessor _Context;

        public const string UserSessionName = "HQAuthorizationUserSessionName";

        public HQAuthorizationStateManager(IHttpContextAccessor Context)
        {
            _Context = Context;
        }

        public void Sigin(HQAuthorizationUser HQUser)
        {
            List<Claim> claims = new List<Claim>();

            //证件单元
            claims.AddRange(new List<Claim>()
                {
                    new Claim("UserID",HQUser.UserID.ToString()),
                    new Claim(ClaimTypes.Name,HQUser.UserName),
                    new Claim("Account", HQUser.Account),
                });

            //使用证件单元创建一张身份证
            var identity = new ClaimsIdentity(claims, "HQAuthorizationIdentityCookie");

            //使用身份证创建一个证件当事人，
            var identityPrincipal = new ClaimsPrincipal(identity);

            ////过安检
            //_Context.HttpContext.SignInAsync("Cookies", identityPrincipal);
            //过安检
            _Context.HttpContext.SignInAsync( identityPrincipal);

            //_Context.HttpContext.RequestServices.GetService
            SessionLogin(_Context.HttpContext, HQUser);
           

        }
        public static void SessionLogin(HttpContext Context, HQAuthorizationUser HQUser)
        {
            Context.Session.Set<HQAuthorizationUser>(UserSessionName, HQUser);
        }

        /// <summary>
        /// 获取登入用户
        /// </summary>
        /// <returns></returns>
        public HQAuthorizationUser GetLoginUser()
        {
            HQAuthorizationUser user= _Context.HttpContext.Session.Get<HQAuthorizationUser>(UserSessionName);
            return user;
        }

        public void Signout()
        {
            _Context.HttpContext.SignOutAsync();

            _Context.HttpContext.Session.Set<HQAuthorizationUser>(UserSessionName, null);
        }
    }
}
