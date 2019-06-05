using System;
using System.Collections.Generic;
using System.Text;
using FriendlyFisherman.SharedKernel.Reports;
using Publishing.Domain.Entities.Categories;
using Publishing.Domain.EntityViewModels.Categories;

namespace Publishing.Domain.EntityViewModels.Reports.Categories
{
    public class MostUsedCategoriesReportViewModel
    {
        public ReportParametersModel Period { get; set; }
        public int AllCategoriesCount { get; set; }

        public Dictionary<string, int> Items { get; set; }
    }
}
