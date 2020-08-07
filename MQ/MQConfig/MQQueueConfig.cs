using System;
using System.Collections.Generic;
using System.Text;

namespace MQ.MQConfig
{
    public class MQQueueConfig
    {
        /// <summary>
        /// 队列名字
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// 交换机名字
        /// </summary>
        public string ExchangeName { get; set; }

        /// <summary>
        /// 绑定规则
        /// </summary>
        public string[] BindingKeys { get; set; }

        int _ThreadCount = 10;
        /// <summary>
        /// 这个队列的处理线程数量
        /// </summary>
        public int ThreadCount
        {
            get { return _ThreadCount; }
            set
            {
                int count = value;
                if (count < 1)
                {
                    count = 10;
                }
                _ThreadCount = count;
            }
        }


    }
}
