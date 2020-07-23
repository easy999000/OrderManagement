using NUnit.Framework;
using RedisDao;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace OrderTest
{
    public class RedisTest
    {
        RedisHelper RedisHelper;
        private readonly IDatabase redisDb;
        public RedisTest()
        {

            //RedisHelper = new RedisHelper("192.168.18.115", "redisTest", 0);
            RedisHelper = new RedisHelper("8.129.197.125:6379,password=Bus01#dwjwlxs", "redisTest");

            //var v2_RedisHelper = new RedisHelper("r-wz911aq5ebvzoo1f97pd.redis.rds.aliyuncs.com:6379,defaultDatabase=1,password=Bus01#dwjwlxs", "redisTest");
            //RedisHelper = new RedisHelper("120.79.20.121:8199,defaultDatabase=15,password=bwerUGF21b", "redisTest");
            redisDb = RedisHelper.GetDatabase();

        }

        /// <summary>
        /// 将对象转换为byte数组
        /// </summary>
        /// <param name="obj">被转换对象</param>
        /// <returns>转换后byte数组</returns>
        public static byte[] Object2Bytes(object obj)
        {
            BinaryFormatter se = new BinaryFormatter();
            MemoryStream memStream = new MemoryStream();
            se.Serialize(memStream, obj);
            byte[] bobj = memStream.ToArray();
            memStream.Close();
            return bobj;

        } 
        // redis config
        private static ConfigurationOptions configurationOptions = ConfigurationOptions.Parse("8.129.197.125:6379,password=Bus01#dwjwlxs,connectTimeout=2000");
        //the lock for singleton
        private static readonly object Locker = new object();
        //singleton
        private static ConnectionMultiplexer redisConn;
        //singleton
        public static ConnectionMultiplexer getRedisConn()
        {
            if (redisConn == null)
            {
                lock (Locker)
                {
                    if (redisConn == null || !redisConn.IsConnected)
                    {
                        redisConn = ConnectionMultiplexer.Connect(configurationOptions);
                    }
                }
            }
            return redisConn;
        }

        [Test]

        public void test()
        {
            redisConn = getRedisConn();
            var db = redisConn.GetDatabase(1);

            //set get
            string strKey = "hello";
            string strValue = "world";
            bool setResult = db.StringSet(strKey, strValue);
            Console.WriteLine("set " + strKey + " " + strValue + ", result is " + setResult);
            //incr
            string counterKey = "counter";
            long counterValue = db.StringIncrement(counterKey);
            Console.WriteLine("incr " + counterKey + ", result is " + counterValue);

        }

        /// <summary>
        /// 将byte数组转换成对象
        /// </summary>
        /// <param name="buff">被转换byte数组</param>
        /// <param name="typ">转换成的类名</param>
        /// <returns>转换完成后的对象</returns>
        public static object Bytes2Object(byte[] buff, Type typ)
        {
            IntPtr ptr = Marshal.UnsafeAddrOfPinnedArrayElement(buff, 0);
            return Marshal.PtrToStructure(ptr, typ);
        }
        [Test]
        public void setString()
        {
            test2 t = new test2() { name = "nam", age = 1, value1 = "value1", value2 = 2 };

            RedisKey key = new RedisKey("time");
            RedisValue value = new RedisValue(DateTime.Now.ToString());

            byte[] bytes = Object2Bytes(t);


            RedisValue value2 = RedisValue.CreateFrom(new System.IO.MemoryStream(bytes));

            var v0 = redisDb.StringGet(key);
            var v1 = redisDb.StringSet(key, value2, expiry: new TimeSpan(0, 10, 0));

            Console.WriteLine(v1);
        }

        [Test]
        public void getString()
        {
            RedisKey key = new RedisKey("time");


            var v1 = redisDb.StringGet(key);

            var v2 = v1.Box();



            Console.WriteLine(v1);

        }
        [Serializable]
        public class test2
        {
            public string name { get; set; }
            public int age { get; set; }

            public string value1;

            public int value2;

            string value3 = "value3";

            int value4 = 4;

        }


    }
}
