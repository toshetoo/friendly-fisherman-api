using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FriendlyFisherman.SharedKernel.Repositories.Abstraction;
using FriendlyFisherman.SharedKernel.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FriendlyFisherman.SharedKernel.Repositories.Impl
{
    public class BaseCrudRepository<T>: IBaseCrudRepository<T> where T: BaseEntity
    {
        readonly DbSet<T> _entities;
        private readonly DbContext _context;

        public BaseCrudRepository(DbSet<T> entities, DbContext context)
        {
            _entities = entities;
            _context = context;
        }

        public void Delete(string id)
        {
            _entities.Remove(_entities.First(e => e.Id == id));
            _context.SaveChanges();
        }

        public T GetById(string id)
        {
            return _entities.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.ToList();
        }

        public void Save(T item)
        {
            if (string.IsNullOrEmpty(item.Id))
            {
                _entities.Add(item);
            }
            else
            {
                _entities.Update(item);
            }

            _context.SaveChanges();
        }
    }
}
