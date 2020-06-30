using Microsoft.Extensions.Options;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Purchase.Dto;
using RGC.WMS.USA.Application.Sku.Dto;
using RGC.WMS.USA.Application.Warehouse.Dto;
using RGC.WMS.USA.Domain;
using RGC.WMS.USA.Domain.Entities.Purchase.Enum;
using RGC.WMS.USA.Domain.Repositories.Bms;
using RGC.WMS.USA.Domain.Repositories.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Application.Purchase
{
    /// <summary>
    /// MeridianGo 2020/06/17
    /// </summary>
    public class PackingListAppService : IPackingListAppService
    {
        private readonly IPackingListRepository _packingListRepository;
        private readonly IBmsUserRepository _bmsUserRepository;
        private readonly IOptions<ApplicationBaseConfig> _configuration;

        public PackingListAppService(
            IPackingListRepository packingListRepository,
            IBmsUserRepository bmsUserRepository,
            IOptions<ApplicationBaseConfig> configuration)
        {
            _packingListRepository = packingListRepository;
            _bmsUserRepository = bmsUserRepository;
            _configuration = configuration;
        }

        /// <summary>
        /// 登陆验证
        /// </summary>
        private void VaildLogin(long loginId)
        {
            if (loginId <= 0)
                throw new CustomException("未登陆", 3);
        }

        public ResponseDto<string> UpdateStatus(long id, CargoStatus status, long loginId)
        {
            VaildLogin(loginId);
            var tInputModel = _packingListRepository.Get(id);
            tInputModel.Status = status;
            _packingListRepository.UpdateStatus(tInputModel, loginId);
            return new ResponseDto<string>
            {
                Code = 0,
                Success = true
            };
        }

        public ResponseDto<PackingListOutput> Get(long id)
        {
            var tInput = _packingListRepository.Get(id);
            var result = new ResponseDto<PackingListOutput>
            {
                Code = 0,
                Success = true
            };

            result.Data = new PackingListOutput
            {
                Id = tInput.Id,
                ToWarehouseId = tInput.ToWarehouseId,
                ContractNo = tInput.ContractNo,
                Remarks = tInput.Remarks,
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

            result.Data.Detail = new List<PackingListDetailOutput>();
            foreach (var tDetail in tInput.DetailDict.Values)
            {
                var tDetailOutput = new PackingListDetailOutput
                {
                    Id = tDetail.Id,
                    ContainerNo = tDetail.ContainerNo,
                    ProductId = tDetail.ProductId,
                    SkuId = tDetail.SkuId,
                    SkuCostId = tDetail.SkuCostId,
                    SkuCostBatchId = tDetail.SkuCostBatchId,
                    SkuStockId = tDetail.SkuStockId,
                    ETD = tDetail.ETD.ToDateZNString(),
                    ETA = tDetail.ETA.ToDateZNString(),
                    Qty = tDetail.Qty,
                    Type = tDetail.Type
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
        public ResponsePageDto<PackingListOutput> GetPage(PackingListFilterInput searchFilter)
        {
            var list = _packingListRepository.GetPage(
                (
                    searchFilter.SearchType,
                    searchFilter.SearchKey,
                    searchFilter.WarehouseId,
                    searchFilter.IsDeleted,
                    searchFilter.PageSize,
                    searchFilter.CurrentPage
                ), out int count);
            var result = new ResponsePageDto<PackingListOutput>
            {
                Code = 0,
                Success = true
            };

            result.Data = new List<PackingListOutput>();
            if (result.Data.Count >= 0)
            {
                var lstUserId = new List<long>();
                lstUserId.AddRange(list.Select(x => x.CreatorUserId).ToList());
                lstUserId.AddRange(list.Select(x => x.LastModifierUserId.ToLong()).ToList());
                lstUserId.AddRange(list.Select(x => x.DeleterUserId.ToLong()).ToList());
                var lstUser = _bmsUserRepository.GetAllUserByKeys(lstUserId);

                foreach (var tInput in list)
                {
                    var tOutput = new PackingListOutput
                    {
                        Id = tInput.Id,
                        ToWarehouseId = tInput.ToWarehouseId,
                        ContractNo = tInput.ContractNo,
                        Remarks = tInput.Remarks,
                        IsDeleted = tInput.IsDeleted,
                        CreationTime = tInput.CreationTime,
                        LastModificationTime = tInput.LastModificationTime,
                        DeletionTime = tInput.DeletionTime
                    };

                    var iCreatorUserId = tInput.CreatorUserId.ToLong();
                    if (iCreatorUserId > 0)
                        tOutput.CreateUser = lstUser.FirstOrDefault(x => x.Id == iCreatorUserId)?.LoginName;
                    var iLastModifierUserId = tInput.LastModifierUserId.ToLong();
                    if (iLastModifierUserId > 0)
                        tOutput.LastModifierUser = lstUser.FirstOrDefault(x => x.Id == iLastModifierUserId)?.LoginName;
                    var iDeleterUserId = tInput.DeleterUserId.ToLong();
                    if (iDeleterUserId > 0)
                        tOutput.DeleterUser = lstUser.FirstOrDefault(x => x.Id == iDeleterUserId)?.LoginName;

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

                    tOutput.Detail = new List<PackingListDetailOutput>();
                    foreach (var tDetail in tInput.DetailDict.Values)
                    {
                        var tDetailOutput = new PackingListDetailOutput
                        {
                            Id = tDetail.Id,
                            ContainerNo = tDetail.ContainerNo,
                            ProductId = tDetail.ProductId,
                            SkuId = tDetail.SkuId,
                            SkuCostId = tDetail.SkuCostId,
                            SkuCostBatchId = tDetail.SkuCostBatchId,
                            SkuStockId = tDetail.SkuStockId,
                            ETD = tDetail.ETD.ToDateZNString(),
                            ETA = tDetail.ETA.ToDateZNString(),
                            Qty = tDetail.Qty - tDetail.ActInQty,
                            Type = tDetail.Type
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
