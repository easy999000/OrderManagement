using RedisDao.IRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisDao.RedisCSRedis
{
    public class HQRedisObject  : HQRedisComm , IHQRedisObjectComm 
    {

        public HQRedisObject(HQRedisDB DB, string Key) : base(DB, Key)
        {
        }

        //public long APPEND(string Str)
        //{
        //    return DB.DB.
        //}

        public DataType GetData<DataType>()
        {
            return DB.DB.Get<DataType>(Key);
        }

        public long GetLength()
        {
            return DB.DB.StrLen(Key);
        }

        public string GetRange(int Start, int End)
        {
            return DB.DB.GetRange(Key, Start, End);
        }

        public long IncrBy(int Num)
        {
            return DB.DB.IncrBy(Key, Num);
        }

        public double IncrByFloat(double Num)
        {
            return (double)DB.DB.IncrByFloat(Key, (decimal)Num);
        }

        public List<string> MGET(params string[] Keys)
        {
            //Type[] types = new Type[Keys.Length];
            //for (int i = 0; i < types.Length; i++)
            //{
            //    types[i] = typeof(string);
            //}

            return DB.DB.MGet(Keys).ToList();
        }

        public bool MSET(params (string, object)[] Keys)
        {
            object[] Param = new object[Keys.Length * 2];

            for (int i = 0; i < Keys.Length; i++)
            {
                Param[i * 2] = Keys[i].Item1;
                Param[i * 2+1] = Keys[i].Item2;
            }

            return DB.DB.MSet(Param);
            
        }

        public bool SetData(object  Data)
        {
            return DB.DB.Set(Key, Data);
             
        }
    }
}
