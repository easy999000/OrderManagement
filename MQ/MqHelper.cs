using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQ
{

    /// <summary>
    ///  RabbitMQ消息队列处理
    /// </summary>
    public class MqHelper
    {
        /// <summary>
        /// RabbitMQ地址
        /// </summary>
        private string HostName = "192.168.18.115";   //ConfigurationManager.AppSettings["RabbitMQHostName"];

        /// <summary>
        /// 账号
        /// </summary>
        private string UserName = "admin";    //ConfigurationManager.AppSettings["RabbitMQUserName"];

        /// <summary>
        /// 密码
        /// </summary>
        private string Password = "admin";     // ConfigurationManager.AppSettings["RabbitMQPassword"];


        /// <summary>
        /// 端口号
        /// </summary>
        private int Prot = 5672;
        /// 连接配置
        private ConnectionFactory connfactory { get; set; } //创建一个工厂连接对象 
        IConnection conn;
        IModel channel;
        public MqHelper()
        {
            if (connfactory == null)
            {
                connfactory = new ConnectionFactory();
                connfactory.HostName = HostName;
                connfactory.UserName = UserName;
                connfactory.Password = Password;
                connfactory.Port = Prot;
                connfactory.AutomaticRecoveryEnabled = true;//网络故障自动连接恢复
            }
        }
        public MqHelper(string vhost) : this()
        {
            connfactory.VirtualHost = vhost;
            conn = connfactory.CreateConnection();
            channel = conn.CreateModel();
        }
        public MqHelper(string hostName, string userName, string password, int port, string vhost = "/", bool automaticRecoveryEnabled = true) : this()
        {
            connfactory.VirtualHost = vhost;
            connfactory.AutomaticRecoveryEnabled = automaticRecoveryEnabled;
            conn = connfactory.CreateConnection();
            channel = conn.CreateModel();
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="exchangeName">交换机名称</param>
        /// <param name="queueName">队列名称</param>
        /// <param name="routingkey">路由名称</param>
        public void SenMsg<TEntity>(string exchangeName, TEntity msgEntity, string routingkey = null, string queueName = null, string exchangeType = ExchangeType.Direct, byte deliveryMode = 2, string msgTimeOut = null)
        {

            channel.ConfirmSelect();//开启消息确认应答模式 ；当Channel设置成confirm模式时，发布的每一条消息都会获得一个唯一的deliveryTag  ；deliveryTag在basicPublish执行的时候加1           
            try
            {
                channel.ExchangeDeclare(exchangeName, exchangeType, true, false);
            }
            catch (Exception ex)
            {

                return;//如果交换机创建不成功,可能MQ服务器中已经存在不同类型的同名交换机了，也有可能是Fanout模式下消费端未先启动，请先启动消费端
            }

            if (exchangeType == ExchangeType.Direct)
            {
                if (queueName == null || routingkey == null)
                {
                    return; //Direct模式下请先声明QueueName,和routingkey
                }
                Dictionary<string, object> dic = new Dictionary<string, object>() { { "x-max-length", 50000 } }; //设定这个队列的最大容量为50000条消息
                channel.QueueDeclare(queueName, true, false, false, arguments: dic);
                channel.QueueBind(queueName, exchangeName, routingkey, arguments: dic);
            }
            if (exchangeType == ExchangeType.Topic)
            {
                if (routingkey == null) return; //Topic模式下请先申明routingkey路由
            }


            /*-------------Return机制：不可达的消息消息监听--------------*/

            //这个事件就是用来监听我们一些不可达的消息的内容的：比如某些情况下，如果我们在发送消息时，当前的exchange不存在或者指定的routingkey路由不到，这个时候如果要监听这种不可达的消息，就要使用 return
            EventHandler<BasicReturnEventArgs> evreturn = new EventHandler<BasicReturnEventArgs>((o, basic) =>
            {
                var rc = basic.ReplyCode; //消息失败的code
                        var rt = basic.ReplyText; //描述返回原因的文本。
                        var msg = Encoding.UTF8.GetString(basic.Body.ToArray()); //失败消息的内容
                                                                                 //在这里我们可能要对这条不可达消息做处理，比如是否重发这条不可达的消息呀，或者这条消息发送到其他的路由中呀，等等
                        System.IO.File.AppendAllText("d:/return.txt", "调用了Return;ReplyCode:" + rc + ";ReplyText:" + rt + ";Body:" + msg);
            });
            channel.BasicReturn += evreturn;


            /*-------------Confirm机制：等待确认所有已发布的消息有两种方式----------------*/
            //--------方式二：异步

            //消息发送成功的时候进入到这个事件：即RabbitMq服务器告诉生产者，我已经成功收到了消息
            EventHandler<BasicAckEventArgs> BasicAcks = new EventHandler<BasicAckEventArgs>((o, basic) =>
            {
                System.IO.File.AppendAllText("d:/ack.txt", "\r\n调用了ack;DeliveryTag:" + basic.DeliveryTag.ToString() + ";Multiple:" + basic.Multiple.ToString() + "时间:" + DateTime.Now.ToString());
            });
            //消息发送失败的时候进入到这个事件：即RabbitMq服务器告诉生产者，你发送的这条消息我没有成功的投递到Queue中，或者说我没有收到这条消息。
            EventHandler<BasicNackEventArgs> BasicNacks = new EventHandler<BasicNackEventArgs>((o, basic) =>
            {
                        //MQ服务器出现了异常，可能会出现Nack的情况
                        System.IO.File.AppendAllText("d:/nack.txt", "\r\n调用了Nacks;DeliveryTag:" + basic.DeliveryTag.ToString() + ";Multiple:" + basic.Multiple.ToString() + "时间:" + DateTime.Now.ToString());
            });
            channel.BasicAcks += BasicAcks;
            channel.BasicNacks += BasicNacks;

            //--------------------------------


            IBasicProperties props = channel.CreateBasicProperties();
            props.DeliveryMode = deliveryMode; //1:非持久化 2:持续久化 （即：当值为2的时候，我们一个消息发送到服务器上之后，如果消息还没有被消费者消费，服务器重启了之后，这条消息依然存在）
            props.Persistent = true;
            props.ContentEncoding = "UTF-8"; //注意要大写
            if (msgTimeOut != null) { props.Expiration = msgTimeOut; }; //消息过期时间:单位毫秒

            props.MessageId = Guid.NewGuid().ToString("N"); //设定这条消息的MessageId(每条消息的MessageId都是唯一的)
            string message = Newtonsoft.Json.JsonConvert.SerializeObject(msgEntity);
            var msgBody = Encoding.UTF8.GetBytes(message); //发送的消息必须是二进制的

            //记住：如果需要EventHandler<BasicReturnEventArgs>事件监听不可达消息的时候，一定要将mandatory设为true
            channel.BasicPublish(exchange: exchangeName, routingKey: routingkey, mandatory: true, basicProperties: props, body: msgBody);

            /*-------------Confirm机制：等待确认所有已发布的消息有两种方式----------------*/

            //--------方式一：同步

            //等待确认所有已发布的消息。 //参考资料：https://www.cnblogs.com/refuge/p/10356750.html
            //channel.WaitForConfirmsOrDie();//WaitForConfirmsOrDie表示等待已经发送给broker的消息act或者nack之后才会继续执行；即：直到所有信息都发布，如果有任何一个消息触发了Nack则抛出IOException异常

            //bool isSendMsgOk = channel.WaitForConfirms(); //WaitForConfirms表示等待已经发送给MQ服务器的消息act或者nack之后才会继续执行。
            //if (isSendMsgOk)
            //{
            //    //消息确认已经发送到MQ服务器
            //}
            //else
            //{
            //    // 进行消息重发
            //    channel.BasicPublish(exchange: exchangeName, routingKey: routingkey, basicProperties: props, body: msgBody);
            //}

            //方式一的缺点：

            //--------------------------------

        }

        public void send2<TEntity>(string exchangeName, TEntity msgEntity, string routingkey = null, string queueName = null, string exchangeType = ExchangeType.Direct, byte deliveryMode = 2, string msgTimeOut = null)
        { 
        }




    }
}
