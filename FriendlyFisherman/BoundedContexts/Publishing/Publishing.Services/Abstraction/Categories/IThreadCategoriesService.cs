using FriendlyFisherman.SharedKernel.Services.Abstraction;
using Publishing.Domain.Entities.Categories;
using Publishing.Domain.EntityViewModels.Categories;

namespace Publishing.Services.Abstraction.Categories
{
    public interface IThreadCategoriesService: IBaseCrudService<ThreadCategoryViewModel, ThreadCategory>
    {
    }
}
