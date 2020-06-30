using HuigeTec.Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RGC.WMS.USA.Models;
using System.Diagnostics;

namespace RGC.WMS.USA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IHttpContextAccessor _accessor;

        public HomeController(IHttpContextAccessor accessor, ILogger<HomeController> logger)
        {
            _accessor = accessor;

            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult ValidateCode()
        {
            var code = new ImageCodeHelper().CreateValidateCode(4);
            _accessor.HttpContext.Session.SetString(WebApiManageBase.SessionValidateCode, code.ToLower());
            var imgByte = new ImageCodeHelper().CreateImage(code);
            //从图片中读取byte
            return File(imgByte, "image/jpeg");
        }
    }
}
