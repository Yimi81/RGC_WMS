using Microsoft.EntityFrameworkCore;
using RGC.WMS.USA.Domain;
using RGC.WMS.USA.Domain.Entities.Purchase;
using RGC.WMS.USA.Domain.Entities.Sku;
using RGC.WMS.USA.Domain.Entities.Warehouse;
using RGC.WMS.USA.Domain.Entities.Warehouse.Enum;
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
    /// MeridianGo 2020/06/24
    /// </summary>
    public class StockInRepository : RepositoryBase<StockIn>, IStockInRepository
    {

        private readonly IStockInDetailRepository _stockInDetailRepository;
        private readonly IPackingListRepository _packingListInfoRepository;
        private readonly IPackingListDetailRepository _packingListDetailRepository;
        private static IWarehouseRepository _warehouseInfoRepository;
        private static ISkuRepository _skuInfoRepository;
        private static ISkuCostBatchRepository _skuCostBatchRepository;

        private static IUnitOfWork _unitOfWork;
        /// <summary>
        /// 锁对象
        /// </summary>
        private static readonly object _locker = new object();

        public StockInRepository(
            DbContext context,
            IStockInDetailRepository stockInDetailRepository,
            IPackingListRepository packingListInfoRepository,
            IPackingListDetailRepository packingListDetailRepository,
            IWarehouseRepository warehouseInfoRepository,
            ISkuRepository skuInfoRepository,
            ISkuCostBatchRepository skuCostBatchRepository,
            IUnitOfWork unitOfWork
            ) : base(context)
        {
            _stockInDetailRepository = stockInDetailRepository;
            _packingListInfoRepository = packingListInfoRepository;
            _packingListDetailRepository = packingListDetailRepository;
            _warehouseInfoRepository = warehouseInfoRepository;
            _skuInfoRepository = skuInfoRepository;
            _skuCostBatchRepository = skuCostBatchRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 获取单个实例
        /// </summary>
        public StockIn Get(long id)
        {
            var result = GetById(id);
            if (result == null)
            {
                result = new StockIn()
                {
                    PackingList = new PackingListInfo(),
                    Warehouse = new WarehouseInfo(),
                    FromWarehouse = new WarehouseInfo(),
                    DetailDict = new Dictionary<long, StockInDetail>()
                };
            }
            else
            {
                if (result.StockInType == StockInType.采购入库)
                    result.PackingList = _packingListInfoRepository.Get(result.PackingId) ?? new PackingListInfo { DetailDict = new Dictionary<long, PackingListDetail>() };
                else
                    result.PackingList = new PackingListInfo { DetailDict = new Dictionary<long, PackingListDetail>() };

                result.Warehouse = _warehouseInfoRepository.GetWarehouseDict().FirstOrDefault(x => x.Id == result.WarehouseId) ?? new WarehouseInfo();
                if (result.StockInType == StockInType.调拨入库)
                    result.FromWarehouse = _warehouseInfoRepository.GetWarehouseDict().FirstOrDefault(x => x.Id == result.FromWarehouseId) ?? new WarehouseInfo();
                else
                    result.FromWarehouse = new WarehouseInfo();

                result.DetailDict = _stockInDetailRepository.TableNoTracking.Where(x => !x.IsDeleted && x.StockInId == id).ToDictionary(x => x.Id);
                if (result.DetailDict == null)
                    result.DetailDict = new Dictionary<long, StockInDetail>();

                var lstPackingDetailId = new List<long>();
                lstPackingDetailId.AddRange(result.DetailDict.Values.Select(x => x.PackingDetailId).ToList());
                List<PackingListDetail> lstPackingDetail = null;
                if (result.StockInType == StockInType.采购入库)
                    lstPackingDetail = _packingListDetailRepository.TableNoTracking.Where(x => lstPackingDetailId.Contains(x.Id)).ToList() ?? new List<PackingListDetail>();
                else
                    lstPackingDetail = new List<PackingListDetail>();

                if (result.DetailDict.Count > 0)
                {
                    var lstSkuId = result.DetailDict.Select(y => y.Value.SkuId).ToList();
                    var lstSkuCostBatchId = result.DetailDict.Select(y => y.Value.SkuCostBatchId).ToList();
                    if (lstPackingDetail.Count > 0)
                    {
                        lstSkuId.AddRange(lstPackingDetail.Select(x => x.SkuId).ToList());
                        lstSkuCostBatchId.AddRange(lstPackingDetail.Select(x => x.SkuCostBatchId).ToList());
                    }

                    var lstSkuDict = _skuInfoRepository.GetSkuInfoDict().Where(x => lstSkuId.Contains(x.Id));
                    var lstSkuCostBatchDict = _skuCostBatchRepository.GetSkuCostBatchDict().Where(x => lstSkuCostBatchId.Contains(x.Id));
                    foreach (var tDetail in result.DetailDict.Values)
                    {
                        if (result.StockInType == StockInType.采购入库)
                        {
                            result.DetailDict[tDetail.Id].PackingDetail = lstPackingDetail.FirstOrDefault(x => x.Id == tDetail.PackingDetailId) ?? new PackingListDetail();
                            result.DetailDict[tDetail.Id].PackingDetail.Sku = lstSkuDict.FirstOrDefault(x => x.Id == result.DetailDict[tDetail.Id].PackingDetail.SkuId) ?? new SkuInfo();
                            result.DetailDict[tDetail.Id].PackingDetail.SkuCostBatch = lstSkuCostBatchDict.FirstOrDefault(x => x.Id == result.DetailDict[tDetail.Id].PackingDetail.SkuCostBatchId) ?? new SkuCostBatch();
                        }
                        else
                            result.DetailDict[tDetail.Id].PackingDetail = new PackingListDetail();

                        result.DetailDict[tDetail.Id].Sku = lstSkuDict.FirstOrDefault(x => x.Id == tDetail.SkuId) ?? new SkuInfo();
                        result.DetailDict[tDetail.Id].SkuCostBatch = lstSkuCostBatchDict.FirstOrDefault(x => x.Id == tDetail.SkuCostBatchId) ?? new SkuCostBatch();
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 新增入库单
        /// </summary>
        public void Create(long loginId, StockIn model)
        {
            lock (_locker)
            {
                using (var transaction = BeginTransaction())
                {
                    try
                    {
                        model.Id = 0;
                        model.ResetAddModel(loginId);
                        Insert(model);
                        var excute = _unitOfWork.SaveChanges();
                        if (excute > 0)
                        {
                            var lstAdd = new List<StockInDetail>();
                            foreach (var tDetail in model.DetailDict.Values)
                            {
                                tDetail.Id = 0;
                                tDetail.StockInId = model.Id;
                                tDetail.ResetAddModel(loginId);
                                lstAdd.Add(tDetail);
                            }

                            if (lstAdd.Count > 0)
                                _stockInDetailRepository.Insert(lstAdd);

                            _unitOfWork.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception error)
                    {
                        transaction.Rollback();
                        throw error;
                    }
                }
            }
        }

        /// <summary>
        /// 更新入库单
        /// </summary>
        public void Update(long loginId, StockIn model)
        {
            lock (_locker)
            {
                using (var transaction = BeginTransaction())
                {
                    try
                    {
                        model.ResetModifyModel(loginId);
                        Update(model);
                        var excute = _unitOfWork.SaveChanges();
                        if (excute > 0)
                        {
                            var lstAdd = new List<StockInDetail>();
                            var lstUpdate = new List<StockInDetail>();
                            foreach (var tDetail in model.DetailDict.Values)
                            {
                                if (tDetail.Id <= 0)
                                {
                                    tDetail.StockInId = model.Id;
                                    tDetail.ResetAddModel(loginId);
                                    lstAdd.Add(tDetail);
                                }
                                else
                                {
                                    if (tDetail.IsDeleted)
                                        tDetail.ResetDeleteModel(loginId);
                                    else
                                        tDetail.ResetAddModel(loginId);
                                    lstUpdate.Add(tDetail);
                                }
                            }

                            var lstDeleteDetail = model.DetailDict.Values.Where(x => !lstUpdate.Select(y => y.Id).ToList().Contains(x.Id)).ToList();
                            foreach (var tDetail in lstDeleteDetail)
                            {
                                tDetail.ResetDeleteModel(loginId);
                                lstUpdate.Add(tDetail);
                            }

                            if (lstAdd.Count > 0)
                                _stockInDetailRepository.Insert(lstAdd);

                            if (lstUpdate.Count > 0)
                                _stockInDetailRepository.Update(lstUpdate);

                            _unitOfWork.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception error)
                    {
                        transaction.Rollback();
                        throw error;
                    }
                }
            }
        }

        /// <summary>
        /// 删除入库单
        /// </summary>
        public int Delete(long loginId, StockIn model)
        {
            lock (_locker)
            {
                model.ResetDeleteModel(loginId);
                Update(model, x => x.IsDeleted, x => x.DeleterUserId, x => x.DeletionTime);
                var excute = _unitOfWork.SaveChanges();
                return excute;
            }
        }

        /// <summary>
        /// 删除入库单明细
        /// </summary>
        public int DetailDelete(long loginId, StockInDetail model)
        {
            lock (_locker)
            {
                var excute = 0;
                using (var transaction = BeginTransaction())
                {
                    try
                    {
                        model.ResetDeleteModel(loginId);
                        _stockInDetailRepository.Update(model, x => x.IsDeleted, x => x.DeleterUserId, x => x.DeletionTime);
                        excute = _unitOfWork.SaveChanges();
                        if (excute > 0)
                        {
                            if (_stockInDetailRepository.TableNoTracking.Count(x => x.StockInId == model.StockInId && x.Id != model.Id && !x.IsDeleted) <= 0)
                            {
                                var tStockIn = GetById(model.StockInId);
                                if (tStockIn != null)
                                    if (this.Delete(loginId, tStockIn) <= 0)
                                        throw new CustomException("删除失败", 1);
                            }
                        }
                        transaction.Commit();
                    }
                    catch (Exception error)
                    {
                        transaction.Rollback();
                        throw error;
                    }
                }
                return excute;
            }
        }

        /// <summary>
        /// 恢复入库单
        /// </summary>
        public int Recovery(long loginId, StockIn model)
        {
            lock (_locker)
            {
                model.ResetRecoveryModel(loginId);
                Update(model, x => x.IsDeleted, x => x.DeleterUserId, x => x.DeletionTime);
                var excute = _unitOfWork.SaveChanges();
                return excute;
            }
        }

        /// <summary>
        /// 恢复入库单明细
        /// </summary>
        public int DetailRecovery(long loginId, StockInDetail model)
        {
            lock (_locker)
            {
                var excute = 0;
                using (var transaction = BeginTransaction())
                {
                    try
                    {
                        model.ResetRecoveryModel(loginId);
                        _stockInDetailRepository.Update(model, x => x.IsDeleted, x => x.DeleterUserId, x => x.DeletionTime);
                        excute = _unitOfWork.SaveChanges();
                        if (excute > 0)
                        {
                            if (TableNoTracking.Count(x => x.Id == model.StockInId && x.IsDeleted) > 0)
                            {
                                var tStockIn = GetById(model.StockInId);
                                if (tStockIn != null)
                                    if (this.Recovery(loginId, tStockIn) <= 0)
                                        throw new CustomException("恢复失败", 1);
                            }
                        }
                        transaction.Commit();
                    }
                    catch (Exception error)
                    {
                        transaction.Rollback();
                        throw error;
                    }
                }
                return excute;
            }
        }

        /// <summary>
        /// 更改入库单状态
        /// </summary>
        public int UpdateStatus(StockIn model, long modifierUserId)
        {
            model.ResetModifyModel(modifierUserId);
            Update(model, x => x.StockInStatus, x => x.LastModifierUserId, x => x.LastModificationTime);
            var excute = _unitOfWork.SaveChanges();
            return excute;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public List<StockIn> GetPage((string SearchType, string SearchKey, long WarehouseId, int Status, int IsDeleted, int PageSize, int CurrentPage) searchFilter, out int count)
        {
            var result = new List<StockIn>();
            count = 0;

            var lstQuery = TableNoTracking;

            searchFilter.SearchKey = searchFilter.SearchKey.ToEmpty().ToLower();

            if (searchFilter.WarehouseId > 0)
            {
                lstQuery = lstQuery.Where(x => x.WarehouseId == searchFilter.WarehouseId);
            }

            if (searchFilter.Status > 0)
            {
                lstQuery = lstQuery.Where(x => x.StockInStatus == (StockInStatus)(searchFilter.Status));
            }

            if (searchFilter.IsDeleted > 0)
            {
                var bIsDeleted = searchFilter.IsDeleted == 2;
                lstQuery = lstQuery.Where(x => x.IsDeleted == bIsDeleted);
            }

            var lstAllSkuDict = _skuInfoRepository.GetSkuInfoDict();
            var lstAllSkuCostBatchDict = _skuCostBatchRepository.GetSkuCostBatchDict();

            var lstSearchStockInId = new List<long>();
            var lstSearchStockInDetailId = new List<long>();
            if (!string.IsNullOrEmpty(searchFilter.SearchType) && !string.IsNullOrEmpty(searchFilter.SearchKey))
            {
                switch (searchFilter.SearchType)
                {
                    case "contractNo":
                        var lstSearchPackingListId = _packingListInfoRepository.TableNoTracking.Where(x => x.ContractNo.ToLower().Contains(searchFilter.SearchKey)).Select(x => x.Id).ToList();
                        lstQuery = lstQuery.Where(x => lstSearchPackingListId.Contains(x.PackingId));
                        break;
                    case "containerNo":
                        {
                            var lstSearchPackingDetailId = _packingListDetailRepository.TableNoTracking.Where(x => x.ContainerNo.ToLower().Contains(searchFilter.SearchKey)).Select(x => x.Id).ToList();
                            var lstStockInDetailQuery = _stockInDetailRepository.TableNoTracking.Where(x => !x.IsDeleted && lstSearchPackingDetailId.Contains(x.PackingDetailId));
                            var lstStockInResult = lstStockInDetailQuery.Select(x => new
                            {
                                x.StockInId,
                                StockInDetailId = x.Id
                            }).ToList();

                            lstSearchStockInId.AddRange(lstStockInResult.Select(x => x.StockInId).Distinct().ToList());
                            lstSearchStockInDetailId.AddRange(lstStockInResult.Select(x => x.StockInDetailId).Distinct().ToList());
                            lstQuery = lstQuery.Where(x => lstSearchStockInId.Contains(x.Id));
                        }
                        break;
                    case "skuName":
                        {
                            var lstSkuId = lstAllSkuDict.Where(x => x.FullEName.ToEmpty().ToLower().Contains(searchFilter.SearchKey)).Select(x => x.Id).ToList();
                            var lstStockInDetailQuery = _stockInDetailRepository.TableNoTracking.Where(x => !x.IsDeleted && lstSkuId.Contains(x.SkuId));
                            var lstStockInResult = lstStockInDetailQuery.Select(x => new
                            {
                                x.StockInId,
                                StockInDetailId = x.Id
                            }).ToList();

                            lstSearchStockInId.AddRange(lstStockInResult.Select(x => x.StockInId).Distinct().ToList());
                            lstSearchStockInDetailId.AddRange(lstStockInResult.Select(x => x.StockInDetailId).Distinct().ToList());
                            lstQuery = lstQuery.Where(x => lstSearchStockInId.Contains(x.Id));
                        }
                        break;
                    case "batchNo":
                        {
                            var lstSkuCostBatchId = lstAllSkuCostBatchDict.Where(x => x.BatchNo.ToEmpty().ToLower().Contains(searchFilter.SearchKey)).Select(x => x.Id).ToList();
                            var lstStockInDetailQuery = _stockInDetailRepository.TableNoTracking.Where(x => !x.IsDeleted && lstSkuCostBatchId.Contains(x.SkuCostBatchId));
                            var lstStockInResult = lstStockInDetailQuery.Select(x => new
                            {
                                x.StockInId,
                                StockInDetailId = x.Id
                            }).ToList();

                            lstSearchStockInId.AddRange(lstStockInResult.Select(x => x.StockInId).Distinct().ToList());
                            lstSearchStockInDetailId.AddRange(lstStockInResult.Select(x => x.StockInDetailId).Distinct().ToList());
                            lstQuery = lstQuery.Where(x => lstSearchStockInId.Contains(x.Id));
                        }
                        break;
                    case "factoryModel":
                        {
                            var lstSkuId = lstAllSkuDict.Where(x => x.FactoryModel.ToEmpty().ToLower().Contains(searchFilter.SearchKey)).Select(x => x.Id).ToList();
                            var lstStockInDetailQuery = _stockInDetailRepository.TableNoTracking.Where(x => !x.IsDeleted && lstSkuId.Contains(x.SkuId));
                            var lstStockInResult = lstStockInDetailQuery.Select(x => new
                            {
                                x.StockInId,
                                StockInDetailId = x.Id
                            }).ToList();

                            lstSearchStockInId.AddRange(lstStockInResult.Select(x => x.StockInId).Distinct().ToList());
                            lstSearchStockInDetailId.AddRange(lstStockInResult.Select(x => x.StockInDetailId).Distinct().ToList());
                            lstQuery = lstQuery.Where(x => lstSearchStockInId.Contains(x.Id));
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
                var lstStockInDetailQuery = _stockInDetailRepository.TableNoTracking.Where(x => !x.IsDeleted && list.Select(y => y.Id).ToList().Contains(x.StockInId)).ToList();
                var lstStockInDetailResult = new List<StockInDetail>();
                if (!string.IsNullOrEmpty(searchFilter.SearchType) && !string.IsNullOrEmpty(searchFilter.SearchKey))
                {
                    switch (searchFilter.SearchType)
                    {
                        case "containerNo":
                        case "skuName":
                        case "factoryModel":
                        case "batchNo":
                            lstStockInDetailResult = lstStockInDetailQuery.Where(x => lstSearchStockInDetailId.Contains(x.Id)).ToList();
                            break;
                        default:
                            lstStockInDetailResult = lstStockInDetailQuery.ToList();
                            break;
                    }
                }
                else
                    lstStockInDetailResult = lstStockInDetailQuery.ToList();

                var lstPackingListInfo = _packingListInfoRepository.TableNoTracking.Where(x => list.Select(x => x.PackingId).ToList().Contains(x.Id)).ToList();
                var lstPackingDetail = _packingListDetailRepository.TableNoTracking.Where(x => list.Select(x => x.PackingId).ToList().Contains(x.PackingListId)).ToList();

                var lstWarehouseId = list.Select(x => x.WarehouseId).ToList();
                lstWarehouseId.AddRange(list.Select(x => x.FromWarehouseId).ToList());
                var lstWarehouseDict = _warehouseInfoRepository.GetWarehouseDict().Where(x => lstWarehouseId.Contains(x.Id));

                var lstSkuId = lstStockInDetailResult.Select(y => y.SkuId).ToList();
                lstSkuId.AddRange(lstPackingDetail.Select(x => x.SkuId).Distinct().ToList());
                var lstSkuDict = lstAllSkuDict.Where(x => lstSkuId.Contains(x.Id));

                var lstSkuCostBatchId = lstStockInDetailResult.Select(y => y.SkuCostBatchId).ToList();
                lstSkuCostBatchId.AddRange(lstPackingDetail.Select(x => x.SkuCostBatchId).Distinct().ToList());
                var lstSkuCostBatchDict = lstAllSkuCostBatchDict.Where(x => lstSkuCostBatchId.Contains(x.Id));

                foreach (var item in list)
                {
                    if (item.StockInType == StockInType.采购入库)
                        item.PackingList = lstPackingListInfo.FirstOrDefault(x => x.Id == item.PackingId) ?? new PackingListInfo();
                    else
                        item.PackingList = new PackingListInfo();

                    item.DetailDict = lstStockInDetailResult.Where(x => x.StockInId == item.Id).ToDictionary(x => x.Id);
                    if (item.DetailDict == null)
                        item.DetailDict = new Dictionary<long, StockInDetail>();

                    item.Warehouse = lstWarehouseDict.FirstOrDefault(x => x.Id == item.WarehouseId) ?? new WarehouseInfo();
                    if (item.StockInType == StockInType.调拨入库)
                        item.FromWarehouse = lstWarehouseDict.FirstOrDefault(x => x.Id == item.FromWarehouseId) ?? new WarehouseInfo();
                    else
                        item.FromWarehouse = new WarehouseInfo();
                    foreach (var tDetail in item.DetailDict.Values)
                    {
                        if (item.StockInType == StockInType.采购入库)
                            item.DetailDict[tDetail.Id].PackingDetail = lstPackingDetail.FirstOrDefault(x => x.Id == tDetail.PackingDetailId) ?? new PackingListDetail();
                        else
                            item.DetailDict[tDetail.Id].PackingDetail = new PackingListDetail();

                        if (item.DetailDict[tDetail.Id].PackingDetail.SkuId > 0)
                            item.DetailDict[tDetail.Id].PackingDetail.Sku = lstSkuDict.FirstOrDefault(x => x.Id == item.DetailDict[tDetail.Id].PackingDetail.SkuId) ?? new SkuInfo();

                        if (item.DetailDict[tDetail.Id].PackingDetail.SkuCostBatchId > 0)
                            item.DetailDict[tDetail.Id].PackingDetail.SkuCostBatch = lstSkuCostBatchDict.FirstOrDefault(x => x.Id == item.DetailDict[tDetail.Id].PackingDetail.SkuCostBatchId) ?? new SkuCostBatch();

                        item.DetailDict[tDetail.Id].Sku = lstSkuDict.FirstOrDefault(x => x.Id == tDetail.SkuId) ?? new SkuInfo();
                        item.DetailDict[tDetail.Id].SkuCostBatch = lstSkuCostBatchDict.FirstOrDefault(x => x.Id == tDetail.SkuCostBatchId) ?? new SkuCostBatch();
                    }
                }
                result = list;
            }
            return result;
        }
    }
}
