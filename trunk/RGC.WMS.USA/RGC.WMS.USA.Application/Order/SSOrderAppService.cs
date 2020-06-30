using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Newtonsoft.Json;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Order;
using RGC.WMS.USA.Domain.Repositories.Orders;
using RGC.WMS.USA.Domain.Repositories.Sku;

namespace RGC.WMS.USA.Application.Order
{
    public class SSOrderAppService : ISSOrderAppService
    {
        private readonly ISSOrderRepository _ssOrderRepository;
        private readonly ISSOrderItemRepository _ssOrderItemRepository;
        private readonly ISkuStockRepository _skuStockRepository;
        //private IMapper _mapper;
        public SSOrderAppService(ISSOrderRepository ssOrderRepository,
      //  IMapper mapper,
        ISSOrderItemRepository ssOrderItemRepository,
        ISkuStockRepository skuStockRepository
        )
        {
            _ssOrderRepository = ssOrderRepository;
           // _mapper = mapper;
            _ssOrderItemRepository = ssOrderItemRepository;
            _skuStockRepository = skuStockRepository;
        }
        public ResponsePageDto<SSOrder> SSOrderPageGet(SSOrderSearchFilterDo search, long userId)
        {
            var result = new ResponsePageDto<SSOrder>();
            result.Data = new List<SSOrder>();
            var count = 0;
            var list = _ssOrderRepository.OrderPageQuery(search, out count);
            result.Data = list;
            result.Page.TotalCount = (int)count;
            result.Page.TotalPages = (int)Math.Ceiling((Decimal)count / search.PageSize);
            result.Page.PageSize = search.PageSize;
            result.Page.CurrentPage = search.CurrentPage;
            result.Page.CurrentCount = result.Data.Count;

            result.Success = true;
            result.Msg = "success";
            result.Code = 0;
            return result;
        }

        public ResponseDto<SSOrder> SSOrderGet(long id, long userId)
        {
            var result = new ResponseDto<SSOrder>();
            result.Data = new SSOrder();
            var count = 0;
            result.Data = _ssOrderRepository.GetById(id);
            if (result.Data != null && result.Data.Id > 0)
            {
                result.Data.Items = _ssOrderItemRepository.TableNoTracking.Where(p => p.SSOrderId == result.Data.Id).ToList();
                result.Data.BillTo = JsonConvert.DeserializeObject<SSAddress>(result.Data.BillToJSON);
                result.Data.ShipTo = JsonConvert.DeserializeObject<SSAddress>(result.Data.ShipToJSON);
                var subTotal = 0f;
                foreach (var item in result.Data.Items)
                {
                    subTotal = subTotal + item.Quantity * item.UnitPrice;
                }
                result.Data.SubTotal = Convert.ToDecimal(subTotal);


            }

            result.Success = true;
            result.Msg = "success";
            result.Code = 0;
            return result;
        }


        public ResponseDto<string> SSOrderSave(List<SSOrder> orders)
        {
            var result = new ResponseDto<string>();
            if (orders == null || orders.Count == 0)
            {
                result.Code = 0;
                result.Success = true;
                result.Msg = "success";
                return result;
            }
            try
            {
                var excute = _ssOrderRepository.BatchInsertOrUpdate(orders);
                if (excute > 0)
                {
                    result.Code = 0;
                    result.Success = true;
                    result.Msg = "success";
                    return result;
                }
                else
                {
                    result.Msg = "数据库写入异常";
                }
            }
            catch (Exception ex)
            {
                result.Msg = ex.Message;
            }
          
            return result;
        }


        public ResponseDto<string> SSOrderStockOut(long id, long userId)
        {
            var result = new ResponseDto<string>();
            if (id <= 0)
            {
                result.Msg = "id不能为0";
                return result;
            }
            try
            {
                var order = _ssOrderRepository.GetById(id);
                if (order != null && order.Id > 0)
                {
                    var orderItems = _ssOrderItemRepository.TableNoTracking.Where(p => p.SSOrderId == order.Id).ToList();
                    foreach (var item in orderItems)
                    {
                        //var skuStockList = _skuStockRepository.GetAllList().Where(p => p.IsDeleted == false && p.sk);
                    }
                }
                result.Code = 0;
                result.Success = true;
                result.Msg = "success";
                return result;
            }
            catch (Exception ex)
            {
                result.Msg = ex.Message;
            }

            return result;
        }
    }
}
