using System;
using System.Collections.Generic;
using System.Text;
using FriendlyFisherman.SharedKernel.Reports;

namespace Publishing.Domain.EntityViewModels.Reports.Threads
{
    public class PostsPerDayReportViewModel
    {
        public ReportParametersModel Periods;
        public Dictionary<DateTime, int> Items { get; set; }
    }
}
