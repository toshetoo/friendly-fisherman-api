using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Users.DataAccess.Repositories;

namespace Users.DataAccess
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
