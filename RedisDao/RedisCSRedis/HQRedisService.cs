using RedisDao.IRedis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisDao.RedisCSRedis
{
    /// <summary>
    /// Redis 服务
    /// </summary>
    public class HQRedisService : IHQRedisService
    { 
        public string  Host { get; set; }
        public string  Password { get; set; }
        public int Prot { get; set; } = 6379;
        public bool SSL { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Host"></param>
        /// <param name="Password"></param>
        /// <param name="Prot"></param>
        /// <param name="SSL"></param>
        public HQRedisService(string Host, string Password, int Prot, bool SSL)
        {
            this.Host = Host;
            this.Password = Password;
            this.Prot = Prot;
            this.SSL = SSL;
            if (Prot==0)
            {
                this.Prot = 6379;
            }
        }




    }
}
