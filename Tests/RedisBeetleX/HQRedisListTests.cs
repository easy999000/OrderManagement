using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedisDao.RedisBeetleX;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Model;

namespace RedisDao.RedisBeetleX.Tests
{
    [TestClass()]
    public class HQRedisListTests
    {
        [TestMethod()]
        public void PushTest()
        {
            HQRedisService service = new HQRedisService("8.129.197.125", "Bus01#dwjwlxs", 0, false);

            HQRedisDB db = new HQRedisDB(service, 4);

            var comm = db.GetListComm<user>("userList");

            user u1 = new user() { id = 1, money = 11.11, name = "b11" };
            user u2 = new user() { id = 2, money = 22.22, name = "b22" };
            user u3 = new user() { id = 3, money = 33.33, name = "b33" };
            user u4 = new user() { id = 4, money = 44.44, name = "b44" };
            user u5 = new user() { id = 5, money = 55.55, name = "b55" };
            user u6 = new user() { id = 3, money = 33.33, name = "b33" };
             

            var v1 = comm.Set(u1);
            var v2 = comm.Set(u2);
            var v3 = comm.Set(u3);
            var v4 = comm.Set(u4);
            var v5 = comm.Insert(true, u3, u5);
            var v6 = comm.Update(u3, 4);
            var v7 = comm.GetCount();
            var v8 = comm.GetOne(4);
            var v9 = comm.GetRange(2, 4);
            var v10 = comm.Remove(2, u6);

            for (int i = 0; i < 20; i++)
            {
                var v11 = comm.GetAndRemoveFirst();
            }


        }
    }
}