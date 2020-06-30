using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoMapper;
using HuigeTec.Core.Helpers;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Order.Dto;
using RGC.WMS.USA.Domain.Entities;
using RGC.WMS.USA.Domain.Entities.Order;
using RGC.WMS.USA.Domain.Repositories.Orders;
using RGC.WMS.USA.Domain.Repositories.Sku;
using RGC.WMS.USA.Domain.Repositories.Warehouse;

namespace RGC.WMS.USA.Application.Order
{
    public class SaleOrderAppService : ISaleOrderAppService
    {
        private static readonly object _locker = new object();
        private readonly ISaleOrderRepository _saleOrderRepository;
        private readonly ISaleOrderItemRepository _saleOrderItemRepository;
        private IMapper _mapper;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly ISkuStockRepository _skuStockRepository;

        public SaleOrderAppService(ISaleOrderRepository saleOrderRepository,
        IMapper mapper,
        ISaleOrderItemRepository saleOrderItemRepository,
        IWarehouseRepository warehouseRepository,
        ISkuStockRepository skuStockRepository
        )
        {
            _saleOrderRepository = saleOrderRepository;
            _mapper = mapper;
            _saleOrderItemRepository = saleOrderItemRepository;
            _warehouseRepository = warehouseRepository;
            _skuStockRepository = skuStockRepository;
        }
        public string GenerateOrderNumber(string shortName = "")
        {
            string code = "";
            lock (_locker)
            {
                code = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                if (string.IsNullOrEmpty(shortName))
                    code = "SO" + code;
                else
                    code = shortName.ToUpper() + code;

               Thread.Sleep(1);
            }
            return code;
        }
        public ResponsePageDto<SaleOrder> SaleOrderPageGet(SaleOrderSearchFilterDo search, long userId)
        {
            var result = new ResponsePageDto<SaleOrder>();
            result.Data = new List<SaleOrder>();

            #region 登入验证
            if (userId <= 0)
            {
                result.Code = 3;
                result.Msg = "请先登录";
                return result;
            }
            #endregion

            var count = 0;
            var list = _saleOrderRepository.OrderPageQuery(search, out count);
            foreach (var item in list)
            {
                item.OrderDateString = item.OrderDate.ToString("yyyy-MM-dd HH:mm");
                item.PaymentDateString = item.PaymentDate.ToString("yyyy-MM-dd HH:mm");
            }
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

        public ResponseDto<SaleOrder> SaleOrderGet(long id, long userId)
        {
            var result = new ResponseDto<SaleOrder>();
            result.Data = new SaleOrder();

            #region 登入验证
            if (userId <= 0)
            {
                result.Code = 3;
                result.Msg = "请先登录";
                return result;
            }
            #endregion

            var count = 0;
            result.Data = _saleOrderRepository.GetById(id);
            if (result.Data != null && result.Data.Id > 0)
            {
                result.Data.Items = _saleOrderItemRepository.TableNoTracking.Where(p => p.SaleOrderId == result.Data.Id).ToList();
              
                foreach (var item in result.Data.Items)
                {
                    item.UnitPriceString = ParseHelper.Fen2YuanString(item.UnitPrice);
                    item.ExtPrice = item.UnitPrice * item.Qty;
                    item.ExtPriceString = ParseHelper.Fen2YuanString(item.ExtPrice);
                }
                result.Data.OrderDateString = result.Data.OrderDate.ToString("yyyy-MM-dd HH:mm");
                result.Data.PaymentDateString = result.Data.PaymentDate.ToString("yyyy-MM-dd HH:mm");
                result.Data.OrderTotalString = ParseHelper.Fen2YuanString(result.Data.OrderTotal);
                result.Data.AmountPaidString = ParseHelper.Fen2YuanString(result.Data.AmountPaid);
                result.Data.ShippingAmountString = ParseHelper.Fen2YuanString(result.Data.ShippingAmount);
                result.Data.TaxAmountString = ParseHelper.Fen2YuanString(result.Data.TaxAmount);


            }

            result.Success = true;
            result.Msg = "success";
            result.Code = 0;
            return result;
        }

        public ResponseDto<string> SaleOrderSave(SaleOrder order, long adminId)
        {
            var result = new ResponseDto<string>();

            #region 登入验证
            if (adminId <= 0)
            {
                result.Code = 3;
                result.Msg = "请先登录";
                return result;
            }
            #endregion

            if (order == null || order.Id <= 0)
            {
                result.Msg = "订单参数错误";
                return result;
            }
            try
            {
                var excute = _saleOrderRepository.SingleInsert(order);
                if (excute != null && excute.Id > 0)
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

        public ResponseDto<string> SaleOrderManualAdd(SaleOrderDto order, long adminId)
        {
            var result = new ResponseDto<string>();

            #region 登入验证
            if (adminId <= 0)
            {
                result.Code = 3;
                result.Msg = "请先登录";
                return result;
            }
            #endregion

            #region 订单基本信息检查

            if (string.IsNullOrEmpty(order.OrderDateString))
            {
                result.Msg = "请输入订单时间";
                return result;
            }
            DateTime dt;
            if (!DateTime.TryParse(order.OrderDateString, out dt))
            {
                result.Msg = "请输入正确的订单时间";
                return result;
            }
            order.OrderDate = dt;
            if (string.IsNullOrEmpty(order.PaymentDateString))
            {
                result.Msg = "请输入支付时间";
                return result;
            }
            DateTime dt1;
            if (!DateTime.TryParse(order.PaymentDateString, out dt1))
            {
                result.Msg = "请输入正确的支付时间";
                return result;
            }
            order.PaymentDate = dt1;
            if (string.IsNullOrEmpty(order.ShippingAmountString))
            {
                result.Msg = "请输入运费";
                return result;
            }
            decimal shipAmt;
            if (!decimal.TryParse(order.ShippingAmountString, out shipAmt))
            {
                result.Msg = "请输入正确的运费";
                return result;
            }
            order.ShippingAmount = ParseHelper.Yuan2Fen(order.ShippingAmountString);
            if (string.IsNullOrEmpty(order.TaxAmountString))
            {
                result.Msg = "请输入税费";
                return result;
            }
            decimal taxAmt;
            if (!decimal.TryParse(order.TaxAmountString, out taxAmt))
            {
                result.Msg = "请输入正确的税费";
                return result;
            }
            order.TaxAmount = ParseHelper.Yuan2Fen(order.TaxAmountString);
            if (string.IsNullOrEmpty(order.AmountPaidString))
            {
                result.Msg = "请输入支付金额";
                return result;
            }
            decimal payAmt;
            if (!decimal.TryParse(order.AmountPaidString, out payAmt))
            {
                result.Msg = "请输入正确的支付金额";
                return result;
            }
            order.AmountPaid = ParseHelper.Yuan2Fen(order.AmountPaidString);

            #endregion

            #region 客户信息检查
            if (string.IsNullOrEmpty(order.CustomerUsername))
            {
                result.Msg = "请输入客户姓名";
                return result;
            }
            if (string.IsNullOrEmpty(order.CustomerEmail))
            {
                result.Msg = "请输入客户邮件地址";
                return result;
            }
            #endregion

            #region 订单地址检查

            if (string.IsNullOrEmpty(order.ShipToName))
            {
                result.Msg = "请完输入联系人名称";
                return result;
            }
            if (string.IsNullOrEmpty(order.ShipToAddress1))
            {
                result.Msg = "请输入地址1信息";
                return result;
            }
            if (string.IsNullOrEmpty(order.ShipToCity))
            {
                result.Msg = "请输入城市";
                return result;
            }
            if (string.IsNullOrEmpty(order.ShipToState))
            {
                result.Msg = "请输入所在洲";
                return result;
            }
            if (string.IsNullOrEmpty(order.ShipToZipcode))
            {
                result.Msg = "请输入地区邮编";
                return result;
            }
            if (string.IsNullOrEmpty(order.ShipToPhone))
            {
                result.Msg = "请输入联系人电话";
                return result;
            }
            order.ShipToCountry = "US";

            #endregion

            #region 账单地址检查

            if (order.IsAddressSame)
            {
                order.BillToName = order.ShipToName;
                order.BillToCompany = order.ShipToCompany;
                order.BillToAddress1 = order.ShipToAddress1;
                order.BillToAddress2 = order.ShipToAddress2;
                order.BillToCity = order.ShipToCity;
                order.BillToState = order.ShipToState;
                order.BillToCountry = order.ShipToCountry;
                order.BillToZipcode = order.ShipToZipcode;
                order.BillToPhone = order.ShipToPhone;
                order.BillToEmail = order.ShipToEmail;
            }
            else
            {
                if (string.IsNullOrEmpty(order.BillToName))
                {
                    result.Msg = "请完输入账单人名称";
                    return result;
                }
                if (string.IsNullOrEmpty(order.BillToAddress1))
                {
                    result.Msg = "请输入账单地址1信息";
                    return result;
                }
                if (string.IsNullOrEmpty(order.BillToCity))
                {
                    result.Msg = "请输入账单城市";
                    return result;
                }
                if (string.IsNullOrEmpty(order.BillToState))
                {
                    result.Msg = "请输入账单洲";
                    return result;
                }
                if (string.IsNullOrEmpty(order.BillToZipcode))
                {
                    result.Msg = "请输入账单地区邮编";
                    return result;
                }
                if (string.IsNullOrEmpty(order.BillToPhone))
                {
                    result.Msg = "请输入账单人电话";
                    return result;
                }
                order.ShipToCountry = "US";
            }

            #endregion

            order.OrderNumber = GenerateOrderNumber();

            foreach (var item in order.Items)
            {
                if (string.IsNullOrEmpty(item.FactoryModel))
                {
                    result.Msg = "选择的产品名称不能为空";
                    return result;
                }
                if (string.IsNullOrEmpty(item.UnitPriceString))
                {
                    result.Msg = "请输入产品" + item.FactoryModel + "单价";
                    return result;
                }
                decimal unitPrice;
                if (!decimal.TryParse(item.UnitPriceString, out unitPrice))
                {
                    result.Msg = "请输入正确的产品" + item.FactoryModel + "单价";
                    return result;
                }
                item.UnitPrice = ParseHelper.Yuan2Fen(item.UnitPriceString);
                order.OrderTotal = order.OrderTotal + item.UnitPrice * item.Qty;

                if (item.WarehouseId <= 0)
                {
                    result.Msg = "产品" + item.FactoryModel + "所在仓库错误";
                    return result;
                }
                var warehouse = _warehouseRepository.Get(item.WarehouseId);
                if (warehouse == null || warehouse.IsDeleted == true)
                {
                    result.Msg = item.FactoryModel + "所在仓库不存在或已停用，请重新选择产品";
                    return result;
                }
                var skuStock = _skuStockRepository.GetAllList().Where(p => p.WarehouseId == item.WarehouseId && p.BatchId == item.BatchId && p.SkuId == item.SkuId).FirstOrDefault();
                if (skuStock == null || skuStock.Id == 0)
                {
                    result.Msg = item.FactoryModel + ",未找到该产品库存，请重新选择";
                    return result;
                }
                item.SkuStockId = skuStock.Id;
                if (skuStock.AvailableStock < item.Qty) 
                {
                    result.Msg = item.FactoryModel + ",该产品库存不足，请检查";
                    return result;
                }

            }
            if (order.OrderTotal + order.ShippingAmount + order.TaxAmount != order.AmountPaid)
            {
                result.Msg = "该笔订单金额与实际输入的支付金额不匹配";
                return result;
            }

            var saleOrder = _mapper.Map<SaleOrder>(order);
        
            try
            {
                var excute = _saleOrderRepository.ManulOrderInsert(saleOrder);
                if (excute != null && excute.Id > 0)
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

        public ResponseDto<string> OrderManualStockOut(long adminId, long orderId) 
        {
            var result = new ResponseDto<string>();
            //var order = _saleOrderRepository.GetById(orderId);

            #region 登入验证

            if (adminId <= 0)
            {
                result.Code = 3;
                result.Msg = "请先登录";
                return result;
            }

            #endregion

            if (orderId > 0 && _saleOrderRepository.GetById(orderId)!= null)
            {
                var excute = _saleOrderRepository.ManulOrderStockOut(adminId, orderId);
            }
            return result;
        }
    }
}
