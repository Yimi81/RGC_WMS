using System;
using System.Threading.Tasks;

namespace RGC.WMS.USA.Domain.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 提交,返回影响的行数
        /// </summary>
        int SaveChanges();
    }
}
