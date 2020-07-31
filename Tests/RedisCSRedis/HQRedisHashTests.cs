using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedisDao.RedisCSRedis;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Model;

namespace RedisDao.RedisCSRedis.Tests
{
    [TestClass()]
    public class HQRedisHashTests
    {
        [TestMethod()]
        public void GetAllKeysTest()
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


            HQRedisService service = new HQRedisService("8.129.197.125", "Bus01#dwjwlxs", 0, false);

            HQRedisDB db = new HQRedisDB(service, 3);

            var comm = db.GetHashComm("userHash");
            var comm2 = db.GetHashComm("userHashfloat");
          

            var v1 = comm.HSet("user1", u1);
            var v21 = comm.HSet("user2", u2);
            var v3 = comm.HSet("user3", u3);
            var v4 = comm.HMSet(("user4", u4), ("user5", u5), ("user6", u6), ("user7", u7), ("user8", u8), ("user9", u9));

            comm2.HMSet(("user10", 10));
            comm2.HMSet(("user11", 11.11));

            //var v5 = comm2.DB.DB.HSet("userHash", "user10", 10);
            //var v6 = comm2.DB.DB.HSet("userHash", "user11", 11.11);

            var v7 = comm.GetAllKeys();
            var v8 = comm.GetKeyLength();
            var v9 = comm.HDel("user2", "user4");
            var v10 = comm.HExiste("user6");
            var v11 = comm.HGet<user>("user7");
            var v12 = comm.HGetAllData<user>();
            var v13 = comm2.HIncrby("user10", 3);
            var v132 = comm2.HIncrby("user10", 4);
            var v133 = comm2.HIncrby("user10", 5);
            var v14 = comm2.HIncrByFloat("user11", 22.22);
            var v15 = comm.HMGet<user>("user7", "user8");


        }
    }
}