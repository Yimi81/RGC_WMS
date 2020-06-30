using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.System;
using RGC.WMS.USA.Domain.Entities.System.Do;
using RGC.WMS.USA.Domain.Repositories.System;
using System;

namespace RGC.WMS.USA.Domain.Services.System
{
    public class PlatformService: IPlatformService
    {
        private readonly IPlatformRepository _platformRepository;

        public PlatformService(
            IPlatformRepository platformRepository
            )
        {
            _platformRepository = platformRepository;
        }

        /// <summary>
        /// 后台新增平台
        /// </summary>
        public ResponseDo<string> ManageCreate(PlatformCreateOrUpdateDo createPlatform, long creatorUserId)
        {
            var result = new ResponseDo<string>();

            var platform = new PlatformInfo();
            platform.EName = createPlatform.EName;
            platform.CName = createPlatform.CName;

            platform.CreationTime = DateTime.Now;
            platform.CreatorUserId = creatorUserId;

            var excute = _platformRepository.SingleAdd(platform);
            if (excute == null || excute.Id <= 0)
                throw new CustomException("平台新增失败", 1);

            result.Code = 0;
            result.Success = true;
            return result;
        }


        /// <summary>
        /// 后台修改平台
        /// </summary>
        public ResponseDo<string> ManageModify(PlatformCreateOrUpdateDo modifyPlatform, long modifierUserId)
        {
            var result = new ResponseDo<string>();

            var platform = _platformRepository.SingleGet(modifyPlatform.Id);
            if (platform == null || platform.Id <= 0)
                throw new CustomException("请求数据异常，操作失败", 1);

            if (_platformRepository.IsENameExists(modifyPlatform.EName, platform.Id))
                throw new CustomException("平台名称已存在", 1);

            platform.CName = modifyPlatform.CName;
            platform.EName = modifyPlatform.EName;

            platform.LastModificationTime = DateTime.Now;
            platform.LastModifierUserId = modifierUserId;

            var excute = _platformRepository.SingleUpdate(platform);
            if (excute <= 0)
                throw new CustomException("平台数据更新失败", 1);

            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 后台伪删除平台
        /// </summary>
        public ResponseDo<string> ManageDelete(long platformId, long modifierUserId)
        {
            var result = new ResponseDo<string>();

            var platform = _platformRepository.SingleGet(platformId);
            if (platform == null || platform.Id <= 0)
                throw new CustomException("请求数据异常，操作失败", 1);

            platform.IsDeleted = true;
            platform.DeleterUserId = modifierUserId;
            platform.DeletionTime = DateTime.Now;

            var excute = _platformRepository.SingleDelete(platform);
            if (excute <= 0)
                throw new CustomException("平台删除失败", 1);

            result.Code = 0;
            result.Success = true;
            return result;
        }


        /// <summary>
        /// 后台根据平台id获取平台详情
        /// </summary>
        public ResponseDo<PlatformCreateOrUpdateDo> ManageGetPlatformDetail(long platformId)
        {
            var result = new ResponseDo<PlatformCreateOrUpdateDo>();            
            var platform = _platformRepository.SingleGet(platformId);
            if (platform == null || platform.Id <= 0)
                throw new CustomException("请求数据异常，平台不存在", 1);

            result.Data = new PlatformCreateOrUpdateDo();
            result.Data.Id = platform.Id;
            result.Data.EName = platform.EName;
            result.Data.CName = platform.CName;

            result.Code = 0;
            result.Success = true;
            return result;
        }
    }
}
