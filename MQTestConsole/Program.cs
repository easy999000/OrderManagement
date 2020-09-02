
using Grpc.Net.Client;

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
        public class testModel
        {
            public string Msg { get; set; }
            public int Age { get; set; }

            public string Name
            { get; set; }

        }


        static void Main(string[] args)
        {


            //createConfig();

            //test2();

            //test3();

            test4();


            Console.ReadLine();
        }

        static string ServerConn = "http://192.168.18.36:35672;http://192.168.18.190:35672;http://192.168.18.11:35672;";
        //static string ServerConn = "http://192.168.18.11:35672;";

        static MQClient.Client grpc = new MQClient.Client(ServerConn);


        static void createConfig()
        {
            ConfigManager configTest = new ConfigManager();

            configTest.ServerConfig = new MQServerConfig();
            configTest.ServerConfig.Host = "192.168.18.115";
            configTest.ServerConfig.Account = "admin";
            configTest.ServerConfig.Pass = "admin";
            configTest.ServerConfig.Port = 0;
            configTest.ServerConfig.VirtualHost = "/";

            configTest.Data = new System.Collections.Concurrent.ConcurrentDictionary<string, MQQueueConfig>();

            MQQueueConfig queueConfig = new MQQueueConfig();

            string QueueName = "Queue" + DateTime.Now.Day.ToString();
            queueConfig.BindingKeys = new string[] { "MQtest.Client.#" };
            queueConfig.ExchangeName = exchangeName;
            queueConfig.QueueName = QueueName;
            queueConfig.ThreadCount = 0;

            configTest.Data.TryAdd(queueConfig.QueueName, queueConfig);

            ConfigManager.SaveConfig(configTest);



        }

        static void test1()
        {
            try
            {
                Log.WriteLine("消息内容:");
                String message = Console.ReadLine();

                testModel M = new testModel();
                M.Age = 1;
                M.Msg = message;
                M.Name = DateTime.Now.ToString();

                string msg = message + ",Date:" + DateTime.Now.ToString();

                var Data = new MQClient.Model.MQWebApiData();
                Data.Host = "http://192.168.18.190:10001/";
                Data.Path = "mqtest/test_0s";
                Data.Data = M;


                grpc.PushData("exchangeTopic:MQtest.Client.#", Data);
                //grpc.PushDataAsync("exchangeTopic:MQtest.Client.#", Data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        static void test3()
        {
            //MQClient.GrpcClient grpc = new MQClient.GrpcClient("https://localhost:35672");
            //MQClient.GrpcClient grpc = new MQClient.GrpcClient("http://127.0.0.1:35672");
            //MQClient.Client grpc = new MQClient.Client("http://192.168.18.190:35672");

            //while (true)
            //{
            Log.WriteLine("消息内容:");
            String message = Console.ReadLine();
            //if (message == "exit")
            //{
            //    break;
            //}

            for (int i = 0; i < 200; i++)
            {
                System.Threading.Thread th = new System.Threading.Thread(Test3_1); ;
                th.Start(message);
            }
            //}

        }


        /// <summary>
        /// 30s 500 一批整数分钟开始
        /// </summary>
        /// <param name="message"></param>
        static void Test3_1(object message)
        {

            //MQClient.Client grpc = new MQClient.Client("http://192.168.18.36:35672");
            //MQClient.Client grpc = new MQClient.Client(ServerConn);

            int second = System.DateTime.Now.Second;

            second = 60 - second;
            if (second > 30)
            {
                second = second - 30;
            }
            System.Threading.Thread.Sleep(second * 1000);
            while (true)
            {
                for (int i = 0; i < 200; i++)
                {
                    try
                    {
                        string msg = message + ", i:" + i.ToString() + ",Date:" + DateTime.Now.ToString();

                        testModel M = new testModel();
                        M.Age = i;
                        M.Msg = msg;
                        M.Name = DateTime.Now.ToString();

                        var Data = new MQClient.Model.MQWebApiData();
                        Data.Host = "http://192.168.18.190:10001/";
                        Data.Path = "mqtest/test_0s";
                        //Data.Path = "mqtest/test_1s";
                        Data.Data = M;


                        grpc.PushData("exchangeTopic:MQtest.Client.#", Data);
                        //grpc.PushDataAsync("exchangeTopic:MQtest.Client.#", Data);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }


                System.Threading.Thread.Sleep(10 * 1000);
            }
        }

        static void test4()
        {
            while (true)
            {
                Log.WriteLine("命令:1update,2 get, 3 remove ,4 updateHost, 5 测试数据 , 6 单条测试");
                String message = Console.ReadLine();

                switch (message)
                {
                    case "1":
                        AddOrUpdateQueueAsyncTest();
                        break;
                    case "2":
                        GetConfigTest();
                        break;
                    case "3":
                        RemoveQueueTest();
                        break;
                    case "4":
                        UpdateServerTest();
                        break;
                    case "5":
                        test3();
                        break;
                    case "6":
                        test1();
                        break;
                    case "7":
                        break;
                    case "8":
                        break;
                    case "9":
                        break;
                    case "0":
                        return;

                    default:
                        break;
                }

            }


        }


        static public void AddOrUpdateQueueAsyncTest()
        {
            //MQClient.Client grpc = new MQClient.Client("http://192.168.18.190:35672");
            MQClient.Client grpc = new MQClient.Client(ServerConn);
            MQGrpcServer.Protos.MQQueueConfig newConfig = new MQGrpcServer.Protos.MQQueueConfig();

            Log.WriteLine("队列名称:");
            String QueueName = Console.ReadLine();

            Log.WriteLine("BindingKeys名称:");
            String BindingKeys = Console.ReadLine();


            newConfig.BindingKeys.Add(BindingKeys);
            newConfig.ExchangeName = "exchangeTopic";
            newConfig.QueueName = QueueName;
            newConfig.ThreadCount = 5;


            grpc.AddOrUpdateQueue(newConfig);


        }

        static public void GetConfigTest()
        {
            //MQClient.Client grpc = new MQClient.Client("http://192.168.18.190:35672");
            MQClient.Client grpc = new MQClient.Client(ServerConn);
            try
            {

                var res = grpc.GetConfig();
                Log.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(res));
            }
            catch (Grpc.Core.RpcException ex2)
            {

                throw;
            }
            catch (System.ArgumentException ex1)
            {

                throw;
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        static public void RemoveQueueTest()
        {
            Log.WriteLine("队列名称:");
            String message = Console.ReadLine();

            //MQClient.Client grpc = new MQClient.Client("http://192.168.18.190:35672");
            MQClient.Client grpc = new MQClient.Client(ServerConn);

            grpc.RemoveQueueAsync(new MQGrpcServer.Protos.RemoveQueueName() { QueueName = message });
        }

        static public void UpdateServerTest()
        {
            Log.WriteLine("mq服务host:");
            String message = Console.ReadLine();

            //MQClient.Client grpc = new MQClient.Client("http://192.168.18.190:35672");
            MQClient.Client grpc = new MQClient.Client(ServerConn);

            MQGrpcServer.Protos.MQServerConfig ServerConfig = new MQGrpcServer.Protos.MQServerConfig();

            ServerConfig.Host = message;
            ServerConfig.Account = "admin";
            ServerConfig.Pass = "admin";
            ServerConfig.Port = 0;
            ServerConfig.VirtualHost = "/";



            grpc.UpdateServer(ServerConfig);
        }

        static string routingKey = "wabapi.test";
        static string exchangeName = "exchangeTopic";
        static void MQHelperTest()
        {


        }



    }
}
