using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspnetMvcDemo.ViewModel
{
    public class ChooseFactoryVM
    {
        public int LocationId { get; set; }
        public int FactoryId { get; set; }
        public string TargetControllerName { get; set; }
        public string TargetActionName { get; set; }
    }
}