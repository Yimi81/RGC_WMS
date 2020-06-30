using Microsoft.EntityFrameworkCore;
using RGC.WMS.USA.Domain.Uow;
using System;

namespace RGC.WMS.USA.Data.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the 
        /// </summary>
        /// <param name="context">The context.</param>
        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }


        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
