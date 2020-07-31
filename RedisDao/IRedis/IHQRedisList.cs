using SpanJson.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisDao.IRedis
{
    /// <summary>
    /// 可重复有序列表
    /// </summary>
    /// <typeparam name="DataType"></typeparam>
    public interface IHQRedisListComm : IHQRedisComm
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public long Set(params object[] Data);
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="Before"></param>
        /// <param name="OfData"></param>
        /// <param name="Data"></param>
        /// <returns></returns>

        public long Insert(bool Before, object OfData, object Data);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="Index"></param>
        /// <returns></returns>

        public bool Update(object Data, int Index);
        /// <summary>
        /// 获取数量
        /// </summary>
        /// <returns></returns>

        public long GetCount();
        /// <summary>
        /// 索引获取
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="Index"></param>
        /// <returns></returns>

        public DataType GetOne<DataType>(long Index);
        /// <summary>
        /// 获取指定范围
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="StartIndex"></param>
        /// <param name="EndIndex"></param>
        /// <returns></returns>
        public DataType[] GetRange<DataType>(int StartIndex, int EndIndex);
        /// <summary>
        /// 移除
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="Count"></param>
        /// <param name="Data"></param>
        /// <returns></returns>

        public long Remove(int Count, object Data);
        /// <summary>
        /// 获取并移除第一个,没有返回null
        /// </summary>
        /// <returns></returns>
        public DataType GetAndRemoveFirst<DataType>();



    }
}
