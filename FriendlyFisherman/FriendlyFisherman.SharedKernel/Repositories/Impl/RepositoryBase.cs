using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FriendlyFisherman.SharedKernel.Repositories.Impl
{
    public abstract class RepositoryBase<T> where T : class
    {
        protected DbContext context;

        public RepositoryBase(DbContext context)
        {
            this.context = context;
        }

        protected BaseRepository<T> CreateRepo() { return new BaseRepository<T>(context); }
    }
}
