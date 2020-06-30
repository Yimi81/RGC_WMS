using System;
using System.Net;

namespace RGC.WMS.USA.Domain.Entities.System.Do
{
    public class CrawlerRequestOptions
    {
        /// <summary>
        /// 请求方式，GET或POST
        /// </summary>
        public string Method { get; set; }

        public long PlatformId { get; set; }
        public string PlatformName { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        public Uri Uri { get; set; }
        /// <summary>
        /// 上一级历史记录链接
        /// </summary>
        public string Referer { get; set; }
        /// <summary>
        /// 超时时间（毫秒）
        /// </summary>
        public int Timeout = 30000;
        /// <summary>
        /// 启用长连接
        /// </summary>
        public bool KeepAlive = false;
        /// <summary>
        /// 禁止自动跳转
        /// </summary>
        public bool AllowAutoRedirect = false;
        /// <summary>
        /// 定义最大连接数
        /// </summary>
        public int ConnectionLimit = int.MaxValue;
        /// <summary>
        /// 请求次数
        /// </summary>
        public int RequestNum = 1;
        /// <summary>
        /// 可通过文件上传提交的文件类型
        /// </summary>
        public string Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";//"*/*";
        /// <summary>
        /// 内容类型
        /// </summary>
        public string ContentType = "application/x-www-form-urlencoded";
        /// <summary>
        /// 实例化头部信息
        /// </summary>
        private WebHeaderCollection header = new WebHeaderCollection();
        /// <summary>
        /// 头部信息
        /// </summary>
        public WebHeaderCollection WebHeader
        {
            get { return header; }
            set { header = value; }
        }
        /// <summary>
        /// 定义请求Cookie字符串
        /// </summary>
        public string RequestCookies { get; set; }
        /// <summary>
        /// 异步参数数据
        /// </summary>
        public string XHRParams { get; set; }

        /// <summary>
        /// 是否需要set-cookie
        /// </summary>
        public bool IsSetCookie { get; set; }

        /// <summary>
        /// 是否代理
        /// </summary>
        public bool IsProxy = true;

        /// <summary>
        /// 1国内 2国外
        /// </summary>
        public int ProxyType = 1;
    }

    public class ProxyAndCookie
    {
        public ProxyAndCookie()
        {
            Proxy = new WebProxy();
            Cookie = "";
            Location = "";
        }
        public WebProxy Proxy { get; set; }

        public string Cookie { get; set; }

        public string Location { get; set; }
    }

    public class CatchData
    {
        public CatchData()
        {
            ReviewCount = "";
            ReviewScore = "";
            PriceString = "";
        }
        public string PriceString { get; set; }
        public string ReviewScore { get; set; }
        public string ReviewCount { get; set; }
    }
}
