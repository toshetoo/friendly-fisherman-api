using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel.Reports;
using FriendlyFisherman.SharedKernel.Services.Models;
using Publishing.Domain.EntityViewModels.Reports.Categories;
using Publishing.Domain.EntityViewModels.Reports.Threads;

namespace Publishing.Services.Abstraction.Reports
{
    public interface IThreadCategoriesReportsService
    {
        Task<ServiceResponseBase<MostUsedCategoriesReportViewModel>> GetMostUsedCategoriesReportAsync(ServiceRequestBase<ReportParametersModel> request);
    }
}
