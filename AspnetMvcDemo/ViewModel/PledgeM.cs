using AspnetMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspnetMvcDemo.ViewModel
{
    public class PledgeM
    {
        public int FactoryId { set; get; }
        public int LocationId { set; get; }
        public DateTime Date { set; get; }
        public List<Factory11> Factories { set; get; }
        public List<Location> locations { set; get; }

    }
    public class ViewPledgeM
    {
        public int ID { set; get; }
        public string FactoryName { set; get; }
        public string Location { set; get; }
        public DateTime? Date { set; get; }
        public HttpPostedFileBase[] files { get; set; }

    }
    public class ViewPledgeAlert
    {
        public string FactoryName { set; get; }
        public int ID{set; get; }
        public DateTime? Date { set; get; }
      
    }
}