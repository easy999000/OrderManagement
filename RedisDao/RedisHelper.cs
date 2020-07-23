using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace RedisDao
{
    public class RedisHelper : IDisposable
    {
        private string _connectionString; //连接字符串
        private string _instanceName; //实例名称
        private int _defaultDB; //默认数据库
        private ConcurrentDictionary<string, ConnectionMultiplexer> _connections;
        public RedisHelper(string connectionString, string instanceName, int defaultDB = 0)
        {
            _connectionString = connectionString;
            _instanceName = instanceName;
            _defaultDB = defaultDB;
            _connections = new ConcurrentDictionary<string, ConnectionMultiplexer>();
        }

        /// <summary>
        /// 获取ConnectionMultiplexer
        /// </summary>
        /// <returns></returns>
        private ConnectionMultiplexer GetConnect()
        {
            return _connections.GetOrAdd(_instanceName, p =>
            {


                ConfigurationOptions options = ConfigurationOptions.Parse(_connectionString);

                options.AsyncTimeout = (10 * 1000);
                options.ConnectTimeout = (10 * 1000);
                options.ResponseTimeout = (10 * 1000);
                options.SyncTimeout = (10 * 1000);

                //  RedisConfig

                //var v2_options = ConfigurationOptions.Parse("120.79.20.121:8199,defaultDatabase = 15,password = bwerUGF21b");

                //options.SslHost = "";
                //options.SslHost = null;

                var v1 = ConnectionMultiplexer.Connect(options);

                return v1;

            });
        }

        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <param name="configName"></param>
        /// <param name="db">默认为0：优先代码的db配置，其次config中的配置</param>
        /// <returns></returns>
        public IDatabase GetDatabase()
        {
            return GetConnect().GetDatabase(2);
        }

        public IServer GetServer(string configName = null, int endPointsIndex = 0)
        {
            var confOption = ConfigurationOptions.Parse(_connectionString);
            return GetConnect().GetServer(confOption.EndPoints[endPointsIndex]);
        }

        public ISubscriber GetSubscriber(string configName = null)
        {
            return GetConnect().GetSubscriber();
        }

        public void Dispose()
        {
            if (_connections != null && _connections.Count > 0)
            {
                foreach (var item in _connections.Values)
                {
                    item.Close();
                }
            }
        }
    }
}
