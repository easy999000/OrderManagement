using SpanJson.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisDao.IRedis
{
    /// <summary>
    /// 键值单对象存储
    /// </summary>
    /// <typeparam name="DataType"></typeparam>
    public interface IHQRedisObjectComm : IHQRedisComm
    {
        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public bool SetData(object Data);

        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        public DataType GetData<DataType>();

        /// <summary>
        /// 获取子字符串
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public string GetRange(int Start, int End);

        /// <summary>
        /// 批量获取多个
        /// </summary>
        /// <param name="Keys"></param>
        /// <returns></returns>
        public List<string> MGET(params string[] Keys);

        /// <summary>
        /// 批量设置多个
        /// </summary>
        /// <param name="Keys"></param>
        /// <returns></returns>
        public bool MSET(params (string, object)[] Keys);

        /// <summary>
        /// 获取字符串长度
        /// </summary>
        /// <returns></returns>
        public long GetLength();

        /// <summary>
        /// 对数字类型增加指定的整数值
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        public long IncrBy(int Num);

        /// <summary>
        /// 对数字类型增加指定的浮点数值
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        public double IncrByFloat(double Num);

        ///// <summary>
        ///// 追加字符串,如果是实体类型,追加了字符串,会导致无法正常反序列化
        ///// </summary>
        ///// <param name="Str"></param>
        ///// <returns></returns>
        //public long APPEND(string Str);
    }
}
