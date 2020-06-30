using HuigeTec.Core.Helpers;
using Microsoft.Extensions.Options;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Product;
using RGC.WMS.USA.Domain.Entities.Product.Do;
using RGC.WMS.USA.Domain.Repositories.Product;
using System;
using System.Drawing;
using System.IO;

namespace RGC.WMS.USA.Domain.Services.Product
{
    /// <summary>
    /// shane 2020/2/14
    /// </summary>
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IOptions<DominBaseConfig> _configuration;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IOptions<DominBaseConfig> configuration)
        {
            _productCategoryRepository = productCategoryRepository;
            _configuration = configuration;
        }
        public ResponseDo<string> Create(ProductCategoryCreateOrUpdateDo category, long loginId)
        {
            ResponseDo<string> result = new ResponseDo<string>();
            #region 校验
            category.ImageSrc = category.ImageSrc ?? "";

            if (string.IsNullOrWhiteSpace(category.EName))
            {
                result.Code = 2;
                result.Msg = "英文名为空";
                result.Success = false;
                return result;
            }
            else
            {
                category.EName = category.EName.Trim();
            }
            //if (DbProductCategory.ProductCategoryDict.Values.Count(p => p.IsShow == true && p.ParentId == 0) >= 8 && category.IsShow == true)
            //{
            //    result.Code = 1;
            //    result.Msg = "前台分类最多显示8个";
            //    result.Success = false;
            //    return result;
            //}
            #endregion

            #region 上传图片

            if (!string.IsNullOrWhiteSpace(category.byteStr))
            {
                var byteStr = category.byteStr;//data:image/jpg;base64,
                int delLength = byteStr.IndexOf(',') + 1;
                string ext = byteStr.Split('/')[1].Split(';')[0];
                if (ext.ToLower() == "jpeg") ext = "jpg";
                string str = byteStr.Substring(delLength, byteStr.Length - delLength);//转换成图片的base64不要data:image/jpg;base64,这个头文件

                string now = Path.Combine(DateTime.Now.Year.ToString("#"), DateTime.Today.ToString("yyyyMMdd"));
                string tmpDir = Path.Combine(@"images\category", now);//文件夹相对路径          
                string tmpRootDir = _configuration.Value.SystemRootAddress;//获取程序根目录             
                string tmpFullDir = Path.Combine(tmpRootDir, tmpDir);//完整路径         
                if (!Directory.Exists(tmpFullDir))//检查文件夹
                {
                    Directory.CreateDirectory(tmpFullDir);
                }
                string saveName = Guid.NewGuid().ToString() + "." + ext;
                string savePath = Path.Combine(tmpFullDir, saveName);

                Image returnImage = ImgHelper.Base64StringToImage(str);

                string message = "";
                bool isSave = ImgHelper.SavePicAndThumbnail(returnImage, tmpFullDir, saveName, 300, 80, out message);
                if (isSave == false)
                {
                    result.Msg = "图片保存失败";
                    return result;
                }

                category.ImageSrc = Path.Combine(tmpDir + @"\thumb", saveName).Replace(@"\", @"/");
            }

            #endregion

            var model = new ProductCategory()
            {
                byteStr = category.byteStr,
                CName = category.CName,
                EName = category.EName,
                SeqNo = category.SeqNo,
                Code = category.Code,
                CreationTime = DateTime.Now,
                CreatorUserId = loginId,
                ImageSrc = category.ImageSrc,
                IsShow = category.IsShow,
                ParentId = category.ParentId,
                Type = category.Type,
            };

            int excute = _productCategoryRepository.Add(model);

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

        public ResponseDo<string> Delete(long loginId,  long id)
        {
            ResponseDo<string> result = new ResponseDo<string>();
            int execute = _productCategoryRepository.Delete(loginId, id);

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

        public ResponseDo<string> Update(ProductCategoryCreateOrUpdateDo category, long loginId)
        {
            ResponseDo<string> result = new ResponseDo<string>();
            #region 校验
            category.ImageSrc = category.ImageSrc ?? "";

            if (string.IsNullOrWhiteSpace(category.EName))
            {
                result.Code = 2;
                result.Msg = "英文名为空";
                result.Success = false;
                return result;
            }
            else
            {
                category.EName = category.EName.Trim();
            }
            //if (DbProductCategory.ProductCategoryDict.Values.Count(p => p.IsShow == true && p.ParentId == 0) >= 8 && category.IsShow == true)
            //{
            //    result.Code = 1;
            //    result.Msg = "前台分类最多显示8个";
            //    result.Success = false;
            //    return result;
            //}
            #endregion

            #region 上传图片

            if (!string.IsNullOrWhiteSpace(category.byteStr))
            {
                var byteStr = category.byteStr;//data:image/jpg;base64,
                int delLength = byteStr.IndexOf(',') + 1;
                string ext = byteStr.Split('/')[1].Split(';')[0];
                if (ext.ToLower() == "jpeg") ext = "jpg";
                string str = byteStr.Substring(delLength, byteStr.Length - delLength);//转换成图片的base64不要data:image/jpg;base64,这个头文件

                string now = Path.Combine(DateTime.Now.Year.ToString("#"), DateTime.Today.ToString("yyyyMMdd"));
                string tmpDir = Path.Combine(@"images\category", now);//文件夹相对路径          
                string tmpRootDir = _configuration.Value.SystemRootAddress;//获取程序根目录             
                string tmpFullDir = Path.Combine(tmpRootDir, tmpDir);//完整路径         
                if (!Directory.Exists(tmpFullDir))//检查文件夹
                {
                    Directory.CreateDirectory(tmpFullDir);
                }
                string saveName = Guid.NewGuid().ToString() + "." + ext;
                string savePath = Path.Combine(tmpFullDir, saveName);

                Image returnImage = ImgHelper.Base64StringToImage(str);

                string message = "";
                bool isSave = ImgHelper.SavePicAndThumbnail(returnImage, tmpFullDir, saveName, 300, 80, out message);
                if (isSave == false)
                {
                    result.Msg = "图片保存失败";
                    return result;
                }

                category.ImageSrc = Path.Combine(tmpDir + @"\thumb", saveName).Replace(@"\", @"/");
            }

            #endregion

            var model = new ProductCategory()
            {
                byteStr = category.byteStr,
                CName = category.CName,
                EName = category.EName,
                SeqNo = category.SeqNo,
                Code = category.Code,
                CreationTime = DateTime.Now,
                CreatorUserId = loginId,
                ImageSrc = category.ImageSrc,
                IsShow = category.IsShow,
                ParentId = category.ParentId,
                Type = category.Type,
            };

            int excute = _productCategoryRepository.SingleUpdate(model);

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
    }
}
