using System;
using System.Collections.Generic;
using System.Text;

namespace FriendlyFisherman.SharedKernel.Reports
{
    public class ReportParametersModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int Limit { get; set; }
    }
}
