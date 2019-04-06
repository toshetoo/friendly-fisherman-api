using System;
using System.Collections.Generic;
using System.Text;

namespace FriendlyFisherman.SharedKernel.Repositories.Abstraction
{
    public interface IBaseCrudRepository<T>
    {
        void Delete(string id);
        T GetById(string id);
        IEnumerable<T> GetAll();
        void Save(T item);
    }
}
