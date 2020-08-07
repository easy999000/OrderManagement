using System;
using System.Collections.Generic;
using System.Text;

namespace MQ.MQClient
{
    public enum ExchangeType
    {
        /// <summary>
        /// 群发
        /// </summary>
        fanout,
        /// <summary>
        /// 全匹配
        /// </summary>
        direct,
        /// <summary>
        /// 模糊匹配
        /// </summary>
        topic,
    }
}
