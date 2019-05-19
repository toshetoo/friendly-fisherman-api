using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel.Helpers;
using FriendlyFisherman.SharedKernel.Services.Impl;
using FriendlyFisherman.SharedKernel.Services.Models;
using Publishing.DataAccess.Repositories.Categories;
using Publishing.Domain.Entities.Categories;
using Publishing.Domain.EntityViewModels.Categories;
using Publishing.Domain.Repositories.Categories;
using Publishing.Domain.Repositories.Threads;
using Publishing.Services.Abstraction.Categories;

namespace Publishing.Services.Implementation.Categories
{
    public class ThreadCategoriesService: BaseCrudService<ThreadCategoryViewModel, ThreadCategory, IThreadCategoriesRepository>, IThreadCategoriesService
    {
        private readonly IThreadsRepository _threadsRepository;

        public ThreadCategoriesService(IThreadCategoriesRepository repo, IThreadsRepository threadsRepository) : base(repo)
        {
            _threadsRepository = threadsRepository;
        }

        public async Task<ServiceResponseBase<Dictionary<string, TrendingCategoryViewModel>>> GetTrendingCategoriesAsync(ServiceRequestBase<ThreadCategoryViewModel> request)
        {
            return await Task.Run(() => GetTrendingCategories(request));
        }

        private ServiceResponseBase<Dictionary<string, TrendingCategoryViewModel>> GetTrendingCategories(ServiceRequestBase<ThreadCategoryViewModel> request)
        {
            var response = new ServiceResponseBase<Dictionary<string, TrendingCategoryViewModel>>();

            try
            {
                var startDate = DateTime.Now.AddDays(-7);
                var categories = _repo.GetAll();
                var threadsInPeriod = _threadsRepository
                    .GetAll(false, t => t.CreatedOn >= startDate);
                var threadsDictionary = threadsInPeriod
                    .GroupBy(c => c.CategoryId)
                    .ToDictionary(c => c.Key,
                        c => _threadsRepository.GetWhere(th => th.CategoryId == c.Key).Count());
                var orderedDictionary = threadsDictionary
                    .OrderByDescending(el => el.Value)
                    .Take(10)
                    .ToDictionary(el => el.Key, el => el.Value);

                var result = new Dictionary<string, TrendingCategoryViewModel>();
                foreach (var th in orderedDictionary)
                {
                    var vm = Mapper<ThreadCategoryViewModel, ThreadCategory>.Map(categories.First(t => t.Id == th.Key));
                    result.Add(vm.Id, new TrendingCategoryViewModel
                    {
                        Id = th.Key,
                        Count = th.Value,
                        Name = vm.Name
                    });
                }

                response.Item = result;
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }

    }
}
