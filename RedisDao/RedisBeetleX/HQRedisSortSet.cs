using BeetleX.Redis;
using Newtonsoft.Json;
using RedisDao.IRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisDao.RedisBeetleX
{
    public class HQRedisSortSet<DataType> : HQRedisComm<DataType>, IHQRedisSortSet<DataType>
    {
        public Sequence Comm;

        public HQRedisSortSet(HQRedisDB DB, string Key) : base(DB, Key)
        {
            Comm = DB.DB.CreateSequence(Key);

        }

        public List< DataType> GetAll()
        {
            return GetRangeByIndex(0, -1);
        }

        public long GetCount()
        {
            return Comm.ZCard().Result;
        }

        public long GetCount(double MinScore, double MaxScore)
        {
            return Comm.ZCount(MinScore, MaxScore).Result;
        }

        /// <summary>
        /// 有问题
        /// </summary>
        /// <param name="MinData"></param>
        /// <param name="MaxData"></param>
        /// <returns></returns>
        public long GetCount(DataType MinData, DataType MaxData)
        {
            //return Comm.ZLexCount(
            //  Json(MinData)
            //    ,
            //   Json(MaxData)
            //    ).Result;

            return 0;
        }

        public long GetIndexAsc(DataType Data)
        {
            return Comm.ZRank(
               Json(Data)).Result;
        }

        public long GetIndexDesc(DataType Data)
        {
            return Comm.ZRevRank(
               Json(Data)).Result;
        }

        /// <summary>
        /// null
        /// </summary>
        /// <param name="MinData"></param>
        /// <param name="MaxData"></param>
        /// <returns></returns>
        public List<DataType> GetRange(DataType MinData, DataType MaxData)
        {
            //var res = Comm.ZRangeByLex(Json(MinData)
            //    , Json(MaxData)).Result;

            //return res.Select(o => (JsonDes<DataType>(o))).ToList();
            return null;
        }

        public List<DataType> GetRangeByIndex(int MinIndex, int MaxIndex)
        {
            var res = Comm.ZRange(MinIndex, MaxIndex).Result;
            return res.Select(o => JsonDes<DataType>(o.Member)).ToList();
        }

        public List<DataType> GetRangeByScore(double MinScore, double MaxScore)
        {
            var res = Comm.ZRangeByScore(MinScore.ToString(), MaxScore.ToString()).Result;
            return res.Select(o => JsonDes<DataType>(o.Member)).ToList();
        }

        public double GetScore(DataType Data)
        {
            return Comm.ZScore(Json(Data)).Result;

        }

        public long GetZINTERSTORE(string NewKey, string CompareKey)
        {
            return Comm.ZInterStore(NewKey, CompareKey).Result;
        }

        public long GetZUNIONSTORE(string NewKey, string CompareKey)
        {
            return Comm.ZUnionsStore(NewKey, CompareKey).Result;
        }

        public long Remove(params DataType[] Data)
        {
            return Comm.ZRem(Data.Select(o => Json(o)).ToArray()).Result;
        }

        public long RemoveIndex(int MinIndex, int MaxIndex)
        {
            return Comm.ZRemRangeByRank(MinIndex, MaxIndex).Result;
        }

        public long RemoveIndex(long MinIndex, long MaxIndex)
        {
            throw new NotImplementedException();
        }

        public long RemoveScore(double MinScore, double MaxScore)
        {
            return Comm.ZRemRangeByScore(MinScore, MaxScore).Result;
        }

        public long Set(params (double, DataType)[] Data)
        {
            return Comm.ZAdd(Data.Select(o => (o.Item1, Json(o.Item2))).ToArray()).Result;
        }

        public double ZIncrBy(DataType Data, double increment)
        {
            return Comm.ZIncrby(increment, Json(Data)).Result;
        }

        public long ZInterStore(string NewKey, params string[] CompareKey)
        {
            throw new NotImplementedException();
        }

        public long ZUnionStore(string NewKey, params string[] CompareKey)
        {
            throw new NotImplementedException();
        }

        List<DataType> IHQRedisSortSet<DataType>.GetAll()
        {
            throw new NotImplementedException();
        }

        long? IHQRedisSortSet<DataType>.GetIndexAsc(DataType MinData)
        {
            throw new NotImplementedException();
        }

        long? IHQRedisSortSet<DataType>.GetIndexDesc(DataType MinData)
        {
            throw new NotImplementedException();
        }
    }
}
