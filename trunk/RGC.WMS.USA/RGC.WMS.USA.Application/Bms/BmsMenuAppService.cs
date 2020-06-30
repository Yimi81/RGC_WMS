using AutoMapper;
using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Application.Bms.Dto;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Domain;
using RGC.WMS.USA.Domain.Repositories.Bms;
using RGC.WMS.USA.Domain.Services.Bms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Application.Bms
{
    public class BmsMenuAppService : IBmsMenuAppService
    {
        private readonly IBmsMenuService _bmsMenuService;
        private readonly IBmsMenuRepository _bmsMenuRepository;
        private readonly IBmsUserService _bmsUserService;
        private readonly IBmsOrganizationService _bmsOrganizationService;
        private IMapper _mapper { get; }
        public BmsMenuAppService(
            IBmsMenuService bmsMenuService,
            IBmsMenuRepository bmsMenuRepository,
            IBmsUserService bmsUserService,
            IBmsOrganizationService bmsOrganizationService,
            IMapper mapper)
        {
            _bmsMenuService = bmsMenuService;
            _bmsMenuRepository = bmsMenuRepository;
            _bmsUserService = bmsUserService;
            _bmsOrganizationService = bmsOrganizationService;
            _mapper = mapper;
        }
        /// <summary>
        /// 后台创建菜单
        /// </summary>
        public ResponseDto<BmsMenuDto> CreateMenu(BmsMenuDto request, long creatorUserId)
        {
            var result = new ResponseDto<BmsMenuDto>();
            result.Data = new BmsMenuDto();
            if (string.IsNullOrEmpty(request.Name))
                throw new CustomException("菜单名称不能为空", 1);

            if (string.IsNullOrEmpty(request.AuthorizeCode))
                throw new CustomException("权限名不能为空", 1);

            request.Name = request.Name.ToEmpty();
            request.IcoName = request.IcoName.ToEmpty();
            var menu = new BmsMenu()
            {
                Type = request.Type,
                AuthorizeCode = request.AuthorizeCode.ToEmpty(),
                Name = request.Name.ToEmpty(),
                SeqNo = request.SeqNo,
                IcoName = request.IcoName.ToEmpty(),
                Path = request.Path.ToEmpty(),
                Remark = request.Remark.ToEmpty(),
                ParentId = request.ParentId,
                CreationTime = DateTime.Now,
                CreatorUserId = creatorUserId
            };

            if (request.Type == BmsMenuType.System)
                menu.ParentId = 0;

            var execute = _bmsMenuService.CreateMenu(menu);
            if (menu.Id <= 0)
                throw new CustomException("菜单添加失败", 1);

            if (execute.Success && menu.Id > 0)
            {
                result.Code = execute.Code;
                result.Msg = execute.Msg;
                result.Success = execute.Success;
                result.Data = new BmsMenuDto()
                {
                    Type = menu.Type,
                    AuthorizeCode = menu.AuthorizeCode,
                    Name = menu.Name,
                    SeqNo = menu.SeqNo,
                    IcoName = menu.IcoName,
                    Path = menu.Path,
                    Remark = menu.Remark,
                    ParentId = menu.ParentId,
                };
            }
            else
            {
                result.Code = execute.Code;
                result.Msg = execute.Msg;
                result.Success = execute.Success;
            }
            return result;
        }

        public ResponseDto<string> DeleteMenu(Int64 menuId, long deleterUserId)
        {
            var result = new ResponseDto<string>();
            if (menuId <= 0)
                throw new CustomException("参数异常", 4);

            var execute = _bmsMenuService.DeleteMenu(menuId, deleterUserId);
            result.Code = execute.Code;
            result.Msg = execute.Msg;
            result.Success = execute.Success;
            return result;
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        public ResponseDto<BmsMenuDto> ModifyMenu(BmsMenuDto request, long modifierUserId)
        {
            var result = new ResponseDto<BmsMenuDto>();
            result.Data = new BmsMenuDto();

            if (string.IsNullOrEmpty(request.Name))
                throw new CustomException("菜单名称不能为空", 4);

            if (string.IsNullOrEmpty(request.AuthorizeCode))
                throw new CustomException("权限名不能为空", 4);

            if (request.Id <= 0)
                throw new CustomException("参数异常", 4);

            var menu = _bmsMenuService.GetMenuDetail(request.Id).Data;
            menu.Name = request.Name.ToEmpty();
            menu.AuthorizeCode = request.AuthorizeCode.ToEmpty();
            menu.IcoName = request.IcoName.ToEmpty();
            menu.Path = request.Path.ToEmpty();
            menu.Type = request.Type;
            menu.Remark = request.Remark.ToEmpty();
            menu.SeqNo = request.SeqNo;

            menu.LastModificationTime = DateTime.Now;
            menu.LastModifierUserId = modifierUserId;

            var execute = _bmsMenuService.ModifyMenu(menu);
            if (!execute.Success || menu.Id <= 0)
                throw new CustomException("菜单修改失败", 1);

            if (execute.Success && menu.Id > 0)
            {
                result.Code = execute.Code;
                result.Msg = execute.Msg;
                result.Success = execute.Success;
                result.Data = new BmsMenuDto()
                {
                    Id = menu.Id,
                    Type = menu.Type,
                    AuthorizeCode = menu.AuthorizeCode,
                    Name = menu.Name,
                    SeqNo = menu.SeqNo,
                    IcoName = menu.IcoName,
                    Path = menu.Path,
                    Remark = menu.Remark,
                    ParentId = menu.ParentId,
                };
            }
            return result;
        }

        /// <summary>
        /// 修改排序
        /// </summary>
        public ResponseDto<string> ModifyMenuSeqNo(List<BmsMenuSeqNoDto> request, long modifierUserId)
        {
            var result = new ResponseDto<string>();
            var list = new List<Tuple<long, int>>();
            foreach (var item in request)
            {
                list.Add(Tuple.Create(item.Id, item.SeqNo));
            }

            var execute = _bmsMenuService.ModifyMenuSeqNo(list, modifierUserId);
            result.Code = execute.Code;
            result.Msg = execute.Msg;
            result.Success = execute.Success;
            return result;
        }

        /// <summary>
        /// 获取系统授权目录和菜单
        /// </summary>
        public ResponseDto<List<BmsMenuTreeDto>> GetPermissionSystemMenuTree(long parentId, long userId, long orgId)
        {
            var result = new ResponseDto<List<BmsMenuTreeDto>>();
            result.Data = new List<BmsMenuTreeDto>();
            var idResp = _bmsUserService.MenuListAllGet(userId);
            if (idResp.Code == 0)
            {
                var idList = idResp.Data.Where(p => p.IsGranted).Select(p => p.MenuId).ToList();
                var orgResp = _bmsOrganizationService.ManageGetOrganizationDetail(orgId);
                if (orgResp.Code == 0)
                {
                    var list = orgResp.Data.OrganizationMenuDict.Values.Select(p => p.MenuId).ToList();
                    idList = idList.Union(list).ToList<long>();
                }
                var menuResp = _bmsMenuService.GetStstemTree(parentId, idList);
                if (menuResp.Code == 0)
                {
                    var menuList = menuResp.Data;
                    foreach (var menu in menuList)
                    {
                        var children = new List<BmsMenuTreeDto>();
                        foreach (var child in menu.Children)
                        {
                            children.Add(new BmsMenuTreeDto()
                            {
                                Id = child.Id,
                                Type = child.Type,
                                Name = child.Name,
                                ParentId = child.ParentId,
                                Path = child.Path,
                                AuthorizeCode = child.AuthorizeCode,
                                IcoName = child.IcoName
                            });
                        }
                        var model = new BmsMenuTreeDto()
                        {
                            Id = menu.Id,
                            Type = menu.Type,
                            Name = menu.Name,
                            ParentId = menu.ParentId,
                            Children = children,
                            Path = menu.Path,
                            AuthorizeCode = menu.AuthorizeCode,
                            IcoName = menu.IcoName
                        };
                        result.Data.Add(model);
                    }
                }

            }

            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        public ResponseDto<List<BmsMenuTreeDto>> GetMenuTree(long parentId)
        {
            var result = new ResponseDto<List<BmsMenuTreeDto>>();

            var execute = _bmsMenuService.GetMenuTree(parentId);
            result.Code = execute.Code;
            if (execute.Success)
                result.Data = _mapper.Map<List<BmsMenuTreeDto>>(execute.Data);
            else
                result.Data = new List<BmsMenuTreeDto>();
            result.Msg = execute.Msg;
            result.Success = execute.Success;
            return result;
        }

        /// <summary>
        /// 获取菜单详细信息
        /// </summary>
        public ResponseDto<BmsMenuTreeDto> GetMenuDetail(long id)
        {
            var result = new ResponseDto<BmsMenuTreeDto>();
            var execute = _bmsMenuService.GetMenuDetail(id);
            result.Data = new BmsMenuTreeDto();
            if (execute.Code == 0)
            {
                result.Data.AuthorizeCode = execute.Data.AuthorizeCode;
                result.Data.IcoName = execute.Data.IcoName;
                result.Data.Id = execute.Data.Id;
                result.Data.IsDeleted = execute.Data.IsDeleted;
                result.Data.Name = execute.Data.Name;
                result.Data.ParentId = execute.Data.ParentId;
                result.Data.Path = execute.Data.Path;
                result.Data.Remark = execute.Data.Remark;
                result.Data.SeqNo = execute.Data.SeqNo;
                result.Data.Type = execute.Data.Type;
                if (result.Data != null && result.Data.Id > 0)
                {
                    result.Data.Children = _mapper.Map<List<BmsMenuTreeDto>>(_bmsMenuService.GetChildren(id).Data);
                    result.Code = 0;
                    result.Success = true;
                }
            }
            return result;
        }

        public ResponseDto<List<string>> GetPowerNameList(List<Int64> menuIds)
        {
            var result = new ResponseDto<List<string>>();
            result.Data = _bmsMenuService.GetPowerNameList(menuIds);
            result.Code = 0;
            result.Success = true;
            return result;

        }

        /// <summary>
        /// 强制刷新用户字典
        /// </summary>
        public ResponseDto<string> ForceRefreshDict()
        {
            var result = new ResponseDto<string>();
            _bmsMenuRepository.ForceRefreshDict();
            result.Code = 0;
            result.Success = true;
            return result;
        }
    }
}
