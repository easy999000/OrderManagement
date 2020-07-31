
using RedisDao.IRedis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisDao.RedisCSRedis
{
    public class HQRedisList  : HQRedisComm , IHQRedisListComm 
    {
        //RedisList<DataType> Comm;

        public HQRedisList(HQRedisDB DB, string Key) : base(DB, Key)
        { 
        }

        public DataType GetAndRemoveFirst<DataType>()
        {
            return DB.DB.LPop<DataType>(Key);
        }

        public long GetCount()
        {
            return DB.DB.LLen(Key);
        }

        public DataType GetOne<DataType>(long Index)
        {
            return DB.DB.LIndex<DataType>(Key, Index); 
        }

        public DataType[] GetRange<DataType>(int StartIndex, int EndIndex)
        {
            return DB.DB.LRange<DataType>(Key, StartIndex, EndIndex); 
        }

        /// <summary>
        /// 这个方法有问题,插入不进去,先不要用
        /// </summary>
        /// <param name="Before"></param>
        /// <param name="OfData"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public long Insert(bool Before, object OfData, object Data)
        {
            return DB.DB.LInsertBefore(Key, OfData, Data); 
        }

        public long Set(params object[] Data)
        {
            return DB.DB.RPush(Key, Data);
        }

        public long Remove(int Count, object Data)
        {
            return DB.DB.LRem(Key, Count, Data);
             
        }


        public bool Update(object Data, int Index)
        {
            return DB.DB.LSet(Key, Index, Data);
             
        }
    }
}
