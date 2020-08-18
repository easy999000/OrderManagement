using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MQWebApiTest.Models;

namespace MQWebApiTest.Controllers
{
    public class MQTestController : Controller
    {
        public string Test_0s(MQTestModel m
            )
        {
            return "ok";
        }
        public string Test_1s(MQTestModel m
            )
        {
            System.Threading.Thread.Sleep(1000);
            return "ok";
        }
        public string Test_5s(MQTestModel m
            )
        {
            System.Threading.Thread.Sleep(5000);
            return "ok";
        }
        public string Test_20s(MQTestModel m
            )
        {
            System.Threading.Thread.Sleep(20*1000);
            return "ok";
        }
        public string Test_2m(MQTestModel m
            )
        {
            System.Threading.Thread.Sleep(2*60*1000);
            return "ok";
        }
        public string Test_10m(MQTestModel m
            )
        {
            System.Threading.Thread.Sleep(10*60*1000);
            return "ok";
        }
    }
}
