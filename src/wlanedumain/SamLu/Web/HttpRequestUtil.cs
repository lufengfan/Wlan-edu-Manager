//HttpRequestUtil.cs

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace SamLu.Web
{
    /// <summary>   
    ///  Http操作类   
    /// </summary>   
    public static class HttpRequestUtil
    {
        private static Encoding DEFAULT_ENCODING = Encoding.GetEncoding("GB2312");
        private static string ACCEPT = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
        private static string CONTENT_TYPE = "application/x-www-form-urlencoded";
        private static string USERAGENT = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; InfoPath.2; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; msn OptimizedIE8;ZHCN)";

        /// <summary>   
        ///  获取网址HTML   
        /// </summary>   
        /// <param name="url">网址 </param>   
        /// <returns> </returns>   
        public static string GetHtmlContent(string url)
        {
            return GetHtmlContent(url, DEFAULT_ENCODING);
        }

        /// <summary>
        ///  获取网址HTML
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="encoding">编码</param>
        /// <returns> </returns>
        public static string GetHtmlContent(string url, Encoding encoding)
        {
            string html;
            using (StreamReader reader = HttpRequestUtil.GetHtmlContentStreamReader(url, encoding))
            {
                html = reader.ReadToEnd();
            }
            return html;
        }

        public static Stream GetHtmlContentStream(string url)
        {
            string html = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = USERAGENT;
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();

            return response.GetResponseStream();
        }

        public static StreamReader GetHtmlContentStreamReader(string url, Encoding encoding)
        {
            return new StreamReader(HttpRequestUtil.GetHtmlContentStream(url), encoding);
        }

        public static string SerializeData<TValue>(this IDictionary<string, TValue> dictionary)
        {
            if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));

            return string.Join("&",
                dictionary
                    .Select(pair => $"{pair.Key ?? string.Empty}={(object)pair.Value ?? string.Empty}")
                    .ToArray()
            );
        }

        public static string SerializeData<TData>(this TData data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            return string.Join("&",
                (
                    from fi in typeof(TData).GetFields(
                        System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                    let attributes = fi.GetCustomAttributes(true)
                    where !attributes.OfType<NonSerializedAttribute>().Any()
                    let defaultValueAttr = attributes.OfType<DefaultValueAttribute>().FirstOrDefault()
                    let hasDefaultValue = defaultValueAttr != null
                    let defaultValue = hasDefaultValue ? defaultValueAttr.Value : null
                    let exactValue = fi.GetValue(data)
                    select string.Format("{0}={1}",
                        fi.Name,
                        HttpUtility.UrlEncode(((
                            (hasDefaultValue && object.Equals(defaultValue, exactValue))
                                ? null : exactValue
                        ) ?? string.Empty).ToString())
                    )
                ).ToArray()
            );
        }

        public static string Submit(this HtmlAgilityPack.HtmlNode form)
        {
            return form.Submit(Encoding.UTF8);
        }

        public static string Submit(this HtmlAgilityPack.HtmlNode form, Encoding encoding)
        {
            if (form == null) throw new ArgumentNullException(nameof(form));
            if (encoding == null) throw new ArgumentNullException(nameof(encoding));

            string postDataStr = form == null ? string.Empty :
                form.SelectNodes("//input").ToList()
                    .ToDictionary(
                        (input => input.GetAttributeValue("name", string.Empty)),
                        (input => HttpUtility.UrlEncode(input.GetAttributeValue("value", string.Empty)))
                    )
                    .SerializeData<string>();

            byte[] responseData = HttpRequestUtil.Post(form?.GetAttributeValue("action", null),
                encoding.GetBytes(postDataStr)
            );

            return encoding.GetString(responseData);
        }

        /// <summary>   
        /// 获取网站cookie   
        /// </summary>   
        /// <param name="url">网址 </param>   
        /// <returns> </returns>   
        public static string GetCookie(string url)
        {
            string cookie = string.Empty;
            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            using (WebResponse response = request.GetResponse())
            {
                cookie = response.Headers.Get("Set-Cookie");
            }
            return cookie;
        }

        /// <summary>   
        /// Post模式浏览   
        /// </summary>   
        /// <param name="url">网址</param>   
        /// <param name="data">流</param>   
        /// <returns> </returns>   
        public static byte[] Post(string url, byte[] data)
        {
            return Post(null, url, data, null);
        }

        /// <summary>   
        /// Post模式浏览   
        /// </summary>   
        /// <param name="server">服务器地址 </param>   
        /// <param name="url">网址</param>   
        /// <param name="data">流</param>   
        /// <param name="cookieHeader">cookieHeader</param>   
        /// <returns> </returns>   
        public static byte[] Post(string server, string url, byte[] data, string cookieHeader)
        {
            if (data == null || data.Length == 0)
            {
                throw new ArgumentNullException("data");
            }
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            if (!string.IsNullOrEmpty(cookieHeader))
            {
                if (string.IsNullOrEmpty(server))
                {
                    throw new ArgumentNullException("server");
                }
                CookieContainer co = new CookieContainer();
                co.SetCookies(new Uri(server), cookieHeader);
                httpWebRequest.CookieContainer = co;
            }
            httpWebRequest.ContentType = CONTENT_TYPE;
            httpWebRequest.Accept = ACCEPT;
            httpWebRequest.Referer = server;
            httpWebRequest.UserAgent = USERAGENT;
            httpWebRequest.Method = "Post";
            httpWebRequest.ContentLength = data.Length;
            using (Stream stream = httpWebRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Close();
            }
            using (HttpWebResponse webResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (Stream stream = webResponse.GetResponseStream())
                {
                    long contentLength = webResponse.ContentLength;
                    byte[] bytes = new byte[contentLength];
                    bytes = ReadFully(stream);
                    stream.Close();
                    return bytes;
                }
            }
        }

        private static byte[] ReadFully(Stream stream)
        {
            byte[] buffer = new byte[128];
            using (MemoryStream ms = new MemoryStream())
            {
                while (true)
                {
                    int read = stream.Read(buffer, 0, buffer.Length);
                    if (read <= 0)
                        return ms.ToArray();
                    ms.Write(buffer, 0, read);
                }
            }
        }

        /// <summary>   
        /// Get模式浏览   
        /// </summary>   
        /// <param name="url">Get网址</param>   
        /// <returns> </returns>   
        public static byte[] Get(string url)
        {
            return Get(null, url, null);
        }

        /// <summary>   
        /// Get模式浏览   
        /// </summary>   
        /// <param name="url">Get网址</param>   
        /// <param name="cookieHeader">cookieHeader</param>   
        /// <param name="server">服务器地址 </param>   
        /// <returns> </returns>   
        public static byte[] Get(string server, string url, string cookieHeader)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            if (!string.IsNullOrEmpty(cookieHeader))
            {
                if (string.IsNullOrEmpty(server))
                {
                    throw new ArgumentNullException("server");
                }
                CookieContainer co = new CookieContainer();
                co.SetCookies(new Uri(server), cookieHeader);
                httpWebRequest.CookieContainer = co;
            }
            httpWebRequest.Accept = "*/*";
            httpWebRequest.Referer = server;
            httpWebRequest.UserAgent = USERAGENT;
            httpWebRequest.Method = "GET";
            HttpWebResponse webResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (Stream stream = webResponse.GetResponseStream())
            {
                long contentLength = webResponse.ContentLength;
                byte[] bytes = new byte[contentLength];
                bytes = ReadFully(stream);
                stream.Close();
                return bytes;
            }
        }
    }
}