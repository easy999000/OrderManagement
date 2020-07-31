using System;
using System.Collections.Generic;
using System.Text;

namespace RedisDao.IRedis
{
    public interface IHQRedisKeyComm : IHQRedisComm
    {
        /// <summary>
        /// 删除键
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public long Del(params string[] Key);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public bool Exists(string Key);
        /// <summary>
        /// 设置超时时长 秒
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Seconds"></param>
        /// <returns></returns>
        public bool Expire(string Key, int Seconds);
        /// <summary>
        /// 设置超时时间
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Time"></param>
        /// <returns></returns>
        public bool ExpireAt(string Key, DateTime Time);

        /// <summary>
        /// 设置超时时长 毫秒
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Milliseconds"></param>
        /// <returns></returns>
        public bool PExpire(string Key, int Milliseconds);

        /// <summary>
        /// 搜索key
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public List<string> SearchKeys(string pattern);



        /// <summary>
        /// 取消过期时间,永不超时
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Milliseconds"></param>
        /// <returns></returns>
        public bool Persist(string Key);

        /// <summary>
        /// 查询剩余过期时间 秒
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Milliseconds"></param>
        /// <returns></returns>
        public long Ttl(string Key);



        ///// <summary>
        ///// 修改key名字
        ///// </summary>
        ///// <param name="Key"></param>
        ///// <param name="Milliseconds"></param>
        ///// <returns></returns>
        //public bool Rename(string Key, string Newkey);

        ///// <summary>
        ///// 迭代数据库
        ///// </summary>
        ///// <returns></returns>
        //public int ScanDB();
        /// <summary>
        /// 查询key的类型
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string GetKeyType(string Key);



    }
}
