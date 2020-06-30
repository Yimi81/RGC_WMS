using Microsoft.EntityFrameworkCore;
using RGC.WMS.USA.Domain;
using RGC.WMS.USA.Domain.Entities.Sku;
using RGC.WMS.USA.Domain.Entities.Warehouse;
using RGC.WMS.USA.Domain.Entities.Warehouse.Enum;
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
    public class StockOutRepository : RepositoryBase<StockOut>, IStockOutRepository
    {

        private readonly IStockOutDetailRepository _stockOutDetailRepository;
        private static IWarehouseRepository _warehouseInfoRepository;
        private static ISkuRepository _skuInfoRepository;
        private static ISkuCostBatchRepository _skuCostBatchRepository;

        private static IUnitOfWork _unitOfWork;
        /// <summary>
        /// 锁对象
        /// </summary>
        private static readonly object _locker = new object();

        public StockOutRepository(
            DbContext context,
            IStockOutDetailRepository stockOutDetailRepository,
            IWarehouseRepository warehouseInfoRepository,
            ISkuRepository skuInfoRepository,
            ISkuCostBatchRepository skuCostBatchRepository,
            IUnitOfWork unitOfWork
            ) : base(context)
        {
            _stockOutDetailRepository = stockOutDetailRepository;
            _warehouseInfoRepository = warehouseInfoRepository;
            _skuInfoRepository = skuInfoRepository;
            _skuCostBatchRepository = skuCostBatchRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 获取单个实例
        /// </summary>
        public StockOut Get(long id)
        {
            var result = GetById(id);
            if (result == null)
            {
                result = new StockOut()
                {
                    Warehouse = new WarehouseInfo(),
                    ToWarehouse = new WarehouseInfo(),
                    DetailDict = new Dictionary<long, StockOutDetail>()
                };
            }
            else
            {
                result.Warehouse = _warehouseInfoRepository.GetWarehouseDict().FirstOrDefault(x => x.Id == result.WarehouseId) ?? new WarehouseInfo();
                if (result.StockOutType == StockOutType.调拨出库)
                    result.ToWarehouse = _warehouseInfoRepository.GetWarehouseDict().FirstOrDefault(x => x.Id == result.ToWarehouseId) ?? new WarehouseInfo();
                else
                    result.ToWarehouse = new WarehouseInfo();

                result.DetailDict = _stockOutDetailRepository.TableNoTracking.Where(x => !x.IsDeleted && x.StockOutId == id).ToDictionary(x => x.Id);
                if (result.DetailDict == null)
                    result.DetailDict = new Dictionary<long, StockOutDetail>();

                if (result.DetailDict.Count > 0)
                {
                    var lstSkuDict = _skuInfoRepository.GetSkuInfoDict().Where(x => result.DetailDict.Select(y => y.Value.SkuId).ToList().Contains(x.Id));
                    var lstSkuCostBatchDict = _skuCostBatchRepository.GetSkuCostBatchDict().Where(x => result.DetailDict.Select(y => y.Value.SkuCostBatchId).ToList().Contains(x.Id));
                    foreach (var tDetail in result.DetailDict.Values)
                    {
                        result.DetailDict[tDetail.Id].Sku = lstSkuDict.FirstOrDefault(x => x.Id == tDetail.SkuId) ?? new SkuInfo();
                        result.DetailDict[tDetail.Id].SkuCostBatch = lstSkuCostBatchDict.FirstOrDefault(x => x.Id == tDetail.SkuCostBatchId) ?? new SkuCostBatch();
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 新增出库单
        /// </summary>
        public void Create(long loginId, StockOut model)
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
                            var lstAdd = new List<StockOutDetail>();
                            foreach (var tDetail in model.DetailDict.Values)
                            {
                                tDetail.Id = 0;
                                tDetail.StockOutId = model.Id;
                                tDetail.ResetAddModel(loginId);
                                lstAdd.Add(tDetail);
                            }

                            if (lstAdd.Count > 0)
                                _stockOutDetailRepository.Insert(lstAdd);

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
        /// 更新出库单
        /// </summary>
        public void Update(long loginId, StockOut model)
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
                            var lstAdd = new List<StockOutDetail>();
                            var lstUpdate = new List<StockOutDetail>();
                            foreach (var tDetail in model.DetailDict.Values)
                            {
                                if (tDetail.Id <= 0)
                                {
                                    tDetail.StockOutId = model.Id;
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
                                _stockOutDetailRepository.Insert(lstAdd);

                            if (lstUpdate.Count > 0)
                                _stockOutDetailRepository.Update(lstUpdate);

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
        /// 删除出库单
        /// </summary>
        public int Delete(long loginId, StockOut model)
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
        /// 删除出库单明细
        /// </summary>
        public int DetailDelete(long loginId, StockOutDetail model)
        {
            lock (_locker)
            {
                var excute = 0;
                using (var transaction = BeginTransaction())
                {
                    try
                    {
                        model.ResetDeleteModel(loginId);
                        _stockOutDetailRepository.Update(model, x => x.IsDeleted, x => x.DeleterUserId, x => x.DeletionTime);
                        excute = _unitOfWork.SaveChanges();
                        if (excute > 0)
                        {
                            if (_stockOutDetailRepository.TableNoTracking.Count(x => x.StockOutId == model.StockOutId && x.Id != model.Id && !x.IsDeleted) <= 0)
                            {
                                var tStockIn = GetById(model.StockOutId);
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
        /// 恢复出库单
        /// </summary>
        public int Recovery(long loginId, StockOut model)
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
        /// 恢复出库单明细
        /// </summary>
        public int DetailRecovery(long loginId, StockOutDetail model)
        {
            lock (_locker)
            {
                var excute = 0;
                using (var transaction = BeginTransaction())
                {
                    try
                    {
                        model.ResetRecoveryModel(loginId);
                        _stockOutDetailRepository.Update(model, x => x.IsDeleted, x => x.DeleterUserId, x => x.DeletionTime);
                        excute = _unitOfWork.SaveChanges();
                        if (excute > 0)
                        {
                            if (TableNoTracking.Count(x => x.Id == model.StockOutId && x.IsDeleted) > 0)
                            {
                                var tStockIn = GetById(model.StockOutId);
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
        /// 更改出库单状态
        /// </summary>
        public int UpdateStatus(StockOut model, long modifierUserId)
        {
            model.ResetModifyModel(modifierUserId);
            Update(model, x => x.StockOutStatus, x => x.LastModifierUserId, x => x.LastModificationTime);
            var excute = _unitOfWork.SaveChanges();
            return excute;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public List<StockOut> GetPage((string SearchType, string SearchKey, long WarehouseId, int Status, int IsDeleted, int PageSize, int CurrentPage) searchFilter, out int count)
        {
            var result = new List<StockOut>();
            count = 0;

            var lstQuery = TableNoTracking;

            searchFilter.SearchKey = searchFilter.SearchKey.ToEmpty().ToLower();

            if (searchFilter.WarehouseId > 0)
            {
                lstQuery = lstQuery.Where(x => x.WarehouseId == searchFilter.WarehouseId);
            }

            if (searchFilter.Status > 0)
            {
                lstQuery = lstQuery.Where(x => x.StockOutStatus == (StockOutStatus)(searchFilter.Status));
            }

            if (searchFilter.IsDeleted > 0)
            {
                var bIsDeleted = searchFilter.IsDeleted == 2;
                lstQuery = lstQuery.Where(x => x.IsDeleted == bIsDeleted);
            }

            var lstAllSkuDict = _skuInfoRepository.GetSkuInfoDict();
            var lstAllSkuCostBatchDict = _skuCostBatchRepository.GetSkuCostBatchDict();

            var lstSearchStockOutId = new List<long>();
            var lstSearchStockOutDetailId = new List<long>();
            if (!string.IsNullOrEmpty(searchFilter.SearchType) && !string.IsNullOrEmpty(searchFilter.SearchKey))
            {
                switch (searchFilter.SearchType)
                {
                    case "skuName":
                        {
                            var lstSkuId = lstAllSkuDict.Where(x => x.FullEName.ToEmpty().ToLower().Contains(searchFilter.SearchKey)).Select(x => x.Id).ToList();
                            var lstStockOutDetailQuery = _stockOutDetailRepository.TableNoTracking.Where(x => !x.IsDeleted && lstSkuId.Contains(x.SkuId));
                            var lstStockOutResult = lstStockOutDetailQuery.Select(x => new
                            {
                                x.StockOutId,
                                StockInDetailId = x.Id
                            }).ToList();

                            lstSearchStockOutId.AddRange(lstStockOutResult.Select(x => x.StockOutId).Distinct().ToList());
                            lstSearchStockOutDetailId.AddRange(lstStockOutResult.Select(x => x.StockInDetailId).Distinct().ToList());
                            lstQuery = lstQuery.Where(x => lstSearchStockOutId.Contains(x.Id));
                        }
                        break;
                    case "batchNo":
                        {
                            var lstSkuCostBatchId = lstAllSkuCostBatchDict.Where(x => x.BatchNo.ToEmpty().ToLower().Contains(searchFilter.SearchKey)).Select(x => x.Id).ToList();
                            var lstStockOutDetailQuery = _stockOutDetailRepository.TableNoTracking.Where(x => !x.IsDeleted && lstSkuCostBatchId.Contains(x.SkuCostBatchId));
                            var lstStockOutResult = lstStockOutDetailQuery.Select(x => new
                            {
                                x.StockOutId,
                                StockOutDetailId = x.Id
                            }).ToList();

                            lstSearchStockOutId.AddRange(lstStockOutResult.Select(x => x.StockOutId).Distinct().ToList());
                            lstSearchStockOutDetailId.AddRange(lstStockOutResult.Select(x => x.StockOutDetailId).Distinct().ToList());
                            lstQuery = lstQuery.Where(x => lstSearchStockOutId.Contains(x.Id));
                        }
                        break;
                    case "factoryModel":
                        {
                            var lstSkuId = lstAllSkuDict.Where(x => x.FactoryModel.ToEmpty().ToLower().Contains(searchFilter.SearchKey)).Select(x => x.Id).ToList();
                            var lstStockOutDetailQuery = _stockOutDetailRepository.TableNoTracking.Where(x => !x.IsDeleted && lstSkuId.Contains(x.SkuId));
                            var lstStockOutResult = lstStockOutDetailQuery.Select(x => new
                            {
                                x.StockOutId,
                                StockOutDetailId = x.Id
                            }).ToList();

                            lstSearchStockOutId.AddRange(lstStockOutResult.Select(x => x.StockOutId).Distinct().ToList());
                            lstSearchStockOutDetailId.AddRange(lstStockOutResult.Select(x => x.StockOutDetailId).Distinct().ToList());
                            lstQuery = lstQuery.Where(x => lstSearchStockOutId.Contains(x.Id));
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
                var lstStockOutDetailQuery = _stockOutDetailRepository.TableNoTracking.Where(x => !x.IsDeleted && list.Select(y => y.Id).ToList().Contains(x.StockOutId)).ToList();
                var lstStockOutDetailResult = new List<StockOutDetail>();
                if (!string.IsNullOrEmpty(searchFilter.SearchType) && !string.IsNullOrEmpty(searchFilter.SearchKey))
                {
                    switch (searchFilter.SearchType)
                    {
                        case "skuName":
                        case "factoryModel":
                        case "batchNo":
                            lstStockOutDetailResult = lstStockOutDetailQuery.Where(x => lstSearchStockOutDetailId.Contains(x.Id)).ToList();
                            break;
                        default:
                            lstStockOutDetailResult = lstStockOutDetailQuery.ToList();
                            break;
                    }
                }
                else
                    lstStockOutDetailResult = lstStockOutDetailQuery.ToList();

                var lstWarehouseId = list.Select(x => x.WarehouseId).ToList();
                lstWarehouseId.AddRange(list.Select(x => x.ToWarehouseId).ToList());
                var lstWarehouseDict = _warehouseInfoRepository.GetWarehouseDict().Where(x => lstWarehouseId.Contains(x.Id));

                var lstSkuDict = lstAllSkuDict.Where(x => lstStockOutDetailResult.Select(y => y.SkuId).ToList().Contains(x.Id));
                var lstSkuCostBatchDict = lstAllSkuCostBatchDict.Where(x => lstStockOutDetailResult.Select(y => y.SkuCostBatchId).ToList().Contains(x.Id));

                foreach (var item in list)
                {
                    item.DetailDict = lstStockOutDetailResult.Where(x => x.StockOutId == item.Id).ToDictionary(x => x.Id);
                    if (item.DetailDict == null)
                        item.DetailDict = new Dictionary<long, StockOutDetail>();

                    item.Warehouse = lstWarehouseDict.FirstOrDefault(x => x.Id == item.WarehouseId) ?? new WarehouseInfo();
                    if (item.StockOutType == StockOutType.调拨出库)
                        item.ToWarehouse = lstWarehouseDict.FirstOrDefault(x => x.Id == item.ToWarehouseId) ?? new WarehouseInfo();
                    else
                        item.ToWarehouse = new WarehouseInfo();
                    foreach (var tDetail in item.DetailDict.Values)
                    {
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
