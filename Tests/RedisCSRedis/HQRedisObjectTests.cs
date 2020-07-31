using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedisDao.RedisCSRedis;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Model;

namespace RedisDao.RedisCSRedis.Tests
{
    [TestClass()]
    public class HQRedisObjectTests
    {

        [TestMethod()]
        public void GetDataTest()
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

            var comm = db.GetObjectComm ("user");
            var comm2 = db.GetObjectComm ("userfloat");

            var v1 = comm.SetData(u1);

            var v5 = comm2.IncrBy(3);
            var v62 = comm2.IncrByFloat(11.11f);
            var v63 = comm2.IncrByFloat(11.11f);
            var v64 = comm2.IncrByFloat(11.11f);
            var v42 = comm.GetRange(13, 37);


            var v4 = comm.GetRange(10, 20);
            var v2 = comm.GetData<user>();

            var v3 = comm.GetLength();
            var v6 = comm2.IncrByFloat(11.11f);
            var v7 = comm.MSET(("user2", u2), ("user3", u3), ("user4", u4), ("user5", u5), ("user6", u6));
            var v8 = comm.MGET("user2", "user3", "user4", "user5");

            
            //var v8 = comm.MGET("user2", "user3", "user4", "user5", "user6");


        }

    }
}