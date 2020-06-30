using elFinder.NetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RGC.WMS.USA.Application;

namespace RGC.WMS.USA.Controllers
{
    public class WarehouseController : Controller
    {
        private readonly IOptions<ApplicationBaseConfig> _configuration;
        public WarehouseController(IOptions<ApplicationBaseConfig> configuration)
        {
            _configuration = configuration;
        }

        public IActionResult List()
        {
            return View();
        }

        public IActionResult StockInList()
        {
            return View();
        }

        public IActionResult StockInEdit(long stockInId)
        {
            ViewBag.stockInId = stockInId;
            return View();
        }

        public IActionResult StockInShow(long stockInId)
        {
            ViewBag.stockInId = stockInId;
            return View();
        }

        public IActionResult StockOutList()
        {
            return View();
        }

        public IActionResult StockOutEdit(long stockOutId)
        {
            ViewBag.stockOutId = stockOutId;
            return View();
        }

        public IActionResult StockOutShow(long stockOutId)
        {
            ViewBag.stockOutId = stockOutId;
            return View();
        }
    }
}