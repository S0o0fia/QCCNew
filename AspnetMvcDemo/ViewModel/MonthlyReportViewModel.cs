using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspnetMvcDemo.ViewModel
{
    public class MonthlyReportViewModel
    {
        public string factoryName { get; set; }
        public string conctreteRank { get; set; }
        public string visitDate { get; set; }
        public Int64? SampleNumber { get; set; }
        public double? ConcreteTemperture { get; set; }
        public double? DownAmount { get; set; }
        public string CementType { get; set; }
        public bool? IsCleanUsage { get; set; }
        public bool? IsBasicUsage { get; set; }
        public bool? IsColumnUsage { get; set; }
        public bool? IsRoofUsage { get; set; }
        public bool? IsOtherUsage { get; set; }
        public string OtherReason { get; set; }
        public double? SevenDaysAverageCompressiveStrength { get; set; }
        public double? MonthlyAverageCompressiveStrength { get; set; }

    }
}