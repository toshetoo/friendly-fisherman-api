using FriendlyFisherman.SharedKernel.Services.Impl;
using Publishing.DataAccess.Repositories.Categories;
using Publishing.Domain.Entities.Categories;
using Publishing.Domain.EntityViewModels.Categories;
using Publishing.Services.Abstraction.Categories;

namespace Publishing.Services.Implementation.Categories
{
    public class ThreadCategoriesService: BaseCrudService<ThreadCategoryViewModel, ThreadCategory, ThreadCategoriesRepository>, IThreadCategoriesService
    {
        public ThreadCategoriesService(ThreadCategoriesRepository repo) : base(repo)
        {
        }
    }
}
