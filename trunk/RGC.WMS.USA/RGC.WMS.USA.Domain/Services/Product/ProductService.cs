using HuigeTec.Core.Helpers;
using Microsoft.Extensions.Options;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Product;
using RGC.WMS.USA.Domain.Entities.Product.Do;
using RGC.WMS.USA.Domain.Entities.Product.Enum;
using RGC.WMS.USA.Domain.Entities.Sku;
using RGC.WMS.USA.Domain.Repositories.Product;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace RGC.WMS.USA.Domain.Services.Product
{
    /// <summary>
    /// shane 2020/2/12
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepositories;
        private readonly IProductModifyFlowRepository _productModifyFlowRepositories;
        private readonly IOptions<DominBaseConfig> _configuration;

        public ProductService(
            IProductRepository productRepositories,
            IProductModifyFlowRepository productModifyFlowRepositories,
            IOptions<DominBaseConfig> configuration)
        {
            _productRepositories = productRepositories;
            _productModifyFlowRepositories = productModifyFlowRepositories;
            _configuration = configuration;
        }
        /// <summary>
        /// 创建产品
        /// shane 2020/2/11
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ResponseDo<string> CreateProduct(long loginId, ProductEditDo dto)
        {
            ResponseDo<string> result = new ResponseDo<string>();


            #region 检查产品重复

            if (string.IsNullOrWhiteSpace(dto.FactoryModel))
            {
                result.Code = 2;
                result.Msg = "工厂型号不能为空";
                result.Success = false;
                return result;
            }
            else
            {
                dto.FactoryModel = dto.FactoryModel.Trim();
            }
            if (string.IsNullOrWhiteSpace(dto.FullEName))
            {
                result.Code = 2;
                result.Msg = "英文名称不能为空";
                result.Success = false;
                return result;
            }
            else
            {
                dto.FullEName = dto.FullEName.Trim();
            }

            if (string.IsNullOrWhiteSpace(dto.ImageMain))
            {
                result.Code = 2;
                result.Msg = "展示图不能为空";
                result.Success = false;
                return result;
            }
            #endregion
            if (!string.IsNullOrWhiteSpace(dto.PackingSize))
            {
                dto.PackingSize = dto.PackingSize.Replace("undefined", "0");
            }
            if (!string.IsNullOrWhiteSpace(dto.ProductSize))
            {
                dto.ProductSize = dto.ProductSize.Replace("undefined", "0");

            }

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
                    dto.PrimaryImageSrc = "images/product/thumb/" + fileName;
                }
            }
            var categoryId = dto.FuncCategoryIds[dto.FuncCategoryIds.Count - 1];
            dto.Product.FuncCategoryId = categoryId;
            dto.Product.CreationTime = DateTime.Now;
            dto.Product.CreatorUserId = loginId;
            var skuInfo = _productRepositories.Add(dto);

            result.Data = dto.PrimaryImageSrc;
            result.Code = 0;
            result.Success = true;

            return result;
        }


        public ResponseDo<string> AddProductFlow(long loginId, ProductModifyFlow entity)
        {
            ResponseDo<string> result = new ResponseDo<string>();
            entity.CreatorUserId = loginId;
            var flow = _productModifyFlowRepositories.AddFlow(entity);
            result.Code = 0;
            result.Success = true;

            return result;
        }

        public ResponseDo<string> UpdateProductFlow(long loginId, ProductModifyFlow entity)
        {
            ResponseDo<string> result = new ResponseDo<string>();
            entity.LastModifierUserId = loginId;
            var flow = _productModifyFlowRepositories.UpdateFlow(entity);
            result.Code = 0;
            result.Success = true;

            return result;
        }

        /// <summary>
        /// 删除产品
        /// shane 2020/2/11
        /// <param name="loginId"></param>
        /// <param name="loginIP"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseDo<string> Delete(long loginId, long id)
        {
            ResponseDo<string> result = new ResponseDo<string>();

            var productIdList = new List<long>();
            int execute = _productRepositories.Delete(loginId, id);

            if (execute > 0)
            {
                result.Code = 0;
                result.Success = true;
            }
            else
            {
                result.Msg = "操作失败";
            }
            return result;
        }

        /// <summary>
        /// 恢复sku
        /// shane 2020/2/11
        /// <param name="loginId"></param>
        /// <param name="loginIP"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseDo<string> Recovery(long loginId, long id)
        {
            ResponseDo<string> result = new ResponseDo<string>();
            int excute = _productRepositories.Recovery(loginId, id);

            if (excute > 0)
            {
                result.Code = 0;
                result.Success = true;
            }
            else
            {
                result.Msg = "操作失败";
            }
            return result;
        }

        /// <summary>
        /// 修改主表
        /// <param name="sku"></param>
        /// <returns></returns>
        public ResponseDo<string> UpdateProduct(long loginId, ProductEditDo dto)
        {
            ResponseDo<string> result = new ResponseDo<string>();

            if (string.IsNullOrWhiteSpace(dto.FactoryModel))
            {
                result.Code = 2;
                result.Msg = "工厂型号不能为空";
                result.Success = false;
                return result;
            }
            else
            {
                dto.FactoryModel = dto.FactoryModel.Trim();
            }
            if (string.IsNullOrWhiteSpace(dto.FullEName))
            {
                result.Code = 2;
                result.Msg = "英文名称不能为空";
                result.Success = false;
                return result;
            }
            else
            {
                dto.FullEName = dto.FullEName.Trim();
            }
            if (!_productRepositories.IfExistProduct(dto.FactoryModel))
            {
                result.Code = 2;
                result.Msg = "产品型号重复";
                result.Success = false;
                return result;
            }
            if (!string.IsNullOrWhiteSpace(dto.PackingSize))
            {
                dto.PackingSize = dto.PackingSize.Replace("undefined", "0");
            }
            if (!string.IsNullOrWhiteSpace(dto.ProductSize))
            {
                dto.ProductSize = dto.ProductSize.Replace("undefined", "0");

            }

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
                    dto.PrimaryImageSrc = "images/product/thumb/" + fileName;
                }
                //删除原图
                //if (path != null)
                //{
                //    if (System.IO.File.Exists(path))
                //    {
                //        System.IO.File.Delete(path);
                //    }
                //}
            }

            //int syncExcute = 0;
            var categoryId = dto.FuncCategoryIds[dto.FuncCategoryIds.Count - 1];
            dto.Product.FuncCategoryId = categoryId;
            dto.Product.LastModificationTime = DateTime.Now;
            dto.Product.LastModifierUserId = loginId;
            dto.Product.PrimaryImageSrc = dto.PrimaryImageSrc;

            int excute = _productRepositories.SingleUpdate(dto);
            if (excute > 0)
            {
                excute = 0;
                result.Code = 0;
                result.Success = true;



                //if (syncExcute > 0)
                //{

                //}
                //else
                //{
                //    result.Msg = "保存成功,同步产品资料失败";
                //}
            }
            else
            {
                result.Msg = "操作失败";
            }



            return result;
        }

        /// <summary>
        /// 单独获取产品
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseDo<ProductDo> Get(long id)
        {
            ResponseDo<ProductDo> result = new ResponseDo<ProductDo>();
            result.Data = new ProductDo();
            var product = _productRepositories.Get(id);
            result.Data.product = product;

            var packageList = product.PackageDict.Values;
            var packageListIds = packageList.OrderBy(p => p.SeqNo).Select(p => p.Id).ToList();
            var partIds = product.PartsDict.Values.Where(p => !p.IsDeleted).OrderBy(p => p.SeqNo).Select(p => p.PackageId).Distinct().ToList();
            partIds = packageListIds.Where(p => partIds.Contains(p)).ToList();
            foreach (var part in partIds)
            {
                var temp = new ProductPackageDetail();
                temp = packageList.FirstOrDefault(p => p.Id == part);
                if (temp != null)
                {
                    var model = new ProductComponentTreeDo();
                    model.ConfigId = temp.ConfigId;
                    model.EName = temp.EName;
                    model.SeqNo = temp.SeqNo;
                    model.Id = temp.Id;
                    model.Type = temp.Type;
                    model.CName = temp.CName;
                    product.PartsDict.Values.Where(p => !p.IsDeleted).OrderBy(p => p.SeqNo).ToList().ForEach(item =>
                    {
                        if (item.PackageId == part)
                            model.Children.Add(item);
                    });
                    result.Data.PartChildren.Add(model);
                }

            }
            var fittingIds = product.FittingDict.Values.Where(p => !p.IsDeleted).OrderBy(p => p.SeqNo).Select(p => p.PackageId).Distinct().ToList();
            fittingIds = packageListIds.Where(p => fittingIds.Contains(p)).ToList();
            foreach (var fitting in fittingIds)
            {
                var temp = new ProductPackageDetail();
                temp = packageList.FirstOrDefault(p => p.Id == fitting);
                if (temp != null)
                {
                    var model = new ProductComponentTreeDo();
                    model.ConfigId = temp.ConfigId;
                    model.EName = temp.EName;
                    model.SeqNo = temp.SeqNo;
                    model.Id = temp.Id;
                    model.Type = temp.Type;
                    model.CName = temp.CName;
                    product.FittingDict.Values.Where(p => !p.IsDeleted).OrderBy(p => p.SeqNo).ToList().ForEach(item =>
                    {
                        if (item.PackageId == fitting)
                        {
                            item.detailList = item.detailList.Where(p => !p.IsDeleted).OrderBy(p => p.SeqNo).ToList();
                            model.Children.Add(item);
                        }

                    });
                    result.Data.FittingChildren.Add(model);
                }

            }

            if (result.Data != null)
            {
                result.Code = 0;
                result.Success = true;
            }
            return result;
        }

        /// <summary>
        /// 产品分类
        /// </summary>
        /// <param name="skuId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public ResponseDo<string> AddCategory(long productId, long categoryId)
        {
            ResponseDo<string> result = new ResponseDo<string>();
            int execute = _productRepositories.AddCategory(productId, categoryId);

            if (execute > 0)
            {
                result.Code = 0;
                result.Success = true;
            }
            else
            {
                result.Msg = "操作失败";
            }
            return result;
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        public ResponseDo<string> UpdateStatus(long id, ProductStatus status, long modifierUserId)
        {
            ResponseDo<string> result = new ResponseDo<string>();

            int excute = _productRepositories.UpdateStatus(id, status, modifierUserId);
            int syncExcute = 0;
            if (excute > 0)
            {
                result.Code = 0;
                result.Success = true;
            }
            else
            {
                result.Msg = "操作失败";
            }
            return result;
        }

        public ResponseDo<string> UpdateFlowStatus(long userId, long flowId)
        {
            ResponseDo<string> result = new ResponseDo<string>();
            var excute = _productModifyFlowRepositories.UpdateSyncStatus(userId, flowId);
            if (excute > 0)
            {
                result.Code = 0;
                result.Success = true;
            }
            return result;

        }
    }
}
