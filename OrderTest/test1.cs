using NUnit.Framework;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderTest
{
    public class test1
    {
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
    }
}
