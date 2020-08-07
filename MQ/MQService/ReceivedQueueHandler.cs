using MQ.Model;
using MQ.MQConfig;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQ.MQService
{
    /// <summary>
    /// 消费队列处理程序
    /// </summary>
    public class ReceivedQueueHandler
    {
        MQQueueConfig Config;
        MQServerConfig Server;

        public ReceivedQueueHandler(MQConfig.MQQueueConfig _Config, MQServerConfig _Server)
        {
            Config = _Config;
            Server = _Server;
            Init();
        }

        private void Init()
        {
            MQ.MQClient.MQConnection conn = new MQ.MQClient.MQConnection(Server.Host, Server.Account, Server.Pass, Server.Port, Server.VirtualHost);

            var Channel = conn.CreateModel();

            //string ExchangeName = Config.ExchangeName;

            Channel.CreateExchange(Config.ExchangeName, MQ.MQClient.ExchangeType.topic);

            //string QueueName = Config.QueueName;

            Channel.CreateQueue(Config.QueueName);

            foreach (var item in Config.BindingKeys)
            {
                Channel.Binding(Config.ExchangeName, Config.QueueName, item);
            }


            Channel.ReceivedDataEvent += ReceivedData;

            Channel.ReceivedMsg(Config.QueueName);

        }

        void ReceivedData(MQWebApiMsg msg)
        {
            System.Threading.Thread.Sleep(2000);
#if DEBUG
            Console.WriteLine($"ReceivedQueueHandler.ReceivedData {Newtonsoft.Json.JsonConvert.SerializeObject(msg)}");
#endif
        }


    }
}
