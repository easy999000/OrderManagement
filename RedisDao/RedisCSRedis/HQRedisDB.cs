
using RedisDao.IRedis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisDao.RedisCSRedis
{
    public class HQRedisDB : IHQRedisDB
    {
        public CSRedis.CSRedisClient DB = null;

        public HQRedisDB(HQRedisService Service, int DBIndex)
        { 
            string connStr = $"{Service.Host}:{Service.Prot},defaultDatabase={DBIndex},password={Service.Password},prefix={Service.Prefix},ssl={Service.SSL},testcluster=false,poolsize=20,tryit=0";
            DB = new CSRedis.CSRedisClient(connStr);
              
        }


        public IHQRedisListComm  GetListComm (string Key)
        {
            return new HQRedisList (this, Key);
        }

        public IHQRedisObjectComm  GetObjectComm (string Key)
        {
            return new HQRedisObject (this, Key);
        }

        public IHQRedisSortSet  GetSortSetComm (string Key)
        {
            return new HQRedisSortSet (this, Key);
        }

        public IHQRedisKeyComm GetKeyComm()
        {
            
            return new HQRedisKeyComm(this);
        }

        public IHQRedisHash  GetHashComm (string Key)
        {
            return new HQRedisHash (this,Key);
        }
    }
}
