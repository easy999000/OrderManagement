using Grpc.Core;
using MQGrpcServer.Protos;
using MQServer;
using MQServer.MQService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MQGrpcServer.Protos.MQWebApiMsgServer;

namespace GrpcService1.Services
{
    public class MQWebApiMsgServer : MQWebApiMsgServerBase
    {
        MainService MQService;
        public MQWebApiMsgServer(MainService _MQService)
        {
            MQService = _MQService;
        }

        public override Task<NullClass> PushData(MQWebApiMsg request, ServerCallContext context)
        {
            var data = new MQServer.Model.MQWebApiMsg();

            data.Data = request.Data;

            data.Host = request.Host;

            data.Path = request.Path;

            MQService.PushQueueClient.PushData(data, request.Key);

            return Task.FromResult(new NullClass() { Res = "ok" });
        }


        public override Task<NullClass> AddOrUpdateQueue(MQQueueConfig request, ServerCallContext context)
        {
            MQServer.MQConfig.MQQueueConfig QueueConfig = new MQServer.MQConfig.MQQueueConfig();
            QueueConfig.BindingKeys = request.BindingKeys.ToArray();
            QueueConfig.ExchangeName = request.ExchangeName;
            QueueConfig.QueueName = request.QueueName;
            QueueConfig.ThreadCount = request.ThreadCount;

            MQService.AddOrUpdateQueue(QueueConfig);

            return Task.FromResult(new NullClass() { Res = "ok" });
        }

        public override Task<NullClass> RemoveQueue(RemoveQueueName request, ServerCallContext context)
        {

            MQService.RemoveQueue(request.QueueName);

            return Task.FromResult(new NullClass() { Res = "ok" });
        }

        public override Task<NullClass> UpdateServer(MQServerConfig request, ServerCallContext context)
        {
            MQServer.MQConfig.MQServerConfig ServerConfig = new MQServer.MQConfig.MQServerConfig();

            ServerConfig.Account = request.Account;
            ServerConfig.Host = request.Host;
            ServerConfig.Pass = request.Pass;
            ServerConfig.Port = request.Port;
            ServerConfig.VirtualHost = request.VirtualHost;

            MQService.UpdateServer(ServerConfig);

            return Task.FromResult(new NullClass() { Res = "ok" });
        }
        public override Task<ConfigManager> GetConfig(NullClass request, ServerCallContext context)
        {
            //throw new Exception("fuck");

            var Config = MQService.GetConfig();

            ConfigManager res = new ConfigManager();

            MQServerConfig ServerConfig = new MQServerConfig();

            ServerConfig.Account = Config.ServerConfig.Account;
            ServerConfig.Host = Config.ServerConfig.Host;
            ServerConfig.Pass = Config.ServerConfig.Pass;
            ServerConfig.Port = Config.ServerConfig.Port;
            ServerConfig.VirtualHost = Config.ServerConfig.VirtualHost;

            res.ServerConfig = ServerConfig;


            foreach (var item in Config.Data)
            {
                MQQueueConfig QueueConfig = new MQQueueConfig();
                QueueConfig.BindingKeys.Add(item.Value.BindingKeys.ToArray());
                QueueConfig.ExchangeName = item.Value.ExchangeName;
                QueueConfig.QueueName = item.Value.QueueName;
                QueueConfig.ThreadCount = item.Value.ThreadCount;
                res.Data.Add(item.Key, QueueConfig);
            }


            return Task.FromResult(res);
        }

    }
}
