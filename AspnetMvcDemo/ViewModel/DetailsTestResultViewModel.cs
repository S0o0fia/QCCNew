using AspnetMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;

namespace AspnetMvcDemo.ViewModel
{
    public class DetailsTestResultViewModel
    {
        public ConcreteSample1 ConcreteSample1 { get; set; }
        public List<sevenDaysResult> sevenDaysResults { get; set; }
        public List<monthlyResult> monthlyResults { get; set; }

    }
}