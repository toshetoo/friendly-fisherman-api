using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel.Reports;
using FriendlyFisherman.SharedKernel.Services.Models;
using Publishing.Domain.Entities.Categories;
using Publishing.Domain.EntityViewModels.Reports.Categories;
using Publishing.Domain.Repositories.Categories;
using Publishing.Domain.Repositories.Threads;
using Publishing.Services.Abstraction.Reports;

namespace Publishing.Services.Implementation.Reports
{
    public class ThreadCategoriesReportsService: IThreadCategoriesReportsService
    {

        private readonly IThreadCategoriesRepository _categoriesRepository;
        private readonly IThreadsRepository _threadsRepository;

        public ThreadCategoriesReportsService(IThreadCategoriesRepository categoriesRepository, IThreadsRepository threadsRepository)
        {
            _categoriesRepository = categoriesRepository;
            _threadsRepository = threadsRepository;
        }

        public async Task<ServiceResponseBase<MostUsedCategoriesReportViewModel>> GetMostUsedCategoriesReportAsync(ServiceRequestBase<ReportParametersModel> request)
        {
            return await Task.Run(() => GetMostUsedCategoriesReport(request));
        }

        private ServiceResponseBase<MostUsedCategoriesReportViewModel> GetMostUsedCategoriesReport(
            ServiceRequestBase<ReportParametersModel> request)
        {
            var response = new ServiceResponseBase<MostUsedCategoriesReportViewModel>();

            try
            {
                var allCategories = _categoriesRepository.GetAll().ToList();
                response.Item = new MostUsedCategoriesReportViewModel
                {
                    AllCategoriesCount = allCategories.Count()
                };

                // TODO optimize
                var dict = allCategories.ToDictionary(c => c.Name,
                    c => _threadsRepository.GetWhere(th => th.CategoryId == c.Id).Count());
                var ordered = dict.OrderByDescending(el => el.Value);
                var dict2 = ordered.Take(request.Item.Limit)
                    .ToDictionary(el => el.Key, el => el.Value);
                response.Item.Items = dict2;



            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }
    }
}
