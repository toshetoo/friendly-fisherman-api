using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FriendlyFisherman.SharedKernel
{
    public interface IBaseRepository<T> where T : class
    {
        void Delete(T item);
        T GetById(int id);
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        List<T> GetAll(Expression<Func<T, bool>> filter = null, int? page = null, int? pageSize = null);
        List<T> GetAll(Expression<Func<T, bool>> filter = null, Expression<Func<T, IComparable>> orderby = null, int? page = null, int? pageSize = null);
        int Count(Expression<Func<T, bool>> filter = null);
        void AddRange(List<T> items);
        void Save(T item);
        void SaveAndCommit(T item);
        void DeleteAndCommit(T item);
    }
}
