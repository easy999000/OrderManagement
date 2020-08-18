using Grpc.Core;
using Grpc.Net.Client;
using MQClient.Model;
using MQGrpcServer.Protos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Channels;
using static MQGrpcServer.Protos.MQWebApiMsgServer;

namespace MQClient
{
    /// <summary>
    /// MQ连接端,尽量复用这个实力,少实例化,否者会造成大量连接被占用,阻塞
    /// </summary>
    public class Client:IDisposable
    {
        public string Host;

        GrpcChannel ChannelAddress = null;



        public Client(string _Host)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            Host = _Host;
            ChannelAddress = GrpcChannel.ForAddress(Host);
        }

        public void Dispose()
        {
            if (ChannelAddress!=null)
            {
                ChannelAddress.Dispose();
            }
        }

        /// <summary>
        /// 推送消息
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="request"></param>
        public async void PushDataAsync(string Key, MQWebApiData request)
        {
            var Msg = new MQWebApiMsg();
            Msg.Data = Newtonsoft.Json.JsonConvert.SerializeObject(request.Data);
            Msg.Host = request.Host;
            Msg.Path = request.Path;
            Msg.Key = Key;



            var client = new MQWebApiMsgServerClient(ChannelAddress);

            var reply = await client.PushDataAsync(Msg);

            await ChannelAddress.ShutdownAsync();

            //ChannelAddress.Dispose();

            return;
        }
    }
}
