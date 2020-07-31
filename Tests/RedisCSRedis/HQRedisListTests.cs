using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedisDao.RedisCSRedis;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Model;

namespace RedisDao.RedisCSRedis.Tests
{
    [TestClass()]
    public class HQRedisListTests
    {
        [TestMethod()]
        public void GetOneTest()
        {
            HQRedisService service = new HQRedisService("8.129.197.125", "Bus01#dwjwlxs", 6379, false);

            HQRedisDB db = new HQRedisDB(service, 4);

            var comm3 = db.GetListComm("userList3");

            comm3.Set("aaa111");
            comm3.Set("aaa222");
            comm3.Set("aaa333");
            comm3.Set("aaa444");
            comm3.Set("aaa555");
            comm3.Set("aaa666");

            var v31 = comm3.Insert(true, "aaa222", "aaa555");

            var comm = db.GetListComm("userList");

            user u1 = new user() { id = 1, money = 11.11, name = "老王老王老王老王老王老王老王老王老王老王" };
            user u2 = new user() { id = 2, money = 22.22, name = "b22老张老张老张老张老张老张老张" };
            user u3 = new user() { id = 3, money = 33.33, name = "b33老刘老刘老刘老刘老刘老刘老刘" };
            user u4 = new user() { id = 4, money = 44.44, name = "b44老李老李老李老李老李老李" };
            user u5 = new user() { id = 5, money = 55.55, name = "b55老黑老黑老黑老黑老黑老黑老黑" };
            user u6 = new user() { id = 6, money = 66.66, name = "b66小王小王小王小王小王小王" };
            user u7 = new user() { id = 7, money = 77.77, name = "b77小张小张小张小张小张小张" };
            user u8 = new user() { id = 8, money = 88.88, name = "b88小李小李小李小李小李小李小李" };
            user u9 = new user() { id = 9, money = 99.99, name = "b99小刘小刘小刘小刘小刘小刘小刘" };



            var v1 = comm.Set(u1);
            var v2 = comm.Set(u2);
            var v3 = comm.Set(u3);
            var v4 = comm.Set(u4);
            var v11 = comm.Set(u5);
            var v12 = comm.Set(u6);
            var v13 = comm.Set(u7);
            var v14 = comm.Set(u8);
            var v5 = comm.Insert(true, u2, u9);
            var v6 = comm.Update(u3, 4);
            var v7 = comm.GetCount();
            var v8 = comm.GetOne<user>(4);
            var v9 = comm.GetRange<user>(2, 4);
            var v10 = comm.Remove(2, u6);

            for (int i = 0; i < 20; i++)
            {
                var v21 = comm.GetAndRemoveFirst<user>();
            }

        }
    }
}