using System;
using System.Collections.Generic;
using System.Text;

namespace MQServer.Model
{
    /// <summary>
    /// WebApi消息模型
    /// </summary>
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

        /// <summary>
        /// 记录当前消息异常次数,发送消息的时候,不用手动设置这个参数.
        /// </summary>
        public int ExceptionTimes { get; set; } = 0;
    }
}
