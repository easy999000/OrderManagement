using BeetleX.Redis.Commands;
using RedisDao.IRedis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisDao.RedisCSRedis
{
    public class HQRedisHash  : HQRedisComm , IHQRedisHash 
    {
        public HQRedisHash(HQRedisDB DB, string Key) : base(DB, Key)
        {
        }

        public string[] GetAllKeys()
        {
            return DB.DB.HKeys(Key);
        }

        public long GetKeyLength()
        {
            return DB.DB.HLen(Key);
        }

        public long HDel(params string[] HKeys)
        {
            return DB.DB.HDel(Key, HKeys);
        }

        public bool HExiste(string HKey)
        {
            return DB.DB.HExists(this.Key, HKey);
        }

        public DataType HGet<DataType>(string HKey)
        {
            return DB.DB.HGet<DataType>(this.Key, HKey);
        }

        public Dictionary<string, DataType> HGetAllData<DataType>()
        {
            return DB.DB.HGetAll<DataType>(this.Key);
        }

 

        public long HIncrby(string HKey, long increment)
        {
            return DB.DB.HIncrBy(this.Key, HKey, increment);
        }

        public double HIncrByFloat(string HKey, double Increment)
        {
            return (double)DB.DB.HIncrByFloat(this.Key, HKey, (decimal)Increment);
        }

        public DataType[] HMGet<DataType>(params string[] HKeys)
        {
            return DB.DB.HMGet<DataType>(this.Key, HKeys);
        }

        public bool HMSet(params (string, object)[] HKeys)
        {
            object[] param = new object[HKeys.Length*2];

            for (int i = 0; i < HKeys.Length; i++)
            {
                param[i * 2] = HKeys[i].Item1;
                param[i * 2 + 1] = HKeys[i].Item2;
            }

            return DB.DB.HMSet(this.Key, param);
        }

        public bool HSet(string HKey, object Data)
        {
            return DB.DB.HSet(this.Key, HKey, Data);
        }
    }
}
