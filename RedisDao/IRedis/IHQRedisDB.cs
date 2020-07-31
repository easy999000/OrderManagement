using System;
using System.Collections.Generic;
using System.Text;

namespace RedisDao.IRedis
{
    public interface IHQRedisDB
    {

        /// <summary>
        /// 获取Object命令
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public IHQRedisObjectComm GetObjectComm (string Key);

        /// <summary>
        /// 获取List命令
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public IHQRedisListComm GetListComm (string Key);

        /// <summary>
        /// 获取SortSet命令
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public IHQRedisSortSet GetSortSetComm (string Key);

        /// <summary>
        /// 对Key操作的命令
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public IHQRedisKeyComm GetKeyComm();

        /// <summary>
        /// 获取Hask操作
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public IHQRedisHash GetHashComm (string Key);


    }
    
}
