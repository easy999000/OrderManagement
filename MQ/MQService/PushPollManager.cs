using MQServer.Model;
using MQServer.MQConfig;
using MQServer.RabbitClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQServer.MQService
{
    /// <summary>
    /// 推送线程池管理器
    /// </summary>
    public class PushPollManager
    {
        MQServerConfig Server;

        ChannelPoolManager ChannelPool = null;


        public PushPollManager(MQServerConfig _Server)
        {
            Server = _Server;
            ChannelPool = new ChannelPoolManager(Server);
            for (int i = 0; i < 20; i++)
            {
                ChannelPool.NewChannel();
            }
        }


        /// <summary>
        /// 推送数据
        /// Routingkey 的格式 交换机名字:routingKey
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="Routingkey">Routingkey 的格式 交换机名字:routingKey</param>
        public void PushData(MQWebApiMsg Data, string Key)
        {
            string[] keys = Key.Split(':');
            if (keys.Length != 2)
            {
                throw new Exception("MQ Routingkey 格式不正确,"); ;
            }
            string ExchangeName = keys[0];

            string Routingkey = keys[1];

            ChannelPool.AutoChannel(Channel =>
            {
                Channel.PushMsg(Data, ExchangeName, Routingkey);
            });


        }
    }
}
