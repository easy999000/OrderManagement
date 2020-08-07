using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MQ.MQClient
{
    public class MQConnection
    {
        private ConnectionFactory connfactory { get; set; } //创建一个工厂连接对象 

        public IConnection Conn;
        public MQConnection(string Host, string Account, string Pass, int Port, string VirtualHost)
        {
            connfactory = new ConnectionFactory();
            connfactory.HostName = Host;
            connfactory.UserName = Account;
            connfactory.Password = Pass;
            connfactory.Port = Port;
            connfactory.VirtualHost = VirtualHost;
            connfactory.AutomaticRecoveryEnabled = true;//网络故障自动连接恢复

            Conn = connfactory.CreateConnection();

        }

        public void Close()
        {
            if (Conn == null)
            {
                return;
            }

            Conn.Close(new TimeSpan(0, 0, 3));

        }

        public MQChannel CreateModel()
        {
            MQChannel Channel = new MQChannel(this);
             

            return Channel;

        }



    }
}
