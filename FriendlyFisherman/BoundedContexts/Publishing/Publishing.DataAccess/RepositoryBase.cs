using Microsoft.EntityFrameworkCore;
using Publishing.DataAccess.Repositories;

namespace Publishing.DataAccess
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
