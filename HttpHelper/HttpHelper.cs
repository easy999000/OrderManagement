using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpHelper
{
    public class HttpHelper
    {

        HttpClient Client;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Timeout">秒</param>
        public HttpHelper(int Timeout = 20)
        {
            Client = new HttpClient();

            Client.Timeout = new TimeSpan(Timeout * 10000000L);


        }

        public async Task<string> Get_StringAsync(string Url)
        {
            var Res = await Client.GetAsync(Url);

            var Value = await Res.Content.ReadAsStringAsync();

            return Value;
        }

        public async Task<string> Post_JsonToStringAsync(string Url, object Data)
        {
            string Msg = Newtonsoft.Json.JsonConvert.SerializeObject(Data);

            StringContent Content = new StringContent(Msg);
            //application / json; charset = utf - 8
            Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            Content.Headers.ContentType.CharSet = "utf-8";

            //Content.Headers.TryAddWithoutValidation("accept", "*/*");

            //var v1 = Content.Headers.Contains("accept");

            var Res = await Client.PostAsync(Url, Content);

            var Value = await Res.Content.ReadAsStringAsync();

            return Value;
        }

        public async Task<string> Post_FormToStringAsync(string Url, List<KeyValuePair<string, string>> Data)
        {
            //string Msg = Newtonsoft.Json.JsonConvert.SerializeObject(Data);

            //StringContent Content = new StringContent(Msg);

            FormUrlEncodedContent Content = new FormUrlEncodedContent(Data);

            Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");

            Content.Headers.ContentType.CharSet = "utf-8";

            //Content.Headers.Add("Accept", "*/*");

            var Res = await Client.PostAsync(Url, Content);

            var Value = await Res.Content.ReadAsStringAsync();

            return Value;
        }



    }
}
