using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Order;
using RGC.WMS.USA.Application.Order.Dto;
using RGC.WMS.USA.Domain.Entities.Order;

namespace RGC.WMS.USA.RestController
{
    [Route("rest/saleorder")]
    //[Route("api/[controller]")]
    [ApiController]
    public class SaleOrderController : ControllerBase
    {
        private readonly ISaleOrderAppService _saleOrderAppService;

        public SaleOrderController(ISaleOrderAppService saleOrderAppService)
        {
            _saleOrderAppService = saleOrderAppService;

        }

        [HttpGet("page/get")]
        [Authorize]
        public JsonResult PageGet(string key, int pageSize, int currentPage, int status=0)
        {
            var result = new ResponsePageDto<SaleOrder>();
            result.Data = new List<SaleOrder>();
            var filter = new Domain.Entities.SaleOrderSearchFilterDo();
            filter.CurrentPage = currentPage;
            filter.PageSize = pageSize;
            filter.SearchKey = key;
            if (status >0)
            {
                filter.OrderStatusList.Add(status);
            }
            var loginId = 0;
            int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
            result = _saleOrderAppService.SaleOrderPageGet(filter, loginId);
            return new JsonResult(result);
        }

        [HttpGet("detail/get")]
        [Authorize]
        public JsonResult DetailGet(long orderId)
        {
            var result = new ResponseDto<SaleOrder>();
            result.Data = new SaleOrder();
            var loginId = 0;
            int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
            result = _saleOrderAppService.SaleOrderGet(orderId, loginId);
            return new JsonResult(result);
        }

        [HttpPost("save")]
        [Authorize]
        public JsonResult SaleOrderSave(SaleOrder order)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            if (order == null || order.Id <= 0)
            {
                return new JsonResult(result);
            }
            try
            {
                var loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                result = _saleOrderAppService.SaleOrderSave(order, loginId);
            }
            catch (Exception ex)
            {
                result.Msg = ex.Message;
                //throw;
            }
            return new JsonResult(result);
        }

        [HttpPost("manual/save")]
        [Authorize]
        public JsonResult SaleOrderManualSave()//SaleOrderDto order
        {
            ResponseDto<string> result = new ResponseDto<string>();
            try
            {
                //Request.EnableBuffering();
                using (var reader = new StreamReader(Request.Body, encoding: Encoding.UTF8))
                {
                    var body = reader.ReadToEndAsync();
                    // Do some processing with body…
                    // Reset the request body stream position so the next middleware can read it
                    //Request.Body.Position = 0;
                    SaleOrderDto order = JsonConvert.DeserializeObject<SaleOrderDto>(body.Result);
                    var loginId = 0;
                    int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                    result = _saleOrderAppService.SaleOrderManualAdd(order, loginId);
                }
            }
            catch (Exception ex)
            {
                result.Msg = ex.Message;
                //throw;
            }
            return new JsonResult(result);
        }
    }
}