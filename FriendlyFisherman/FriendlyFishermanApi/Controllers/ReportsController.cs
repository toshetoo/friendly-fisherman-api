using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel.Reports;
using FriendlyFisherman.SharedKernel.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Publishing.Services.Abstraction.Reports;
using Users.Services.Request;

namespace FriendlyFishermanApi.Controllers
{
    [Authorize("Bearer")]
    public class ReportsController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly IThreadReportsService _threadReportsService;
        private readonly IThreadCategoriesReportsService _categoriesReportsService;

        public ReportsController(ILogger<ReportsController> logger, IThreadReportsService threadReportsService, IThreadCategoriesReportsService categoriesReportsService)
        {
            _logger = logger;
            _threadReportsService = threadReportsService;
            _categoriesReportsService = categoriesReportsService;
        }

        [HttpGet]
        [Route("ThreadsPerDayReport")]
        public async Task<IActionResult> GetThreadsPerDayReport(ReportParametersModel model)
        {
            var response = await _threadReportsService.GetThreadsPerDayReportAsync(
                new ServiceRequestBase<ReportParametersModel>()
                {
                    Item = model
                });

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));
        }

        [HttpGet]
        [Route("MostActiveThreadsReport")]
        public async Task<IActionResult> GetMostActiveThreadsReport(ReportParametersModel model)
        {
            var response = await _threadReportsService.GetMostActiveThreadsReportAsync(
                new ServiceRequestBase<ReportParametersModel>()
                {
                    Item = model
                });

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));
        }

        [HttpGet]
        [Route("MostUsedCategories")]
        public async Task<IActionResult> GetMostUsedCategoriesReport(ReportParametersModel model)
        {
            var response = await _categoriesReportsService.GetMostUsedCategoriesReportAsync(
                new ServiceRequestBase<ReportParametersModel>()
                {
                    Item = model
                });

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));
        }

        [HttpGet]
        [Route("PostsPerDayReport")]
        public async Task<IActionResult> GetPostsPerDayReport(ReportParametersModel model)
        {
            var response = await _threadReportsService.GetPostsPerDayReportAsync(
                new ServiceRequestBase<ReportParametersModel>()
                {
                    Item = model
                });

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));
        }
    }
}