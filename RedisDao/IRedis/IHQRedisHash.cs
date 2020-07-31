using System;
using System.Collections.Generic;
using System.Text;

namespace RedisDao.IRedis
{
    public interface IHQRedisHash : IHQRedisComm
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Keys"></param>
        /// <returns></returns>
        public long HDel(params string[] HKeys);

        /// <summary>
        /// 是否存在键
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public bool HExiste(string HKey);

        /// <summary>
        /// 获取键值
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public DataType HGet<DataType>(string HKey);

        /// <summary>
        /// 累加整形
        /// </summary>
        /// <param name="Key"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public long HIncrby(string HKey, long increment);

        /// <summary>
        /// 累加浮点
        /// </summary>
        /// <param name="Key"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public double HIncrByFloat(string HKey, double Increment);

        /// <summary>
        /// 获取所有键
        /// </summary>
        /// <returns></returns>
        public string[] GetAllKeys();

        /// <summary>
        /// 获取键的数量
        /// </summary>
        /// <returns></returns>
        public long GetKeyLength();

        /// <summary>
        /// 获取指定的键值
        /// </summary>
        /// <param name="Keys"></param>
        /// <returns></returns>
        public DataType[] HMGet<DataType>(params string[] HKeys);


        /// <summary>
        /// 设置指定的键值
        /// </summary>
        /// <param name="Keys"></param>
        /// <returns></returns>
        public bool HMSet(params (string, object)[] HKeys);


        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="HKey"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public bool HSet(string HKey, object Data);


        /// <summary>
        /// 获取所有的值
        /// </summary>
        /// <param name="Keys"></param>
        /// <returns></returns>
        public Dictionary<string, DataType> HGetAllData<DataType>();




    }
}
