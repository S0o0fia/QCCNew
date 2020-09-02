using AspnetMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspnetMvcDemo.ViewModel
{
        public class BlockPledgeM
        {
            public int FactoryId { set; get; }
            public int LocationId { set; get; }
            public DateTime Date { set; get; }
            public List<BlockFactory> Factories { set; get; }
            public List<Location> locations { set; get; }

        }
        public class BlockViewPledgeM
        {
            public int ID { set; get; }
            public string FactoryName { set; get; }
            public string Location { set; get; }
            public DateTime? Date { set; get; }
            public HttpPostedFileBase[] files { get; set; }

        }
        public class BlockViewPledgeAlert
        {
            public string FactoryName { set; get; }
            public int ID { set; get; }
            public DateTime? Date { set; get; }

        }
    }