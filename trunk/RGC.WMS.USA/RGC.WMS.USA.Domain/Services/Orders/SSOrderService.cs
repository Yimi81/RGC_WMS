using System;
using System.Collections.Generic;
using System.Text;
using RGC.WMS.USA.Domain.Repositories.Orders;

namespace RGC.WMS.USA.Domain.Services.Orders
{
    /// <summary>
    /// 订单领域服务
    /// 创建:jerry 2020/03/06
    /// </summary>
    public class SSOrderService : ISSOrderService
    {
        private readonly ISSOrderRepository _orderRepository;

        public SSOrderService(ISSOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
    }
}
