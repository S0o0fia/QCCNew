using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspnetMvcDemo.ViewModel
{
    public class FactoryInfractionAlertStatus
    {
        public int factoryId { get; set; }
        public int FactoryName { get; set; }
        public int tempretureCount { get; set; }
        public int landingCount { get; set; }
        public int twentyEightdaysCount { get; set; }

    }
}