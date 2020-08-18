using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MQServer.RabbitClient
{
    public class MQConnection : IDisposable
    {
        private ConnectionFactory connfactory { get; set; } //创建一个工厂连接对象 

        public IConnection RabbitConn;

        /// <summary>
        /// 信道集合
        /// </summary>
        public List<MQChannel> ChannelList = new List<MQChannel>();

        /// <summary>
        /// 信道数量 
        /// </summary>
        public int ChannelCount
        {
            get
            {
                return ChannelList.Count;
            }
        }

        public MQConnection(string Host, string Account, string Pass, int Port, string VirtualHost)
        {
            connfactory = new ConnectionFactory();
            connfactory.HostName = Host;
            connfactory.UserName = Account;
            connfactory.Password = Pass;
            connfactory.Port = Port;
            connfactory.VirtualHost = VirtualHost;
            connfactory.AutomaticRecoveryEnabled = true;//网络故障自动连接恢复
            connfactory.ContinuationTimeout = new TimeSpan(0, 3, 0);
            connfactory.RequestedConnectionTimeout = new TimeSpan(0, 3, 0);
            connfactory.SocketReadTimeout = new TimeSpan(0, 3, 0);
            connfactory.SocketWriteTimeout = new TimeSpan(0, 3, 0);



            try
            {

                RabbitConn = connfactory.CreateConnection();


            }
            catch (Exception ex)
            {
                string exStr = Newtonsoft.Json.JsonConvert.SerializeObject(ex);
                throw;
            }

        }

        public void Close()
        {
            if (RabbitConn == null)
            {
                return;
            }

            RabbitConn.Close(new TimeSpan(0, 0, 3));

        }

        /// <summary>
        /// 创建一个信道
        /// </summary>
        /// <returns></returns>
        public MQChannel CreateChannel()
        {
            MQChannel Channel = new MQChannel(this);

            ChannelList.Add(Channel);

            return Channel;
        }

        /// <summary>
        /// 移除一个信道
        /// </summary>
        /// <param name="Channel"></param>
        /// <returns></returns>
        public bool RemoveChannel(MQChannel Channel)
        {
            return ChannelList.Remove(Channel);

        }

        public void Dispose()
        {
            foreach (var item in ChannelList)
            {
                item.Dispose();
            }

            if (RabbitConn != null)
            {
                RabbitConn.Dispose();
            }
        }
    }
}
