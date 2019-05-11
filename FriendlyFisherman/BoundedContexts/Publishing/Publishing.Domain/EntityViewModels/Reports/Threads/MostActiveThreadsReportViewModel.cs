using System;
using System.Collections.Generic;
using System.Text;
using FriendlyFisherman.SharedKernel.Reports;
using Publishing.Domain.EntityViewModels.Threads;

namespace Publishing.Domain.EntityViewModels.Reports.Threads
{
    public class MostActiveThreadsReportViewModel
    {
        public ReportParametersModel Periods;
        public IEnumerable<ThreadViewModel> Items { get; set; }
    }
}
