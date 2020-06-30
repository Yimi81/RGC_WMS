using Microsoft.EntityFrameworkCore;
using RGC.WMS.USA.Domain;
using RGC.WMS.USA.Domain.Entities.Purchase;
using RGC.WMS.USA.Domain.Entities.Sku;
using RGC.WMS.USA.Domain.Entities.Warehouse;
using RGC.WMS.USA.Domain.Repositories.Purchase;
using RGC.WMS.USA.Domain.Repositories.Sku;
using RGC.WMS.USA.Domain.Repositories.Warehouse;
using RGC.WMS.USA.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Data.Repositories.Warehouse
{
    /// <summary>
    /// MeridianGo 2020/06/17
    /// </summary>
    public class PackingListRepository : RepositoryBase<PackingListInfo>, IPackingListRepository
    {
        private static IUnitOfWork _unitOfWork;
        /// <summary>
        /// 锁对象
        /// </summary>
        private static readonly object _locker = new object();

        private readonly IPackingListDetailRepository _packingListDetailRepository;
        private readonly IWarehouseRepository _warehouseInfoRepository;
        private readonly ISkuRepository _skuInfoRepository;
        private readonly ISkuCostBatchRepository _skuCostBatchRepository;

        public PackingListRepository(
            DbContext context,
            IPackingListDetailRepository packingListDetailRepository,
            IWarehouseRepository warehouseInfoRepository,
            ISkuRepository skuInfoRepository,
            ISkuCostBatchRepository skuCostBatchRepository,
            IUnitOfWork unitOfWork) : base(context)
        {
            _packingListDetailRepository = packingListDetailRepository;
            _warehouseInfoRepository = warehouseInfoRepository;
            _skuInfoRepository = skuInfoRepository;
            _skuCostBatchRepository = skuCostBatchRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 获取单个实例
        /// </summary>
        public PackingListInfo Get(long id)
        {
            var result = GetById(id);
            if (result == null)
            {
                result = new PackingListInfo
                {
                    ToWarehouse = new WarehouseInfo(),
                    DetailDict = new Dictionary<long, PackingListDetail>()
                };
            }
            else
            {
                result.DetailDict = _packingListDetailRepository.TableNoTracking.Where(x => x.PackingListId == id).OrderBy(x => x.ContainerNo).ToDictionary(x => x.Id);
                if (result.DetailDict == null)
                    result.DetailDict = new Dictionary<long, PackingListDetail>();

                result.ToWarehouse = _warehouseInfoRepository.GetWarehouseDict().FirstOrDefault(x => x.Id == result.ToWarehouseId) ?? new WarehouseInfo();
                if (result.DetailDict.Count > 0)
                {
                    var lstSkuDict = _skuInfoRepository.GetSkuInfoDict().Where(x => result.DetailDict.Select(y => y.Value.SkuId).ToList().Contains(x.Id));
                    var lstSkuCostBatchDict = _skuCostBatchRepository.GetSkuCostBatchDict().Where(x => result.DetailDict.Select(y => y.Value.SkuCostBatchId).ToList().Contains(x.Id));
                    foreach (var tCargo in result.DetailDict.Values)
                    {
                        result.DetailDict[tCargo.Id].Sku = lstSkuDict.FirstOrDefault(x => x.Id == tCargo.SkuId) ?? new SkuInfo();
                        result.DetailDict[tCargo.Id].SkuCostBatch = lstSkuCostBatchDict.FirstOrDefault(x => x.Id == tCargo.SkuCostBatchId) ?? new SkuCostBatch();
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 更改状态
        /// </summary>
        public int UpdateStatus(PackingListInfo model, long modifierUserId)
        {
            lock (_locker)
            {
                var id = model.Id;
                model.ResetModifyModel(modifierUserId);
                Update(model, x => x.Status, x => x.LastModifierUserId, x => x.LastModificationTime);
                var excute = _unitOfWork.SaveChanges();
                return excute;
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public List<PackingListInfo> GetPage((string SearchType, string SearchKey, long WarehouseId, int IsDeleted, int PageSize, int CurrentPage) searchFilter, out int count)
        {
            var result = new List<PackingListInfo>();
            count = 0;

            var lstQuery = TableNoTracking;

            searchFilter.SearchKey = searchFilter.SearchKey.ToEmpty().ToLower();

            if (searchFilter.WarehouseId > 0)
            {
                lstQuery = lstQuery.Where(x => x.ToWarehouseId == searchFilter.WarehouseId);
            }

            if (searchFilter.IsDeleted > 0)
            {
                var bIsDeleted = searchFilter.IsDeleted == 2;
                lstQuery = lstQuery.Where(x => x.IsDeleted == bIsDeleted);
            }

            var lstSearchPackingId = new List<long>();
            var lstSearchPackingDetailId = new List<long>();
            if (!string.IsNullOrEmpty(searchFilter.SearchType) && !string.IsNullOrEmpty(searchFilter.SearchKey))
            {
                switch (searchFilter.SearchType)
                {
                    case "contractNo":
                        lstQuery = lstQuery.Where(x => x.ContractNo.ToLower().Contains(searchFilter.SearchKey));
                        break;
                    case "containerNo":
                        {
                            var lstPackingDetailQuery = _packingListDetailRepository.TableNoTracking.Where(x => !x.IsDeleted && (x.Qty - x.ActInQty) > 0 && x.ContainerNo.ToLower().Contains(searchFilter.SearchKey));
                            var lstPackingResult = lstPackingDetailQuery.Select(x => new
                            {
                                PackingId = x.PackingListId,
                                PackingDetailId = x.Id
                            }).ToList();

                            lstSearchPackingId.AddRange(lstPackingResult.Select(x => x.PackingId).Distinct().ToList());
                            lstSearchPackingDetailId.AddRange(lstPackingResult.Select(x => x.PackingDetailId).Distinct().ToList());
                            lstQuery = lstQuery.Where(x => lstSearchPackingId.Contains(x.Id));
                        }
                        break;
                    case "skuName":
                        {
                            var lstSkuId = _skuInfoRepository.GetSkuInfoDict().Where(x => !x.IsDeleted && x.FullEName.ToEmpty().ToLower().Contains(searchFilter.SearchKey)).Select(x => x.Id).ToList();
                            var lstPackingDetailQuery = _packingListDetailRepository.TableNoTracking.Where(x => !x.IsDeleted && (x.Qty - x.ActInQty) > 0 && lstSkuId.Contains(x.SkuId));
                            var lstPackingResult = lstPackingDetailQuery.Select(x => new
                            {
                                PackingId = x.PackingListId,
                                PackingDetailId = x.Id
                            }).ToList();

                            lstSearchPackingId.AddRange(lstPackingResult.Select(x => x.PackingId).Distinct().ToList());
                            lstSearchPackingDetailId.AddRange(lstPackingResult.Select(x => x.PackingDetailId).Distinct().ToList());
                            lstQuery = lstQuery.Where(x => lstSearchPackingId.Contains(x.Id));
                        }
                        break;
                    case "batchNo":
                        {
                            var lstSkuCostBatchId = _skuCostBatchRepository.GetSkuCostBatchDict().Where(x => x.BatchNo.ToLower().Contains(searchFilter.SearchKey)).Select(x => x.Id).ToList();
                            var lstPackingDetailQuery = _packingListDetailRepository.TableNoTracking.Where(x => !x.IsDeleted && (x.Qty - x.ActInQty) > 0 && lstSkuCostBatchId.Contains(x.SkuCostBatchId));
                            var lstPackingResult = lstPackingDetailQuery.Select(x => new
                            {
                                PackingId = x.PackingListId,
                                PackingDetailId = x.Id
                            }).ToList();

                            lstSearchPackingId.AddRange(lstPackingResult.Select(x => x.PackingId).Distinct().ToList());
                            lstSearchPackingDetailId.AddRange(lstPackingResult.Select(x => x.PackingDetailId).Distinct().ToList());
                            lstQuery = lstQuery.Where(x => lstSearchPackingId.Contains(x.Id));
                        }
                        break;
                    case "factoryModel":
                        {
                            var lstSkuId = _skuInfoRepository.GetSkuInfoDict().Where(x => !x.IsDeleted && x.FactoryModel.ToEmpty().ToLower().Contains(searchFilter.SearchKey)).Select(x => x.Id).ToList();
                            var lstPackingDetailQuery = _packingListDetailRepository.TableNoTracking.Where(x => !x.IsDeleted && (x.Qty - x.ActInQty) > 0 && lstSkuId.Contains(x.SkuId));
                            var lstPackingResult = lstPackingDetailQuery.Select(x => new
                            {
                                PackingId = x.PackingListId,
                                PackingDetailId = x.Id
                            }).ToList();

                            lstSearchPackingId.AddRange(lstPackingResult.Select(x => x.PackingId).Distinct().ToList());
                            lstSearchPackingDetailId.AddRange(lstPackingResult.Select(x => x.PackingDetailId).Distinct().ToList());
                            lstQuery = lstQuery.Where(x => lstSearchPackingId.Contains(x.Id));
                        }
                        break;
                }
            }

            count = lstQuery.Count();

            var list = lstQuery
                .OrderByDescending(p => p.CreationTime)
                .Skip(searchFilter.CurrentPage * searchFilter.PageSize - searchFilter.PageSize)
                .Take(searchFilter.PageSize)
                .ToList();

            if (list.Any())
            {
                var lstPackingDetailQuery = _packingListDetailRepository.TableNoTracking.Where(x => !x.IsDeleted && (x.Qty - x.ActInQty) > 0 && list.Select(y => y.Id).ToList().Contains(x.PackingListId)).ToList();
                var lstPackingDetailResult = new List<PackingListDetail>();
                if (!string.IsNullOrEmpty(searchFilter.SearchType) && !string.IsNullOrEmpty(searchFilter.SearchKey))
                {
                    switch (searchFilter.SearchType)
                    {
                        case "containerNo":
                        case "skuName":
                        case "factoryModel":
                        case "batchNo":
                            lstPackingDetailResult = lstPackingDetailQuery.Where(x => lstSearchPackingDetailId.Contains(x.Id)).ToList();
                            break;
                        default:
                            lstPackingDetailResult = lstPackingDetailQuery.ToList();
                            break;
                    }
                }
                else
                    lstPackingDetailResult = lstPackingDetailQuery.ToList();

                var lstWarehouseDict = _warehouseInfoRepository.GetWarehouseDict().Where(x => list.Select(y => y.ToWarehouseId).ToList().Contains(x.Id));
                var lstSkuDict = _skuInfoRepository.GetSkuInfoDict().Where(x => lstPackingDetailResult.Select(y => y.SkuId).ToList().Contains(x.Id));
                var lstSkuCostBatchDict = _skuCostBatchRepository.GetSkuCostBatchDict().Where(x => lstPackingDetailResult.Select(y => y.SkuCostBatchId).ToList().Contains(x.Id));

                foreach (var item in list)
                {
                    item.DetailDict = lstPackingDetailResult.Where(x => x.PackingListId == item.Id).OrderBy(x => x.ContainerNo).ToDictionary(x => x.Id);
                    if (item.DetailDict == null)
                        item.DetailDict = new Dictionary<long, PackingListDetail>();

                    item.ToWarehouse = lstWarehouseDict.FirstOrDefault(x => x.Id == item.ToWarehouseId) ?? new WarehouseInfo();
                    foreach (var tCargo in item.DetailDict.Values)
                    {
                        item.DetailDict[tCargo.Id].Sku = lstSkuDict.FirstOrDefault(x => x.Id == tCargo.SkuId) ?? new SkuInfo();
                        item.DetailDict[tCargo.Id].SkuCostBatch = lstSkuCostBatchDict.FirstOrDefault(x => x.Id == tCargo.SkuCostBatchId) ?? new SkuCostBatch();
                    }
                }
                result = list;
            }
            return result;
        }
    }
}
