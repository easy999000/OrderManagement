using Microsoft.Extensions.Configuration;
using MQServer.Tools;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MQServer.MQConfig
{
    public class ConfigManager
    {
        public MQServerConfig ServerConfig { get; set; } = null;
        public ConcurrentDictionary<string, MQQueueConfig> Data { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="QueueConfig"></param>
        public void AddOrUpdateQueue(MQQueueConfig QueueConfig)
        {
            if (QueueConfig == null)
            {
                return;
            }
            Data.AddOrUpdate(QueueConfig.QueueName, o => { return QueueConfig; }, (p, p2) => { return QueueConfig; });

        }
        /// <summary>
        /// 更新服务配置
        /// </summary>
        /// <param name="_ServerConfig"></param>
        public void UpdateServer(MQServerConfig _ServerConfig)
        {
            ServerConfig = _ServerConfig;
        }

        /// <summary>
        /// 移除队列
        /// </summary>
        /// <param name="QueueName"></param>
        public void RemoveQueue(string QueueName)
        {
            _ = Data.TryRemove(QueueName, out _);
             
        }



        public static ConfigManager LoadConfig(string FileName = "MQServiceConfig.json")
        {

            string basePath = Directory.GetCurrentDirectory();

            string FilePath = Path.Combine(basePath, "Config", FileName);

            string FileStr = HQFile.GetFile(FilePath);

            var Data = Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigManager>(FileStr);

            return Data;
        }

        public static void SaveConfig(ConfigManager Data, string FileName = "MQServiceConfig.json")
        {
            string str = Newtonsoft.Json.JsonConvert.SerializeObject(Data);

            string basePath = Directory.GetCurrentDirectory();

            string FilePath = Path.Combine(basePath, "Config", FileName);

            HQFile.SaveFile(FilePath, str);

            return;
        }

    }
}
