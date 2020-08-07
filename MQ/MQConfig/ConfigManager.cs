using Microsoft.Extensions.Configuration;
using MQ.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MQ.MQConfig
{
    public class ConfigManager
    {
        public MQServerConfig ServerConfig { get; set; } = null;
        public List<MQQueueConfig> Data { get; set; } = null;

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
