using Microsoft.VisualStudio.TestTools.UnitTesting;
using MQClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQClient.Tests
{
    [TestClass()]
    public class ClientTests
    {
        [TestMethod()]
        public void AddOrUpdateQueueAsyncTest()
        {

            //MQClient.Client grpc = new MQClient.Client("http://192.168.18.190:35672");
            MQClient.Client grpc = new MQClient.Client("http://192.168.18.11:35672");

            MQGrpcServer.Protos.MQQueueConfig newConfig = new MQGrpcServer.Protos.MQQueueConfig();

            newConfig.BindingKeys.Add("MQtest.Client.#");
            newConfig.ExchangeName = "exchangeTopic";
            newConfig.QueueName = "Queue10";
            newConfig.ThreadCount = 15;


            grpc.AddOrUpdateQueue(newConfig);


        }

        [TestMethod()]
        public void GetConfigTest()
        {
            //MQClient.Client grpc = new MQClient.Client("http://192.168.18.190:35672");
            MQClient.Client grpc = new MQClient.Client("http://192.168.18.11:35672");

            var res = grpc.GetConfig();

        }

        [TestMethod()]
        public void RemoveQueueTest()
        {
            //MQClient.Client grpc = new MQClient.Client("http://192.168.18.190:35672");
            MQClient.Client grpc = new MQClient.Client("http://192.168.18.11:35672");

            grpc.RemoveQueueAsync(new MQGrpcServer.Protos.RemoveQueueName() { QueueName= "Queue10" });
        }

        [TestMethod()]
        public void UpdateServerTest()
        {
            //MQClient.Client grpc = new MQClient.Client("http://192.168.18.190:35672");
            MQClient.Client grpc = new MQClient.Client("http://192.168.18.11:35672");

            MQGrpcServer.Protos.MQServerConfig ServerConfig = new  MQGrpcServer.Protos.MQServerConfig();
            int host = 2;
            if (host == 1)
            { 
                ServerConfig.Host = "192.168.18.115";
                ServerConfig.Account = "admin";
                ServerConfig.Pass = "admin";
                ServerConfig.Port = 0;
                ServerConfig.VirtualHost = "/";

            }
            else
            {
                ServerConfig.Host = "192.168.18.7";
                ServerConfig.Account = "admin";
                ServerConfig.Pass = "admin";
                ServerConfig.Port = 0;
                ServerConfig.VirtualHost = "/";
            }

            grpc.UpdateServer(ServerConfig);
        }
    }
}