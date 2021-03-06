﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FriendlyFisherman.SharedKernel.Repositories.Abstraction
{
    public interface IBaseRepository<T> where T : class
    {
        void Delete(T item);
        void Delete(Func<T, bool> whereClause);
        T Get(Expression<Func<T, bool>> whereClause, params Expression<Func<T, object>>[] includes);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll<TProperty>(params Expression<Func<T, object>>[] includes);
        IEnumerable<T> GetWhere(Expression<Func<T, bool>> whereClause, params Expression<Func<T, object>>[] includes);

        IEnumerable<T> GetAll<TProperty>(bool isDescending, Expression<Func<T, TProperty>> orderBy,
            params Expression<Func<T, object>>[] includes);
        void Create(T item);
        void Update(T item);
    }
}
