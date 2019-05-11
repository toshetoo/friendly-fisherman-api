using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel.Reports;
using FriendlyFisherman.SharedKernel.Services.Models;
using Publishing.Domain.EntityViewModels.Reports;
using Publishing.Domain.EntityViewModels.Reports.Threads;

namespace Publishing.Services.Abstraction.Reports
{
    public interface IThreadReportsService
    {
        Task<ServiceResponseBase<ThreadsPerDayReportViewModel>> GetThreadsPerDayReportAsync(ServiceRequestBase<ReportParametersModel> request);
        Task<ServiceResponseBase<MostActiveThreadsReportViewModel>> GetMostActiveThreadsReportAsync(ServiceRequestBase<ReportParametersModel> request);
        Task<ServiceResponseBase<PostsPerDayReportViewModel>> GetPostsPerDayReportAsync(ServiceRequestBase<ReportParametersModel> request);
    }
}
