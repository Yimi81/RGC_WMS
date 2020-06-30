using HuigeTec.Core.Domain.Repositories;
using RGC.WMS.USA.Domain.Entities.Bms;
using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Repositories.Bms
{
    public interface IBmsRoleRepository : IRepository<BmsRole>
    {
        void ForceRefreshDict();
        bool IsNameExists(string name);
        bool IsNameExists(string name, long roleId);
        BmsRole SingleGet(Int64 id);
        BmsRole SingleAdd(BmsRole request);
        int SingleUpdate(BmsRole obj);
        int SingleDelete(BmsRole role);
        List<BmsRole> AllListGet(string key);
    }
}
