using MQServer.Model;
using MQServer.Tools;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MQServer.RabbitClient
{
    public class MQChannel : IDisposable
    {
        public MQConnection Conn;

        IModel RabbitChannel;
        internal MQChannel(MQConnection Conn)
        {
            this.Conn = Conn;
            RabbitChannel = Conn.RabbitConn.CreateModel();

        }


        /// <summary>
        /// 创建交换机
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Type"></param>
        public void CreateExchange(string Name, ExchangeType Type)
        {
            RabbitChannel.ExchangeDeclare(Name, Type.ToString(), true, false);
        }

        /// <summary>
        /// 创建队列
        /// </summary>
        /// <param name="Name"></param>
        public void CreateQueue(string Name)
        {
            RabbitChannel.QueueDeclare(Name, true, false, false);
        }
        /// <summary>
        /// 设置信道排队数量
        /// </summary>
        /// <param name="Count"></param>
        public void BasicQos(ushort Count)
        {
            RabbitChannel.BasicQos(0, Count, false);
        }

        /// <summary>
        /// 绑定队列和交换机
        /// </summary>
        /// <param name="Name"></param>
        public void Binding(string ExchangeName, string QueueName, string BindingKey)
        {
            RabbitChannel.QueueBind(QueueName, ExchangeName, BindingKey);
        }

        /// <summary>
        /// 数据接收时间,用来处理数据消息
        /// </summary>
        public Func<MQWebApiMsg, bool> ReceivedDataEvent;

        /// <summary>
        /// 推送失败事件
        /// </summary>
        public Action<MQWebApiMsg> PushErrorEvent;

        /// <summary>
        /// 接收处理消息,失败事件
        /// </summary>
        public Action<MQWebApiMsg> ReceivedErrorEvent;

        /// <summary>
        /// 推送参数是否初始化
        /// </summary>
        bool IsInitPush = false;

        /// <summary>
        /// 这个事件就是用来监听我们一些不可达的消息的内容的：比如某些情况下，如果我们在发送消息时，当前的exchange不存在或者指定的routingkey路由不到，这个时候如果要监听这种不可达的消息，就要使用 return
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BasicReturnEventHandler(object? o, BasicReturnEventArgs basic)
        {


            var rc = basic.ReplyCode; //消息失败的code
            var rt = basic.ReplyText; //描述返回原因的文本。
            var msg = Encoding.UTF8.GetString(basic.Body.ToArray()); //失败消息的内容
                                                                     //在这里我们可能要对这条不可达消息做处理，比如是否重发这条不可达的消息呀，或者这条消息发送到其他的路由中呀，等等
                                                                     //System.IO.File.AppendAllText("d:/return.txt", "调用了Return;ReplyCode:" + rc + ";ReplyText:" + rt + ";Body:" + msg);

            MQWebApiMsg Data = Newtonsoft.Json.JsonConvert.DeserializeObject<MQWebApiMsg>(msg);
            PushErrorEvent?.Invoke(Data);

            Log.WriteLine($"异常事件 BasicReturn: ReplyCode:{rc};ReplyText:{rt};Body:{msg},all:{Newtonsoft.Json.JsonConvert.SerializeObject(basic)}");
            //Console.WriteLine($"-------- BasicReturn: ReplyCode:{rc};ReplyText:{rt};Body:{msg}");

        }
        /// <summary>
        /// 消息发送成功的时候进入到这个事件：即RabbitMq服务器告诉生产者，我已经成功收到了消息
        /// </summary>
        /// <param name="o"></param>
        /// <param name="basic"></param>
        void BasicAcksEventHandler(object? o, BasicAckEventArgs basic)
        {


        }
        /// <summary>
        /// MQ服务器出现了异常，可能会出现Nack的情况
        /// </summary>
        /// <param name="o"></param>
        /// <param name="basic"></param>
        void BasicNacksEventHandler(object? o, BasicNackEventArgs basic)
        {
            PushErrorEvent?.Invoke(null);

            Log.WriteLine($"异常事件 BasicNacks: DeliveryTag:{basic.DeliveryTag.ToString()};Multiple:{basic.Multiple.ToString()}时间:{DateTime.Now.ToString()},all:{Newtonsoft.Json.JsonConvert.SerializeObject(basic)}");

        }


        /// <summary>
        /// 初始化推送参数
        /// </summary>
        void InitPush()
        {
            RabbitChannel.ConfirmSelect();

            ///*-------------Return机制：不可达的消息消息监听--------------*/

            RabbitChannel.BasicReturn += BasicReturnEventHandler;

            RabbitChannel.BasicAcks += BasicAcksEventHandler;

            RabbitChannel.BasicNacks += BasicNacksEventHandler;

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

            IBasicProperties props = RabbitChannel.CreateBasicProperties();
            props.DeliveryMode = 2; //1:非持久化 2:持续久化 （即：当值为2的时候，我们一个消息发送到服务器上之后，如果消息还没有被消费者消费，服务器重启了之后，这条消息依然存在）
            props.Persistent = true;
            props.ContentEncoding = "UTF-8"; //注意要大写
                                             //if (msgTimeOut != null) { props.Expiration = msgTimeOut; }; //消息过期时间:单位毫秒

            props.MessageId = Guid.NewGuid().ToString("N"); //设定这条消息的MessageId(每条消息的MessageId都是唯一的)
            string message = Newtonsoft.Json.JsonConvert.SerializeObject(Msg);
            var msgBody = Encoding.UTF8.GetBytes(message); //发送的消息必须是二进制的

            //记住：如果需要EventHandler<BasicReturnEventArgs>事件监听不可达消息的时候，一定要将mandatory设为true
            RabbitChannel.BasicPublish(exchange: ExchangeName, routingKey: Routingkey, mandatory: true, basicProperties: props, body: msgBody);

        }

        /// <summary>
        /// 启动消息接收
        /// </summary>
        /// <param name="QueueName"></param>
        public void ReceivedMsg(string QueueName)
        {
            BasicQos(3);

            EventingBasicConsumer consumer = new EventingBasicConsumer(RabbitChannel); //创建一个消费者 

            consumer.Received += ReceivedEventFun;

            RabbitChannel.BasicConsume(QueueName, autoAck: false, consumer: consumer);//第二个参数autoAck设为true为自动应答，false为手动ack ;这里一定要将autoAck设置false,告诉MQ服务器，发送消息之后，消息暂时不要删除，等消费者处理完成再说

        }
        /// <summary>
        /// 再次推送失败消息
        /// </summary>
        /// <param name="basic"></param>
        void PushExceptionMsgAgain(BasicDeliverEventArgs basic, MQWebApiMsg Data)
        {
            ///先作废本次消息
            RabbitChannel.BasicReject(deliveryTag: basic.DeliveryTag, false);

            if (Data == null)
            {
                ReceivedErrorEvent?.Invoke(null);
                return;
            }

            if (Data.ExceptionTimes > 2)
            {
                ReceivedErrorEvent?.Invoke(Data);

                Log.WriteLine($"消息失败超过3次,丢弃 routingKey:{basic.RoutingKey}");
                return;
            }
            ///失败次数递增
            Data.ExceptionTimes += 1;

            this.PushMsg(Data, basic.Exchange, basic.RoutingKey);
            return;
        }

        /// <summary>
        /// 消费消息处理方法
        /// </summary>
        /// <param name="o"></param>
        /// <param name="basic"></param>
        void ReceivedEventFun(object? o, BasicDeliverEventArgs basic)
        {
            try
            {
                //int aa = 1; int bb = 0; int cc = aa / bb; //模拟异常，这条消息消费失败
                var msgBody = basic.Body; //获取消息内容
                //var a = basic.ConsumerTag;
                //var c = basic.DeliveryTag;
                //var d = basic.Redelivered;
                //var f = basic.RoutingKey;
                //var e = basic.BasicProperties.Headers;

                var DataStr = Encoding.UTF8.GetString(msgBody.ToArray());
                var Data = Newtonsoft.Json.JsonConvert.DeserializeObject<MQWebApiMsg>(DataStr);

                try
                {
                    var b = ReceivedDataEvent?.Invoke(Data);

                    if (b.GetValueOrDefault(false))
                    {
                        //手动ACK确认分两种:BasicAck:肯定确认 和 BasicNack:否定确认
                        RabbitChannel.BasicAck(deliveryTag: basic.DeliveryTag, multiple: false);//这种情况是消费者告诉RabbitMQ服务器，我已经确认收到了消息
                         
                    }
                    else
                    {
                        PushExceptionMsgAgain(basic, Data);
                    } 

                }
                catch (Exception ex3)
                {
                    PushExceptionMsgAgain(basic, Data);

                }
            }
            catch (Exception ex)
            {
                PushExceptionMsgAgain(basic, null);
            }
            finally
            {

            }
        }

        public bool IsDispose { get; set; } = false;

        public void Dispose()
        {
            IsDispose = true;
            if (RabbitChannel != null)
            {
                RabbitChannel.Dispose();
            }
        }
    }



}
