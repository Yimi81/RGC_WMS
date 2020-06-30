using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RGC.WMS.USA.Controllers
{
    public class AdministrationController : Controller
    {
        public new IActionResult User()
        {
            return View();
        }
        [Authorize]
        public IActionResult Menu()
        {
            return View();
        }
        /// <summary>
        /// 组织架构
        /// </summary>
        /// <returns></returns>
        public IActionResult OrganizationUnits()
        {
            return View();
        }

        public IActionResult System()
        {
            return View();
        }

        public IActionResult Role()
        {
            return View();
        }

        public IActionResult Platform()
        {
            return View();
        }

         public IActionResult Maint()
        {
            return View();
        }
    }
}