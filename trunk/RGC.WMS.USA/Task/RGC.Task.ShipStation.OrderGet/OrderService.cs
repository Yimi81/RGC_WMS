using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RGC.Task.ShipStation.OrderGet.Dto;

namespace RGC.Task.ShipStation.OrderGet
{
    public class OrderService
    {

        private string Api_key = "73f1b84e9da94b29b82ca5d15392c49c";
        private string Api_secret = "b91cc6a1ecc146d6bac2792fa84fb3a3";
        private string Authorization;
        private string GetOrder_StartTime = "2020-05-21";
        public OrderService()
        {
            var builder = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json")
                 .Build();
            if (!string.IsNullOrWhiteSpace(builder.GetSection("AppSettings:api_key").Value))
            {
                Api_key = builder.GetSection("AppSettings:api_key").Value;
            }
            if (!string.IsNullOrWhiteSpace(builder.GetSection("AppSettings:api_secret").Value))
            {
                Api_secret = builder.GetSection("AppSettings:api_secret").Value;
            }
            Authorization = "Basic " + EncryptHelper.Base64Code(Api_key + ":" + Api_secret);
        }
        /// <summary>
        /// 创建/更新订单
        /// </summary>
        public int CreateOrUpdateOrder1(OrderDto order)
        {
            var client = new RestClient("https://ssapi.shipstation.com/orders/createorder");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Host", "ssapi.shipstation.com");
            request.AddHeader("Authorization", Authorization);
            request.AddHeader("Content-Type", "application/json");
            //request.AddParameter("application/json", "{\n  \"orderNumber\": \"TEST-ORDER-API-DOCS\",\n  \"orderKey\": \"0f6bec18-3e89-4881-83aa-f392d84f4c74\",\n  \"orderDate\": \"2015-06-29T08:46:27.0000000\",\n  \"paymentDate\": \"2015-06-29T08:46:27.0000000\",\n  \"shipByDate\": \"2015-07-05T00:00:00.0000000\",\n  \"orderStatus\": \"awaiting_shipment\",\n  \"customerId\": 37701499,\n  \"customerUsername\": \"headhoncho@whitehouse.gov\",\n  \"customerEmail\": \"headhoncho@whitehouse.gov\",\n  \"billTo\": {\n    \"name\": \"The President\",\n    \"company\": null,\n    \"street1\": null,\n    \"street2\": null,\n    \"street3\": null,\n    \"city\": null,\n    \"state\": null,\n    \"postalCode\": null,\n    \"country\": null,\n    \"phone\": null,\n    \"residential\": null\n  },\n  \"shipTo\": {\n    \"name\": \"The President\",\n    \"company\": \"US Govt\",\n    \"street1\": \"1600 Pennsylvania Ave\",\n    \"street2\": \"Oval Office\",\n    \"street3\": null,\n    \"city\": \"Washington\",\n    \"state\": \"DC\",\n    \"postalCode\": \"20500\",\n    \"country\": \"US\",\n    \"phone\": \"555-555-5555\",\n    \"residential\": true\n  },\n  \"items\": [\n    {\n      \"lineItemKey\": \"vd08-MSLbtx\",\n      \"sku\": \"ABC123\",\n      \"name\": \"Test item #1\",\n      \"imageUrl\": null,\n      \"weight\": {\n        \"value\": 24,\n        \"units\": \"ounces\"\n      },\n      \"quantity\": 2,\n      \"unitPrice\": 99.99,\n      \"taxAmount\": 2.5,\n      \"shippingAmount\": 5,\n      \"warehouseLocation\": \"Aisle 1, Bin 7\",\n      \"options\": [\n        {\n          \"name\": \"Size\",\n          \"value\": \"Large\"\n        }\n      ],\n      \"productId\": 123456,\n      \"fulfillmentSku\": null,\n      \"adjustment\": false,\n      \"upc\": \"32-65-98\"\n    },\n    {\n      \"lineItemKey\": null,\n      \"sku\": \"DISCOUNT CODE\",\n      \"name\": \"10% OFF\",\n      \"imageUrl\": null,\n      \"weight\": {\n        \"value\": 0,\n        \"units\": \"ounces\"\n      },\n      \"quantity\": 1,\n      \"unitPrice\": -20.55,\n      \"taxAmount\": null,\n      \"shippingAmount\": null,\n      \"warehouseLocation\": null,\n      \"options\": [],\n      \"productId\": 123456,\n      \"fulfillmentSku\": \"SKU-Discount\",\n      \"adjustment\": true,\n      \"upc\": null\n    }\n  ],\n  \"amountPaid\": 218.73,\n  \"taxAmount\": 5,\n  \"shippingAmount\": 10,\n  \"customerNotes\": \"Please ship as soon as possible!\",\n  \"internalNotes\": \"Customer called and would like to upgrade shipping\",\n  \"gift\": true,\n  \"giftMessage\": \"Thank you!\",\n  \"paymentMethod\": \"Credit Card\",\n  \"requestedShippingService\": \"Priority Mail\",\n  \"carrierCode\": \"fedex\",\n  \"serviceCode\": \"fedex_2day\",\n  \"packageCode\": \"package\",\n  \"confirmation\": \"delivery\",\n  \"shipDate\": \"2015-07-02\",\n  \"weight\": {\n    \"value\": 25,\n    \"units\": \"ounces\"\n  },\n  \"dimensions\": {\n    \"units\": \"inches\",\n    \"length\": 7,\n    \"width\": 5,\n    \"height\": 6\n  },\n  \"insuranceOptions\": {\n    \"provider\": \"carrier\",\n    \"insureShipment\": true,\n    \"insuredValue\": 200\n  },\n  \"internationalOptions\": {\n    \"contents\": null,\n    \"customsItems\": null\n  },\n  \"advancedOptions\": {\n    \"warehouseId\": 98765,\n    \"nonMachinable\": false,\n    \"saturdayDelivery\": false,\n    \"containsAlcohol\": false,\n    \"mergedOrSplit\": false,\n    \"mergedIds\": [],\n    \"parentId\": null,\n    \"storeId\": 12345,\n    \"customField1\": \"Custom data that you can add to an order. See Custom Field #2 & #3 for more info!\",\n    \"customField2\": \"Per UI settings, this information can appear on some carrier's shipping labels. See link below\",\n    \"customField3\": \"https://help.shipstation.com/hc/en-us/articles/206639957\",\n    \"source\": \"Webstore\",\n    \"billToParty\": null,\n    \"billToAccount\": null,\n    \"billToPostalCode\": null,\n    \"billToCountryCode\": null\n  },\n  \"tagIds\": [\n    53974\n  ]\n}", ParameterType.RequestBody);
            request.AddParameter("application/json", JsonConvert.SerializeObject(order, new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var excute = response.StatusCode == HttpStatusCode.OK ? 1 : 0;
            Console.WriteLine(response.Content);
            return excute;
        }

        public ResponseResult<OrderDto> CreateOrUpdateOrder(OrderDto order)
        {
            var result = new ResponseResult<OrderDto>();
            result.Data = new OrderDto();

            var client = new RestClient("https://ssapi.shipstation.com/orders/createorder");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Host", "ssapi.shipstation.com");
            request.AddHeader("Authorization", Authorization);
            request.AddHeader("Content-Type", "application/json");
            //request.AddParameter("application/json", "{\n  \"orderNumber\": \"TEST-ORDER-API-DOCS\",\n  \"orderKey\": \"0f6bec18-3e89-4881-83aa-f392d84f4c74\",\n  \"orderDate\": \"2015-06-29T08:46:27.0000000\",\n  \"paymentDate\": \"2015-06-29T08:46:27.0000000\",\n  \"shipByDate\": \"2015-07-05T00:00:00.0000000\",\n  \"orderStatus\": \"awaiting_shipment\",\n  \"customerId\": 37701499,\n  \"customerUsername\": \"headhoncho@whitehouse.gov\",\n  \"customerEmail\": \"headhoncho@whitehouse.gov\",\n  \"billTo\": {\n    \"name\": \"The President\",\n    \"company\": null,\n    \"street1\": null,\n    \"street2\": null,\n    \"street3\": null,\n    \"city\": null,\n    \"state\": null,\n    \"postalCode\": null,\n    \"country\": null,\n    \"phone\": null,\n    \"residential\": null\n  },\n  \"shipTo\": {\n    \"name\": \"The President\",\n    \"company\": \"US Govt\",\n    \"street1\": \"1600 Pennsylvania Ave\",\n    \"street2\": \"Oval Office\",\n    \"street3\": null,\n    \"city\": \"Washington\",\n    \"state\": \"DC\",\n    \"postalCode\": \"20500\",\n    \"country\": \"US\",\n    \"phone\": \"555-555-5555\",\n    \"residential\": true\n  },\n  \"items\": [\n    {\n      \"lineItemKey\": \"vd08-MSLbtx\",\n      \"sku\": \"ABC123\",\n      \"name\": \"Test item #1\",\n      \"imageUrl\": null,\n      \"weight\": {\n        \"value\": 24,\n        \"units\": \"ounces\"\n      },\n      \"quantity\": 2,\n      \"unitPrice\": 99.99,\n      \"taxAmount\": 2.5,\n      \"shippingAmount\": 5,\n      \"warehouseLocation\": \"Aisle 1, Bin 7\",\n      \"options\": [\n        {\n          \"name\": \"Size\",\n          \"value\": \"Large\"\n        }\n      ],\n      \"productId\": 123456,\n      \"fulfillmentSku\": null,\n      \"adjustment\": false,\n      \"upc\": \"32-65-98\"\n    },\n    {\n      \"lineItemKey\": null,\n      \"sku\": \"DISCOUNT CODE\",\n      \"name\": \"10% OFF\",\n      \"imageUrl\": null,\n      \"weight\": {\n        \"value\": 0,\n        \"units\": \"ounces\"\n      },\n      \"quantity\": 1,\n      \"unitPrice\": -20.55,\n      \"taxAmount\": null,\n      \"shippingAmount\": null,\n      \"warehouseLocation\": null,\n      \"options\": [],\n      \"productId\": 123456,\n      \"fulfillmentSku\": \"SKU-Discount\",\n      \"adjustment\": true,\n      \"upc\": null\n    }\n  ],\n  \"amountPaid\": 218.73,\n  \"taxAmount\": 5,\n  \"shippingAmount\": 10,\n  \"customerNotes\": \"Please ship as soon as possible!\",\n  \"internalNotes\": \"Customer called and would like to upgrade shipping\",\n  \"gift\": true,\n  \"giftMessage\": \"Thank you!\",\n  \"paymentMethod\": \"Credit Card\",\n  \"requestedShippingService\": \"Priority Mail\",\n  \"carrierCode\": \"fedex\",\n  \"serviceCode\": \"fedex_2day\",\n  \"packageCode\": \"package\",\n  \"confirmation\": \"delivery\",\n  \"shipDate\": \"2015-07-02\",\n  \"weight\": {\n    \"value\": 25,\n    \"units\": \"ounces\"\n  },\n  \"dimensions\": {\n    \"units\": \"inches\",\n    \"length\": 7,\n    \"width\": 5,\n    \"height\": 6\n  },\n  \"insuranceOptions\": {\n    \"provider\": \"carrier\",\n    \"insureShipment\": true,\n    \"insuredValue\": 200\n  },\n  \"internationalOptions\": {\n    \"contents\": null,\n    \"customsItems\": null\n  },\n  \"advancedOptions\": {\n    \"warehouseId\": 98765,\n    \"nonMachinable\": false,\n    \"saturdayDelivery\": false,\n    \"containsAlcohol\": false,\n    \"mergedOrSplit\": false,\n    \"mergedIds\": [],\n    \"parentId\": null,\n    \"storeId\": 12345,\n    \"customField1\": \"Custom data that you can add to an order. See Custom Field #2 & #3 for more info!\",\n    \"customField2\": \"Per UI settings, this information can appear on some carrier's shipping labels. See link below\",\n    \"customField3\": \"https://help.shipstation.com/hc/en-us/articles/206639957\",\n    \"source\": \"Webstore\",\n    \"billToParty\": null,\n    \"billToAccount\": null,\n    \"billToPostalCode\": null,\n    \"billToCountryCode\": null\n  },\n  \"tagIds\": [\n    53974\n  ]\n}", ParameterType.RequestBody);
            request.AddParameter("application/json", JsonConvert.SerializeObject(order, new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                result.Success = true;
                result.Code = 0;
                result.Msg = "";
                result.Data = JsonConvert.DeserializeObject<OrderDto>(response.Content);
            }
            else
            {
                result.Msg = response.ErrorMessage ?? "导出订单失败";
            }

            //Console.WriteLine(response.Content);


            return result;
        }

        public int CreateOrUpdateMultipleOrder(List<OrderDto> orderList)
        {
            var client = new RestClient("https://ssapi.shipstation.com/orders/createorder");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Host", "ssapi.shipstation.com");
            request.AddHeader("Authorization", Authorization);
            request.AddHeader("Content-Type", "application/json");
            //request.AddParameter("application/json", "{\n  \"orderNumber\": \"TEST-ORDER-API-DOCS\",\n  \"orderKey\": \"0f6bec18-3e89-4881-83aa-f392d84f4c74\",\n  \"orderDate\": \"2015-06-29T08:46:27.0000000\",\n  \"paymentDate\": \"2015-06-29T08:46:27.0000000\",\n  \"shipByDate\": \"2015-07-05T00:00:00.0000000\",\n  \"orderStatus\": \"awaiting_shipment\",\n  \"customerId\": 37701499,\n  \"customerUsername\": \"headhoncho@whitehouse.gov\",\n  \"customerEmail\": \"headhoncho@whitehouse.gov\",\n  \"billTo\": {\n    \"name\": \"The President\",\n    \"company\": null,\n    \"street1\": null,\n    \"street2\": null,\n    \"street3\": null,\n    \"city\": null,\n    \"state\": null,\n    \"postalCode\": null,\n    \"country\": null,\n    \"phone\": null,\n    \"residential\": null\n  },\n  \"shipTo\": {\n    \"name\": \"The President\",\n    \"company\": \"US Govt\",\n    \"street1\": \"1600 Pennsylvania Ave\",\n    \"street2\": \"Oval Office\",\n    \"street3\": null,\n    \"city\": \"Washington\",\n    \"state\": \"DC\",\n    \"postalCode\": \"20500\",\n    \"country\": \"US\",\n    \"phone\": \"555-555-5555\",\n    \"residential\": true\n  },\n  \"items\": [\n    {\n      \"lineItemKey\": \"vd08-MSLbtx\",\n      \"sku\": \"ABC123\",\n      \"name\": \"Test item #1\",\n      \"imageUrl\": null,\n      \"weight\": {\n        \"value\": 24,\n        \"units\": \"ounces\"\n      },\n      \"quantity\": 2,\n      \"unitPrice\": 99.99,\n      \"taxAmount\": 2.5,\n      \"shippingAmount\": 5,\n      \"warehouseLocation\": \"Aisle 1, Bin 7\",\n      \"options\": [\n        {\n          \"name\": \"Size\",\n          \"value\": \"Large\"\n        }\n      ],\n      \"productId\": 123456,\n      \"fulfillmentSku\": null,\n      \"adjustment\": false,\n      \"upc\": \"32-65-98\"\n    },\n    {\n      \"lineItemKey\": null,\n      \"sku\": \"DISCOUNT CODE\",\n      \"name\": \"10% OFF\",\n      \"imageUrl\": null,\n      \"weight\": {\n        \"value\": 0,\n        \"units\": \"ounces\"\n      },\n      \"quantity\": 1,\n      \"unitPrice\": -20.55,\n      \"taxAmount\": null,\n      \"shippingAmount\": null,\n      \"warehouseLocation\": null,\n      \"options\": [],\n      \"productId\": 123456,\n      \"fulfillmentSku\": \"SKU-Discount\",\n      \"adjustment\": true,\n      \"upc\": null\n    }\n  ],\n  \"amountPaid\": 218.73,\n  \"taxAmount\": 5,\n  \"shippingAmount\": 10,\n  \"customerNotes\": \"Please ship as soon as possible!\",\n  \"internalNotes\": \"Customer called and would like to upgrade shipping\",\n  \"gift\": true,\n  \"giftMessage\": \"Thank you!\",\n  \"paymentMethod\": \"Credit Card\",\n  \"requestedShippingService\": \"Priority Mail\",\n  \"carrierCode\": \"fedex\",\n  \"serviceCode\": \"fedex_2day\",\n  \"packageCode\": \"package\",\n  \"confirmation\": \"delivery\",\n  \"shipDate\": \"2015-07-02\",\n  \"weight\": {\n    \"value\": 25,\n    \"units\": \"ounces\"\n  },\n  \"dimensions\": {\n    \"units\": \"inches\",\n    \"length\": 7,\n    \"width\": 5,\n    \"height\": 6\n  },\n  \"insuranceOptions\": {\n    \"provider\": \"carrier\",\n    \"insureShipment\": true,\n    \"insuredValue\": 200\n  },\n  \"internationalOptions\": {\n    \"contents\": null,\n    \"customsItems\": null\n  },\n  \"advancedOptions\": {\n    \"warehouseId\": 98765,\n    \"nonMachinable\": false,\n    \"saturdayDelivery\": false,\n    \"containsAlcohol\": false,\n    \"mergedOrSplit\": false,\n    \"mergedIds\": [],\n    \"parentId\": null,\n    \"storeId\": 12345,\n    \"customField1\": \"Custom data that you can add to an order. See Custom Field #2 & #3 for more info!\",\n    \"customField2\": \"Per UI settings, this information can appear on some carrier's shipping labels. See link below\",\n    \"customField3\": \"https://help.shipstation.com/hc/en-us/articles/206639957\",\n    \"source\": \"Webstore\",\n    \"billToParty\": null,\n    \"billToAccount\": null,\n    \"billToPostalCode\": null,\n    \"billToCountryCode\": null\n  },\n  \"tagIds\": [\n    53974\n  ]\n}", ParameterType.RequestBody);
            request.AddParameter("application/json", JsonConvert.SerializeObject(orderList, new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var excute = response.StatusCode == HttpStatusCode.OK ? 1 : 0;
            Console.WriteLine(response.Content);
            return excute;
        }

        /// <summary>
        /// 获取订单
        /// </summary>
        public void GetOrder(long orderId)
        {

            var client = new RestClient("https://ssapi.shipstation.com/orders?orderId=" + orderId);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Host", "ssapi.shipstation.com");
            request.AddHeader("Authorization", Authorization);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content);
        }

        public ResponseResult<OrderDto> GetOrder(string orderId)
        {
            var result = new ResponseResult<OrderDto>();
            result.Data = new OrderDto();

            var client = new RestClient("https://ssapi.shipstation.com/orders/" + orderId);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Host", "ssapi.shipstation.com");
            request.AddHeader("Authorization", Authorization);
            IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.StatusCode);
            //Console.WriteLine(response.Content);
            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                result.Success = true;
                result.Code = 0;
                result.Msg = "";
                result.Data = JsonConvert.DeserializeObject<OrderDto>(response.Content);
            }
            else
            {
                result.Msg = response.ErrorMessage ?? "获取订单失败";
            }

            return result;
        }

        public ResponseResult<OrderListResponse> GetOrderList(OrderListRequestDto dto)
        {
            Console.WriteLine("request dto modifyDateStart: " + dto.modifyDateStart);
            var result = new ResponseResult<OrderListResponse>();
            result.Data = new OrderListResponse();

            var client = new RestClient("https://ssapi.shipstation.com/orders");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Host", "ssapi.shipstation.com");
            request.AddHeader("Authorization", Authorization);
            request.Timeout = 30 * 1000;//请求超时时间
            request.ReadWriteTimeout = 30 * 1000;//数据下载、处理时间


            foreach (PropertyInfo p in dto.GetType().GetProperties())
            {
                if (!string.IsNullOrWhiteSpace(p.GetValue(dto)?.ToString()))
                {
                    request.AddParameter(p.Name, p.GetValue(dto), ParameterType.QueryString);
                }
            }
            Stopwatch st = new Stopwatch();
            st.Start();
            IRestResponse response = client.Execute(request);
            st.Stop();
            Console.WriteLine("Get Order time: " + st.Elapsed.TotalSeconds.ToString());
            Console.WriteLine("Get Response code: " + response.StatusCode);
            //Console.WriteLine("Response content: " + response.Content);
            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                result.Success = true;
                result.Code = 0;
                result.Msg = "";
                result.Data = JsonConvert.DeserializeObject<OrderListResponse>(response.Content);
                Console.WriteLine("Get Order Result: ");
                int i = 1;
                foreach (var item in result.Data.Orders)
                {
                    Console.WriteLine("order " + i + ": " + item.OrderNumber);
                    i++;
                }
            }
            else
            {
                result.Msg = response.ErrorMessage ?? "获取订单列表失败";
            }

            return result;
        }

        public ResponseResult<string> SaveOrders(List<OrderDto> orders)
        {
            var result = new ResponseResult<string>();


            var client = new RestClient("http://localhost:58092/rest/ssorder/save");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(orders), ParameterType.RequestBody);
            request.Timeout = 30 * 1000;//请求超时时间
            request.ReadWriteTimeout = 30 * 1000;//数据下载、处理时间

            Stopwatch st = new Stopwatch();
            st.Start();
            IRestResponse response = client.Execute(request);
            st.Stop();
            Console.WriteLine("Save Order time: " + st.Elapsed.TotalSeconds.ToString());
            Console.WriteLine("Save Response code: " + response.StatusCode);
            //Console.WriteLine(response.Content);
            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                result.Success = true;
                result.Code = 0;
                result.Msg = "";

            }
            else
            {
                result.Msg = response.ErrorMessage ?? "保存订单失败";
            }

            return result;

        }
        /// <summary>
        /// 标记订单已发货
        /// </summary>
        public void MarkOrderShipped()
        {
            var client = new RestClient("https://ssapi.shipstation.com/orders/markasshipped");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Host", "ssapi.shipstation.com");
            request.AddHeader("Authorization", Authorization);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\n  \"orderId\": 93348442,\n  \"carrierCode\": \"usps\",\n  \"shipDate\": \"2014-04-01\",\n  \"trackingNumber\": \"913492493294329421\",\n  \"notifyCustomer\": true,\n  \"notifySalesChannel\": true\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }
        /// <summary>
        /// 取消订单
        /// </summary>
        public void CancelOrder(OrderDto order)
        {
            if (order.OrderStatus != "shipped" && order.OrderStatus != "cancelled")
            {
                return;
            }
            order.OrderStatus = "cancelled";
            var client = new RestClient("https://ssapi.shipstation.com/orders/createorder");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Host", "ssapi.shipstation.com");
            request.AddHeader("Authorization", Authorization);
            request.AddHeader("Content-Type", "application/json");
            //request.AddParameter("application/json", "{\n  \"orderNumber\": \"TEST-ORDER-API-DOCS\",\n  \"orderKey\": \"0f6bec18-3e89-4881-83aa-f392d84f4c74\",\n  \"orderDate\": \"2015-06-29T08:46:27.0000000\",\n  \"paymentDate\": \"2015-06-29T08:46:27.0000000\",\n  \"shipByDate\": \"2015-07-05T00:00:00.0000000\",\n  \"orderStatus\": \"awaiting_shipment\",\n  \"customerId\": 37701499,\n  \"customerUsername\": \"headhoncho@whitehouse.gov\",\n  \"customerEmail\": \"headhoncho@whitehouse.gov\",\n  \"billTo\": {\n    \"name\": \"The President\",\n    \"company\": null,\n    \"street1\": null,\n    \"street2\": null,\n    \"street3\": null,\n    \"city\": null,\n    \"state\": null,\n    \"postalCode\": null,\n    \"country\": null,\n    \"phone\": null,\n    \"residential\": null\n  },\n  \"shipTo\": {\n    \"name\": \"The President\",\n    \"company\": \"US Govt\",\n    \"street1\": \"1600 Pennsylvania Ave\",\n    \"street2\": \"Oval Office\",\n    \"street3\": null,\n    \"city\": \"Washington\",\n    \"state\": \"DC\",\n    \"postalCode\": \"20500\",\n    \"country\": \"US\",\n    \"phone\": \"555-555-5555\",\n    \"residential\": true\n  },\n  \"items\": [\n    {\n      \"lineItemKey\": \"vd08-MSLbtx\",\n      \"sku\": \"ABC123\",\n      \"name\": \"Test item #1\",\n      \"imageUrl\": null,\n      \"weight\": {\n        \"value\": 24,\n        \"units\": \"ounces\"\n      },\n      \"quantity\": 2,\n      \"unitPrice\": 99.99,\n      \"taxAmount\": 2.5,\n      \"shippingAmount\": 5,\n      \"warehouseLocation\": \"Aisle 1, Bin 7\",\n      \"options\": [\n        {\n          \"name\": \"Size\",\n          \"value\": \"Large\"\n        }\n      ],\n      \"productId\": 123456,\n      \"fulfillmentSku\": null,\n      \"adjustment\": false,\n      \"upc\": \"32-65-98\"\n    },\n    {\n      \"lineItemKey\": null,\n      \"sku\": \"DISCOUNT CODE\",\n      \"name\": \"10% OFF\",\n      \"imageUrl\": null,\n      \"weight\": {\n        \"value\": 0,\n        \"units\": \"ounces\"\n      },\n      \"quantity\": 1,\n      \"unitPrice\": -20.55,\n      \"taxAmount\": null,\n      \"shippingAmount\": null,\n      \"warehouseLocation\": null,\n      \"options\": [],\n      \"productId\": 123456,\n      \"fulfillmentSku\": \"SKU-Discount\",\n      \"adjustment\": true,\n      \"upc\": null\n    }\n  ],\n  \"amountPaid\": 218.73,\n  \"taxAmount\": 5,\n  \"shippingAmount\": 10,\n  \"customerNotes\": \"Please ship as soon as possible!\",\n  \"internalNotes\": \"Customer called and would like to upgrade shipping\",\n  \"gift\": true,\n  \"giftMessage\": \"Thank you!\",\n  \"paymentMethod\": \"Credit Card\",\n  \"requestedShippingService\": \"Priority Mail\",\n  \"carrierCode\": \"fedex\",\n  \"serviceCode\": \"fedex_2day\",\n  \"packageCode\": \"package\",\n  \"confirmation\": \"delivery\",\n  \"shipDate\": \"2015-07-02\",\n  \"weight\": {\n    \"value\": 25,\n    \"units\": \"ounces\"\n  },\n  \"dimensions\": {\n    \"units\": \"inches\",\n    \"length\": 7,\n    \"width\": 5,\n    \"height\": 6\n  },\n  \"insuranceOptions\": {\n    \"provider\": \"carrier\",\n    \"insureShipment\": true,\n    \"insuredValue\": 200\n  },\n  \"internationalOptions\": {\n    \"contents\": null,\n    \"customsItems\": null\n  },\n  \"advancedOptions\": {\n    \"warehouseId\": 98765,\n    \"nonMachinable\": false,\n    \"saturdayDelivery\": false,\n    \"containsAlcohol\": false,\n    \"mergedOrSplit\": false,\n    \"mergedIds\": [],\n    \"parentId\": null,\n    \"storeId\": 12345,\n    \"customField1\": \"Custom data that you can add to an order. See Custom Field #2 & #3 for more info!\",\n    \"customField2\": \"Per UI settings, this information can appear on some carrier's shipping labels. See link below\",\n    \"customField3\": \"https://help.shipstation.com/hc/en-us/articles/206639957\",\n    \"source\": \"Webstore\",\n    \"billToParty\": null,\n    \"billToAccount\": null,\n    \"billToPostalCode\": null,\n    \"billToCountryCode\": null\n  },\n  \"tagIds\": [\n    53974\n  ]\n}", ParameterType.RequestBody);
            request.AddParameter("application/json", JsonConvert.SerializeObject(order, new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }

        /// <summary>
        /// 锁定订单
        /// </summary>
        public void HoldOrder(long orderId, string holeUntilDate)
        {
            var order = new OrderDto()
            {

            };
            var client = new RestClient("https://ssapi.shipstation.com/orders/holduntil");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Host", "ssapi.shipstation.com");
            request.AddHeader("Authorization", Authorization);
            request.AddHeader("Content-Type", "application/json");
            //request.AddParameter("application/json", "{\n  \"orderId\": " + orderId + ",\n  \"holdUntilDate\": \"" + holeUntilDate + "\"\n}", ParameterType.RequestBody);
            request.AddParameter("application/json", JsonConvert.SerializeObject(new
            {
                orderId,
                holeUntilDate
            }, new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }
    }
}
