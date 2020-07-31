using RedisDao.IRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisDao.RedisCSRedis
{
    public class HQRedisKeyComm : HQRedisComm , IHQRedisKeyComm
    {
        public HQRedisKeyComm(HQRedisDB DB) : base(DB, "")
        {
        }

        public long Del(params string[] Key)
        {
            return DB.DB.Del(Key);
        }

        public bool Exists(string Key)
        {
            return DB.DB.Exists(Key);
        }

        public bool Expire(string Key, int Seconds)
        {
            return DB.DB.Expire(Key, Seconds);
        }

        public bool ExpireAt(string Key, DateTime Time)
        {
            return DB.DB.ExpireAt(Key, Time);
        }

        public string GetKeyType(string Key)
        {
            return DB.DB.Type(Key).ToString();
        }

        public bool Persist(string Key)
        {
            return DB.DB.Persist(Key);
        }

        public bool PExpire(string Key, int Milliseconds)
        {
            return DB.DB.PExpire(Key, Milliseconds);
        }

        //public bool Rename(string Key, string Newkey)
        //{
        //    return DB.DB.Rename(Key, Newkey);
        //}

        public List<string> SearchKeys(string pattern)
        {
            return DB.DB.Keys(pattern).ToList();
        }

        public long Ttl(string Key)
        {
            return DB.DB.Ttl(Key);
        }
    }
}
