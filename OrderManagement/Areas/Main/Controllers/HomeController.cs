using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}