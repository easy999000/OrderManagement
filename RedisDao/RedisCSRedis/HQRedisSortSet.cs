
using Newtonsoft.Json;
using RedisDao.IRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisDao.RedisCSRedis
{
    public class HQRedisSortSet : HQRedisComm, IHQRedisSortSet
    {

        public HQRedisSortSet(HQRedisDB DB, string Key) : base(DB, Key)
        {

        }

        public List<DataType> GetAll<DataType>()
        {
            return GetRangeByIndex<DataType>(0, -1);
        }

        public long GetCount()
        {
            return DB.DB.ZCard(Key);

        }

        public long GetCount(double MinScore, double MaxScore)
        {
            return DB.DB.ZCount(Key, (decimal)MinScore, (decimal)MaxScore);

        }
         
        public long GetCount(object MinData, object MaxData)
        {
            return DB.DB.ZLexCount(Key,"["+ Json(MinData), "[" + Json(MaxData));

        }

        public long? GetIndexAsc(object Data)
        {
            return DB.DB.ZRank(Key, Data);

            //return Comm.ZRank(
            //   Json(Data)).Result;
        }

        public long? GetIndexDesc(object Data)
        {
            return DB.DB.ZRevRank(Key, Data);
            //return Comm.ZRevRank(
            //   Json(Data)).Result;
        }
         
        public List<DataType> GetRange<DataType>(object MinData, object MaxData)
        {
            return DB.DB.ZRangeByLex<DataType>(Key, "[" + Json(MinData), "[" + Json(MaxData)).ToList()
            //.Select(o => JsonDes<DataType>(o)).ToList();
            ; 
        }

        public List<DataType> GetRangeByIndex<DataType>(int MinIndex, int MaxIndex)
        {
            //var v1 = DB.DB.ZRange<object>(Key, MinIndex, MaxIndex).ToList();


            return DB.DB.ZRange<DataType>(Key, MinIndex, MaxIndex).ToList();

            //var res = Comm.ZRange(MinIndex, MaxIndex).Result;
            //return res.Select(o => (o.Score, JsonDes<DataType>(o.Member))).ToList();
        }

        public List<DataType> GetRangeByScore<DataType>(double MinScore, double MaxScore)
        {

            //var res = Comm.ZRangeByScore(MinScore.ToString(), MaxScore.ToString()).Result;
            //return res.Select(o => (o.Score, JsonDes<DataType>(o.Member))).ToList();
            return DB.DB.ZRangeByScore<DataType>(Key, (decimal)MinScore, (decimal)MaxScore).ToList();
        }

        public double GetScore(object Data)
        {
            //return Comm.ZScore(Json(Data)).Result;

            return (double)DB.DB.ZScore(Key, Data);

        }
         
        public long ZInterStore(string NewKey, params string[] CompareKey)
        {
            var v1 = CompareKey.Append(Key).ToArray();

            return DB.DB.ZInterStore(NewKey, null, CSRedis.RedisAggregate.Max, v1);


        }

        public long ZUnionStore(string NewKey, params string[] CompareKey)
        {
            var v1 = CompareKey.Append(Key).ToArray();

            return DB.DB.ZUnionStore(NewKey, null, CSRedis.RedisAggregate.Max, v1);

        }

        public long Remove(params object[] Data)
        {
            //return Comm.ZRem(Data.Select(o => Json(o)).ToArray()).Result;

            return DB.DB.ZRem(Key, Data);


        }

        public long RemoveIndex(long MinIndex, long MaxIndex)
        {
            //return Comm.ZRemRangeByRank(MinIndex, MaxIndex).Result;

            return DB.DB.ZRemRangeByRank(Key, MinIndex, MaxIndex);


        }

        public long RemoveScore(double MinScore, double MaxScore)
        {
            //return Comm.ZRemRangeByScore(MinScore, MaxScore).Result;

            return DB.DB.ZRemRangeByScore(Key, (decimal)MinScore, (decimal)MaxScore);
        }

        public long Set(params (double, object)[] Data)
        {
            //return Comm.ZAdd(Data.Select(o => (o.Item1, Json(o.Item2))).ToArray()).Result;

            return DB.DB.ZAdd(Key,
                Data.Select(o => ((decimal)o.Item1, o.Item2)).ToArray()
         );


        }

        public double ZIncrBy(object Data, double increment)
        {
            //return Comm.ZIncrby(increment, Json(Data)).Result;

            return (double)DB.DB.ZIncrBy(Key, Data, (decimal)increment);
        }

    }
}
