using Microsoft.Extensions.Options;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Sku.Dto;
using RGC.WMS.USA.Application.Warehouse.Dto;
using RGC.WMS.USA.Domain;
using RGC.WMS.USA.Domain.Entities.Warehouse;
using RGC.WMS.USA.Domain.Entities.Warehouse.Enum;
using RGC.WMS.USA.Domain.Repositories.Bms;
using RGC.WMS.USA.Domain.Repositories.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Application.Warehouse
{
    /// <summary>
    /// MeridianGo 2020/06/27
    /// </summary>
    public class StockOutAppService : IStockOutAppService
    {
        private readonly IStockOutRepository _stockOutRepositories;
        private readonly IStockOutDetailRepository _stockOutDetailRepositories;
        private readonly IBmsUserRepository _bmsUserRepository;

        private readonly IOptions<ApplicationBaseConfig> _configuration;
        public StockOutAppService(
            IStockOutRepository stockOutRepositories,
            IStockOutDetailRepository stockOutDetailRepositories,
            IBmsUserRepository bmsUserRepository,
            IOptions<ApplicationBaseConfig> configuration)
        {
            _stockOutRepositories = stockOutRepositories;
            _stockOutDetailRepositories = stockOutDetailRepositories;
            _bmsUserRepository = bmsUserRepository;
            _configuration = configuration;
        }

        private void VaildLogin(long loginId)
        {
            if (loginId <= 0)
                throw new CustomException("未登陆", 3);
        }

        /// <summary>
        /// 数据验证，并组装成接口需要的参数
        /// </summary>
        private StockOut GetInputModelAfterVaildData(StockOutEditInput model, bool bIsUpdate = false)
        {
            if (model == null)
                throw new CustomException("参数异常", 4);

            if (bIsUpdate && model.Id <= 0)
                throw new CustomException("主数据参数异常", 4);

            if (model.StockOutType <= 0)
                throw new CustomException("请选择出库类型", 4);

            if (model.WarehouseId <= 0)
                throw new CustomException("请选择出库仓库", 4);

            if (model.StockOutType == StockOutType.调拨出库)
            {
                if (model.ToWarehouseId <= 0)
                    throw new CustomException("请选择调拨目标的仓库", 4);
            }

            model.Remark = model.Remark.ToEmpty();
            if (model.Remark.Length > 512)
                throw new CustomException("备注的长度不能超过512", 4);

            if (model.Detail == null || model.Detail.Count <= 0)
                throw new CustomException("出库明细数据不能为空", 4);

            StockOut tInputModel;
            if (bIsUpdate)
            {
                tInputModel = _stockOutRepositories.Get(model.Id);
                if (tInputModel == null)
                    throw new CustomException("数据提取异常", 4);

                if (tInputModel.StockOutStatus == StockOutStatus.Extracted)
                    throw new CustomException("已出库，无法修改", 4);

                if (tInputModel.DetailDict == null)
                    tInputModel.DetailDict = new Dictionary<long, StockOutDetail>();

            }
            else
            {
                tInputModel = new StockOut();
                tInputModel.StockOutStatus = StockOutStatus.Initial;
                tInputModel.DetailDict = new Dictionary<long, StockOutDetail>();
            }

            tInputModel.StockOutType = model.StockOutType;

            tInputModel.WarehouseId = model.WarehouseId;

            if (model.StockOutType == StockOutType.调拨出库)
                tInputModel.ToWarehouseId = model.ToWarehouseId;

            tInputModel.Remark = model.Remark.ToEmpty();
            var k = 1;

            foreach (var item in model.Detail)
            {
                if (item.ProductId <= 0)
                    throw new CustomException("请选择到货产品", 4);

                if (item.PlanOutQty <= 0)
                    throw new CustomException("请填写预提数量", 4);

                if (item.ActOutQty <= 0)
                    throw new CustomException("请填写出库数量", 4);

                item.StoragerackNum = item.StoragerackNum.ToEmpty();
                if (item.StoragerackNum.Length > 128)
                    throw new CustomException("货架标识的长度不能超过128", 4);

                item.Remark = item.Remark.ToEmpty();
                if (item.Remark.Length > 512)
                    throw new CustomException("备注的长度不能超过512", 4);

                StockOutDetail tInputDetail;
                if (bIsUpdate && item.Id > 0)
                {
                    tInputDetail = tInputModel.DetailDict.Values.FirstOrDefault(x => x.Id == item.Id);
                    if (tInputDetail == null)
                        throw new CustomException("明细数据提取异常", 4);

                    if (tInputDetail.Status == StockOutStatus.Extracted)
                        throw new CustomException("已出库，无法修改", 4);
                }
                else
                {
                    tInputDetail = new StockOutDetail();
                    tInputDetail.Status = StockOutStatus.Initial;
                }

                tInputDetail.StockOutId = tInputModel.Id;

                tInputDetail.ProductId = item.ProductId;
                tInputDetail.SkuId = item.SkuId;
                tInputDetail.SkuCostId = item.SkuCostId;
                tInputDetail.SkuCostBatchId = item.SkuCostBatchId;
                tInputDetail.PlanOutQty = item.PlanOutQty;
                tInputDetail.ActOutQty = item.ActOutQty;
                tInputDetail.StoragerackNum = item.StoragerackNum;
                tInputDetail.Remark = item.Remark;

                tInputModel.DetailDict.Add(k, tInputDetail);
                k++;
            }

            return tInputModel;
        }

        public ResponseDto<string> Create(long loginId, StockOutEditInput model)
        {
            VaildLogin(loginId);
            var tInputModel = GetInputModelAfterVaildData(model);
            _stockOutRepositories.Create(loginId, tInputModel);
            return new ResponseDto<string>
            {
                Code = 0,
                Success = true
            };
        }

        public ResponseDto<string> Update(long loginId, StockOutEditInput model)
        {
            VaildLogin(loginId);
            var tInputModel = GetInputModelAfterVaildData(model);
            _stockOutRepositories.Update(loginId, tInputModel);
            return new ResponseDto<string>
            {
                Code = 0,
                Success = true
            };
        }

        public ResponseDto<string> Delete(long loginId, long id)
        {
            #region 验证登陆
            VaildLogin(loginId);
            #endregion

            if (id <= 0)
                throw new CustomException("参数异常，请联系管理员", 4);

            var mode = _stockOutRepositories.GetById(id);
            if (mode == null)
                throw new CustomException("数据提取异常，请联系管理员", 4);

            if (mode.IsDeleted)
                throw new CustomException("出库已删除", 1);

            if (_stockOutRepositories.Delete(loginId, mode) <= 0)
                throw new CustomException("删除失败", 1);

            return new ResponseDto<string>
            {
                Code = 0,
                Success = true
            };
        }

        public ResponseDto<string> Recovery(long loginId, long id)
        {
            #region 验证登陆
            VaildLogin(loginId);
            #endregion

            if (id <= 0)
                throw new CustomException("参数异常，请联系管理员", 4);

            var mode = _stockOutRepositories.GetById(id);
            if (mode == null)
                throw new CustomException("数据提取异常，请联系管理员", 4);

            if (!mode.IsDeleted)
                throw new CustomException("出库已恢复", 1);

            if (_stockOutRepositories.Recovery(loginId, mode) <= 0)
                throw new CustomException("恢复失败", 1);

            return new ResponseDto<string>
            {
                Code = 0,
                Success = true
            };
        }

        public ResponseDto<string> DetailDelete(long loginId, long id)
        {
            VaildLogin(loginId);

            if (id <= 0)
                throw new CustomException("参数异常", 4);

            var mode = _stockOutDetailRepositories.GetById(id);
            if (mode == null)
                throw new CustomException("数据提取异常", 4);

            if (mode.IsDeleted)
                throw new CustomException("出库单记录已删除", 1);

            if (mode.Status == StockOutStatus.Extracted)
                throw new CustomException("已出库，无法删除", 4);

            if (_stockOutRepositories.DetailDelete(loginId, mode) <= 0)
                throw new CustomException("删除失败", 1);

            return new ResponseDto<string>
            {
                Code = 0,
                Success = true
            };
        }

        public ResponseDto<string> DetailRecovery(long loginId, long id)
        {
            #region 验证登陆
            VaildLogin(loginId);
            #endregion

            if (id <= 0)
                throw new CustomException("参数异常", 4);

            var mode = _stockOutDetailRepositories.GetById(id);
            if (mode == null)
                throw new CustomException("数据提取异常", 4);

            if (!mode.IsDeleted)
                throw new CustomException("出库单记录已恢复", 1);

            if (_stockOutRepositories.DetailRecovery(loginId, mode) <= 0)
                throw new CustomException("恢复失败", 1);

            return new ResponseDto<string>
            {
                Code = 0,
                Success = true
            };
        }

        public ResponseDto<string> UpdateStatus(long id, StockOutStatus status, long modifierUserId)
        {
            #region 验证登陆
            VaildLogin(modifierUserId);
            #endregion

            var mode = _stockOutRepositories.GetById(id);
            if (mode == null)
                throw new CustomException("参数异常，请联系管理员", 4);
            mode.StockOutStatus = status;

            if (_stockOutRepositories.UpdateStatus(mode, modifierUserId) <= 0)
                throw new CustomException("更新失败", 1);

            return new ResponseDto<string>
            {
                Code = 0,
                Success = true
            };
        }

        public ResponseDto<StockOutOutput> Get(long id)
        {
            var tInput = _stockOutRepositories.Get(id);
            var result = new ResponseDto<StockOutOutput>
            {
                Code = 0,
                Success = true
            };

            result.Data = new StockOutOutput
            {
                Id = tInput.Id,
                StockOutNum = tInput.StockOutNum,
                OrderNum = tInput.OrderNum,
                StockOutType = tInput.StockOutType,
                WarehouseId = tInput.WarehouseId,
                ToWarehouseId = tInput.ToWarehouseId,
                StockOutStatus = tInput.StockOutStatus,
                Remark = tInput.Remark,
                CreationTime = tInput.CreationTime,
                LastModificationTime = tInput.LastModificationTime,
                DeletionTime = tInput.DeletionTime
            };

            var lstUserId = new List<long>();
            var iCreatorUserId = tInput.CreatorUserId.ToLong();
            lstUserId.Add(iCreatorUserId);
            var iLastModifierUserId = tInput.LastModifierUserId.ToLong();
            lstUserId.Add(iLastModifierUserId);
            var iDeleterUserId = tInput.DeleterUserId.ToLong();
            lstUserId.Add(tInput.DeleterUserId.ToLong());
            var lstUser = _bmsUserRepository.GetAllUserByKeys(lstUserId);

            if (iCreatorUserId > 0)
                result.Data.CreateUser = lstUser.FirstOrDefault(x => x.Id == iCreatorUserId)?.LoginName;
            if (iLastModifierUserId > 0)
                result.Data.LastModifierUser = lstUser.FirstOrDefault(x => x.Id == iLastModifierUserId)?.LoginName;
            if (iDeleterUserId > 0)
                result.Data.DeleterUser = lstUser.FirstOrDefault(x => x.Id == iDeleterUserId)?.LoginName;

            if (tInput.Warehouse != null)
            {
                result.Data.Warehouse = new WarehouseOutput
                {
                    Number = tInput.Warehouse.Number,
                    Name = tInput.Warehouse.Name,
                    Status = tInput.Warehouse.Status,
                    PostCodePrefix = tInput.Warehouse.PostCodePrefix,
                    Address = tInput.Warehouse.Address
                };
            }
            else
                result.Data.Warehouse = new WarehouseOutput();

            if (tInput.ToWarehouse != null)
            {
                result.Data.ToWarehouse = new WarehouseOutput
                {
                    Number = tInput.ToWarehouse.Number,
                    Name = tInput.ToWarehouse.Name,
                    Status = tInput.ToWarehouse.Status,
                    PostCodePrefix = tInput.ToWarehouse.PostCodePrefix,
                    Address = tInput.ToWarehouse.Address
                };
            }
            else
                result.Data.ToWarehouse = new WarehouseOutput();

            if (tInput.DetailDict == null)
                tInput.DetailDict = new Dictionary<long, StockOutDetail>();

            result.Data.Detail = new List<StockOutDetailOutput>();
            foreach (var tDetail in tInput.DetailDict.Values)
            {
                var tDetailOutput = new StockOutDetailOutput
                {
                    Id = tDetail.Id,
                    Status = tDetail.Status,
                    ProductId = tDetail.ProductId,
                    SkuId = tDetail.SkuId,
                    SkuCostId = tDetail.SkuCostId,
                    SkuCostBatchId = tDetail.SkuCostBatchId,
                    PlanOutQty = tDetail.PlanOutQty,
                    ActOutQty = tDetail.ActOutQty,
                    StoragerackNum = tDetail.StoragerackNum,
                    Remark = tDetail.Remark
                };

                if (tDetail.Sku != null)
                {
                    tDetailOutput.Sku = new SkuOutput
                    {
                        CName = tDetail.Sku.CName,
                        FullCName = tDetail.Sku.FullCName,
                        EName = tDetail.Sku.EName,
                        FullEName = tDetail.Sku.FullEName,
                        Status = tDetail.Sku.Status,
                        SKU = tDetail.Sku.SKU,
                        FactoryModel = tDetail.Sku.FactoryModel,
                        PrimaryImageSrcFull = _configuration.Value.ImgSiteRootAddress + tDetail.Sku.PrimaryImageSrc
                    };
                }
                else
                    tDetailOutput.Sku = new SkuOutput();

                if (tDetail.SkuCostBatch != null)
                {
                    tDetailOutput.SkuCostBatch = new SkuCostBatchOutput
                    {
                        BatchNo = tDetail.SkuCostBatch.BatchNo,
                        Status = tDetail.SkuCostBatch.Status,
                        Remark = tDetail.SkuCostBatch.Remark
                    };
                }
                else
                    tDetailOutput.SkuCostBatch = new SkuCostBatchOutput();
                result.Data.Detail.Add(tDetailOutput);
            }
            return result;
        }

        public ResponsePageDto<StockOutOutput> GetPage(StockOutFilterInput searchFilter)
        {
            var list = _stockOutRepositories.GetPage(
                (
                    searchFilter.SearchType,
                    searchFilter.SearchKey,
                    searchFilter.WarehouseId,
                    searchFilter.Status,
                    searchFilter.IsDeleted,
                    searchFilter.PageSize,
                    searchFilter.CurrentPage
                ), out int count);
            var result = new ResponsePageDto<StockOutOutput>
            {
                Code = 0,
                Success = true
            };

            //将model中角色实体，转换为前台需要的实体
            result.Data = new List<StockOutOutput>();
            if (result.Data.Count >= 0)
            {
                var lstUserId = new List<long>();
                lstUserId.AddRange(list.Select(x => x.CreatorUserId).ToList());
                lstUserId.AddRange(list.Select(x => x.LastModifierUserId.ToLong()).ToList());
                lstUserId.AddRange(list.Select(x => x.DeleterUserId.ToLong()).ToList());
                var lstUser = _bmsUserRepository.GetAllUserByKeys(lstUserId);

                foreach (var tInput in list)
                {
                    var tOutput = new StockOutOutput
                    {
                        Id = tInput.Id,
                        StockOutNum = tInput.StockOutNum,
                        OrderNum = tInput.OrderNum,
                        StockOutType = tInput.StockOutType,
                        WarehouseId = tInput.WarehouseId,
                        ToWarehouseId = tInput.ToWarehouseId,
                        StockOutStatus = tInput.StockOutStatus,
                        Remark = tInput.Remark,
                        CreationTime = tInput.CreationTime,
                        LastModificationTime = tInput.LastModificationTime,
                        DeletionTime = tInput.DeletionTime
                    };

                    var iCreatorUserId = tInput.CreatorUserId;
                    if (iCreatorUserId > 0)
                        tOutput.CreateUser = lstUser.FirstOrDefault(x => x.Id == iCreatorUserId)?.LoginName;

                    var iLastModifierUserId = tInput.LastModifierUserId.ToInt();
                    if (iLastModifierUserId > 0)
                        tOutput.LastModifierUser = lstUser.FirstOrDefault(x => x.Id == iLastModifierUserId)?.LoginName;

                    var iDeleterUserId = tInput.DeleterUserId.ToInt();
                    if (iDeleterUserId > 0)
                        tOutput.DeleterUser = lstUser.FirstOrDefault(x => x.Id == iDeleterUserId)?.LoginName;
                    
                    if (tInput.Warehouse != null)
                    {
                        tOutput.Warehouse = new WarehouseOutput
                        {
                            Number = tInput.Warehouse.Number,
                            Name = tInput.Warehouse.Name,
                            Status = tInput.Warehouse.Status,
                            PostCodePrefix = tInput.Warehouse.PostCodePrefix,
                            Address = tInput.Warehouse.Address
                        };
                    }
                    else
                        tOutput.Warehouse = new WarehouseOutput();

                    if (tInput.ToWarehouse != null)
                    {
                        tOutput.ToWarehouse = new WarehouseOutput
                        {
                            Number = tInput.ToWarehouse.Number,
                            Name = tInput.ToWarehouse.Name,
                            Status = tInput.ToWarehouse.Status,
                            PostCodePrefix = tInput.ToWarehouse.PostCodePrefix,
                            Address = tInput.ToWarehouse.Address
                        };
                    }
                    else
                        tOutput.ToWarehouse = new WarehouseOutput();

                    if (tInput.DetailDict == null)
                        tInput.DetailDict = new Dictionary<long, StockOutDetail>();

                    tOutput.Detail = new List<StockOutDetailOutput>();
                    foreach (var tDetail in tInput.DetailDict.Values)
                    {
                        var tDetailOutput = new StockOutDetailOutput
                        {
                            Id = tDetail.Id,
                            Status = tDetail.Status,
                            ProductId = tDetail.ProductId,
                            SkuId = tDetail.SkuId,
                            SkuCostId = tDetail.SkuCostId,
                            SkuCostBatchId = tDetail.SkuCostBatchId,
                            PlanOutQty = tDetail.PlanOutQty,
                            ActOutQty = tDetail.ActOutQty,
                            StoragerackNum = tDetail.StoragerackNum,
                            Remark = tDetail.Remark
                        };

                        if (tDetail.Sku != null)
                        {
                            tDetailOutput.Sku = new SkuOutput
                            {
                                CName = tDetail.Sku.CName,
                                FullCName = tDetail.Sku.FullCName,
                                EName = tDetail.Sku.EName,
                                FullEName = tDetail.Sku.FullEName,
                                Status = tDetail.Sku.Status,
                                SKU = tDetail.Sku.SKU,
                                FactoryModel = tDetail.Sku.FactoryModel,
                                PrimaryImageSrcFull = _configuration.Value.ImgSiteRootAddress + tDetail.Sku.PrimaryImageSrc
                            };
                        }
                        else
                            tDetailOutput.Sku = new SkuOutput();

                        if (tDetail.SkuCostBatch != null)
                        {
                            tDetailOutput.SkuCostBatch = new SkuCostBatchOutput
                            {
                                BatchNo = tDetail.SkuCostBatch.BatchNo,
                                Status = tDetail.SkuCostBatch.Status,
                                Remark = tDetail.SkuCostBatch.Remark
                            };
                        }
                        else
                            tDetailOutput.SkuCostBatch = new SkuCostBatchOutput();
                        tOutput.Detail.Add(tDetailOutput);
                    }

                    result.Data.Add(tOutput);
                }

                result.Page.TotalCount = (int)count;
                result.Page.TotalPages = (int)Math.Ceiling((Decimal)count / searchFilter.PageSize);
                result.Page.PageSize = searchFilter.PageSize;
                result.Page.CurrentPage = searchFilter.CurrentPage;
                result.Page.CurrentCount = result.Data.Count;
            }
            return result;
        }
    }
}
