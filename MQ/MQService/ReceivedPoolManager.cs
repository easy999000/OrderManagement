using MQServer.Handler.MQService;
using MQServer.MQConfig;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MQServer.MQService
{
    /// <summary>
    /// 消费处理程序池 管理器
    /// </summary>
    public class ReceivedPoolManager
    {
        /// <summary>
        /// 队列处理程序字典
        /// </summary>
        ConcurrentDictionary<string, ReceivedQueueHandler> DicQueue = new ConcurrentDictionary<string, ReceivedQueueHandler>();

        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="Config"></param>
        public void LoadConfig(ConfigManager Config)
        {
            ////
            if (Config == null)
            {
                return;
            }
            ////保留旧对象
            ConcurrentDictionary<string, ReceivedQueueHandler> OldDic = DicQueue;

            ////创建新对象
            DicQueue = new ConcurrentDictionary<string, ReceivedQueueHandler>();
            ////添加处理程序
            foreach (var item in Config.Data)
            {
                AddQueue(item.Value, Config.ServerConfig);
            }

            ////异步停止旧对象
            Task.Run(() =>
            {
                foreach (var item in OldDic)
                {
                    item.Value.SafeStop();
                    item.Value.Dispose();
                }
            });
        }


        /// <summary>
        /// 安全停止, 逐一对连接进行停止,时间有可能会很长,建议异步
        /// </summary>
        public void SafeStop()
        {
            ////保留旧对象
            ConcurrentDictionary<string, ReceivedQueueHandler> OldDic = DicQueue;

            ////创建新对象
            DicQueue = new ConcurrentDictionary<string, ReceivedQueueHandler>();

            foreach (var item in OldDic)
            {
                item.Value.SafeStop();
                item.Value.Dispose();
            }

        }



        /// <summary>
        /// 添加处理程序
        /// </summary>
        /// <param name="Config"></param>
        private void AddQueue(MQConfig.MQQueueConfig Config, MQServerConfig Server)
        {
            if (Config == null || Server == null)
            {
                return;
            }

            if (!DicQueue.ContainsKey(Config.QueueName))
            {
                ReceivedQueueHandler handler = new ReceivedQueueHandler(Config, Server);
                DicQueue.TryAdd(Config.QueueName, handler);
            }
        }

        /// <summary>
        /// 移除处理程序
        /// </summary>
        /// <param name="QueueName"></param>
        public void RemoveQueue(string QueueName)
        {
            //var Handler = DicQueue[QueueName];

            ReceivedQueueHandler Handler = null;

            DicQueue.Remove(QueueName, out Handler);

            if (Handler != null)
            {
                Handler.Dispose();
            }
        }
    }
}
