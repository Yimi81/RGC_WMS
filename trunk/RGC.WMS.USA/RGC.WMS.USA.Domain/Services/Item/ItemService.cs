using HuigeTec.Core.Helpers;
using Microsoft.Extensions.Options;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Item;
using RGC.WMS.USA.Domain.Entities.Item.Do;
using RGC.WMS.USA.Domain.Repositories.Item;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace RGC.WMS.USA.Domain.Services.Item
{
    /// <summary>
    /// shane 2020/2/17
    /// </summary>
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IItemModifyFlowRepository _itemModifyFlowRepository;
        private readonly IItemPriceRecordRepository _itemPriceRecordRepository;
        private readonly IOptions<DominBaseConfig> _configuration;
        public ItemService(IItemRepository itemRepository,
            IItemPriceRecordRepository itemPriceRecordRepository,
            IItemModifyFlowRepository itemModifyFlowRepository,
            IOptions<DominBaseConfig> configuration)
        {
            _itemRepository = itemRepository;
            _itemPriceRecordRepository = itemPriceRecordRepository;
            _itemModifyFlowRepository = itemModifyFlowRepository;
            _configuration = configuration;
        }
        public ResponseDo<string> CreateItem(ItemEditDo obj)
        {
            ResponseDo<string> result = new ResponseDo<string>();

            if (!string.IsNullOrWhiteSpace(obj.ImageMain))
            {
                FileInfo file = new FileInfo(obj.ImageMain);
                var dir = _configuration.Value.SystemRootAddress + @"images\item\"; //保存目录

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                if (file.Exists)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(obj.ImageMain);
                    string savePath = Path.Combine(dir, fileName);
                    var image = Image.FromFile(obj.ImageMain);
                    image.Save(savePath, ImageFormat.Jpeg);
                    ImgHelper.SaveImage(image, dir + "thumb", fileName, 320, 100);
                    obj.Src = "images/item/thumb/" + fileName;
                }
            }

            var model = new ItemInfo
            {
                Id = obj.Id,
                SKU = obj.SKU,
                Src = obj.Src,
                Status = obj.Status,
                UniqueId = obj.UniqueId,
                UPC = obj.UPC,
                PlatformId = obj.PlatformId,
                FactoryModel = obj.FactoryModel,
                ItemName = obj.ItemName,
                IsSpecial = obj.IsSpecial,
                WholeSalePrice = obj.WholeSalePrice,
                PreRetailPrice = 0,
                Remarks = obj.Remarks,
                PlatformItemUrl = obj.PlatformItemUrl,
                PlatformName = obj.PlatformName,
                ProductId = obj.ProductId,
                RetailPrice = obj.RetailPrice,
                Msrp = obj.Msrp,
                Map = obj.Map,
                CreationTime = DateTime.Now,
                CreatorUserId = obj.CreatorUserId,
            };
            var excute = _itemRepository.Add(model, obj.ProductList);
            if (excute > 0)
            {
                result.Code = 0;
                result.Success = true;
            }
            return result;
        }
        public ResponseDo<string> AddItemFlow(ItemModifyFlow entity)
        {
            ResponseDo<string> result = new ResponseDo<string>();

            var excute = _itemModifyFlowRepository.AddFlow(entity);
            if (excute > 0)
            {
                result.Code = 0;
                result.Success = true;
            }
            return result;
        }

        public ResponseDo<string> Delete(long loginId, long id)
        {
            ResponseDo<string> result = new ResponseDo<string>();

            var excute = _itemRepository.Delete(loginId, id);
            if (excute > 0)
            {
                result.Code = 0;
                result.Success = true;
            }
            return result;
        }

        public ResponseDo<string> UpdateAndAddRecord(long adminId, long ItemId, string wholeSalePriceString, string retailPriceString)
        {
            ResponseDo<string> result = new ResponseDo<string>();
            var excute = _itemRepository.UpdateAndAddRecord(adminId, ItemId, ParseHelper.Yuan2Fen(wholeSalePriceString), ParseHelper.Yuan2Fen(retailPriceString));
            if (excute > 0)
            {
                result.Code = 0;
                result.Success = true;
            }
            return result;
        }

        public ResponseDo<string> PriceApplyAndAddRecord(long adminId, long ItemId, string retailPriceString, DateTime validTime, int level)
        {
            ResponseDo<string> result = new ResponseDo<string>();
            var item = _itemRepository.Get(ItemId);
            var model = new ItemPriceRecord()
            {
                ItemId = ItemId,
                Src = item.Src,
                FactoryModel = item.FactoryModel,
                RetailPrice = ParseHelper.Yuan2Fen(retailPriceString),
                ValidTime = validTime,
                ItemName = item.ItemName,
                ProductId = item.ProductId,
                PlatformId = item.PlatformId,
                PlatformName = item.PlatformName,
                CreatorUserId = adminId,
                CreationTime = DateTime.Now,
                Level = level,
                Status = 1
            };

            var excute = _itemPriceRecordRepository.Add(model);
            if (excute > 0)
            {
                result.Code = 0;
                result.Success = true;
            }
            return result;
        }

        /// <summary>
        /// shane 2020/2/21
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifierUserId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ResponseDo<string> UpdateItemPriceVertifyStatus(long id, long modifierUserId, int status)
        {
            ResponseDo<string> result = new ResponseDo<string>();
            var excute = 0;
            if (status == 0)
            {
                excute = _itemRepository.ApplyPriceAndUpdate(id, modifierUserId);
            }
            else
            {
                excute = _itemPriceRecordRepository.UpdateVertifyStatus(modifierUserId, id, status);
            }
            if (excute > 0)
            {
                result.Code = 0;
                result.Success = true;
            }
            return result;
        }

        public ResponseDo<string> UpdateItem(ItemEditDo obj)
        {
            ResponseDo<string> result = new ResponseDo<string>();
            if (!string.IsNullOrWhiteSpace(obj.ImageMain))
            {
                FileInfo file = new FileInfo(obj.ImageMain);
                var dir = _configuration.Value.SystemRootAddress + @"images\item\"; //保存目录

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                if (file.Exists)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(obj.ImageMain);//shane 2019/3/37
                    string savePath = Path.Combine(dir, fileName);

                    var image = Image.FromFile(obj.ImageMain);
                    image.Save(savePath, ImageFormat.Jpeg);
                    ImgHelper.SaveImage(image, dir + "thumb", fileName, 320, 100);
                    obj.Src = "images/item/thumb/" + fileName;
                }
            }
            var model = new ItemInfo
            {
                Id = obj.Id,
                SKU = obj.SKU,
                Src = obj.Src,
                Status = obj.Status,
                UniqueId = obj.UniqueId,
                UPC = obj.UPC,
                PlatformId = obj.PlatformId,
                FactoryModel = obj.FactoryModel,
                ItemName = obj.ItemName,
                IsSpecial = obj.IsSpecial,
                WholeSalePrice = obj.WholeSalePrice,
                PreRetailPrice = 0,
                Remarks = obj.Remarks,
                PlatformItemUrl = obj.PlatformItemUrl,
                PlatformName = obj.PlatformName,
                ProductId = obj.ProductId,
                RetailPrice = obj.RetailPrice,
                Msrp = obj.Msrp,
                Map = obj.Map,
                CreationTime = DateTime.Now,
                CreatorUserId = obj.CreatorUserId,
            };
            var excute = _itemRepository.SingleUpdate(model, obj.ProductList);
            if (excute > 0)
            {
                result.Code = 0;
                result.Success = true;
            }
            return result;
        }

        public ResponseDo<string> UpdatePlatformStatus(long loginId, long id, int status)
        {
            throw new NotImplementedException();
        }
        public ResponseDo<string> UpdateStatus(long id, long modifierUserId)
        {
            ResponseDo<string> result = new ResponseDo<string>();

            var excute = _itemRepository.UpdateStatus(id, modifierUserId);
            if (excute > 0)
            {
                result.Code = 0;
                result.Success = true;
            }
            return result;
        }
        public ResponsePageDo<(string value, long id)> GetItemSearchList(string key)
        {
            ResponsePageDo<(string value, long id)> result = new ResponsePageDo<(string value, long id)>();
            result.Data = new List<(string value, long id)>();
            var data = _itemRepository.SearchPageQuery(key);
            result.Data = data;
            if (result.Data != null && result.Data.Count >= 0)
            {
                result.Code = 0;
                result.Success = true;
            }
            return result;
        }

    }
}
