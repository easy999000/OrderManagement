using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MQServer.Tools
{
    public class HttpHelper
    {
        private static readonly string DefaultUserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:41.0) Gecko/20100101 Firefox/41.0";

        /// <summary>
        /// 创建GET方式的HTTP请求  
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="host"> HTTP的Host标头值</param>
        /// <param name="referer"> HTTP的Referer标头值</param>
        /// <param name="timeout">请求的超时时间</param>
        /// <param name="userAgent">请求的客户端浏览器信息</param>
        /// <param name="headers">Headers的参数名称及参数值字典</param>
        /// <param name="cookies">随同HTTP请求发送的Cookie信息</param>
        /// <returns></returns>
        public static HttpWebResponse CreateGetHttpResponse(string url, string host = "", string referer = "", int? timeout = 3000, string userAgent = "", Dictionary<string, string> headers = null, CookieCollection cookies = null, string requestMethod = "GET")
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }

            HttpWebRequest request;
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = requestMethod;
            request.Accept = "application/json,text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.Headers.Add("Accept-Encoding", "gzip, deflate");
            request.Headers.Add("Accept-Language", "zh-cn,zh;q=0.8,en-us;q=0.5,en;q=0.3");
            if (!string.IsNullOrEmpty(referer))
                request.Referer = referer;

            request.Timeout = timeout.GetValueOrDefault(3000);

            if (!string.IsNullOrEmpty(userAgent))
                request.UserAgent = userAgent;
            else
                request.UserAgent = DefaultUserAgent;

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }

            request.Proxy = null;
            HttpWebResponse response = null;
            try
            {
                response = request.GetResponse() as HttpWebResponse;
                if (cookies != null && response != null)
                {
                    cookies.Add(response.Cookies);
                }
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        /// <summary>
        /// GET方式的HTTP请求结果 
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="host"> HTTP的Host标头值</param>
        /// <param name="referer"> HTTP的Referer标头值</param>
        /// <param name="timeout">请求的超时时间</param>
        /// <param name="userAgent">请求的客户端浏览器信息</param>
        /// <param name="headers">Headers的参数名称及参数值字典</param>
        /// <param name="cookies">随同HTTP请求发送的Cookie信息</param>
        /// <returns></returns>
        public static string Get(string url, Encoding responseEncoding = null, string host = "", string referer = "", int? timeout = 3000, string userAgent = "", Dictionary<string, string> headers = null, CookieCollection cookies = null)
        {
            HttpWebResponse response = CreateGetHttpResponse(url, host, referer, timeout, userAgent, headers, cookies);

            string html = string.Empty;
            if (response == null) return html;

            if (responseEncoding == null) responseEncoding = Encoding.UTF8;
            try
            {
                if (response.ContentEncoding.ToLower() == "gzip")//如果使用了GZip则先解压
                {
                    using (Stream streamReceive = response.GetResponseStream())
                    {
                        using (var zipStream = new GZipStream(streamReceive, CompressionMode.Decompress))
                        {
                            using (StreamReader sr = new StreamReader(zipStream, responseEncoding))
                            {
                                html = sr.ReadToEnd();
                            }
                        }
                    }
                }
                else
                {
                    using (Stream streamReceive = response.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(streamReceive, responseEncoding))
                        {
                            html = sr.ReadToEnd();
                        }
                    }
                }
                response.Close();
                response = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return html;
        }

        /// <summary>
        /// 创建POST方式的HTTP请求  
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>
        /// <param name="requestEncoding">发送HTTP请求时所用的编码</param>
        /// <param name="host">HTTP的Host标头值</param>
        /// <param name="referer">HTTP的Referer标头值</param>
        /// <param name="timeout">请求的超时时间</param>
        /// <param name="userAgent">请求的客户端浏览器信息</param>
        /// <param name="headers">Headers的参数名称及参数值字典</param>
        /// <param name="cookies">随同HTTP请求发送的Cookie信息</param>
        /// <returns></returns>
        public static HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters, Encoding requestEncoding = null, string contentType = "", string host = "", string referer = "", int? timeout = 3000, string userAgent = "", Dictionary<string, string> headers = null, CookieCollection cookies = null)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            if (string.IsNullOrEmpty(contentType))
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }
            else
            {
                request.ContentType = contentType;
            }
            request.Accept = "text/html,application/xhtml+xml,application/xml,json;q=0.9,*/*;q=0.8";
            request.Timeout = timeout.GetValueOrDefault(1000);

            if (!string.IsNullOrEmpty(referer))
                request.Referer = referer;

            if (!string.IsNullOrEmpty(userAgent))
                request.UserAgent = userAgent;
            else
                request.UserAgent = DefaultUserAgent;

            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
            request.Proxy = null;
            HttpWebResponse response = null;
            try
            {
                if (parameters != null && parameters.Count > 0)
                {
                    //mod by csf@2017/1/6 以Json的方式进行请求时,参数已Json流的形式传送
                    string buffer;
                    if (contentType.ToLower() == "application/json; charset=utf-8")
                        buffer = JsonConvert.SerializeObject(parameters);
                    else
                        buffer = CreateParameter(parameters);

                    if (requestEncoding == null) requestEncoding = Encoding.UTF8;
                    byte[] data = requestEncoding.GetBytes(buffer.ToString());
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
                else
                {
                    request.ContentLength = 0;
                }

                response = request.GetResponse() as HttpWebResponse;
                if (cookies != null && response != null)
                {
                    cookies.Add(response.Cookies);
                }
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
        /// <summary>
        /// 创建POST方式的HTTP请求  
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="parameters">随同请求POST的参数名称及参数值字符串</param>
        /// <param name="requestEncoding">发送HTTP请求时所用的编码</param>
        /// <param name="host">HTTP的Host标头值</param>
        /// <param name="referer">HTTP的Referer标头值</param>
        /// <param name="timeout">请求的超时时间</param>
        /// <param name="userAgent">请求的客户端浏览器信息</param>
        /// <param name="headers">Headers的参数名称及参数值字典</param>
        /// <param name="cookies">随同HTTP请求发送的Cookie信息</param>
        /// <returns></returns>
        public static HttpWebResponse CreatePostHttpResponse(string url, string parameters, Encoding requestEncoding = null
            , string contentType = "", string host = "", string referer = "", int? timeout = 3000, string userAgent = "", Dictionary<string, string> headers = null, CookieCollection cookies = null)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            if (string.IsNullOrEmpty(contentType))
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }
            else
            {
                request.ContentType = contentType;
            }
            request.Accept = "text/html,application/xhtml+xml,application/xml,json;q=0.9,*/*;q=0.8";
            request.Timeout = timeout.GetValueOrDefault(1000);

            if (!string.IsNullOrEmpty(referer))
                request.Referer = referer;

            if (!string.IsNullOrEmpty(userAgent))
                request.UserAgent = userAgent;
            else
                request.UserAgent = DefaultUserAgent;

            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
            request.Proxy = null;
            HttpWebResponse response = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(parameters))
                {
                    //mod by csf@2017/1/6 以Json的方式进行请求时,参数已Json流的形式传送
                    string buffer = parameters;
                    if (requestEncoding == null) requestEncoding = Encoding.UTF8;
                    byte[] data = requestEncoding.GetBytes(buffer.ToString());
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
                else
                {
                    request.ContentLength = 0;
                }

                response = request.GetResponse() as HttpWebResponse;
                if (cookies != null && response != null)
                {
                    cookies.Add(response.Cookies);
                }
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
        /// <summary>
        /// 拼接PostData
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string CreateParameter(IDictionary<string, string> parameters)
        {
            StringBuilder buffer = new StringBuilder();
            foreach (string key in parameters.Keys)
            {
                buffer.AppendFormat("&{0}={1}", key, parameters[key]);
            }
            return buffer.ToString().TrimStart('&');
        }

        /// <summary>
        /// 拼接PostData
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string CreateParameter(IDictionary<string, object> parameters)
        {
            StringBuilder buffer = new StringBuilder();
            foreach (string key in parameters.Keys)
            {
                buffer.AppendFormat("&{0}={1}", key, parameters[key]);
            }
            return buffer.ToString().TrimStart('&');
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }

        /// <summary>
        /// 创建POST方式的HTTP请求  
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>
        /// <param name="requestEncoding">发送HTTP请求时所用的编码</param>
        /// <param name="host">HTTP的Host标头值</param>
        /// <param name="referer">HTTP的Referer标头值</param>
        /// <param name="timeout">请求的超时时间</param>
        /// <param name="userAgent">请求的客户端浏览器信息</param>
        /// <param name="headers">Headers的参数名称及参数值字典</param>
        /// <param name="cookies">随同HTTP请求发送的Cookie信息</param>
        /// <returns></returns>
        public static string Post(string url, IDictionary<string, string> parameters, Encoding requestEncoding = null, Encoding responseEncoding = null, string contentType = "", string host = "", string referer = "", int? timeout = 15000, string userAgent = "", Dictionary<string, string> headers = null, CookieCollection cookies = null)
        {
            HttpWebResponse response = CreatePostHttpResponse(url, parameters, requestEncoding, contentType, host, referer, timeout, userAgent, headers, cookies);

            string html = string.Empty;
            if (response == null) return html;

            if (responseEncoding == null) responseEncoding = Encoding.UTF8;
            try
            {
                if (response.ContentEncoding.ToLower() == "gzip")//如果使用了GZip则先解压
                {
                    using (Stream streamReceive = response.GetResponseStream())
                    {
                        using (var zipStream = new GZipStream(streamReceive, CompressionMode.Decompress))
                        {
                            using (StreamReader sr = new StreamReader(zipStream, responseEncoding))
                            {
                                html = sr.ReadToEnd();
                            }
                        }
                    }
                }
                else
                {
                    using (Stream streamReceive = response.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(streamReceive, responseEncoding))
                        {
                            html = sr.ReadToEnd();
                        }
                    }
                }
                response.Close();
                response = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return html;
        }
        /// <summary>
        /// 创建POST方式的HTTP请求  
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="parameters">随同请求POST的参数名称及参数值字符串</param>
        /// <param name="requestEncoding">发送HTTP请求时所用的编码</param>
        /// <param name="host">HTTP的Host标头值</param>
        /// <param name="referer">HTTP的Referer标头值</param>
        /// <param name="timeout">请求的超时时间</param>
        /// <param name="userAgent">请求的客户端浏览器信息</param>
        /// <param name="headers">Headers的参数名称及参数值字典</param>
        /// <param name="cookies">随同HTTP请求发送的Cookie信息</param>
        /// <returns></returns>
        public static string Post(string url, string parameters, Encoding requestEncoding = null, Encoding responseEncoding = null, string contentType = "", string host = "", string referer = "", int? timeout = 15000, string userAgent = "", Dictionary<string, string> headers = null, CookieCollection cookies = null)
        {
            HttpWebResponse response = CreatePostHttpResponse(url, parameters, requestEncoding, contentType, host, referer, timeout, userAgent, headers, cookies);

            string html = string.Empty;
            if (response == null) return html;

            if (responseEncoding == null) responseEncoding = Encoding.UTF8;
            try
            {
                if (response.ContentEncoding != null && response.ContentEncoding.ToLower() == "gzip")//如果使用了GZip则先解压
                {
                    using (Stream streamReceive = response.GetResponseStream())
                    {
                        using (var zipStream = new GZipStream(streamReceive, CompressionMode.Decompress))
                        {
                            using (StreamReader sr = new StreamReader(zipStream, responseEncoding))
                            {
                                html = sr.ReadToEnd();
                            }
                        }
                    }
                }
                else
                {
                    using (Stream streamReceive = response.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(streamReceive, responseEncoding))
                        {
                            html = sr.ReadToEnd();
                        }
                    }
                }
                response.Close();
                response = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return html;
        }
    }
}
