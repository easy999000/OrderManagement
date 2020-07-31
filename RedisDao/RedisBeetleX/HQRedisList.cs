using BeetleX.Redis;
using RedisDao.IRedis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisDao.RedisBeetleX
{
    public class HQRedisList<DataType> : HQRedisComm<DataType>, IHQRedisListComm<DataType>
    {
        RedisList<DataType> Comm;

        public HQRedisList(HQRedisDB DB, string Key) : base(DB, Key)
        {
            Comm = DB.DB.CreateList<DataType>(Key);
        }

        public DataType GetAndRemoveFirst()
        {

            return Comm.Pop().Result;
        }

        public long GetCount()
        {

            return Comm.Len().Result;
        }

        public DataType GetOne(int Index)
        {

            return Comm.Index(Index).Result;
        }

        public DataType[] GetRange(int StartIndex, int EndIndex)
        {

            return Comm.Range(StartIndex, EndIndex).Result;
        }

        public long Insert(bool Before, DataType OfData, DataType Data)
        {

            return Comm.Insert(true, OfData, Data).Result;
        }

        public long Set(DataType Data)
        {

            return Comm.RPush(Data).Result;
        }

        public long Remove(int Count, DataType Data)
        {

            return Comm.Rem(Count, Data).Result;
        }


        public bool Update(DataType Data, int Index)
        {

            string res = Comm.Set(Index, Data).Result;


            if (res == "OK")
            {
                return true;
            }

            return false;
        }

        public long Set(params DataType[] Data)
        {
            throw new NotImplementedException();
        }

        public DataType GetOne(long Index)
        {
            throw new NotImplementedException();
        }
    }
}
