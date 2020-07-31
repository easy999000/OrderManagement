using System;
using System.Collections.Generic;
using System.Text;

namespace RedisDao.IRedis
{
    /// <summary>
    /// 有序集合
    /// </summary>
    public interface IHQRedisSortSet : IHQRedisComm
    {

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public long Set(params (double, object)[] Data);

        /// <summary>0
        /// 获取数量
        /// </summary>
        /// <returns></returns>
        public long GetCount();
        /// <summary>0
        /// 获取数量
        /// 注意使用字典序列返回区间的所有函数的使用隐含前提
        /// 是该有序集合内的所有元素的分数相同，在有序集合中
        /// 相同分数的元素之间的顺序是通过字典序排列的
        /// </summary>
        /// <returns></returns>
        public long GetCount(double MinScore, double MaxScore);
        /// <summary>0
        /// 获取数量
        /// </summary>
        /// <returns></returns>
        public long GetCount(object MinData, object MaxData);
        /// <summary>
        /// 获取指定索引范围的数据
        /// </summary>
        /// <param name="MinIndex"></param>
        /// <param name="MaxIndex"></param>
        /// <returns></returns>
        public List<DataType> GetRangeByIndex<DataType>(int MinIndex, int MaxIndex);

        /// <summary>
        /// 获取指定分数范围的值
        /// </summary>
        /// <param name="MinScore"></param>
        /// <param name="MaxScore"></param>
        /// <returns></returns>
        public List<DataType> GetRangeByScore<DataType>(double MinScore, double MaxScore);

        /// <summary>
        /// 获取两个数据之间的值
        /// 注意使用字典序列返回区间的所有函数的使用隐含前提
        /// 是该有序集合内的所有元素的分数相同，在有序集合中
        /// 相同分数的元素之间的顺序是通过字典序排列的
        /// </summary>
        /// <param name="MinData"></param>
        /// <param name="MaxData"></param>
        /// <returns></returns>
        public List<DataType> GetRange<DataType>(object MinData, object MaxData);

        /// <summary>
        /// 获取指定数据的索引,正序
        /// </summary>
        /// <param name="MinData"></param>
        /// <returns></returns>
        public long? GetIndexAsc(object MinData);

        /// <summary>
        /// 获取指定数据的索引,倒叙
        /// </summary>
        /// <param name="MinData"></param>
        /// <returns></returns>
        public long? GetIndexDesc(object MinData);

        /// <summary>
        /// 获取指定数据的分数
        /// </summary>
        /// <param name="MinData"></param>
        /// <returns></returns>
        public double GetScore(object MinData);

        /// <summary>
        /// 给指定数据加分
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="increment"></param>
        /// <returns></returns>
        public double ZIncrBy(object Data, double increment);

        /// <summary>
        /// 把交集存入新的Key中
        /// </summary>
        /// <param name="NewKey"></param>
        /// <param name="CompareKey"></param>
        /// <returns></returns>
        public long ZInterStore(string NewKey, params string[] CompareKey);


        /// <summary>
        /// 获取全部
        /// </summary>
        /// <returns></returns>
        public List<DataType> GetAll<DataType>();

        /// <summary>
        /// 把并集存入新的Key中
        /// </summary>
        /// <param name="NewKey"></param>
        /// <param name="CompareKey"></param>
        /// <returns></returns>
        public long ZUnionStore(string NewKey, params string[] CompareKey);

        /// <summary>
        /// 移除指定对象
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public long Remove(params object[] Data);
        /// <summary>
        /// 移除,索引范围
        /// </summary>
        /// <param name="MinIndex"></param>
        /// <param name="MaxIndex"></param>
        /// <returns></returns>
        public long RemoveIndex(long MinIndex, long MaxIndex);

        /// <summary>
        /// 移除,分数范围
        /// </summary>
        /// <param name="MinScore"></param>
        /// <param name="MaxScore"></param>
        /// <returns></returns>
        public long RemoveScore(double MinScore, double MaxScore);



    }
}
