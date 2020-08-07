using MQ;
using MQ.Model;
using MQ.MQConfig;
using MQ.MQService;
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
            ConfigManager configTest = new ConfigManager();

            configTest.ServerConfig = new MQServerConfig();
            configTest.ServerConfig.Host = "192.168.18.115";
            configTest.ServerConfig.Account = "admin";
            configTest.ServerConfig.Pass = "admin";
            configTest.ServerConfig.Port = 0;
            configTest.ServerConfig.VirtualHost = "/";

            configTest.Data = new System.Collections.Generic.List<MQQueueConfig>();
            MQQueueConfig queueConfig = new MQQueueConfig();

            string QueueName = "Queue" + DateTime.Now.Day.ToString();
            queueConfig.BindingKeys = new string[] { "MQtest.Client.#" };
            queueConfig.ExchangeName = exchangeName;
            queueConfig.QueueName = QueueName;
            queueConfig.ThreadCount = 0;

            configTest.Data.Add(queueConfig);

            ConfigManager.SaveConfig(configTest);

            MainService server = new MainService();
            server.Start();






            ///////////////////////////////
            MQ.MQClient.MQConnection conn = new MQ.MQClient.MQConnection("192.168.18.115", "admin", "admin", 5672, "/");

            var Channel = conn.CreateModel();

            //string ExchangeName = "exchangeTopic";

            //Channel.CreateExchange(ExchangeName, MQ.MQClient.ExchangeType.topic);

            //string QueueName = "Queue" + DateTime.Now.Day.ToString();

            //Channel.CreateQueue(QueueName);

            //Channel.Binding(exchangeName, QueueName, "MQtest.Client.#");

            //Channel.ReceivedMsg(QueueName);


            while (true)
            {
                Console.WriteLine("输入消息");
                var msg = Console.ReadLine();
                MQWebApiMsg msgData = new MQWebApiMsg();
                msgData.Host = "www.baidu.com";
                msgData.Path = "www.baidu.com";
                
                for (int i = 0; i < 30; i++)
                {
                    msgData.Data =  msg + i.ToString();
                    if (i % 3 == 0)
                    {

                        Channel.PushMsg(msgData, exchangeName, "MQtest.Client2.test1");
                        continue;
                    }

                    Channel.PushMsg(msgData, exchangeName, "MQtest.Client.test1");

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
