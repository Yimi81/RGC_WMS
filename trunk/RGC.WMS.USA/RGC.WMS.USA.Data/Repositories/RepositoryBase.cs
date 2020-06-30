using HuigeTec.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace RGC.WMS.USA.Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private DbSet<TEntity> _entities;

        public RepositoryBase(DbContext context)
        {
            _context = context;
        }
        public RepositoryBase()
        {

        }
        public TEntity GetById(object id)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            return Entities.Find(id);
        }

        public ValueTask<TEntity> GetByIdAsync(object id)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            return Entities.FindAsync(id);
        }


        public void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Entities.Add(entity);
            //_context.Set<TEntity>().Add(entity);

        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            if (!entities.Any())
                throw new ArgumentNullException("entities");

            Entities.AddRange(entities);
        }

        public ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            return Entities.AddAsync(entity);
        }

        public Task InsertAsync(IEnumerable<TEntity> entities)
        {
            if (!entities.Any())
                throw new ArgumentNullException("entities");

            return Entities.AddRangeAsync(entities);
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Entities.Attach(entity);
            _context.Update(entity);
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            if (!entities.Any())
                throw new ArgumentNullException("entities");

            _context.UpdateRange(entities);

        }

        public void Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                if (string.IsNullOrEmpty(propertyName))
                {
                    propertyName = GetPropertyName(property.Body.ToString());
                }
                _context.Entry(entity).Property(propertyName).IsModified = true;

            }
        }

        string GetPropertyName(string str)
        {
            return str.Split(',')[0].Split('.')[1];
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            _context.Remove(entity);
        }


        public void Delete(IEnumerable<TEntity> entities)
        {
            if (!entities.Any())
                throw new ArgumentNullException("entities");

            _context.RemoveRange(entities);

        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            _context.RemoveRange(Entities.Where(predicate));
        }


        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<TEntity> Table => Entities;

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        /// <summary>
        /// Gets an entity set
        /// </summary>
        protected virtual DbSet<TEntity> Entities => _entities ?? (_entities = _context.Set<TEntity>());

        #endregion
        private void AttachIfNot(TEntity entity)
        {
            var entry = _context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }
            _context.Attach(entity);
        }

        #region 事务
        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }
        #endregion

        #region 深拷贝
        public static T DeepCopyByReflect<T>(T obj)
        {
            if (obj == null) return obj;
            //如果是字符串或值类型则直接返回
            if (obj is string || obj.GetType().IsValueType) return obj;
            object retval = Activator.CreateInstance(obj.GetType());
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            var baseObj = obj.GetType().BaseType;
            while (baseObj != null)
            {
                FieldInfo[] temp = baseObj.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                fields = fields.Union(temp).ToArray();
                baseObj = baseObj.BaseType;
            }
            //FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            foreach (FieldInfo field in fields)
            {
                try { field.SetValue(retval, DeepCopyByReflect(field.GetValue(obj))); }
                catch { }
            }
            return (T)retval;
        }
        #endregion
    }
}
