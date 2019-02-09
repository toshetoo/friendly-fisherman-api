using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Users.DataAccess.Repositories
{
    public class BaseRepository<T> where T : class
    {
        DbSet<T> entities;
        private DbContext _context;
        bool disposed = false;
        bool isInternalContext = false;

        public BaseRepository(UsersDbContext context)
        {
            _context = context;
            isInternalContext = true;
        }

        public BaseRepository(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            this._context = context;
            entities = context.Set<T>();
        }

        private DbSet<T> GetContext()
        {
            this.entities = this.entities ?? this._context.Set<T>();

            return this.entities;
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
                if (isInternalContext && _context != null)
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
                if (isInternalContext && _context != null)
                {
                    _context.Dispose();
                    _context = null;
                    entities = null;
                }

            }
        }

        internal void Create(T entity)
        {
            ExecuteAction(e =>
            {
                if (ReferenceEquals(entity, null))
                    throw new ArgumentNullException(nameof(entity));

                e.Add(entity);

                if (isInternalContext)
                    _context.SaveChanges();
            });
        }

        internal T Get(Expression<Func<T, bool>> whereClause, params Expression<Func<T, object>>[] includes)
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

        internal IEnumerable<T> GetAll()
        {
            return ExecuteAction<IEnumerable<T>>(e => e.ToList());
        }

        internal IEnumerable<T> GetAll<TProperty>(params Expression<Func<T, object>>[] includes)
        {
            return GetAll<TProperty>(false, null, includes);
        }
        internal IEnumerable<T> GetAll<TProperty>(bool isDescending, Expression<Func<T, TProperty>> orderBy, params Expression<Func<T, object>>[] includes)
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

        internal IEnumerable<T> GetWhere(Expression<Func<T, bool>> whereClause, params Expression<Func<T, object>>[] includes)
        {
            return GetWhere<T>(whereClause, null, isDescending: false, includes: includes);
        }
        internal IEnumerable<T> GetWhere<TProperty>(Expression<Func<T, bool>> whereClause,
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

        internal void Update(T newEntity)
        {
            ExecuteInOneContext(e =>
            {
                if (ReferenceEquals(newEntity, null))
                    throw new ArgumentNullException(nameof(newEntity));

                _context.Set<T>().Attach(newEntity);
                _context.Entry(newEntity).State = EntityState.Modified;

                if (isInternalContext)
                    _context.SaveChanges();
            });
        }

        internal void Delete(Func<T, bool> whereClause)
        {
            ExecuteAction(e =>
            {
                if (ReferenceEquals(whereClause, null))
                    throw new ArgumentNullException(nameof(whereClause));

                var entity = e.FirstOrDefault(whereClause);
                e.Remove(entity);

                if (isInternalContext)
                    _context.SaveChanges();
            });
        }

        internal void Delete(T @object)
        {
            ExecuteAction(e =>
            {
                if (ReferenceEquals(@object, null))
                    throw new ArgumentNullException(nameof(@object));

                //context.Entry(@object).State = EntityState.Deleted;
                _context.Set<T>().Attach(@object);
                _context.Set<T>().Remove(@object);

                if (isInternalContext)
                    _context.SaveChanges();
            });
        }

        internal void DeleteRange(IEnumerable<T> objects)
        {
            ExecuteAction(e =>
            {
                if (ReferenceEquals(objects, null))
                    throw new ArgumentNullException(nameof(objects));

                _context.Set<T>().RemoveRange(objects);

                if (isInternalContext)
                    _context.SaveChanges();
            });
        }

        internal void ExecuteInOneContext(Action<DbContext> e)
        {
            var temp = isInternalContext;
            try
            {
                isInternalContext = false;
                GetContext();
                e(_context);
            }
            finally
            {
                isInternalContext = temp;
                if (isInternalContext && _context != null)
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
