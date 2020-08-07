using MQ.MQConfig;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MQ.MQService
{
    /// <summary>
    /// 主要服务
    /// </summary>
    public class MainService
    {
        /// <summary>
        /// 配置管理器
        /// </summary>
        MQConfig.ConfigManager Config = null;
        /// <summary>
        /// 消费队列处理程序
        /// </summary>
        ReceivedQueueManager QueueManager = null;
        public void Start()
        {
            var con = ConfigManager.LoadConfig();
            Config = con;
              

            QueueManager = new ReceivedQueueManager();
            QueueManager.LoadConfig(Config);


        }
    }
}
