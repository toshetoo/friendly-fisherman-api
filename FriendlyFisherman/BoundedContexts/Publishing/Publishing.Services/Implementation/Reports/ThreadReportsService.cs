using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel.Helpers;
using FriendlyFisherman.SharedKernel.Reports;
using FriendlyFisherman.SharedKernel.Services.Models;
using Publishing.Domain.Entities.Threads;
using Publishing.Domain.EntityViewModels.Reports.Threads;
using Publishing.Domain.EntityViewModels.Threads;
using Publishing.Domain.Repositories.Threads;
using Publishing.Services.Abstraction.Reports;
using Publishing.Services.Abstraction.Threads;

namespace Publishing.Services.Implementation.Reports
{
    public class ThreadReportsService: IThreadReportsService
    {

        private readonly IThreadsRepository _threadsRepository;
        private readonly IThreadReplyRepository _threadReplyRepository;

        public ThreadReportsService(IThreadsRepository threadsRepository, IThreadReplyRepository threadReplyRepository)
        {
            _threadsRepository = threadsRepository;
            _threadReplyRepository = threadReplyRepository;
        }

        public async Task<ServiceResponseBase<ThreadsPerDayReportViewModel>> GetThreadsPerDayReportAsync(ServiceRequestBase<ReportParametersModel> request)
        {
            return await Task.Run(() => GetThreadsPerDayReport(request));
        }

        private ServiceResponseBase<ThreadsPerDayReportViewModel> GetThreadsPerDayReport(ServiceRequestBase<ReportParametersModel> request)
        {
            var response = new ServiceResponseBase<ThreadsPerDayReportViewModel>();

            try
            {
                var startInterval = request.Item.StartDate;
                var endInterval = request.Item.EndDate;

                var allThreadsForPeriod =
                    _threadsRepository.GetWhere(t => t.CreatedOn.Date >= DateTime.Parse(startInterval) && t.CreatedOn.Date <= DateTime.Parse(endInterval));

                var grouped = allThreadsForPeriod.GroupBy(th => th.CreatedOn.Date);

                response.Item = new ThreadsPerDayReportViewModel
                {
                    Periods = request.Item,
                    Items = grouped.ToDictionary(g => g.Key, g => g.ToList().Count())
                        .OrderByDescending(g => g.Value)
                        .Take(request.Item.Limit)
                        .ToDictionary(e => e.Key, e => e.Value)
                };
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }

        public async Task<ServiceResponseBase<MostActiveThreadsReportViewModel>> GetMostActiveThreadsReportAsync(ServiceRequestBase<ReportParametersModel> request)
        {
            return await Task.Run(() => GetMostActiveThreadsReport(request));
        }

        private ServiceResponseBase<MostActiveThreadsReportViewModel> GetMostActiveThreadsReport(ServiceRequestBase<ReportParametersModel> request)
        {
            var response = new ServiceResponseBase<MostActiveThreadsReportViewModel>();

            try
            {
                var startInterval = request.Item.StartDate;
                var endInterval = request.Item.EndDate;

                var allThreadsForPeriod = _threadsRepository.GetWhere(
                        t => t.CreatedOn.Date >= DateTime.Parse(startInterval) && t.CreatedOn.Date <= DateTime.Parse(endInterval),
                        t => t.Replies)
                        .OrderByDescending(thread => thread.Replies.Count())
                        .Take(request.Item.Limit).Select(t => new ThreadViewModel()
                        {
                            Title = t.Title,
                            AnswersCount = t.Replies.Count
                        });

                response.Item = new MostActiveThreadsReportViewModel()
                {
                    Items = allThreadsForPeriod,
                    Periods = request.Item
                };

            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }

        public async Task<ServiceResponseBase<PostsPerDayReportViewModel>> GetPostsPerDayReportAsync(ServiceRequestBase<ReportParametersModel> request)
        {
            return await Task.Run(() => GetPostsPerDayReport(request));
        }

        private ServiceResponseBase<PostsPerDayReportViewModel> GetPostsPerDayReport(ServiceRequestBase<ReportParametersModel> request)
        {
            var response = new ServiceResponseBase<PostsPerDayReportViewModel>();

            try
            {
                var startInterval = request.Item.StartDate;
                var endInterval = request.Item.EndDate;

                var allPostsForPeriod =
                    _threadReplyRepository.GetWhere(threadReply => threadReply.CreatedOn.Date >= DateTime.Parse(startInterval) && threadReply.CreatedOn.Date <= DateTime.Parse(endInterval));

                var grouped = allPostsForPeriod.GroupBy(th => th.CreatedOn.Date);

                response.Item = new PostsPerDayReportViewModel
                {
                    Periods = request.Item,
                    Items = grouped.ToDictionary(g => g.Key, g => g.ToList().Count())
                        .OrderByDescending(g => g.Value)
                        .Take(request.Item.Limit)
                        .ToDictionary(e => e.Key, e => e.Value)
                };
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }
    }
}
