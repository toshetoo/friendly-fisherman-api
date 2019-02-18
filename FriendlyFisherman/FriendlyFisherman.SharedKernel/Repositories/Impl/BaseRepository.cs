using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FriendlyFisherman.SharedKernel.Repositories.Impl
{
    public class BaseRepository<T> where T : class
    {
        DbSet<T> entities;
        private DbContext _context;
        bool disposed = false;
        bool isContext = false;

        public BaseRepository(DbContext context)
        {

            _context = context ?? throw new ArgumentNullException(nameof(context)); ;
            entities = context.Set<T>();
        }

        private DbSet<T> GetContext()
        {
            entities = entities ?? _context.Set<T>();

            return entities;
        }
        private K ExecuteAction<K>(Func<DbSet<T>, K> action)
        {
            try
            {
                var e = GetContext();
                return action(e);
            }
            finally
            {
                if (isContext && _context != null)
                {
                    _context.Dispose();
                    entities = null;
                    _context = null;
                }

            }
        }
        private void ExecuteAction(Action<DbSet<T>> action)
        {
            try
            {
                var e = GetContext();
                action(e);
            }
            finally
            {
                if (isContext && _context != null)
                {
                    _context.Dispose();
                    _context = null;
                    entities = null;
                }

            }
        }

        public void Create(T entity)
        {
            ExecuteAction(e =>
            {
                if (ReferenceEquals(entity, null))
                    throw new ArgumentNullException(nameof(entity));

                e.Add(entity);

                if (isContext)
                    _context.SaveChanges();
            });
        }

        public T Get(Expression<Func<T, bool>> whereClause, params Expression<Func<T, object>>[] includes)
        {
            return ExecuteAction<T>(e =>
            {
                if (ReferenceEquals(whereClause, null))
                    throw new ArgumentNullException(nameof(whereClause));

                IQueryable<T> query = e;
                foreach (var include in includes)
                    query = query.Include(include);

                var entity = query.FirstOrDefault(whereClause);

                return entity;
            });
        }

        public IEnumerable<T> GetAll()
        {
            return ExecuteAction<IEnumerable<T>>(e => e.ToList());
        }

        public IEnumerable<T> GetAll<TProperty>(params Expression<Func<T, object>>[] includes)
        {
            return GetAll<TProperty>(false, null, includes);
        }
        public IEnumerable<T> GetAll<TProperty>(bool isDescending, Expression<Func<T, TProperty>> orderBy, params Expression<Func<T, object>>[] includes)
        {
            if (orderBy == null)
                return ExecuteAction<IEnumerable<T>>(e => e.ToList());

            return ExecuteAction<IEnumerable<T>>(e =>
            {
                IQueryable<T> query = e;
                foreach (var include in includes)
                    query = query.Include(include);

                return (isDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy)).ToList();
            });
        }

        public IEnumerable<T> GetWhere(Expression<Func<T, bool>> whereClause, params Expression<Func<T, object>>[] includes)
        {
            return GetWhere<T>(whereClause, null, isDescending: false, includes: includes);
        }
        public IEnumerable<T> GetWhere<TProperty>(Expression<Func<T, bool>> whereClause,
            Expression<Func<T, TProperty>> orderBy,
            bool isDescending = false,
            params Expression<Func<T, object>>[] includes)
        {
            return ExecuteAction<IEnumerable<T>>(e =>
            {
                IQueryable<T> query = e;

                foreach (var include in includes)
                    query = query.Include(include);

                if (ReferenceEquals(orderBy, null))
                    return query.Where(whereClause).ToList();

                query = query.OrderBy(orderBy);
                var result = query.Where(whereClause);
                return (isDescending ? result.OrderByDescending(orderBy) : result.OrderBy(orderBy)).ToList();
            });
        }

        public void Update(T newEntity)
        {
            ExecuteInOneContext(e =>
            {
                if (ReferenceEquals(newEntity, null))
                    throw new ArgumentNullException(nameof(newEntity));

                _context.Set<T>().Attach(newEntity);
                _context.Entry(newEntity).State = EntityState.Modified;

                if (isContext)
                    _context.SaveChanges();
            });
        }

        public void Delete(Func<T, bool> whereClause)
        {
            ExecuteAction(e =>
            {
                if (ReferenceEquals(whereClause, null))
                    throw new ArgumentNullException(nameof(whereClause));

                var entity = e.FirstOrDefault(whereClause);
                e.Remove(entity);

                if (isContext)
                    _context.SaveChanges();
            });
        }

        public void Delete(T @object)
        {
            ExecuteAction(e =>
            {
                if (ReferenceEquals(@object, null))
                    throw new ArgumentNullException(nameof(@object));

                //context.Entry(@object).State = EntityState.Deleted;
                _context.Set<T>().Attach(@object);
                _context.Set<T>().Remove(@object);

                if (isContext)
                    _context.SaveChanges();
            });
        }

        public void DeleteRange(IEnumerable<T> objects)
        {
            ExecuteAction(e =>
            {
                if (ReferenceEquals(objects, null))
                    throw new ArgumentNullException(nameof(objects));

                _context.Set<T>().RemoveRange(objects);

                if (isContext)
                    _context.SaveChanges();
            });
        }

        public void ExecuteInOneContext(Action<DbContext> e)
        {
            var temp = isContext;
            try
            {
                isContext = false;
                GetContext();
                e(_context);
            }
            finally
            {
                isContext = temp;
                if (isContext && _context != null)
                {
                    _context.Dispose();
                    entities = null;
                    _context = null;
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                _context.Dispose();
            }

            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
