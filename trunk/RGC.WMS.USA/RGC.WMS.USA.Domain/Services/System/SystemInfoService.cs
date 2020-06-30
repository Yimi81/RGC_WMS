using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.System;
using RGC.WMS.USA.Domain.Entities.System.Do;
using RGC.WMS.USA.Domain.Repositories.System;
using System;

namespace RGC.WMS.USA.Domain.Services.System
{
    public class SystemInfoService : ISystemInfoService
    {
        private readonly ISystemInfoRepository _systemInfoRepository;

        public SystemInfoService(
            ISystemInfoRepository systemInfoRepository
            )
        {
            _systemInfoRepository = systemInfoRepository;
        }

        /// <summary>
        /// 新增系统
        /// </summary>
        public ResponseDo<string> CreateSystem(SystemCreateOrUpdateDo createSystem, long creatorUserId)
        {
            var result = new ResponseDo<string>();
            var system = new SystemInfo();
            system.Name = createSystem.Name;
            system.DisplayName = createSystem.DisplayName;
            system.DomainName = createSystem.DomainName;
            system.IPAddress = createSystem.IPAddress;
            system.isStatic = createSystem.isStatic;
            system.Desc = createSystem.Desc;

            system.CreationTime = DateTime.Now;
            system.CreatorUserId = creatorUserId;

            var excute = _systemInfoRepository.SingleInsert(system);
            if (!excute)
                throw new CustomException("系统资料新增失败", 1);

            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 获取系统编辑对象
        /// </summary>
        public ResponseDo<SystemCreateOrUpdateDo> GetSystemDetail(long systemId)
        {
            var result = new ResponseDo<SystemCreateOrUpdateDo>();
            var system = _systemInfoRepository.SingleGet(systemId);
            if (system == null || system.Id <= 0)
                throw new CustomException("系统不存在", 1);

            result.Data = new SystemCreateOrUpdateDo();
            result.Data.Id = system.Id;
            result.Data.Name = system.Name;
            result.Data.DisplayName = system.DisplayName;
            result.Data.DomainName = system.DomainName;
            result.Data.IPAddress = system.IPAddress;
            result.Data.isStatic = system.isStatic;
            result.Data.Desc = system.Desc;

            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 修改系统资料
        /// </summary>
        public ResponseDo<string> ModifySystem(SystemCreateOrUpdateDo modifySystem, long modifierUserId)
        {
            var result = new ResponseDo<string>();
            var system = _systemInfoRepository.SingleGet(modifySystem.Id);
            if (system == null || system.Id <= 0)
                    throw new CustomException("请求数据异常", 1);

            if (_systemInfoRepository.IsNameExists(modifySystem.Name, system.Id))
                throw new CustomException("系统名称已存在", 1);

            system.Name = modifySystem.Name;
            system.DisplayName = modifySystem.DisplayName;
            system.DomainName = modifySystem.DomainName;
            system.IPAddress = modifySystem.IPAddress;
            system.isStatic = modifySystem.isStatic;
            system.Desc = modifySystem.Desc;
            system.LastModificationTime = DateTime.Now;
            system.LastModifierUserId = modifierUserId;

            var excute = _systemInfoRepository.SingleUpdate(system);
            if (!excute)
                throw new CustomException("系统资料修改失败", 1);

            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 系统伪删除
        /// </summary>
        public ResponseDo<string> DeleteSystem(long systemId, long deleterUserId)
        {
            var result = new ResponseDo<string>();
            var system = _systemInfoRepository.SingleGet(systemId);
            if (system == null || system.Id <= 0)
                throw new CustomException("数据错误", 1);

            if (system.isStatic)
                throw new CustomException("该系统无法被删除", 1);

            system.IsDeleted = true;
            system.DeleterUserId = deleterUserId;
            system.DeletionTime = DateTime.Now;
            var excute = _systemInfoRepository.SigleDelete(system);
            if (!excute)
                throw new CustomException("系统删除失败", 1);

            result.Code = 0;
            result.Success = true;
            return result;
        }
    }
}
