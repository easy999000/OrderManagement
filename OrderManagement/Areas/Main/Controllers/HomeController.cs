using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace OrderManagement.Areas.Main.Controllers
{
    [Area("main")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            var feature = this.HttpContext.Features.Get<IExceptionHandlerFeature>();
            var error = feature?.Error;

            if (error == null)
            {
                return View();
            }

            ViewData["error"] = Newtonsoft.Json.JsonConvert.SerializeObject(error);

            Console.Write(ViewData["error"]);

            return View();
        }
    }
}