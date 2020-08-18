using System;
using System.Collections.Generic;
using System.Text;

namespace MQServer.MQConfig
{
    public class MQServerConfig
    {
        /// <summary>
        /// 服务地址
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Pass { get; set; }

        int _Port= 5672;

        /// <summary>
        /// 端口
        /// </summary>
        public int Port
        {
            get { return _Port; }
            set
            {
                int count = value;
                if (count < 1)
                {
                    count = 5672;
                }
                _Port = count;
            }
        }

        /// <summary>
        /// 虚拟路径名字
        /// </summary>
        public string VirtualHost { get; set; } = "/";


         
    }
}
