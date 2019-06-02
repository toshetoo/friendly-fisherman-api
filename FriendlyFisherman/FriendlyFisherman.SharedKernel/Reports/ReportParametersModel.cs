using System;
using System.Collections.Generic;
using System.Text;

namespace FriendlyFisherman.SharedKernel.Reports
{
    public class ReportParametersModel
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public int Limit { get; set; }
    }
}
