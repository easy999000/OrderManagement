using MQServer.MQConfig;
using MQServer.MQService;
using MQServer.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MQServer
{
    public class MainService : IDisposable
    {
        /// <summary>
        /// 配置管理器
        /// </summary>
        MQConfig.ConfigManager Config = null;

        /// <summary>
        /// 消费队列处理程序
        /// </summary>
        ReceivedPoolManager ReceivedQueueManager = null;

        /// <summary>
        /// 
        /// </summary>
        public PushPollManager PushQueueClient;

        public MainService()
        {
            var con = ConfigManager.LoadConfig();
            Config = con;

        }
        #region 接口

        /// <summary>
        /// 更新队列配置
        /// </summary>
        /// <param name="QueueConfig"></param>
        public void AddOrUpdateQueue(MQQueueConfig QueueConfig)
        {
            Config.AddOrUpdateQueue(QueueConfig);
            ConfigManager.SaveConfig(Config);
            if (ReceivedQueueManager!=null)
            {
                ReceivedQueueManager.LoadConfig(Config);
            }

        }


        /// <summary>
        /// 移除队列
        /// </summary>
        /// <param name="QueueName"></param>
        public void RemoveQueue(string QueueName)
        {
            Config.RemoveQueue(QueueName);
            ConfigManager.SaveConfig(Config);
            if (ReceivedQueueManager != null)
            {
                ReceivedQueueManager.LoadConfig(Config);
            }
        }


        /// <summary>
        /// 更新服务配置
        /// </summary>
        /// <param name="_ServerConfig"></param>
        public void UpdateServer(MQServerConfig _ServerConfig)
        {
            Config.UpdateServer(_ServerConfig);
            ConfigManager.SaveConfig(Config);
            if (ReceivedQueueManager != null)
            {
                ReceivedQueueManager.LoadConfig(Config);
            }
        }

        public ConfigManager GetConfig()
        {
            return Config;
        }



        #endregion


        /// <summary>
        /// 启动服务
        /// </summary>
        public void Start()
        {

            ReceivedQueueManager = new ReceivedPoolManager();
            ReceivedQueueManager.LoadConfig(Config);

            PushQueueClient = new PushPollManager(Config.ServerConfig);

            Log.WriteLine(@"
=================================================================
=================================================================
=================================================================

            MQ服务启动成功

=================================================================
=================================================================
=================================================================
");

        }

        public void Dispose()
        {
            Log.WriteLine(@"
=================================================================
=================================================================
=================================================================

            MQ服务  关闭了

=================================================================
=================================================================
=================================================================
");
        }
    }
}
