using elFinder.NetCore;
using elFinder.NetCore.Drivers.FileSystem;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RGC.WMS.USA.Application;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RGC.WMS.USA.Controllers
{
    public class SkuController : Controller
    {
        private Connector _connector;
        private readonly IOptions<ApplicationBaseConfig> _configuration;

        public SkuController(IOptions<ApplicationBaseConfig> configuration)
        {
            _configuration = configuration;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Class()
        {
            return View();
        }


        public IActionResult Update(int id)
        {
            ViewBag.skuId = id;
            return View();
        }

        public IActionResult Detail(int id)
        {
            ViewBag.skuId = id;
            return View();
        }
        public IActionResult Attachment()
        {
            return View();
        }

        public IActionResult SkuStock()
        {
            return View();
        }
        public IActionResult AddSkuStock()
        {
            return View();
        }

        public IActionResult SkuDetail(long skuId, long skuCostId)
        {
            ViewBag.skuId = skuId;
            ViewBag.skuCostId = skuCostId;
            return View();
        }


        public IActionResult Inherite(long productId)
        {
            ViewBag.productId = productId;
            return View();
        }
        /// <summary>
        /// 加载文件
        /// shane 2020/2/13
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Connector(string path = "", bool isLocked = false, bool isReadOnly = false, bool isShowOnly = false)
        {

            var connector = GetConnector(path, isLocked, isReadOnly, isShowOnly);
            return await connector.ProcessAsync(Request);
        }
        /// <summary>
        /// 获取缩略图
        /// shane 2020/2/13
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="_t"></param>
        /// <returns></returns>
        public async Task<IActionResult> Thumbs(string hash, string _t)
        {
            var connector = GetConnector();
            return await connector.GetThumbnailAsync(HttpContext.Request, HttpContext.Response, hash);
        }
        /// <summary>
        ///返回选中文件绝对路径
        ///shane 2020/2/13
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public async Task<IActionResult> SelectFile(string target)
        {
            var connector = GetConnector();
            var file = await connector.GetFileByHashAsync(target);
            return Json(file?.FullName);
        }
        /// <summary>
        /// 连接文件加载权限
		///shane 2020/2/13
        /// </summary>
        /// <returns></returns>
        private Connector GetConnector(string path = "", bool isLocked = false, bool isReadOnly = false, bool isShowOnly = false)
        {
            try
            {
                if (_connector == null)
                {
                    FileSystemDriver driver = new FileSystemDriver();
                    var filePath = _configuration.Value.FileUploadAddress + "product" + path;
                    var thumbPath = _configuration.Value.FileUploadAddress + "thumbs" + path;
                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);
                    if (!Directory.Exists(thumbPath))
                        Directory.CreateDirectory(thumbPath);
                    DirectoryInfo thumbsStorage = new DirectoryInfo(thumbPath);//缩略图存储路径
                    string absoluteUrl = UriHelper.BuildAbsolute(Request.Scheme, Request.Host);
                    var uri = new Uri(absoluteUrl);
                    var root = new RootVolume(
                        filePath,
                          $"{uri.Scheme}://{uri.Authority}/File/Sku/",
                        "Thumbs?hash=")
                    {
                        Alias = string.IsNullOrEmpty(path) ? "sku" : path.Replace(@"/", ""),
                        IsLocked = isLocked,//是否锁定
                        IsReadOnly = isReadOnly,//是否只读
                                                //IsShowOnly = isShowOnly,//是否显示
                                                // StartPath = new DirectoryInfo(filePath),
                                                //ThumbnailsStorage = thumbsStorage,
                        MaxUploadSizeInMb = 100,
                        //ThumbnailsUrl = "Thumbs?hash=",
                    };

                    driver.AddRoot(root);
                    _connector = new Connector(driver);
                }
            }
            catch (Exception ex)
            {

            }
            return _connector;
        }

        public IActionResult SkuCost()
        {
            return View();
        }
        public IActionResult SkuCostDetail(long skuId = 0, long skuCostId = 0, long productId = 0)
        {
            ViewBag.skuId = skuId;
            ViewBag.skuCostId = skuCostId;
            ViewBag.productId = productId;
            return View();
        }

        public IActionResult SkuCostUpdate()
        {
            return View();
        }
    }
}