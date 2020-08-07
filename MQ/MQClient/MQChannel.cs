using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQ.MQClient
{
    public class MQChannel
    {
        MQConnection Conn;

        IModel Channel;
        public MQChannel(MQConnection Conn)
        {
            this.Conn = Conn;
            Channel = Conn.Conn.CreateModel();

            //声明为手动确认
            Channel.BasicQos(0, 3, false);

        }
        /// <summary>
        /// 创建交换机
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Type"></param>
        public void CreateExchange(string Name, ExchangeType Type)
        {
            Channel.ExchangeDeclare(Name, Type.ToString(), true, false);
        }

        /// <summary>
        /// 创建队列
        /// </summary>
        /// <param name="Name"></param>
        public void CreateQueue(string Name)
        {
            Channel.QueueDeclare(Name, true, false, false);
        }

        /// <summary>
        /// 绑定队列和交换机
        /// </summary>
        /// <param name="Name"></param>
        public void Binding(string ExchangeName, string QueueName, string BindingKey)
        {
            Channel.QueueBind(QueueName, ExchangeName, BindingKey);
        }

        /// <summary>
        /// 推送参数是否初始化
        /// </summary>
        bool IsInitPush = false;

        /// <summary>
        /// 初始化推送参数
        /// </summary>
        void InitPush()
        {
            Channel.ConfirmSelect();
            
            /*-------------Return机制：不可达的消息消息监听--------------*/

            //这个事件就是用来监听我们一些不可达的消息的内容的：比如某些情况下，如果我们在发送消息时，当前的exchange不存在或者指定的routingkey路由不到，这个时候如果要监听这种不可达的消息，就要使用 return
            EventHandler<BasicReturnEventArgs> evreturn = new EventHandler<BasicReturnEventArgs>((o, basic) =>
            {
                var rc = basic.ReplyCode; //消息失败的code
                var rt = basic.ReplyText; //描述返回原因的文本。
                var msg = Encoding.UTF8.GetString(basic.Body.ToArray()); //失败消息的内容
                                                                         //在这里我们可能要对这条不可达消息做处理，比如是否重发这条不可达的消息呀，或者这条消息发送到其他的路由中呀，等等
                                                                         //System.IO.File.AppendAllText("d:/return.txt", "调用了Return;ReplyCode:" + rc + ";ReplyText:" + rt + ";Body:" + msg);
                Console.WriteLine($"异常事件 BasicReturn: ReplyCode:{rc};ReplyText:{rt};Body:{msg},all:{Newtonsoft.Json.JsonConvert.SerializeObject(basic)}");
                //Console.WriteLine($"-------- BasicReturn: ReplyCode:{rc};ReplyText:{rt};Body:{msg}");
            });
            Channel.BasicReturn += evreturn;


            /*-------------Confirm机制：等待确认所有已发布的消息有两种方式----------------*/
            //--------方式二：异步

            //消息发送成功的时候进入到这个事件：即RabbitMq服务器告诉生产者，我已经成功收到了消息
            EventHandler<BasicAckEventArgs> BasicAcks = new EventHandler<BasicAckEventArgs>((o, basic) =>
            {
                Console.WriteLine($"异常事件 BasicAcks: DeliveryTag:{basic.DeliveryTag.ToString()};Multiple:{basic.Multiple.ToString()}时间:{DateTime.Now.ToString()},all:{Newtonsoft.Json.JsonConvert.SerializeObject(basic)}");
                //System.IO.File.AppendAllText("d:/ack.txt", "\r\n调用了ack;DeliveryTag:" + basic.DeliveryTag.ToString() + ";Multiple:" + basic.Multiple.ToString() + "时间:" + DateTime.Now.ToString());
            });
            //消息发送失败的时候进入到这个事件：即RabbitMq服务器告诉生产者，你发送的这条消息我没有成功的投递到Queue中，或者说我没有收到这条消息。
            EventHandler<BasicNackEventArgs> BasicNacks = new EventHandler<BasicNackEventArgs>((o, basic) =>
            {
                Console.WriteLine($"异常事件 BasicNacks: DeliveryTag:{basic.DeliveryTag.ToString()};Multiple:{basic.Multiple.ToString()}时间:{DateTime.Now.ToString()},all:{Newtonsoft.Json.JsonConvert.SerializeObject(basic)}");
                //MQ服务器出现了异常，可能会出现Nack的情况
                //System.IO.File.AppendAllText("d:/nack.txt", "\r\n调用了Nacks;DeliveryTag:" + basic.DeliveryTag.ToString() + ";Multiple:" + basic.Multiple.ToString() + "时间:" + DateTime.Now.ToString());
            });
            Channel.BasicAcks += BasicAcks;

            Channel.BasicNacks += BasicNacks;

            IsInitPush = true;
            //-------------------------------- 
        }

        /// <summary>
        /// 推送消息
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="Msg"></param>
        /// <param name="ExchangeName"></param>
        /// <param name="Routingkey">项目集.项目.功能</param>
        public void PushMsg<T1>(T1 Msg, string ExchangeName, string Routingkey)
        {
            if (!IsInitPush)
            {
                InitPush();
            }

            IBasicProperties props = Channel.CreateBasicProperties();
            props.DeliveryMode = 2; //1:非持久化 2:持续久化 （即：当值为2的时候，我们一个消息发送到服务器上之后，如果消息还没有被消费者消费，服务器重启了之后，这条消息依然存在）
            props.Persistent = true;
            props.ContentEncoding = "UTF-8"; //注意要大写
            //if (msgTimeOut != null) { props.Expiration = msgTimeOut; }; //消息过期时间:单位毫秒

            props.MessageId = Guid.NewGuid().ToString("N"); //设定这条消息的MessageId(每条消息的MessageId都是唯一的)
            string message = Newtonsoft.Json.JsonConvert.SerializeObject(Msg);
            var msgBody = Encoding.UTF8.GetBytes(message); //发送的消息必须是二进制的

            //记住：如果需要EventHandler<BasicReturnEventArgs>事件监听不可达消息的时候，一定要将mandatory设为true
            Channel.BasicPublish(exchange: ExchangeName, routingKey: Routingkey, mandatory: true, basicProperties: props, body: msgBody);

        }


        /// <summary>
        /// 推送参数是否初始化
        /// </summary>
        bool IsInitReceived = false;

        /// <summary>
        /// 初始化推送参数
        /// </summary>
        void InitReceived()
        {
        }

        /// <summary>
        /// 接受消息
        /// </summary>
        /// <param name="QueueName"></param>
        public void ReceivedMsg(string QueueName)
        {
            //if (!IsInitReceived)
            //{
            //    InitReceived();
            //}

            EventingBasicConsumer consumer = new EventingBasicConsumer(Channel); //创建一个消费者                    
            consumer.Received += (o, basic) =>//EventHandler<BasicDeliverEventArgs>类型事件
            {
                try
                {
                    //int aa = 1; int bb = 0; int cc = aa / bb; //模拟异常，这条消息消费失败
                    var msgBody = basic.Body; //获取消息内容
                    var a = basic.ConsumerTag;
                    var c = basic.DeliveryTag;
                    var d = basic.Redelivered;
                    var f = basic.RoutingKey;
                    var e = basic.BasicProperties.Headers;

                    Console.WriteLine(string.Format("接收时间:{0}，消息内容：{1}:全文:{2}", DateTime.Now.ToString("HH:mm:ss"), Encoding.UTF8.GetString(msgBody.ToArray())
                        , Newtonsoft.Json.JsonConvert.SerializeObject(basic)));
                    
                    //手动ACK确认分两种:BasicAck:肯定确认 和 BasicNack:否定确认
                    Channel.BasicAck(deliveryTag: basic.DeliveryTag, multiple: false);//这种情况是消费者告诉RabbitMQ服务器，我已经确认收到了消息
                }
                catch (Exception)
                {
                    //requeue:被拒绝的是否重新入队列；true：重新进入队列 fasle：抛弃此条消息
                    //multiple：是否批量.true:将一次性拒绝所有小于deliveryTag的消息
                    Channel.BasicNack(deliveryTag: basic.DeliveryTag, multiple: false, requeue: false);//这种情况是消费者告诉RabbitMQ服务器,因为某种原因我无法立即处理这条消息，这条消息重新回到队列，或者丢弃吧.requeue: false表示丢弃这条消息，为true表示重回队列
                }
            };

            Channel.BasicConsume(QueueName, autoAck: false, consumer: consumer);//第二个参数autoAck设为true为自动应答，false为手动ack ;这里一定要将autoAck设置false,告诉MQ服务器，发送消息之后，消息暂时不要删除，等消费者处理完成再说

        }


    }



}
