using HuigeTec.Core.Domain.Repositories;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Repositories.System
{
    public interface ISystemInfoRepository: IRepository<SystemInfo>
    {
        void ForceRefreshDict();
        bool IsNameExists(string name);
        bool IsNameExists(string name,long systemId);
        SystemInfo SingleGet(long Id);
        List<SystemInfo> AllGet();
        bool SingleInsert(SystemInfo system);
        bool SingleUpdate(SystemInfo system);
        bool SigleDelete(SystemInfo system);
        List<SystemInfo> PageQuery(SearchFilterDo searchFilter, out int count);
    }
}
