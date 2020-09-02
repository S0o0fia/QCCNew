using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspnetMvcDemo.ViewModel
{
    public class TestReportViewModel
    {
        public long? sampleNumber { get; set; }
        public string factoryName { get; set; }
        public DateTime? testDate { get; set; }
        public string concreteRank { get; set; }
        public double?  testResult { get; set; }
        public string location { get; set; }
    }
}