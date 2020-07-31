using RedisDao.IRedis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisDao.RedisBeetleX
{
    public class HQRedisComm<DataType> : IHQRedisComm<DataType>
    {
        public string _Key;
        public HQRedisDB DB;

        public string Key
        {
            get { return _Key; }
        }

        public HQRedisComm(HQRedisDB DB, string Key)
        {
            this.DB = DB;
            this._Key = Key;
        }

        public string Json(object Data)
        {
            string str = Newtonsoft.Json.JsonConvert.SerializeObject(Data);
            //string str2 = Newtonsoft.Json.JsonConvert.SerializeObject(Data, Newtonsoft.Json.Formatting.None);
            //string str = Newtonsoft.Json.JsonConvert.SerializeObject(Data,  Newtonsoft.Json.Formatting.Indented);
            return str;
        }
        public T1 JsonDes<T1>(string Data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T1>(Data);
        }

    }
}
