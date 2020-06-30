using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Order;
using RGC.WMS.USA.Domain.Entities.Order;

namespace RGC.WMS.USA.RestController
{
    [Route("rest/ssorder")]
    //[Route("api/[controller]")]
    [ApiController]
    public class SSOrderController : ControllerBase
    {
        private readonly ISSOrderAppService _ssOrderAppService;

        public SSOrderController(ISSOrderAppService ssOrderAppService)
        {
            _ssOrderAppService = ssOrderAppService;

        }

        [HttpGet("page/get")]
        [Authorize]
        public JsonResult PageGet(string key, string status, int pageSize, int currentPage)
        {
            var result = new ResponsePageDto<SSOrder>();
            result.Data = new List<SSOrder>();
            var filter = new Domain.Entities.SSOrderSearchFilterDo();
            filter.CurrentPage = currentPage;
            filter.PageSize = pageSize;
            filter.SearchKey = key;
            if (!string.IsNullOrEmpty(status))
            {
                filter.OrderStatusList.Add(status);
            }
            result = _ssOrderAppService.SSOrderPageGet(filter, 0);
            return new JsonResult(result);
        }

        [HttpGet("detail/get")]
        [Authorize]
        public JsonResult DetailGet(long orderId)
        {
            var result = new ResponseDto<SSOrder>();
            result.Data = new SSOrder();
            result = _ssOrderAppService.SSOrderGet(orderId, 0);
            return new JsonResult(result);
        }

        [HttpPost("save")]
        public JsonResult SSOrderSave(List<SSOrder> orders)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            if (orders == null || orders.Count == 0)
            {
                return new JsonResult(result);
            }
            try
            {
                foreach (var order in orders)
                {

                    order.BillToJSON = JsonConvert.SerializeObject(order.BillTo);
                    order.ShipToJSON = JsonConvert.SerializeObject(order.ShipTo);
                    order.WeightJSON = JsonConvert.SerializeObject(order.Weight);
                    order.TagIdsJSON = JsonConvert.SerializeObject(order.TagIds);
                    order.DimensionsJSON = JsonConvert.SerializeObject(order.Dimensions);
                    order.InsuranceOptionsJSON = JsonConvert.SerializeObject(order.InsuranceOptions);
                    order.InternationalOptionsJSON = JsonConvert.SerializeObject(order.InternationalOptions);
                    order.AdvancedOptionsJSON = JsonConvert.SerializeObject(order.AdvancedOptions);

                    foreach (var item in order.Items)
                    {
                        item.OptionsJSON = JsonConvert.SerializeObject(item.Options);
                        item.WeightJSON = JsonConvert.SerializeObject(item.Weight);
                    }
                }
                result = _ssOrderAppService.SSOrderSave(orders);
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