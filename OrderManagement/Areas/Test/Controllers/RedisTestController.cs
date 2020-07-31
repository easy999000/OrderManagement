using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedisDao;
using StackExchange.Redis;

namespace OrderManagement.Areas.Test.Controllers
{
    public class RedisTestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        private readonly IDatabase _redis;
        public RedisTestController(RedisHelper client)
        {
            //_redis = client.GetDatabase();
        }

        public IActionResult Index2()
        {
            //_redis.HashGet("TestRedis", "11111");



            //string temp = _redis.StringGet("TestRedis");
            return View();
        }
         




    }
}
