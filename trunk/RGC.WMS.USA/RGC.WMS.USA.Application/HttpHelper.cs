using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace RGC.WMS.USA.Application
{
    public class HttpHelper
    {
        public static HttpResponseMessage ResponseJson(Object obj)
        {
            string str = JsonConvert.SerializeObject(obj);

            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(str, Encoding.GetEncoding("UTF-8"), "application/json") };

            return result;
        }
        public static HttpResponseMessage ResponseCamelCaseJson(Object obj)
        {
            var setting = new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            };
            string str = JsonConvert.SerializeObject(obj, Formatting.None, setting);

            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(str, Encoding.GetEncoding("UTF-8"), "application/json") };

            return result;
        }

        public static HttpResponseMessage ResponseString(string str)
        {
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(str, Encoding.GetEncoding("UTF-8"), "application/json") };

            return result;
        }


        public static string GetPostData(HttpContext context)
        {
            string data = string.Empty;

            HttpRequest request = context.Request;
            Stream stream = request.Body;

            if (stream.Length != 0)
            {
                StreamReader streamReader = new StreamReader(stream);
                data = streamReader.ReadToEnd();
            }

            return data;
        }

        public static NameValueCollection GetPostJsonDataForm(HttpContext context)
        {
            NameValueCollection values = new NameValueCollection();
            string jsonString = HttpUtility.UrlDecode(GetPostData(context));
            Dictionary<string, object> dictionary = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);

            foreach (string str2 in dictionary.Keys)
            {
                if (dictionary[str2] != null)
                {
                    values.Add(str2, dictionary[str2].ToString());
                }
            }
            return values;
        }

        public static Dictionary<string, object> GetPostFormDict(HttpContext context)
        {
            string jsonString = HttpUtility.UrlDecode(GetPostData(context));

            Dictionary<string, object> dict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);

            return dict;
        }

        public static string Get(string url, string data)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + (data == "" ? "" : "?") + data);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        public static string Post(String param, String url)
        {
            String result = "";
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(param);

                WebRequest webReq = WebRequest.Create(new Uri(url));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
                webReq.ContentLength = data.Length;

                Stream newStream = webReq.GetRequestStream();
                newStream.Write(data, 0, data.Length);//写入参数
                newStream.Close();

                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);

                result = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public static string Post(String param, String url, String userName, String password)
        {
            String result = "";
            try
            {
                byte[] b = Encoding.UTF8.GetBytes(param);
                WebRequest webReq = WebRequest.Create(new Uri(url));
                webReq.Method = "POST";
                webReq.ContentType = "application/json; charset=utf-8";

                webReq.Credentials = new NetworkCredential(userName, password);//设置身份验证
                webReq.PreAuthenticate = true;//随请求携带身份验证

                webReq.ContentLength = b.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(b, 0, b.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);

                result = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public static string PostJson(String param, String url)
        {
            String result = "";
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(param);

                WebRequest webReq = WebRequest.Create(new Uri(url));
                webReq.Method = "POST";
                webReq.ContentType = "application/json; charset=utf-8";
                webReq.ContentLength = data.Length;

                Stream newStream = webReq.GetRequestStream();
                newStream.Write(data, 0, data.Length);//写入参数
                newStream.Close();

                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);

                result = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }


        public static string PostJsonAuth(String param, String url, String auth)
        {
            String result = "";
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(param);

                HttpWebRequest webReq = WebRequest.Create(new Uri(url)) as HttpWebRequest;
                webReq.Method = "POST";
                webReq.Accept = "application/json";
                webReq.ContentType = "application/json; charset=utf-8";
                webReq.ContentLength = data.Length;
                webReq.Headers.Add("Authorization", auth);

                Stream newStream = webReq.GetRequestStream();
                newStream.Write(data, 0, data.Length);//写入参数
                newStream.Close();

                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);

                result = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public static string PostXmlAuthReturnJson(String param, String url, String auth)
        {
            String result = "";
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(param);

                HttpWebRequest webReq = WebRequest.Create(new Uri(url)) as HttpWebRequest;
                webReq.Method = "POST";
                webReq.Accept = "application/json";
                webReq.ContentType = "application/xml; charset=utf-8";
                webReq.ContentLength = data.Length;
                webReq.Headers.Add("Authorization", auth);

                Stream newStream = webReq.GetRequestStream();
                newStream.Write(data, 0, data.Length);//写入参数
                newStream.Close();

                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);

                result = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }





    }
}
