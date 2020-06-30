using HuigeTec.Core.Helpers;
using Microsoft.Extensions.Options;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Bidding;
using RGC.WMS.USA.Domain.Entities.Bidding.Do;
using RGC.WMS.USA.Domain.Repositories.Bidding;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace RGC.WMS.USA.Domain.Services.Bidding
{
    public class CompetitionService : ICompetitionService
    {
        private readonly ICompetitionRepository _competitionRepository;
        private readonly IOptions<DominBaseConfig> _configuration;

        public CompetitionService(ICompetitionRepository competitionRepository,
            IOptions<DominBaseConfig> configuration)
        {
            _competitionRepository = competitionRepository;
            _configuration = configuration;
        }
        public ResponseDo<string> Create(long loginId, CompetitionEditDo dto)
        {
            ResponseDo<string> result = new ResponseDo<string>();

            #region 检查参数

            if (string.IsNullOrWhiteSpace(dto.FactoryModel))
            {
                result.Code = 2;
                result.Msg = "型号不能为空";
                result.Success = false;
                return result;
            }
            else
            {
                dto.FactoryModel = dto.FactoryModel.Trim();
            }
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                //result.code = 2;
                //result.msg = "产品名称不能为空";
                //result.success = false;
                //return result;
            }
            else
            {
                dto.Name = dto.Name.Trim();
            }

            if (string.IsNullOrWhiteSpace(dto.ImageMain) && string.IsNullOrWhiteSpace(dto.Src))
            {
                //result.code = 2;
                //result.msg = "展示图不能为空";
                //result.success = false;
                //return result;
            }
            if (dto.PlatformId == 0)
            {
                result.Code = 2;
                result.Msg = "平台不能为空";
                result.Success = false;
                return result;
            }

            #endregion
            dto.CreatorUserId = loginId;
            dto.CreationTime = DateTime.Now;




            if (!string.IsNullOrWhiteSpace(dto.ImageMain))
            {
                FileInfo file = new FileInfo(dto.ImageMain);
                var dir = _configuration.Value.SystemRootAddress + @"images\product\"; //保存目录

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                if (file.Exists)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ImageMain);//shane 2019/3/27
                    string savePath = Path.Combine(dir, fileName);
                    var image = Image.FromFile(dto.ImageMain);
                    image.Save(savePath, ImageFormat.Jpeg);
                    ImgHelper.SaveImage(image, dir + "thumb", fileName, 320, 100);
                    dto.Src = "images/item/thumb/" + fileName;
                }
            }
            var model = new Competition()
            {
                Id=dto.Id,
                BrandName=dto.BrandName,
                FactoryModel=dto.FactoryModel,
                Name=dto.Name,
                IsValid=dto.IsValid,
                PlatformId=dto.PlatformId,
                PlatformUrl=dto.PlatformUrl,
                ProductId=dto.ProductId,
                Remarks=dto.Remarks,
                ItemId=dto.ItemId,
                Src=dto.Src,
                RetailPrice=dto.RetailPrice,
                Status=dto.Status,
                UniqueId=dto.UniqueId

            };
            var skuInfo = _competitionRepository.SingleAdd(model);

            result.Data = dto.Src;
            result.Code = 0;
            result.Success = true;
            return result;
        }

        public ResponseDo<string> IfExist(long platformId, long compId, string factoryModel)
        {
            ResponseDo<string> result = new ResponseDo<string>();

            var CompetitionList = _competitionRepository.GetAllList();
            if (CompetitionList.Where(p => p.FactoryModel.ToLower() == factoryModel.ToLower().Trim() && p.PlatformId == platformId &&(compId>0? p.Id != compId:true)).Any())
            {
                result.Code = 1;
                result.Msg = "本产品在该平台已存在此型号产品";
                result.Success = false;
                return result;
            }
            result.Success = true;
            return result;
        }

        public ResponseDo<string> Update(long loginId, CompetitionEditDo dto)
        {
            ResponseDo<string> result = new ResponseDo<string>();




            dto.LastModifierUserId = loginId;
            dto.LastModificationTime = DateTime.Now;


            if (!string.IsNullOrWhiteSpace(dto.ImageMain))
            {
                FileInfo file = new FileInfo(dto.ImageMain);
                var dir = _configuration.Value.SystemRootAddress + @"images\product\"; //保存目录

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                if (file.Exists)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ImageMain);//shane 2019/3/27
                    string savePath = Path.Combine(dir, fileName);
                    var image = Image.FromFile(dto.ImageMain);
                    image.Save(savePath, ImageFormat.Jpeg);
                    ImgHelper.SaveImage(image, dir + "thumb", fileName, 320, 100);
                    dto.Src = "images/item/thumb/" + fileName;
                }
            }
            var model = new Competition()
            {
                Id = dto.Id,
                BrandName = dto.BrandName,
                FactoryModel = dto.FactoryModel,
                Name = dto.Name,
                IsValid = dto.IsValid,
                PlatformId = dto.PlatformId,
                PlatformUrl = dto.PlatformUrl,
                ProductId = dto.ProductId,
                Remarks = dto.Remarks,
                ItemId = dto.ItemId,
                Src = dto.Src,
                RetailPrice = dto.RetailPrice,
                Status = dto.Status,
                UniqueId = dto.UniqueId

            };


            int excute = _competitionRepository.SingleUpdate(model);
            if (excute > 0)
            {
                excute = 0;
                result.Code = 0;
                result.Success = true;
            }
            else
            {
                result.Msg = "操作失败";
            }



            return result;
        }

        public ResponsePageDo<(string value, long id)> GetSearchList(string key)
        {
            ResponsePageDo<(string value, long id)> result = new ResponsePageDo<(string value, long id)>();
            result.Data = new List<(string value, long id)>();
            var data = _competitionRepository.SearchPageQuery(key);
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
