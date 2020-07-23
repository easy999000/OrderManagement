using AspWeb.code;
using Fluent.Infrastructure.FluentModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AspWeb.Controllers
{
    public class Account2Controller : Controller
    {
        public Account2Controller()
     : this(new UserManager<UserIdentity>(new UserStore()))
        { }


        public Account2Controller(UserManager<UserIdentity> userManager)
        {
            UserManager = userManager;
        }
        public ActionResult Main()
        {
            return View();
        }


        public UserManager<UserIdentity> UserManager { get; private set; }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Login(UserIdentity model)
        {
            HQAuthenticationManager.HQLogin("admin");

            return Content("admin");
            //var v = zhuche(model).Result;

            // var user = await UserManager.FindAsync(model.UserName, model.Password);
            var user = await UserManager.FindByNameAsync(model.UserName );

            //UserIdentity Uidentity = new UserIdentity();
            //Uidentity.


            if (user != null)
            {
                await SignInAsync(user, model.RememberMe);
              //  return Content("ok");
                return RedirectToAction("Main");
            }
            return Content("");
        }

        public async Task<ActionResult> zhuche(UserIdentity model)
        {
            var user = new UserIdentity() { UserName = model.UserName };
            var result = await UserManager.CreateAsync(user );

            return Content("");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {

                return HttpContext.GetOwinContext().Authentication;

            }
        }


        private async Task SignInAsync(UserIdentity Account, bool RememberMe)
        {
            // 1. 利用ASP.NET Identity获取用户对象
            //var user = await UserManager.FindAsync("UserName", "Password");
            
            var user = await UserManager.FindByNameAsync(Account.UserName);
            // 2. 利用ASP.NET Identity获取identity 对象
            var identity = await UserManager.CreateIdentityAsync(user
                , DefaultAuthenticationTypes.ApplicationCookie);
            // 3. 将上面拿到的identity对象登录
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);
        }
    }
}