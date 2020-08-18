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
        ReceivedPoolManager QueueManager = null;

        public PushQueueManager PushQueueClient;

        public MainService()
        {

        }


        /// <summary>
        /// 启动服务
        /// </summary>
        public void Start()
        {
            var con = ConfigManager.LoadConfig();
            Config = con;


            QueueManager = new ReceivedPoolManager();
            QueueManager.LoadConfig(Config);

            PushQueueClient = new PushQueueManager(Config.ServerConfig);

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
