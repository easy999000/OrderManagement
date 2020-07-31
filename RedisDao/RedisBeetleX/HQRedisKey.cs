using RedisDao.IRedis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisDao.RedisBeetleX
{
    public class HQRedisKey : HQRedisComm<string>, IHQRedisKeyComm
    {

        public HQRedisKey(HQRedisDB DB, string Key) : base(DB, Key)
        {
        }

        public long Del(params string[] Key)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string Key)
        {
            throw new NotImplementedException();
        }

        public bool Expire(string Key, int Seconds)
        {
            throw new NotImplementedException();
        }

        public bool ExpireAt(string Key, DateTime Time)
        {
            throw new NotImplementedException();
        }

        public string GetKeyType(string Key)
        {
            throw new NotImplementedException();
        }

        public bool Persist(string Key)
        {
            throw new NotImplementedException();
        }

        public bool PExpire(string Key, int Milliseconds)
        {
            throw new NotImplementedException();
        }

        public bool Rename(string Key, string Newkey)
        {
            throw new NotImplementedException();
        }

        public List<string> SearchKeys(string pattern)
        {
            throw new NotImplementedException();
        }

        public long Ttl(string Key)
        {
            throw new NotImplementedException();
        }
    }
}
