using System;
using Microsoft.Extensions.Configuration;
using RGC.Task.ShipStation.OrderGet.Dto;

namespace RGC.Task.ShipStation.OrderGet
{
    public class Program
    {
        public static OrderListRequestDto dto;//请求参数类
        public static decimal interval_seconds = 20;//轮训秒数
        public static decimal interval_minutes = 30;//轮训分钟数
        private static object LockObject = new Object();
        // 检查更新锁
        private static int CheckLock = 0;
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json")
                 .Build();
            if (!string.IsNullOrWhiteSpace(builder.GetSection("AppSettings:interval_seconds").Value))
            {
                interval_seconds = Convert.ToDecimal(builder.GetSection("AppSettings:interval_seconds").Value);
            }
            if (!string.IsNullOrWhiteSpace(builder.GetSection("AppSettings:interval_minutes").Value))
            {
                interval_minutes = Convert.ToDecimal(builder.GetSection("AppSettings:interval_minutes").Value);
            }

            Console.WriteLine("ShipStation Order Get Task Begin...");
            dto = new OrderListRequestDto();
            dto.modifyDateStart = "2020/6/1 00:00:00";
            if (!string.IsNullOrWhiteSpace(builder.GetSection("AppSettings:getOrder_startModifyTime").Value))
            {
                dto.modifyDateStart = builder.GetSection("AppSettings:getOrder_startModifyTime").Value;
            }
            dto.sortBy = "ModifyDate";
            dto.sortDir = "ASC";
            dto.page = 1;
            dto.pageSize = 10;

            //var dto = new OrderListRequestDto();
            //dto.createDateStart = "2020-6-1";
            //dto.createDateEnd = "2020-6-11";
            //dto.page = 1;
            //dto.pageSize = 2;
            //var orderService = new OrderService();
            //var result = orderService.GetOrderList(dto);
            //if (result.Success)
            //{
            //    var s = orderService.SaveOrders(result.Data.Orders);
            //}

            System.Timers.Timer t = new System.Timers.Timer(Convert.ToDouble(interval_minutes) * 60 * 1000);   //实例化Timer类，设置间隔时间为10000毫秒；   
            t.Elapsed += new System.Timers.ElapsedEventHandler(GetSSOrder); //到达时间的时候执行事件；   
            t.AutoReset = true;   //设置是执行一次（false）还是一直执行(true)；   
            t.Enabled = true;     //是否执行System.Timers.Timer.Elapsed事件；   

            Console.WriteLine("Task Start Time: " + DateTime.Now);
            //Console.WriteLine("Task finish!");
            Console.ReadLine();
        }

        public static void GetSSOrder(object source, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("GetSSOrder Method Start Time: " + DateTime.Now);
            // 加锁检查更新锁
            lock (LockObject)
            {
                if (CheckLock == 0) CheckLock = 1;
                else return;
            }
            var orderService = new OrderService();
            var result = orderService.GetOrderList(dto);
            if (result.Success)
            {
                //测试轮训算法
                //if (result.Data != null && result.Data.Orders != null && result.Data.Orders.Count > 0)
                //{
                //    dto.modifyDateStart = result.Data.Orders[result.Data.Orders.Count - 1].ModifyDate;
                //    Console.WriteLine("Last ModifyDate: " + dto.modifyDateStart);
                //    dto.modifyDateStart = DateTime.Parse(dto.modifyDateStart).AddMilliseconds(1000).ToString();
                //    Console.WriteLine("Last ModifyDate add 1 second: " + dto.modifyDateStart);
                //}

                //写入wms数据库
                if (result.Data != null && result.Data.Orders != null && result.Data.Orders.Count > 0)
                {
                    var save = orderService.SaveOrders(result.Data.Orders);
                    if (save.Success)
                    {
                        dto.modifyDateStart = result.Data.Orders[result.Data.Orders.Count - 1].ModifyDate;//取最后一条数据的modifyDate 
                        Console.WriteLine("Last ModifyDate: " + dto.modifyDateStart);
                        dto.modifyDateStart = DateTime.Parse(dto.modifyDateStart).AddMilliseconds(1000).ToString();//最后一条数据的modifyDate加1秒 作为下次获取订单任务的开始时间
                        Console.WriteLine("Last ModifyDate add 1 second: " + dto.modifyDateStart);
                    }

                }

            }
            // 解锁更新检查锁
            lock (LockObject)
            {
                CheckLock = 0;
            }
        }
        
    }
}
