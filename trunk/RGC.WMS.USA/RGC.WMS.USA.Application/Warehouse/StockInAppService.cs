using Microsoft.Extensions.Options;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Purchase.Dto;
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
    /// MeridianGo 2020/06/24
    /// </summary>
    public class StockInAppService : IStockInAppService
    {
        private readonly IStockInRepository _stockInRepositories;
        private readonly IStockInDetailRepository _stockInDetailRepositories;
        private readonly IBmsUserRepository _bmsUserRepository;

        private readonly IOptions<ApplicationBaseConfig> _configuration;
        public StockInAppService(
            IStockInRepository stockInRepositories,
            IStockInDetailRepository stockInDetailRepositories,
            IBmsUserRepository bmsUserRepository,
            IOptions<ApplicationBaseConfig> configuration)
        {
            _stockInRepositories = stockInRepositories;
            _stockInDetailRepositories = stockInDetailRepositories;
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
        private StockIn GetInputModelAfterVaildData(StockInEditInput model, bool bIsUpdate = false)
        {
            if (model == null)
                throw new CustomException("参数异常", 4);

            if (bIsUpdate && model.Id <= 0)
                throw new CustomException("主数据参数异常", 4);

            if (model.StockInType <= 0)
                throw new CustomException("请选择入库类型", 4);

            if (model.StockInType == StockInType.采购入库)
            {
                if (model.PackingId <= 0)
                    throw new CustomException("请选择发货单", 4);
            }

            if (model.WarehouseId <= 0)
                throw new CustomException("请选择入库仓库", 4);

            if (model.StockInType == StockInType.调拨入库)
            {
                if (model.FromWarehouseId <= 0)
                    throw new CustomException("请选择调拨出库的仓库", 4);
            }

            model.Remark = model.Remark.ToEmpty();
            if (model.Remark.Length > 512)
                throw new CustomException("备注的长度不能超过512", 4);

            if (model.Detail == null || model.Detail.Count <= 0)
                throw new CustomException("入库明细数据不能为空", 4);

            StockIn tInputModel;
            if (bIsUpdate)
            {
                tInputModel = _stockInRepositories.Get(model.Id);
                if (tInputModel == null)
                    throw new CustomException("数据提取异常", 4);

                if (tInputModel.StockInStatus == StockInStatus.Received)
                    throw new CustomException("已入库，无法修改", 4);

                if (tInputModel.DetailDict == null)
                    tInputModel.DetailDict = new Dictionary<long, StockInDetail>();

                foreach (var tDetailDict in tInputModel.DetailDict)
                {
                    if (!model.Detail.Select(y => y.Id).ToList().Contains(tDetailDict.Key))
                        tDetailDict.Value.IsDeleted = true;
                }
            }
            else
            {
                tInputModel = new StockIn();
                tInputModel.StockInStatus = StockInStatus.Initial;
                tInputModel.DetailDict = new Dictionary<long, StockInDetail>();
            }

            tInputModel.StockInType = model.StockInType;

            if (model.StockInType == StockInType.采购入库)
                tInputModel.PackingId = model.PackingId;

            tInputModel.WarehouseId = model.WarehouseId;

            if (model.StockInType == StockInType.调拨入库)
                tInputModel.FromWarehouseId = model.FromWarehouseId;

            tInputModel.Remark = model.Remark.ToEmpty();
            var k = 1;

            foreach (var item in model.Detail)
            {
                if (model.StockInType == StockInType.采购入库)
                    if (item.PackingDetailId <= 0)
                        throw new CustomException("请选择发货单的明细数据", 4);

                if (item.ProductId <= 0)
                    throw new CustomException("请选择到货产品", 4);

                if (item.PlanInQty <= 0)
                    throw new CustomException("请填写发货数量", 4);

                if (!item.ATAWarehouse.ToEmptyDateTime().HasValue)
                    throw new CustomException("请选择到仓日期", 4);

                item.ActFactoryModel = item.ActFactoryModel.ToEmpty();
                if (string.IsNullOrEmpty(item.ActFactoryModel))
                    throw new CustomException("请填写实际型号", 4);

                if (item.ActFactoryModel.Length > 128)
                    throw new CustomException("实际型号的长度不能超过128", 4);

                if (item.ActInQty <= 0)
                    throw new CustomException("请填写到货数量", 4);

                item.Reason = item.Reason.ToEmpty();
                if (item.Reason.Length > 512)
                    throw new CustomException("延迟原因的长度不能超过512", 4);

                item.StoragerackNum = item.StoragerackNum.ToEmpty();
                if (item.StoragerackNum.Length > 128)
                    throw new CustomException("货架标识的长度不能超过128", 4);

                item.Remark = item.Remark.ToEmpty();
                if (item.Remark.Length > 512)
                    throw new CustomException("备注的长度不能超过512", 4);

                StockInDetail tInputDetail;
                if (bIsUpdate && item.Id > 0)
                {
                    tInputDetail = tInputModel.DetailDict.Values.FirstOrDefault(x => x.Id == item.Id);
                    if (tInputDetail == null)
                        throw new CustomException("明细数据提取异常", 4);

                    if (tInputDetail.Status == StockInStatus.Received)
                        throw new CustomException("已入库，无法修改", 4);
                }
                else
                {
                    tInputDetail = new StockInDetail();
                    tInputDetail.Status = StockInStatus.Initial;
                }

                tInputDetail.StockInId = tInputModel.Id;
                tInputDetail.PackingDetailId = item.PackingDetailId;

                tInputDetail.ProductId = item.ProductId;
                tInputDetail.SkuId = item.SkuId;
                tInputDetail.SkuCostId = item.SkuCostId;
                tInputDetail.SkuCostBatchId = item.SkuCostBatchId;
                tInputDetail.PlanInQty = item.PlanInQty;
                tInputDetail.ETD = item.ETD.ToEmptyDateTime();
                tInputDetail.ETA = item.ETA.ToEmptyDateTime();
                tInputDetail.ActInQty = item.ActInQty;
                tInputDetail.ActFactoryModel = item.ActFactoryModel;
                tInputDetail.ATD = item.ATD.ToEmptyDateTime();
                tInputDetail.ATAPort = item.ATAPort.ToEmptyDateTime();
                tInputDetail.ATAWarehouse = item.ATAWarehouse.ToEmptyDateTime();
                tInputDetail.Reason = item.Reason;
                tInputDetail.StoragerackNum = item.StoragerackNum;
                tInputDetail.Remark = item.Remark;

                if (bIsUpdate && item.Id > 0)
                    tInputModel.DetailDict[item.Id] = tInputDetail;
                else
                    tInputModel.DetailDict.Add(k, tInputDetail);
                k++;
            }

            return tInputModel;
        }

        public ResponseDto<string> Create(long loginId, StockInEditInput model)
        {
            VaildLogin(loginId);
            var tInputModel = GetInputModelAfterVaildData(model);
            _stockInRepositories.Create(loginId, tInputModel);
            return new ResponseDto<string>
            {
                Code = 0,
                Success = true
            };
        }

        public ResponseDto<string> Update(long loginId, StockInEditInput model)
        {
            VaildLogin(loginId);
            var tInputModel = GetInputModelAfterVaildData(model, true);
            _stockInRepositories.Update(loginId, tInputModel);
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

            var mode = _stockInRepositories.GetById(id);
            if (mode == null)
                throw new CustomException("数据提取异常，请联系管理员", 4);

            if (mode.IsDeleted)
                throw new CustomException("入库已删除", 1);

            if (_stockInRepositories.Delete(loginId, mode) <= 0)
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

            var mode = _stockInRepositories.GetById(id);
            if (mode == null)
                throw new CustomException("数据提取异常，请联系管理员", 4);

            if (!mode.IsDeleted)
                throw new CustomException("入库已恢复", 1);

            if (_stockInRepositories.Recovery(loginId, mode) <= 0)
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

            var mode = _stockInDetailRepositories.GetById(id);
            if (mode == null)
                throw new CustomException("数据提取异常", 4);

            if (mode.IsDeleted)
                throw new CustomException("入库单记录已删除", 1);

            if (mode.Status == StockInStatus.Received)
                throw new CustomException("已入库，无法删除", 4);

            if (_stockInRepositories.DetailDelete(loginId, mode) <= 0)
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

            var mode = _stockInDetailRepositories.GetById(id);
            if (mode == null)
                throw new CustomException("数据提取异常", 4);

            if (!mode.IsDeleted)
                throw new CustomException("入库单记录已恢复", 1);

            if (_stockInRepositories.DetailRecovery(loginId, mode) <= 0)
                throw new CustomException("恢复失败", 1);

            return new ResponseDto<string>
            {
                Code = 0,
                Success = true
            };
        }

        public ResponseDto<string> UpdateStatus(long id, StockInStatus status, long modifierUserId)
        {
            #region 验证登陆
            VaildLogin(modifierUserId);
            #endregion

            var mode = _stockInRepositories.GetById(id);
            if (mode == null)
                throw new CustomException("参数异常，请联系管理员", 4);
            mode.StockInStatus = status;

            if (_stockInRepositories.UpdateStatus(mode, modifierUserId) <= 0)
                throw new CustomException("更新失败", 1);

            return new ResponseDto<string>
            {
                Code = 0,
                Success = true
            };
        }

        public ResponseDto<StockInOutput> Get(long id)
        {
            var tInput = _stockInRepositories.Get(id);
            var result = new ResponseDto<StockInOutput>
            {
                Code = 0,
                Success = true
            };

            result.Data = new StockInOutput
            {
                Id = tInput.Id,
                StockInNum = tInput.StockInNum,
                StockInType = tInput.StockInType,
                PackingId = tInput.PackingId,
                WarehouseId = tInput.WarehouseId,
                FromWarehouseId = tInput.FromWarehouseId,
                StockInStatus = tInput.StockInStatus,
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

            if (tInput.PackingList != null)
            {
                result.Data.PackingList = new PackingListOutput
                {
                    ToWarehouseId = tInput.PackingList.ToWarehouseId,
                    ContractNo = tInput.PackingList.ContractNo,
                    Remarks = tInput.PackingList.Remarks
                };
            }
            else
                result.Data.PackingList = new PackingListOutput();

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

            if (tInput.FromWarehouse != null)
            {
                result.Data.FromWarehouse = new WarehouseOutput
                {
                    Number = tInput.FromWarehouse.Number,
                    Name = tInput.FromWarehouse.Name,
                    Status = tInput.FromWarehouse.Status,
                    PostCodePrefix = tInput.FromWarehouse.PostCodePrefix,
                    Address = tInput.FromWarehouse.Address
                };
            }
            else
                result.Data.FromWarehouse = new WarehouseOutput();

            if (tInput.DetailDict == null)
                tInput.DetailDict = new Dictionary<long, StockInDetail>();

            result.Data.Detail = new List<StockInDetailOutput>();
            foreach (var tDetail in tInput.DetailDict.Values)
            {
                var tDetailOutput = new StockInDetailOutput
                {
                    Id = tDetail.Id,
                    PackingDetailId = tDetail.PackingDetailId,
                    Status = tDetail.Status,
                    ProductId = tDetail.ProductId,
                    SkuId = tDetail.SkuId,
                    SkuCostId = tDetail.SkuCostId,
                    SkuCostBatchId = tDetail.SkuCostBatchId,
                    PlanInQty = tDetail.PlanInQty,
                    ETD = tDetail.ETD.ToDateZNString(),
                    ETA = tDetail.ETA.ToDateZNString(),
                    ActInQty = tDetail.ActInQty,
                    ActFactoryModel = tDetail.ActFactoryModel,
                    ATD = tDetail.ATD.ToDateZNString(),
                    ATAPort = tDetail.ATAPort.ToDateZNString(),
                    ATAWarehouse = tDetail.ATAWarehouse.ToDateZNString(),
                    Reason = tDetail.Reason,
                    StoragerackNum = tDetail.StoragerackNum,
                    Remark = tDetail.Remark
                };

                if (tDetail.PackingDetail != null)
                {
                    tDetailOutput.PackingDetail = new PackingListDetailOutput
                    {
                        Id = tDetail.PackingDetail.Id,
                        ContainerNo = tDetail.PackingDetail.ContainerNo,
                        ProductId = tDetail.PackingDetail.ProductId,
                        SkuId = tDetail.PackingDetail.SkuId,
                        SkuCostId = tDetail.PackingDetail.SkuCostId,
                        SkuCostBatchId = tDetail.PackingDetail.SkuCostBatchId,
                        SkuStockId = tDetail.PackingDetail.SkuStockId,
                        Qty = tDetail.PackingDetail.Qty,
                        Type = tDetail.PackingDetail.Type,
                        ETD = tDetail.PackingDetail.ETD.ToDateZNString(),
                        ETA = tDetail.PackingDetail.ETA.ToDateZNString()
                    };
                }
                else
                    tDetailOutput.PackingDetail = new PackingListDetailOutput();

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

        public ResponsePageDto<StockInOutput> GetPage(StockInFilterInput searchFilter)
        {
            var list = _stockInRepositories.GetPage(
                (
                    searchFilter.SearchType,
                    searchFilter.SearchKey,
                    searchFilter.WarehouseId,
                    searchFilter.Status,
                    searchFilter.IsDeleted,
                    searchFilter.PageSize,
                    searchFilter.CurrentPage
                ), out int count);
            var result = new ResponsePageDto<StockInOutput>
            {
                Code = 0,
                Success = true
            };

            //将model中角色实体，转换为前台需要的实体
            result.Data = new List<StockInOutput>();
            if (result.Data.Count >= 0)
            {
                var lstUserId = new List<long>();
                lstUserId.AddRange(list.Select(x => x.CreatorUserId).ToList());
                lstUserId.AddRange(list.Select(x => x.LastModifierUserId.ToLong()).ToList());
                lstUserId.AddRange(list.Select(x => x.DeleterUserId.ToLong()).ToList());
                var lstUser = _bmsUserRepository.GetAllUserByKeys(lstUserId);

                foreach (var tInput in list)
                {
                    var tOutput = new StockInOutput
                    {
                        Id = tInput.Id,
                        StockInNum = tInput.StockInNum,
                        StockInType = tInput.StockInType,
                        PackingId = tInput.PackingId,
                        WarehouseId = tInput.WarehouseId,
                        FromWarehouseId = tInput.FromWarehouseId,
                        StockInStatus = tInput.StockInStatus,
                        Remark = tInput.Remark,
                        IsDeleted = tInput.IsDeleted,
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

                    if (tInput.PackingList != null)
                    {
                        tOutput.PackingList = new PackingListOutput
                        {
                            ToWarehouseId = tInput.PackingList.ToWarehouseId,
                            ContractNo = tInput.PackingList.ContractNo,
                            Remarks = tInput.PackingList.Remarks
                        };
                    }
                    else
                        tOutput.PackingList = new PackingListOutput();

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

                    if (tInput.FromWarehouse != null)
                    {
                        tOutput.FromWarehouse = new WarehouseOutput
                        {
                            Number = tInput.FromWarehouse.Number,
                            Name = tInput.FromWarehouse.Name,
                            Status = tInput.FromWarehouse.Status,
                            PostCodePrefix = tInput.FromWarehouse.PostCodePrefix,
                            Address = tInput.FromWarehouse.Address
                        };
                    }
                    else
                        tOutput.FromWarehouse = new WarehouseOutput();

                    if (tInput.DetailDict == null)
                        tInput.DetailDict = new Dictionary<long, StockInDetail>();

                    tOutput.Detail = new List<StockInDetailOutput>();
                    foreach (var tDetail in tInput.DetailDict.Values)
                    {
                        var tDetailOutput = new StockInDetailOutput
                        {
                            Id = tDetail.Id,
                            PackingDetailId = tDetail.PackingDetailId,
                            Status = tDetail.Status,
                            ProductId = tDetail.ProductId,
                            SkuId = tDetail.SkuId,
                            SkuCostId = tDetail.SkuCostId,
                            SkuCostBatchId = tDetail.SkuCostBatchId,
                            PlanInQty = tDetail.PlanInQty,
                            ETD = tDetail.ETD.ToDateZNString(),
                            ETA = tDetail.ETA.ToDateZNString(),
                            ActInQty = tDetail.ActInQty,
                            ActFactoryModel = tDetail.ActFactoryModel,
                            ATD = tDetail.ATD.ToDateZNString(),
                            ATAPort = tDetail.ATAPort.ToDateZNString(),
                            ATAWarehouse = tDetail.ATAWarehouse.ToDateZNString(),
                            Reason = tDetail.Reason,
                            StoragerackNum = tDetail.StoragerackNum,
                            Remark = tDetail.Remark
                        };

                        if (tDetail.PackingDetail != null)
                        {
                            tDetailOutput.PackingDetail = new PackingListDetailOutput
                            {
                                Id = tDetail.PackingDetail.Id,
                                ContainerNo = tDetail.PackingDetail.ContainerNo,
                                ProductId = tDetail.PackingDetail.ProductId,
                                SkuId = tDetail.PackingDetail.SkuId,
                                SkuCostId = tDetail.PackingDetail.SkuCostId,
                                SkuCostBatchId = tDetail.PackingDetail.SkuCostBatchId,
                                SkuStockId = tDetail.PackingDetail.SkuStockId,
                                Qty = tDetail.PackingDetail.Qty,
                                Type = tDetail.PackingDetail.Type,
                                ETD = tDetail.PackingDetail.ETD.ToDateZNString(),
                                ETA = tDetail.PackingDetail.ETA.ToDateZNString()
                            };

                            if (tDetail.PackingDetail.Sku != null)
                            {
                                tDetailOutput.PackingDetail.Sku = new SkuOutput
                                {
                                    CName = tDetail.PackingDetail.Sku.CName,
                                    FullCName = tDetail.PackingDetail.Sku.FullCName,
                                    EName = tDetail.PackingDetail.Sku.EName,
                                    FullEName = tDetail.PackingDetail.Sku.FullEName,
                                    Status = tDetail.PackingDetail.Sku.Status,
                                    SKU = tDetail.PackingDetail.Sku.SKU,
                                    FactoryModel = tDetail.PackingDetail.Sku.FactoryModel,
                                    PrimaryImageSrcFull = _configuration.Value.ImgSiteRootAddress + tDetail.PackingDetail.Sku.PrimaryImageSrc
                                };
                            }
                            else
                                tDetailOutput.PackingDetail.Sku = new SkuOutput();

                            if (tDetail.PackingDetail.SkuCostBatch != null)
                            {
                                tDetailOutput.PackingDetail.SkuCostBatch = new SkuCostBatchOutput
                                {
                                    BatchNo = tDetail.PackingDetail.SkuCostBatch.BatchNo,
                                    Status = tDetail.PackingDetail.SkuCostBatch.Status,
                                    Remark = tDetail.PackingDetail.SkuCostBatch.Remark
                                };
                            }
                            else
                                tDetailOutput.PackingDetail.SkuCostBatch = new SkuCostBatchOutput();
                        }
                        else
                            tDetailOutput.PackingDetail = new PackingListDetailOutput
                            {
                                Sku = new SkuOutput(),
                                SkuCostBatch = new SkuCostBatchOutput()
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
