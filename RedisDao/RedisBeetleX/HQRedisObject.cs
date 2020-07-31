using RedisDao.IRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisDao.RedisBeetleX
{
    public class HQRedisObject<DataType> : HQRedisComm<DataType>, IHQRedisObjectComm<DataType>
    {

        public HQRedisObject(HQRedisDB DB, string Key) : base(DB, Key)
        {
        }

        //public long APPEND(string Str)
        //{
        //    return DB.DB.
        //}

        public DataType GetData()
        {
            return DB.DB.Get<DataType>(Key).Result;
        }

        public long GetLength()
        {
            return DB.DB.Strlen(Key).Result;
        }

        public string GetRange(int Start, int End)
        {
            return DB.DB.GetRange(Key, Start, End).Result;
        }

        public long INCRBY(int Num)
        {
            return DB.DB.Incrby(Key, Num).Result;
        }

        public long IncrBy(int Num)
        {
            throw new NotImplementedException();
        }

        public float INCRBYFLOAT(float Num)
        {
            return DB.DB.IncrbyFloat(Key, Num).Result;
        }

        public double IncrByFloat(double Num)
        {
            throw new NotImplementedException();
        }

        public List<string> MGET(params string[] Keys)
        {
            Type[] types = new Type[Keys.Length];
            for (int i = 0; i < types.Length; i++)
            {
                types[i] = typeof(string);
            }

            return DB.DB.MGet(Keys, types).Result.Select(o => o.ToString()).ToList();
        }

        public bool MSET(params (string, object)[] Keys)
        {
            var res = DB.DB.MSet(Keys).Result;
            if (res == "OK")
            {
                return true;

            }
            return false;
        }

        public bool SetData(DataType Data)
        {
            string res = DB.DB.Set(Key, Data).Result;

            if (res == "OK")
            {
                return true;
            }

            return false;
        }
    }
}
