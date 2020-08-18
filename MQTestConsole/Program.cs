
using Grpc.Net.Client;
using GrpcService1;
using MQServer;
using MQServer.Model;
using MQServer.RabbitClient;
using MQServer.MQConfig;
using MQServer.MQService;
using RabbitMQ.Client;
using System;
using System.Data;
using System.Diagnostics;
using MQServer.Tools;

namespace MQTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            test3();


            /////grpc



            //// The port number(5001) must match the port of the gRPC server.
            //using var channel = GrpcChannel.ForAddress("http://localhost:25672");
            //var client = new Greeter.GreeterClient(channel);
            //var reply = client.SayHelloAsync(
            //                  new HelloRequest { Name = "GreeterClient" }).ResponseAsync.Result;

            //var reply2 = client.grpcTest1(
            //                  new HelloRequest { Name = "test" });



            //test2();
            Console.ReadLine();
        }

        static void test1()
        {
            try
            {

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
            MQConnection conn = new MQConnection("192.168.18.115", "admin", "admin", 5672, "/");

            var Channel = conn.CreateChannel();

            //string ExchangeName = "exchangeTopic";

            //Channel.CreateExchange(ExchangeName, MQ.MQClient.ExchangeType.topic);

            //string QueueName = "Queue" + DateTime.Now.Day.ToString();

            //Channel.CreateQueue(QueueName);

            //Channel.Binding(exchangeName, QueueName, "MQtest.Client.#");

            //Channel.ReceivedMsg(QueueName);


            while (true)
            {
                Log.WriteLine("输入消息");
                var msg = Console.ReadLine();
                MQWebApiMsg msgData = new MQWebApiMsg();
                msgData.Host = "www.baidu.com";
                msgData.Path = "www.baidu.com";

                for (int i = 0; i < 30; i++)
                {
                    msgData.Data = msg + i.ToString();
                    if (i % 3 == 0)
                    {

                        Channel.PushMsg(msgData, exchangeName, "MQtest.Client2.test1");
                        continue;
                    }

                    Channel.PushMsg(msgData, exchangeName, "MQtest.Client.test1");

                }

            }

        }

        static void test3()
        {
            //MQClient.GrpcClient grpc = new MQClient.GrpcClient("https://localhost:35672");
            //MQClient.GrpcClient grpc = new MQClient.GrpcClient("http://127.0.0.1:35672");
            MQClient.Client grpc = new MQClient.Client("http://192.168.18.190:35672");

            while (true)
            {
                Log.WriteLine("消息内容:");
                String message = Console.ReadLine();
                if (message == "exit")
                {
                    break;
                }

                for (int i = 0; i < 4; i++)
                {
                    System.Threading.Thread th = new System.Threading.Thread(test3_1); ;
                    th.Start(message);
                }
            }




        }

        /// <summary>
        /// 30s 500 一批整数分钟开始
        /// </summary>
        /// <param name="message"></param>
        static void test3_1(object message)
        {

            MQClient.Client grpc = new MQClient.Client("http://192.168.18.190:35672");
            int second = System.DateTime.Now.Second;


            while (true)
            {
                System.Threading.Thread.Sleep((60 - second) * 1000);
                for (int i = 0; i < 500; i++)
                {
                    try
                    {
                        string msg = message + ", i:" + i.ToString();

                        var Data = new MQClient.Model.MQWebApiData();
                        Data.Host = "http://192.168.18.190:10001/";
                        Data.Path = "mqtest/test_0s";
                        Data.Data = msg;


                        grpc.PushDataAsync("exchangeTopic:MQtest.Client.#", Data);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }


                //System.Threading.Thread.Sleep(30 * 1000);
            }
        }

        static string routingKey = "wabapi.test";
        static string exchangeName = "exchangeTopic";
        static void MQHelperTest()
        {


        }



    }
}
