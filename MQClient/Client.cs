using Grpc.Core;
using Grpc.Net.Client;
using MQClient.Model;
using MQClient.Tools;
using MQGrpcServer.Protos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static MQGrpcServer.Protos.MQWebApiMsgServer;

namespace MQClient
{
    /// <summary>
    /// MQ连接端,尽量复用这个实力,少实例化,否者会造成大量连接被占用,阻塞
    /// </summary>
    public class Client : IDisposable
    {
        public string Host;



        GrpcChannel[] ChannelAddressArray;

        /// <summary>
        /// 索引是个计数结果,实际索引需要求模
        /// </summary>
        int CurrentAddressIndex = 0;

        /// <summary>
        /// 获取当前索引
        /// </summary>
        /// <returns></returns>
        int GetCurrentAddressIndex()
        {
            return CurrentAddressIndex;
        }

        /// <summary>
        /// 设置当前索引
        /// </summary>
        void SetCurrentAddressIndex()
        {
            CurrentAddressIndex++;

            if (CurrentAddressIndex > 100000)
            {
                CurrentAddressIndex = CurrentAddressIndex % ChannelAddressArray.Length;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ErrorAddressIndex"></param>
        /// <returns></returns>
        private GrpcChannel GetChannelAddress(List<int> ErrorAddressIndex)
        {
            return null;
        }

        private int GetChannelIndex(List<int> ErrorAddressIndex)
        {


            return -1;
        }



        public Client(string _Host)
        {
            ///取消ssl限制
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            Host = _Host;
            Random r = new Random();

            CurrentAddressIndex = r.Next(0, 100);

            string[] AddressArray = _Host.Split(';', StringSplitOptions.RemoveEmptyEntries);

            //ChannelAddress = GrpcChannel.ForAddress(_Host);
            ChannelAddressArray = new GrpcChannel[AddressArray.Length];

            for (int i = 0; i < AddressArray.Length; i++)
            {
                ChannelAddressArray[i] = GrpcChannel.ForAddress(AddressArray[i]);

            }

        }

        public void Dispose()
        {
            if (ChannelAddressArray != null)
            {
                foreach (var item in ChannelAddressArray)
                {
                    item.Dispose();
                }
            }
        }

        /// <summary>
        /// 推送消息
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="request"></param>
        public void PushData(string Key, MQWebApiData request)
        {
            AutoChannelAddress(ChannelAddress =>
            {

                var Msg = new MQWebApiMsg();
                Msg.Data = Newtonsoft.Json.JsonConvert.SerializeObject(request.Data);
                Msg.Host = request.Host;
                Msg.Path = request.Path;
                Msg.Key = Key;


                try
                {
                    var GrpcClient = new MQWebApiMsgServerClient(ChannelAddress);

                    //var time = DateTime.Now.AddSeconds(60);

                    var reply = GrpcClient.PushData(Msg);

#if DEBUG
                    Log.WriteLine("成功发射一条消息:" + ChannelAddress.Target.ToString());
#endif

                }
                catch (Exception ex2)
                {

                    throw;
                }
                finally
                {

                }

                return 0;
            });



            return;
        }
         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Config"></param>
        public void AddOrUpdateQueue(MQQueueConfig Config)
        {
            AutoChannelAddress(ChannelAddress =>
            {
                var client = new MQWebApiMsgServerClient(ChannelAddress);

                var reply = client.AddOrUpdateQueue(Config);

                return 0;
            });

            return;

        }



        /// <summary>
        /// 移除队列
        /// </summary>
        /// <param name="QueueName"></param>
        public void RemoveQueueAsync(RemoveQueueName QueueName)
        {
            AutoChannelAddress(ChannelAddress =>
            {
                var client = new MQWebApiMsgServerClient(ChannelAddress);

                var reply = client.RemoveQueue(QueueName);

                return 0;
            });

            return;


        }


        /// <summary>
        /// 更新服务配置
        /// </summary>
        /// <param name="_ServerConfig"></param>
        public void UpdateServer(MQServerConfig _ServerConfig)
        {
            AutoChannelAddress(ChannelAddress =>
             {
                 var client = new MQWebApiMsgServerClient(ChannelAddress);

                 var reply = client.UpdateServer(_ServerConfig);

                 return 0;
             });

            return;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ConfigManager GetConfig()
        {

            return AutoChannelAddress(ChannelAddress =>
                    {

                        var client = new MQWebApiMsgServerClient(ChannelAddress);
                        var reply = client.GetConfig(new NullClass());
                        return reply;
                    });

        }

        /// <summary>
        /// 自动管理服务端连接地址
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="Fun"></param>
        /// <returns></returns>
        private T1 AutoChannelAddress<T1>(Func<GrpcChannel, T1> Fun)
        {
            IndexManage IndexM = new IndexManage(this.ChannelAddressArray.Length, GetCurrentAddressIndex());

            this.SetCurrentAddressIndex();

            while (true)
            {
                int Index = IndexM.GetIndexNext();

                if (Index < 0)
                {
                    throw new Exception("无可用连接端");
                }

                var ChannelAddress = this.ChannelAddressArray[Index];

                try
                {
                    var t = Fun.Invoke(ChannelAddress);
 
                    return t;
                }
                catch (Grpc.Core.RpcException ex2)
                {

                    if (ex2 != null && ex2.Message != null && !ex2.Message.Contains("HttpRequestException:"))
                    {
                        throw;
                    }
#if DEBUG
                    Log.WriteLine("一个服务端连接失败:" + ChannelAddress.Target.ToString());
#endif

                    ////连接不上,更换服务
                    ///
                    continue;

                }
            }
        }



        /// <summary>
        /// 自动管理服务端连接地址
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="Fun"></param>
        /// <returns></returns>
        private T1 AutoChannelAddress2<T1>(Func<GrpcChannel, T1> Fun)
        {
            IndexManage IndexM = new IndexManage(this.ChannelAddressArray.Length, GetCurrentAddressIndex());

            this.SetCurrentAddressIndex();

            while (true)
            {
                int Index = IndexM.GetIndexNext();

                if (Index < 0)
                {
                    throw new Exception("无可用连接端");
                }

                var ChannelAddress = this.ChannelAddressArray[Index];

                try
                {
                    var t = Fun.Invoke(ChannelAddress);
#if DEBUG
                    Log.WriteLine("成功发射一条消息:" + ChannelAddress.Target.ToString());
#endif
                    return t;
                }
                catch (Grpc.Core.RpcException ex2)
                {

                    if (ex2 != null && ex2.Message != null && !ex2.Message.Contains("HttpRequestException:"))
                    {
                        throw;
                    }
#if DEBUG
                    Log.WriteLine("一个服务端连接失败:" + ChannelAddress.Target.ToString());
#endif

                    ////连接不上,更换服务
                    ///
                    continue;

                }
            }
        }




    }
}
