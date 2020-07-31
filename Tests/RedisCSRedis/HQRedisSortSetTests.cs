using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedisDao.RedisCSRedis;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Model;

namespace RedisDao.RedisCSRedis.Tests
{
    [TestClass()]
    public class HQRedisSortSetTests
    {
        [TestMethod()]
        public void GetAllTest()
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

            HQRedisDB db = new HQRedisDB(service, 4);

            var comm = db.GetSortSetComm("userSortSet");
            var comm3 = db.GetSortSetComm("userSortSet3");

            var comm2 = comm as HQRedisSortSet ;

            var v1 = comm.Set((11.11, u1));
            var v2 = comm.Set((22.22, u2));
            var v3 = comm.Set((33.33, u3));
            var v4 = comm.Set((44.44, u4));
            var v5 = comm.Set((55.55, u5));
            var v6 = comm.Set((66.66, u6));
            var v7 = comm.Set((77.77, u7)); 

            var v35 = comm3.Set((55.55, u5));
            var v36 = comm3.Set((66.66, u6));
            var v37 = comm3.Set((77.77, u7));
            var v38 = comm3.Set((88.88, u8));
            var v39 = comm3.Set((99.99, u9));

            var v10 = comm.GetAll<user>();
            var v11 = comm.GetCount();
            var v12 = comm.GetCount(2, 55);
            var v13 = comm.GetCount(u2, u4);

            var v14 = comm.GetIndexAsc(u4);
            var v15 = comm.GetIndexDesc(u4);

            var v16 = comm.GetRange<user>(u2, u6);

            var v17 = comm.GetRangeByIndex<user>(3, 7);
             

            var v18 = comm.GetRangeByScore<user>(3, 70);
            var v19 = comm.GetScore(u7);
            var v20 = comm.ZInterStore("userSortSet_ZInterStore", "userSortSet3");
            //var v21 = comm.ZInterStore("userSortSet_GetZUNIONSTORE", "userSortSet2");
            var v26 = comm.ZUnionStore("userSortSet_GetZUNIONSTORE", "userSortSet3");
            var v22 = comm.ZIncrBy(u8, 1);
            var v23 = comm.Remove(u8);
            var v24 = comm.RemoveIndex(6, 9);
            var v25 = comm.RemoveScore(3, 30);





        }
    }
}