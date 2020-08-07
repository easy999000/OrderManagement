using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace MQ
{
    public class Customer
    {
        public static void Main()
        {
            //创建一个随机数,以创建不同的消息队列
            Console.WriteLine("Start");
            IConnectionFactory connFactory = new ConnectionFactory//创建连接工厂对象
            {
                HostName = "192.168.18.115",//IP地址
                Port = 5672,//端口号
                UserName = "admin",//用户账号
                Password = "admin"//用户密码
                ,
                AutomaticRecoveryEnabled = true
                ,
                VirtualHost = "/"
                ,
                RequestedChannelMax = 10
            };


            IConnection conn = connFactory.CreateConnection();

            for (int i = 0; i < 3; i++)
            {
                CreateChannel(conn, $"channel:{i}");

            }


        }


        public static void CreateChannel(IConnection conn, string Title)
        {
            IModel channel = conn.CreateModel();

            int random = DateTime.Now.Day;

            //交换机名称
            String exchangeName = String.Empty;

            exchangeName = "exchangeTopic";

            //声明交换机
            channel.ExchangeDeclare(exchange: exchangeName, type: "topic",durable:true,autoDelete:false);

            //消息队列名称
            String queueName = "queueName" + "_" + random.ToString();

            //声明队列
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            //将队列与交换机进行绑定
            channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: "wabapi.test");


            //声明为手动确认
            channel.BasicQos(0, 8, false);

            for (int i = 0; i < 3; i++)
            {
                CreateConsume(channel, queueName, Title + $"_Consume:{i}");

            }

            ////定义消费者
            //var consumer = new EventingBasicConsumer(channel);

            ////接收事件
            //consumer.Received += (model, ea) =>
            //{
            //    try
            //    {

            //        int ThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;

            //        byte[] message = ea.Body.ToArray();//接收到的消息

            //        string msg = Encoding.UTF8.GetString(message);

            //        Random r = new Random();
            //        var num = r.Next(0, 2);

            //        System.Threading.Thread.Sleep((10 + num) * 1000);


            //        Console.WriteLine($"接收到信息为:{msg},线程id:{ThreadId},时间:{DateTime.Now.ToString()}");


            //        //返回消息确认
            //        channel.BasicAck(ea.DeliveryTag, true);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //    }
            //};

            ////开启监听
            //channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            ////var v1 = Console.ReadKey();
            ////while (v1.Key == ConsoleKey.Q)
            ////{
            ////    return;
            ////}
        }

        static void CreateConsume(IModel channel, String queueName, string Title)
        {
            //定义消费者
            var consumer = new EventingBasicConsumer(channel);

            //接收事件
            consumer.Received += (model, ea) =>
            {
                try
                {

                    int ThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;

                    byte[] message = ea.Body.ToArray();//接收到的消息

                    string msg = Encoding.UTF8.GetString(message);

                    Random r = new Random();

                    var num = r.Next(0, 2);

                    System.Threading.Thread.Sleep((10 + num) * 1000);

                    Console.WriteLine($"接收到信息为:{msg},线程id:{ThreadId},Title:{Title},时间:{DateTime.Now.ToString()}");

                    //返回消息确认
                    channel.BasicAck(ea.DeliveryTag, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            };

            //开启监听
            channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
        }


    }

}
