using BeetleX.Redis;
using RedisDao.IRedis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisDao.RedisBeetleX
{
    public class HQRedisDB : IHQRedisDB
    {
        public RedisDB DB = null;
   
        public HQRedisDB(HQRedisService Service, int DBIndex)
        {
            //this.Service = Service;
            //this.DBIndex = DBIndex;
            DB = new RedisDB(DBIndex, new JsonFormater());

            DB.Host.AddWriteHost(Service.Host, Service.Prot, Service.SSL).Password = Service.Password;
             
        }


        public IHQRedisListComm<DataType> GetListComm<DataType>(string Key)
        {
            return new HQRedisList<DataType>(this, Key);
        }

        public IHQRedisObjectComm<DataType> GetObjectComm<DataType>(string Key)
        {
            return new HQRedisObject<DataType>(this,Key);
        }

        public IHQRedisSortSet<DataType> GetSortSetComm<DataType>(string Key)
        {
            return new HQRedisSortSet<DataType>(this, Key);
        }

        public IHQRedisKeyComm GetKeyComm()
        {
            throw new NotImplementedException();
        }

        public IHQRedisHash<DataType> GetHashComm<DataType>(string Key)
        {
            throw new NotImplementedException();
        }
    }
}
