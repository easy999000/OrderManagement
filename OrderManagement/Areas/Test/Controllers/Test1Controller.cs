﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderManagement.Areas.Test.Controllers
{
    [Area("Test")]
    public class Test1Controller : Controller
    {
        public IActionResult Index()
        { 
            ViewData["msg"] = "Index";
            return View();
        }
        public IActionResult a1()
        {
            this.HttpContext.Session.SetString("sessionTest", DateTime.Now.ToString());

            ViewData["msg"] = "a1";
            return View("Index");
        }
        public IActionResult a2()
        {
            ViewData["msg"] = "a2   " + this.HttpContext.Session.GetString("sessionTest");
            return View("Index");
        }
        public IActionResult a3()
        {
            ViewData["msg"] = "a3";
            return View("Index");
        }
    }
}