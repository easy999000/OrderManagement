using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedisDao.RedisBeetleX;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Model;

namespace RedisDao.RedisBeetleX.Tests
{
    [TestClass()]
    public class HQRedisSetTests
    {
        [TestMethod()]
        public void SetTest()
        {
            user u1 = new user() { id = 1, money = 11.11, name = "b11" };
            user u2 = new user() { id = 2, money = 22.22, name = "b22" };
            user u3 = new user() { id = 3, money = 33.33, name = "b33" };
            user u4 = new user() { id = 4, money = 44.44, name = "b44" };
            user u5 = new user() { id = 5, money = 55.55, name = "b55" };
            user u6 = new user() { id = 6, money = 66.66, name = "b66" };
            user u7 = new user() { id = 7, money = 77.77, name = "b77" };
            user u8 = new user() { id = 8, money = 88.88, name = "b88" };
            user u9 = new user() { id = 9, money = 99.99, name = "b99" };

            HQRedisService service = new HQRedisService("8.129.197.125", "Bus01#dwjwlxs", 0, false);

            HQRedisDB db = new HQRedisDB(service, 4);

            var comm = db.GetSortSetComm<user>("userSortSet");

            var comm2 = comm as HQRedisSortSet<user>;

            var v1 = comm.Set((11.11, u1));
            var v2 = comm.Set((22.22, u2));
            var v3 = comm.Set((33.33, u3));
            var v4 = comm.Set((44.44, u4));
            var v5 = comm.Set((55.55, u5));
            var v6 = comm.Set((66.66, u6));
            var v7 = comm.Set((77.77, u7));
            var v8 = comm.Set((88.88, u8));
            var v9 = comm.Set((99.99, u9));
            comm2.Comm.ZAdd((22.33, "bb2233"));
            comm2.Comm.ZAdd((33.44, "bb3344"));
            comm2.Comm.ZAdd((44.55, "bb4455"));
            comm2.Comm.ZAdd((55.66, "bb5566"));
            comm2.Comm.ZAdd((66.77, "bb6677"));

            var vv2 = comm2.Comm.ZRangeByLex("[bb3344", "[bb5566", true).Result;

            var v10 = comm.GetAll();
            var v11 = comm.GetCount();
            var v12 = comm.GetCount(2, 55);
            //var v13 = comm.GetCount(u2, u4);

            var v14 = comm.GetIndexAsc(u4);
            var v15 = comm.GetIndexDesc(u4);

            var v16 = comm.GetRange(u2, u6);

            var v17 = comm.GetRangeByIndex(3, 7);

            var v18 = comm.GetRangeByScore(3, 6);
            var v19 = comm.GetScore(u7);
            var v20 = comm.ZInterStore("userSortSet_GetZINTERSTORE", "userSortSet2");
            var v21 = comm.ZInterStore("userSortSet_GetZUNIONSTORE", "userSortSet2");
            var v22 = comm.ZIncrBy(u8, 1);
            var v23 = comm.Remove(u8);
            var v24 = comm.RemoveIndex(6, 9);
            var v25 = comm.RemoveScore(5, 8);





        }
    }
}