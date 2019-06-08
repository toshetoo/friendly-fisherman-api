using System.Collections.Generic;
using FriendlyFisherman.SharedKernel.Repositories.Impl;
using Publishing.Domain.Entities.Categories;
using Publishing.Domain.Repositories.Categories;

namespace Publishing.DataAccess.Repositories.Categories
{
    public class ThreadCategoriesRepository: BaseRepository<ThreadCategory>, IThreadCategoriesRepository
    {

        public ThreadCategoriesRepository(PublishingDbContext context) : base(context)
        {
        }
    }
}
