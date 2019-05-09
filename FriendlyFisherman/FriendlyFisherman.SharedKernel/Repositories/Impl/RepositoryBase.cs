using Microsoft.EntityFrameworkCore;

namespace FriendlyFisherman.SharedKernel.Repositories.Impl
{
    public abstract class RepositoryBase<T> where T : class
    {
        protected DbContext Context;

        public RepositoryBase(DbContext context)
        {
            this.Context = context;
        }

        protected BaseRepository<T> CreateRepo() { return new BaseRepository<T>(Context); }
    }
}
