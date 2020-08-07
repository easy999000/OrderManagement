using MQ;
using RabbitMQ.Client;
using System;
using System.Data;

namespace MQTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //test1();

            test2();
        }

        static void test1()
        {
            try
            {

                Customer.Main();

                //Producer.Main(args);

                MQHelperTest();


                var v1 = Console.ReadKey();
                while (v1.Key == ConsoleKey.Q)
                {
                    return;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        static void test2()
        {
            MQ.MQClient.MQConnection conn = new MQ.MQClient.MQConnection("192.168.18.115", "admin", "admin", 5672, "/");

            var Channel = conn.CreateModel();

            string ExchangeName = "exchangeTopic";

            Channel.CreateExchange(ExchangeName, MQ.MQClient.ExchangeType.topic);

            string QueueName = "Queue" + DateTime.Now.Day.ToString();

            Channel.CreateQueue(QueueName);

            Channel.Binding(exchangeName, QueueName, "MQtest.Client.#");

            Channel.ReceivedMsg(QueueName);


            while (true)
            {
                Console.WriteLine("输入消息");
                var msg = Console.ReadLine();

                for (int i = 0; i < 30; i++)
                {
                    var msg2 = msg + i.ToString();
                    if (i % 3 == 0)
                    {

                        Channel.PushMsg(msg2, exchangeName, "MQtest.Client2.test1");
                        continue;
                    }

                    Channel.PushMsg(msg2, exchangeName, "MQtest.Client.test1");

                }
                 
            }

        }




        static string routingKey = "wabapi.test";
        static string exchangeName = "exchangeTopic";
        static void MQHelperTest()
        {
            var Helper = new MqHelper("/");

            Console.WriteLine("消息内容:");

            String message = Console.ReadLine();

            for (int i = 0; i < 30; i++)
            {
                string msg = message + i.ToString();

                Helper.SenMsg(exchangeName, msg, routingKey, "CMS.USER", exchangeType: ExchangeType.Topic);

            }


        }



    }
}
