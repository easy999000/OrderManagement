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

        public override Task<PushDataReturn> PushData(MQWebApiMsg request, ServerCallContext context)
        {
            var data = new MQServer.Model.MQWebApiMsg();

            data.Data = request.Data;

            data.Host = request.Host;

            data.Path = request.Path;

            MQService.PushQueueClient.PushData(data, request.Key);

            return Task.FromResult(new PushDataReturn());
        }
    }
}
