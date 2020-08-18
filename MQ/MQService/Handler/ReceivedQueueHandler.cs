using MQServer.Model;
using MQServer.RabbitClient;
using MQServer.MQConfig;
using System;
using System.Collections.Generic;
using System.Text;
using MQServer.MQService;

namespace MQServer.Handler.MQService
{
    /// <summary>
    /// 消费队列处理程序
    /// </summary>
    public class ReceivedQueueHandler : IDisposable
    {
        /// <summary>
        /// 队列配置
        /// </summary>
        MQQueueConfig Config;
        /// <summary>
        /// 服务器配置
        /// </summary>
        MQServerConfig Server;

        /// <summary>
        /// Post处理程序
        /// </summary>
        PostWebApiHandler PostHandler = new PostWebApiHandler();


        /// <summary>
        /// 信道管理器
        /// </summary>
        ChannelPoolManager ChannelManager;



        public ReceivedQueueHandler(MQConfig.MQQueueConfig _Config, MQServerConfig _Server)
        {
            Config = _Config;
            Server = _Server;
            Init();
        }


        public bool IsDispose { get; set; } = false;
        public void Dispose()
        {
            IsDispose = true;
            ChannelManager.Dispose();
        }

        private void Init()
        {
            ChannelManager = new ChannelPoolManager(Server);

            ChannelManager.NewChannel();

            var Channel = ChannelManager.DequeueChannel();


            Channel.CreateExchange(Config.ExchangeName, MQServer.RabbitClient.ExchangeType.topic);


            Channel.CreateQueue(Config.QueueName);

            foreach (var item in Config.BindingKeys)
            {
                Channel.Binding(Config.ExchangeName, Config.QueueName, item);
            }

            Channel.ReceivedDataEvent += PostHandler.Post;

            Channel.ReceivedMsg(Config.QueueName);


            for (int i = 1; i < Config.ThreadCount; i++)
            {
                ChannelManager.NewChannel();

                var Channel2 = ChannelManager.DequeueChannel();

                Channel2.ReceivedDataEvent += PostHandler.Post;

                Channel2.ReceivedMsg(Config.QueueName);

            }

        }


    }
}
