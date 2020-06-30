using HuigeTec.Core.Domain.Repositories;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.System;
using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Repositories.System
{
    public interface IPlatformRepository: IRepository<PlatformInfo>
    {
        void ForceRefreshDict();
        bool IsENameExists(string name);
        bool IsENameExists(string name, long roleId);
        PlatformInfo SingleGet(Int64 id);
        PlatformInfo SingleAdd(PlatformInfo request);
        int SingleUpdate(PlatformInfo obj);
        int SingleDelete(PlatformInfo platform);
        List<PlatformInfo> AllListGet();
        List<PlatformInfo> PageQuery(SearchFilterDo searchFilter, out int count);
    }
}
