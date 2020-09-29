using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace AspnetMvcDemo.ViewModel
{

    public class VisitDetailsForReports
    {
        public string monitorname { get; set; }
        public string factroyname { get; set; }
        public int Mid { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? visitDate { get; set; }
        public long visitId { get; set; }
        public string location { get; set; }
    }
}