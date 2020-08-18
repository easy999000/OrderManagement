using MQServer.Model;
using MQServer.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace MQServer.Handler.MQService
{
    public class PostWebApiHandler
    {
        ////post超时给20分钟, 考虑到一些借口时间比较长.
        int Timeout = 1200000;
        public bool Post(MQWebApiMsg Data)
        {
            if (Data == null || Data.Host == null)
            {
                return false;
            }
            string Url = Path.Combine(Data.Host, Data.Path);
            string PostJson = Newtonsoft.Json.JsonConvert.SerializeObject(Data.Data);


            string res = HttpHelper.Post(Url, PostJson, requestEncoding: Encoding.UTF8, timeout: Timeout
                   );
#if DEBUG
            Log.WriteLine("完成一条消息推送,线程id:" + System.Threading.Thread.CurrentThread.ManagedThreadId+",Data:"+ Data.Data.ToString());
#endif
            return true;
        }
    }
}
