using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedisDao.RedisCSRedis;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Model;

namespace RedisDao.RedisCSRedis.Tests
{
    [TestClass()]
    public class HQRedisKeyCommTests
    {
        [TestMethod()]
        public void DelTest()
        {
            user u1 = new user() { id = 1, money = 11.11, name = "老王老王老王老王老王老王老王老王老王老王" };
            user u2 = new user() { id = 2, money = 22.22, name = "b22老张老张老张老张老张老张老张" };
            user u3 = new user() { id = 3, money = 33.33, name = "b33老刘老刘老刘老刘老刘老刘老刘" };
            user u4 = new user() { id = 4, money = 44.44, name = "b44老李老李老李老李老李老李" };
            user u5 = new user() { id = 5, money = 55.55, name = "b55老黑老黑老黑老黑老黑老黑老黑" };
            user u6 = new user() { id = 6, money = 66.66, name = "b66小王小王小王小王小王小王" };
            user u7 = new user() { id = 7, money = 77.77, name = "b77小张小张小张小张小张小张" };
            user u8 = new user() { id = 8, money = 88.88, name = "b88小李小李小李小李小李小李小李" };
            user u9 = new user() { id = 9, money = 99.99, name = "b99小刘小刘小刘小刘小刘小刘小刘" };

            HQRedisService service = new HQRedisService("8.129.197.125", "Bus01#dwjwlxs", 6379, false);
            //HQRedisService service = new HQRedisService("192.168.18.115", null, 0, false);

            HQRedisDB db = new HQRedisDB(service, 4);

            var comm2 = db.GetObjectComm ("userKey2");
            comm2.SetData(u2);
            var comm3 = db.GetObjectComm ("userKey3");
            comm3.SetData(u3);
            var comm4 = db.GetObjectComm ("userKey4");
            comm4.SetData(u4);
            var comm5 = db.GetObjectComm ("userKey5");
            comm5.SetData(u5);
            var comm6 = db.GetObjectComm ("userKey6");
            comm6.SetData(u6);
            var comm7 = db.GetObjectComm ("userKey7");
            comm7.SetData(u7);

            var comm = db.GetKeyComm();


            var v1 = comm.Exists("userKey5");

            var v2 = comm.Expire("userKey5", 20);
            var v3 = comm.Expire("userKey7", 200);

            var v4 = comm.ExpireAt("userKey6", DateTime.Now.AddSeconds(2000));

            var v5 = comm.GetKeyType("userKey2");

            var v6 = comm.Persist("userKey7");

            var v7 = comm.PExpire("userKey7", 10000);

            var v10 = comm.Ttl("userKey6");
            var v12 = comm.Ttl("userKey7");

            //var v8 = comm.Rename("user2", "user2222eee");

            var v9 = comm.SearchKeys("userKey*");


            var v11 = comm.Del("userKey3", "userKey4");

        }
    }
}