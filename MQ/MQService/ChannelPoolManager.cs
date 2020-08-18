using MQServer.MQConfig;
using MQServer.RabbitClient;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;

namespace MQServer.MQService
{
    /// <summary>
    /// 信道连接池管理器, 自动创建连接
    /// </summary>
    public class ChannelPoolManager : IDisposable
    {

        /// <summary>
        /// 线程最大信道数量
        /// </summary>
        public static int ConnecitonMaxChannelCount = 10;

        /// <summary>
        /// 连接集合
        /// </summary>
        public List<MQConnection> ConnList = new List<MQConnection>();


        /// <summary>
        /// 数据集合,线程安全形,先进先出集合
        /// </summary>
        ConcurrentQueue<MQChannel> ChannelQueue = new ConcurrentQueue<MQChannel>();

        /// <summary>
        /// 服务器配置
        /// </summary>
        MQServerConfig Serverconfg;

        public ChannelPoolManager(MQServerConfig _Serverconfg)
        {
            Serverconfg = _Serverconfg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Action"></param>
        /// <returns></returns>
        public bool AutoChannel(Action<MQChannel> Action)
        {
            MQChannel Channel = null;

            bool TryDeq = false;
            int TryDeqCount = 0;

            while (true)
            {
                TryDeq = ChannelQueue.TryDequeue(out Channel);

                if (TryDeq && !Channel.IsDispose)
                {
                    break;
                }

                System.Threading.Thread.Sleep(50);

                TryDeqCount++;

                if (TryDeqCount > 100)
                {
                    return false;
                }
            }

            try
            {
                Action.Invoke(Channel);

            }
            catch (Exception)
            {

            }
            finally
            {
                ChannelQueue.Enqueue(Channel);
            }
            return true;




        }

        /// <summary>
        /// 用到用完返回信道池
        /// 
        /// </summary>
        /// <param name="Channel"></param>
        public void EnqueueChannel(MQChannel Channel)
        {
            if (this.ConnList.Contains(Channel.Conn))
            {
                ChannelQueue.Enqueue(Channel);

            }
        }



        /// <summary>
        /// 获取一个信道,如果获取失败返回null
        /// </summary>
        /// <returns></returns>
        public MQChannel  DequeueChannel ()
        {

            MQChannel Channel = null;

            bool TryDeq = false;

            int TryDeqCount = 0;

            while (true)
            {
                TryDeq = ChannelQueue.TryDequeue(out Channel);

                if (TryDeq && !Channel.IsDispose)
                {
                    break;
                }

                System.Threading.Thread.Sleep(50);
                TryDeqCount++;

                if (TryDeqCount > 100)
                {
                    return null;
                }
            }

            return Channel;
        }

        /// <summary>
        /// 创建信道,添加一个信道到信道池
        /// </summary>
        /// <returns></returns>
        public bool NewChannel()
        {
            var q = ConnList.Where(o =>
            {
                return o.ChannelCount < ConnecitonMaxChannelCount;
            });
            q = q.OrderBy(
                o => o.ChannelCount
                );
            var Conn = q.FirstOrDefault();


            if (Conn == null)
            {
                MQConnection newConn = new MQConnection(Serverconfg.Host, Serverconfg.Account, Serverconfg.Pass, Serverconfg.Port, Serverconfg.VirtualHost);

                ConnList.Add(newConn);

                Conn = newConn;
            }

            var Channel = Conn.CreateChannel();

            this.EnqueueChannel(Channel);

            return true;

        }

        public void Dispose()
        {
            foreach (var item in ConnList)
            {
                item.Dispose();
            }
        }
    }
}
