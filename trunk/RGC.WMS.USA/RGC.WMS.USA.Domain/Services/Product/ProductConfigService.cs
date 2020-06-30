using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Product;
using RGC.WMS.USA.Domain.Repositories.Product;
using System;

namespace RGC.WMS.USA.Domain.Services.Product
{
    /// <summary>
    /// shane 2020/2/14
    /// </summary>
    public class ProductConfigService : IProductConfigService
    {
        private readonly IProductConfigRepository _productConfigRepository;
        public ProductConfigService(IProductConfigRepository productConfigRepository)
        {
            _productConfigRepository = productConfigRepository;
        }

        public ResponseDo<string> Create(ProductConfig config)
        {
            ResponseDo<string> result = new ResponseDo<string>();

                if (string.IsNullOrWhiteSpace(config.EName))
                {
                    result.Code = 1;
                    result.Msg = "请填写英文名";
                    result.Success = false;
                    return result;
                }
                if (string.IsNullOrWhiteSpace(config.CName))
                {
                    config.CName = "";
                }
                
                int excute = _productConfigRepository.Add(config);
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

        public ResponseDo<string> CreateDetail(ProductConfigDetail config)
        {
            ResponseDo<string> result = new ResponseDo<string>();

                if (string.IsNullOrWhiteSpace(config.EName))
                {
                    result.Code = 1;
                    result.Msg = "请填写英文名";
                    result.Success = false;
                    return result;
                }
                if (string.IsNullOrWhiteSpace(config.CName))
                {
                    config.CName = "";
                }
                //if (db.GetSkuConfigDictFromDB().Values.Where(p => p.CName.ToLower() == config.CName.ToLower().Trim() && p.EName.ToLower() == config.EName.ToLower().Trim() ).Any())
                //{
                //    result.Code = 1;
                //    result.Msg = "已存在该配置";
                //    result.Success = false;
                //    return result;
                //}
                int excute = _productConfigRepository.AddDetail(config);
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

        public ResponseDo<string> Delete(long loginId, long id)
        {
            ResponseDo<string> result = new ResponseDo<string>();
                int execute = _productConfigRepository.Delete(loginId, id);

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

        public ResponseDo<string> DeleteDetail(long loginId, long id, long proConfigId)
        {
            ResponseDo<string> result = new ResponseDo<string>();
            int execute = _productConfigRepository.DeleteDetail(loginId, id, proConfigId);

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

        public ResponseDo<bool> IfExistConfig(ProductConfig config)
        {
            ResponseDo<bool> result = new ResponseDo<bool>();
            result.Data = false;
            if (config.Id == 0)
            {
                result.Data=_productConfigRepository.IfExist(config);
                
            }
            result.Success = true;
            result.Code = 0;
            return result;
        }

        public ResponseDo<string> Update(ProductConfig config)
        {
            ResponseDo<string> result = new ResponseDo<string>();

                if (string.IsNullOrWhiteSpace(config.EName))
                {
                    result.Code = 1;
                    result.Msg = "请填写英文名";
                    result.Success = false;
                    return result;
                }
                if (string.IsNullOrWhiteSpace(config.CName))
                {
                    config.CName = "";
                }

                var model = _productConfigRepository.Get(config.Id);
                model.Type = config.Type;
                model.CName = config.CName;
                model.EName = config.EName;
                model.SeqNo = config.SeqNo;
                model.PackageId = config.PackageId;
                model.detailList = config.detailList;
                model.LastModificationTime = DateTime.Now;
                model.LastModifierUserId = config.LastModifierUserId;

                int execute = _productConfigRepository.SingleUpdate(model);
                if (execute > 0)
                {
                    //_productConfigRepository.SyncUpdate(model);
                    result.Code = 0;
                    result.Success = true;
                }
                else
                {
                    result.Msg = "操作失败";
                }
            return result;
        }

        public ResponseDo<string> UpdateDetail(ProductConfigDetail detail)
        {
            ResponseDo<string> result = new ResponseDo<string>();

                if (string.IsNullOrWhiteSpace(detail.EName))
                {
                    result.Code = 1;
                    result.Msg = "请填写英文名";
                    result.Success = false;
                    return result;
                }
                if (string.IsNullOrWhiteSpace(detail.CName))
                {
                    detail.CName = "";
                }
                int execute = _productConfigRepository.UpdateDetail(detail);
                if (execute > 0)
                {
                _productConfigRepository.SyncUpdateDetail(detail);
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
