﻿using MQ.MQConfig;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQ.MQService
{
    /// <summary>
    /// 消费队列管理器
    /// </summary>
    public class ReceivedQueueManager
    {
        /// <summary>
        /// 队列处理程序字典
        /// </summary>
        Dictionary<string, ReceivedQueueHandler> DicQueue = new Dictionary<string, ReceivedQueueHandler>();

        public void LoadConfig(ConfigManager Config)
        {
            if (Config == null)
            {
                return;
            }
            foreach (var item in Config.Data)
            {
                AddQueue(item, Config.ServerConfig);
            }
        }
        /// <summary>
        /// 添加处理程序
        /// </summary>
        /// <param name="Config"></param>
        public void AddQueue(MQConfig.MQQueueConfig Config, MQServerConfig Server)
        {
            if (Config == null)
            {
                return;
            }
            if (!DicQueue.ContainsKey(Config.QueueName))
            {
                ReceivedQueueHandler handler = new ReceivedQueueHandler(Config, Server);
                DicQueue.Add(Config.QueueName, handler);

            }
        }
        /// <summary>
        /// 移除处理程序
        /// </summary>
        /// <param name="QueueName"></param>
        public void RemoveQueue(string QueueName)
        {
            DicQueue.Remove(QueueName);
        }

    }
}
