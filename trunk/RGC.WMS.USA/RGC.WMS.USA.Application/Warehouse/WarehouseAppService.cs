using AutoMapper;
using RGC.WMS.USA.Application.Dto;
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
    /// MeridianGo 2020/06/11
    /// </summary>
    public class WarehouseAppService : IWarehouseAppService
    {
        private readonly IWarehouseRepository _warehouseRepositories;
        private readonly IBmsUserRepository _bmsUserRepository;
        private readonly IMapper _mapper;
        public WarehouseAppService(
            IWarehouseRepository warehouseRepositories,
            IBmsUserRepository bmsUserRepository,
            IMapper mapper)
        {
            _warehouseRepositories = warehouseRepositories;
            _bmsUserRepository = bmsUserRepository;
            _mapper = mapper;
        }

        private void VaildLogin(long loginId)
        {
            if (loginId <= 0)
                throw new CustomException("未登陆", 3);
        }

        private void VaildWarehouseData(WarehouseEditInput dto)
        {
            dto.Number = dto.Number.ToEmpty();
            if (string.IsNullOrEmpty(dto.Number))
                throw new CustomException("仓库编号不能为空", 4);

            if (dto.Number.Length > 50)
                throw new CustomException("仓库编号的长度不能超过50", 4);

            dto.Name = dto.Name.ToEmpty();
            if (string.IsNullOrEmpty(dto.Name))
                throw new CustomException("名称不能为空", 4);

            if (dto.Name.Length > 256)
                throw new CustomException("名称的长度不能超过256", 4);

            if (!dto.Status.HasValue)
                throw new CustomException("仓库状态不能为空", 4);

            dto.PostCodePrefix = dto.PostCodePrefix.ToEmpty();
            if (string.IsNullOrEmpty(dto.PostCodePrefix))
                throw new CustomException("邮编前缀不能为空", 4);

            if (dto.PostCodePrefix.Length > 25)
                throw new CustomException("邮编前缀的长度不能超过25", 4);

            dto.Longitude = dto.Longitude.ToEmpty();
            if (dto.Longitude.Length > 64)
                throw new CustomException("经度的长度不能超过64", 4);

            dto.Latitude = dto.Latitude.ToEmpty();
            if (dto.Latitude.Length > 64)
                throw new CustomException("纬度的长度不能超过64", 4);

            dto.Address = dto.Address.ToEmpty();
            if (dto.Address.Length > 320)
                throw new CustomException("地址的长度不能超过320", 4);

            dto.Remarks = dto.Remarks.ToEmpty();
            if (dto.Remarks.Length > 512)
                throw new CustomException("备注的长度不能超过512", 4);
        }

        public ResponseDto<string> Create(long loginId, WarehouseEditInput tInputModel)
        {
            #region 验证登陆
            VaildLogin(loginId);
            #endregion

            #region 添加库存时的验证
            VaildWarehouseData(tInputModel);

            #region 验证仓库编号是否唯一性
            if (_warehouseRepositories.IsExistWarehouseNumber(0, tInputModel.Number))
                throw new CustomException("仓库编号已存在，无法添加", 4);
            #endregion

            #endregion

            var tAddModel = _mapper.Map<WarehouseInfo>(tInputModel);
            _warehouseRepositories.Create(loginId, tAddModel);
            return new ResponseDto<string>
            {
                Code = 0,
                Success = true
            };
        }

        public ResponseDto<string> Update(long loginId, WarehouseEditInput tInputModel)
        {
            #region 验证登陆
            VaildLogin(loginId);
            #endregion

            #region 添加库存时的验证
            if (tInputModel.Id <= 0)
                throw new CustomException("参数异常，请联系管理员", 4);
            VaildWarehouseData(tInputModel);

            #region 验证仓库编号是否唯一性
            if (_warehouseRepositories.IsExistWarehouseNumber(tInputModel.Id, tInputModel.Number))
                throw new CustomException("仓库编号已存在，无法添加", 4);
            #endregion

            var tEditModel = _warehouseRepositories.GetById(tInputModel.Id);
            if (tEditModel == null)
                throw new CustomException("数据提取异常，请联系管理员", 4);
            #endregion

            tEditModel.Number = tInputModel.Number;
            tEditModel.Name = tInputModel.Name;
            tEditModel.Status = tInputModel.Status;
            tEditModel.PostCodePrefix = tInputModel.PostCodePrefix;
            tEditModel.Longitude = tInputModel.Longitude;
            tEditModel.Latitude = tInputModel.Latitude;
            tEditModel.Address = tInputModel.Address;
            tEditModel.Remarks = tInputModel.Remarks;

            _warehouseRepositories.Update(loginId, tEditModel);
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

            var tEditModel = _warehouseRepositories.GetById(id);
            if (tEditModel == null)
                throw new CustomException("数据提取异常，请联系管理员", 4);

            if (tEditModel.IsDeleted)
                throw new CustomException("仓库已删除", 1);

            _warehouseRepositories.Delete(loginId, tEditModel);
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

            var tEditModel = _warehouseRepositories.GetById(id);
            if (tEditModel == null)
                throw new CustomException("数据提取异常，请联系管理员", 4);

            if (!tEditModel.IsDeleted)
                throw new CustomException("仓库已恢复", 1);

            _warehouseRepositories.Recovery(loginId, tEditModel);
            return new ResponseDto<string>
            {
                Code = 0,
                Success = true
            };
        }

        public ResponseDto<string> UpdateStatus(long id, WarehouseStatus status, long modifierUserId)
        {
            #region 验证登陆
            VaildLogin(modifierUserId);
            #endregion

            var tEditModel = _warehouseRepositories.GetById(id);
            if (tEditModel == null)
                throw new CustomException("参数异常，请联系管理员", 4);
            tEditModel.Status = status;

            _warehouseRepositories.UpdateStatus(tEditModel, modifierUserId);
            return new ResponseDto<string>
            {
                Code = 0,
                Success = true
            };
        }

        public ResponseDto<WarehouseDto> Get(long id)
        {
            var result = new ResponseDto<WarehouseDto>
            {
                Code = 0,
                Success = true
            };
            result.Data = _mapper.Map<WarehouseDto>(_warehouseRepositories.Get(id));
            if (result.Data != null)
            {
                var lstUserId = new List<long>();
                var iCreatorUserId = result.Data.CreatorUserId;
                lstUserId.Add(iCreatorUserId);
                var iLastModifierUserId = result.Data.LastModifierUserId.ToInt();
                lstUserId.Add(iLastModifierUserId);
                var iDeleterUserId = result.Data.DeleterUserId.ToInt();
                lstUserId.Add(iDeleterUserId);

                var lstUser = _bmsUserRepository.GetAllUserByKeys(lstUserId);

                if (iCreatorUserId > 0)
                    result.Data.createUser = lstUser.FirstOrDefault(x => x.Id == iCreatorUserId)?.LoginName;

                if (iLastModifierUserId > 0)
                    result.Data.lastModifierUser = lstUser.FirstOrDefault(x => x.Id == iLastModifierUserId)?.LoginName;

                if (iDeleterUserId > 0)
                    result.Data.deleterUser = lstUser.FirstOrDefault(x => x.Id == iDeleterUserId)?.LoginName;
            }
            return result;
        }

        public ResponsePageDto<WarehouseDto> GetPage(WarehouseFilterInput searchFilter)
        {
            var lstWarehouse = _warehouseRepositories.GetPage(
                (
                    searchFilter.SearchKey,
                    searchFilter.Status,
                    searchFilter.IsDeleted,
                    searchFilter.PageSize,
                    searchFilter.CurrentPage
                ), out int count);
            var result = new ResponsePageDto<WarehouseDto>
            {
                Code = 0,
                Success = true
            };

            if (lstWarehouse == null)
                lstWarehouse = new List<WarehouseInfo>();

            //将model中角色实体，转换为前台需要的实体
            result.Data = _mapper.Map<List<WarehouseDto>>(lstWarehouse);
            if (result.Data == null)
                result.Data = new List<WarehouseDto>();

            if (result.Data.Count >= 0)
            {
                var lstUserId = new List<long>();
                lstUserId.AddRange(result.Data.Select(x => x.CreatorUserId).ToList());
                lstUserId.AddRange(result.Data.Select(x => x.LastModifierUserId.ToLong()).ToList());
                lstUserId.AddRange(result.Data.Select(x => x.DeleterUserId.ToLong()).ToList());

                var lstUser = _bmsUserRepository.GetAllUserByKeys(lstUserId);

                foreach (var item in result.Data)
                {
                    var iCreatorUserId = item.CreatorUserId;
                    if (iCreatorUserId > 0)
                        item.createUser = lstUser.FirstOrDefault(x => x.Id == iCreatorUserId)?.LoginName;

                    var iLastModifierUserId = item.LastModifierUserId.ToInt();
                    if (iLastModifierUserId > 0)
                        item.lastModifierUser = lstUser.FirstOrDefault(x => x.Id == iLastModifierUserId)?.LoginName;

                    var iDeleterUserId = item.DeleterUserId.ToInt();
                    if (iDeleterUserId > 0)
                        item.deleterUser = lstUser.FirstOrDefault(x => x.Id == iDeleterUserId)?.LoginName;
                }

                result.Page.TotalCount = (int)count;
                result.Page.TotalPages = (int)Math.Ceiling((Decimal)count / searchFilter.PageSize);
                result.Page.PageSize = searchFilter.PageSize;
                result.Page.CurrentPage = searchFilter.CurrentPage;
                result.Page.CurrentCount = result.Data.Count;
            }
            return result;
        }

        public ResponseDto<List<WarehouseFilterSimpleOutput>> GetWarehouseSimpleList()
        {
            var lstResult = _warehouseRepositories.GetWarehouseSimpleList();
            if (lstResult == null)
                lstResult = new List<(long Id, string Number, string Name, string PostCodePrefix)>();

            var result = new ResponseDto<List<WarehouseFilterSimpleOutput>>
            {
                Code = 0,
                Success = true
            };
            result.Data = new List<WarehouseFilterSimpleOutput>();
            result.Data.AddRange(lstResult.Select(x => new WarehouseFilterSimpleOutput
            {
                Id = x.Id,
                Number = x.Number,
                Name = x.Name,
                PostCodePrefix = x.PostCodePrefix
            }));
            return result;
        }

        public ResponseDto<string> ForceRefreshWarehouseDict()
        {
            _warehouseRepositories.ForceRefreshWarehouseDict();
            return new ResponseDto<string>
            {
                Code = 0,
                Success = true
            };
        }
    }
}
