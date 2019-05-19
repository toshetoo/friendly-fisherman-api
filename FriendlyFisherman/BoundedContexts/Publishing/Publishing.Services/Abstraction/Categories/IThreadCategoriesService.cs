using System.Collections.Generic;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel.Services.Abstraction;
using FriendlyFisherman.SharedKernel.Services.Models;
using Publishing.Domain.Entities.Categories;
using Publishing.Domain.EntityViewModels.Categories;

namespace Publishing.Services.Abstraction.Categories
{
    public interface IThreadCategoriesService: IBaseCrudService<ThreadCategoryViewModel, ThreadCategory>
    {
        Task<ServiceResponseBase<Dictionary<string, TrendingCategoryViewModel>>> GetTrendingCategoriesAsync(
            ServiceRequestBase<ThreadCategoryViewModel> request);
    }
}
