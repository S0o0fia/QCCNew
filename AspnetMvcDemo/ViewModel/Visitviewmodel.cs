using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspnetMvcDemo.ViewModel
{
    public class Visitviewmodel
    {
        public string monitorname { get; set; }
        public string factroyname { get; set; }
        public DateTime visitDate { get; set; }
        public Boolean state { get; set; }
        public long visitId { get; set; }

    }
}