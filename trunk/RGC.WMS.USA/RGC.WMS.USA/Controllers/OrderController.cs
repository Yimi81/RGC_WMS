using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RGC.WMS.USA.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult SSOrderList()
        {
            return View();
        }
        public IActionResult SaleOrderList()
        {
            return View();
        }
        public IActionResult SSOrderDetail(long orderId = 0)
        {
            ViewBag.orderId = orderId;
            return View();
        }
        public IActionResult SaleOrderDetail(long orderId = 0)
        {
            ViewBag.orderId = orderId;
            return View();
        }
        public IActionResult SaleOrderAdd(long orderId = 0)
        {
            ViewBag.orderId = orderId;
            return View();
        }
        public IActionResult SSPackingSlips(long orderId = 0)
        {
            ViewBag.orderId = orderId;
            return View();
        }
        public IActionResult SalePackingSlips(long orderId = 0)
        {
            ViewBag.orderId = orderId;
            return View();
        }
    }
}