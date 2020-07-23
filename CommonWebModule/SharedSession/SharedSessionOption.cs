using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonWebModule.SharedSession
{
    public class SharedSessionOption
    {
        /// <summary>
        /// redis连接串
        /// </summary>
        public string Configuration { get; set; }
     
        ///// <summary>
        ///// 连接配置
        ///// </summary>
        //public ConfigurationOptions ConfigurationOptions { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// 实例名字
        /// </summary>
        public string InstanceName { get; set; }

        /// <summary>
        /// session的作用域名
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// 默认数据库
        /// </summary>
        public int? DefaultDatabase = 0;
    }
}
