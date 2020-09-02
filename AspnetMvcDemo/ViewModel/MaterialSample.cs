using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspnetMvcDemo.ViewModel
{
    public class MaterialSample
    {
        public MaterialSample()
        {
            this.locations = new List<SelectListItem>();
            this.factories = new List<SelectListItem>();
        }

        public List<SelectListItem> locations { get; set; }
        public List<SelectListItem> factories { get; set; }

        public int locationId { get; set; }
        public int factoryId { get; set; }
    }
}