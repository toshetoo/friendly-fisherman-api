using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FriendlyFisherman.SharedKernel.Repositories.Abstraction;

namespace FriendlyFisherman.SharedKernel.Repositories.Impl
{
    public class BaseRepository<T>: IBaseRepository<T> where T : class
    {
        DbSet<T> _entities;
        private DbContext _context;
        bool _disposed = false;
        bool _isContext = true;

        public BaseRepository(DbContext context)
        {

            _context = context ?? throw new ArgumentNullException(nameof(context)); ;
            _entities = context.Set<T>();
        }

        private DbSet<T> GetContext()
        {
            _entities = _entities ?? _context.Set<T>();

            return _entities;
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
                if (_isContext && _context != null)
                {
                    //_context.Dispose();
                    //_entities = null;
                    //_context = null;
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
                //if (_isContext && _context != null)
                //{
                //    //_context.Dispose();
                //    _context = null;
                //    _entities = null;
                //}

            }
        }

        public void Create(T entity)
        {
            ExecuteAction(e =>
            {
                if (ReferenceEquals(entity, null))
                    throw new ArgumentNullException(nameof(entity));

                e.Add(entity);

                if (_isContext)
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

                if (_isContext)
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
                if (entity == null)
                    return;

                e.Remove(entity);

                if (_isContext)
                    _context.SaveChanges();
            });
        }

        public void Delete(T @object)
        {
            ExecuteAction(e =>
            {
                if (ReferenceEquals(@object, null))
                    throw new ArgumentNullException(nameof(@object));
                
                _context.Set<T>().Attach(@object);
                _context.Set<T>().Remove(@object);

                if (_isContext)
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

                if (_isContext)
                    _context.SaveChanges();
            });
        }

        public void ExecuteInOneContext(Action<DbContext> e)
        {
            var temp = _isContext;
            try
            {
                //isContext = false;
                GetContext();
                e(_context);
            }
            finally
            {
                _isContext = temp;
                if (_isContext && _context != null)
                {
                    //_context.Dispose();
                    //_entities = null;
                    //_context = null;
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
