using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQ
{
    public class Producer
    {
        public static void Main(string[] args)
        {

            string routingKey = "webApi2.test";


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

            };
            IConnection conn = connFactory.CreateConnection();



            IModel channel = conn.CreateModel();

            

            //交换机名称
            String exchangeName = String.Empty;

            exchangeName = "exchangeTopic";


            channel.BasicAcks += (sender, eventArgs) =>
            {
                ulong tag = eventArgs.DeliveryTag;
            };
            channel.BasicReturn += (sender, eventArgs) =>
            {

            };


            ////声明交换机
            //channel.ExchangeDeclare(exchange: exchangeName, type: "Topic");
            while (true)
            {
                Console.WriteLine("消息内容:");
                String message = Console.ReadLine();
                if (message == "exit")
                {
                    break;
                }
                for (int i = 0; i < 30; i++)
                {
                    var msg = message + i.ToString();

                    //消息内容
                    byte[] body = Encoding.UTF8.GetBytes(msg);

                    try
                    { 

                        channel.ConfirmSelect();

                        //发送消息
                        channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: null, body: body);


                        //channel.WaitForConfirmsOrDie();

                        var v1 = channel.WaitForConfirms();
                    }
                    catch (Exception ex)
                    {


                        throw;
                    }

                    Console.WriteLine("成功发送消息:" + message);





                }
            }


        }
    }
}
