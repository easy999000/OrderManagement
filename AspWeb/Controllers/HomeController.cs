 
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AspWeb.Controllers
{
    public class HomeController : Controller
    {
        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        private async Task SignInAsync()
        {
            //// 1. 利用ASP.NET Identity获取用户对象
            //var user = await UserManager.FindAsync("UserName", "Password");
            //// 2. 利用ASP.NET Identity获取identity 对象
            //var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            //// 3. 将上面拿到的identity对象登录
            //AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);
        } 
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult show2()
        {
            

            return View();
        }
    }
}