using System;
using System.Collections.Generic;
using System.Text;

namespace RedisDao.IRedis
{
    public interface IHQRedisService
    {
        /// <summary>
        /// 主机ip地址或域名
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int Prot { get; set; }
        /// <summary>
        /// 是否ssl
        /// </summary>
        public bool SSL { get; set; }
        /// <summary>
        /// Key前缀
        /// </summary>
        public string Prefix { get; set; }

        

    }
}
