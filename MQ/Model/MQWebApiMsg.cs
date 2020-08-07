using System;
using System.Collections.Generic;
using System.Text;

namespace MQ.Model
{
    public class MQWebApiMsg
    {
        /// <summary>
        /// 主机
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 接口路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
    }
}
