//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AspnetMvcDemo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BringMaterialVisit
    {
        public int id { get; set; }
        public Nullable<int> fact_id { get; set; }
        public Nullable<int> user_id { get; set; }
        public Nullable<System.DateTime> visitdate { get; set; }
        public Nullable<int> location_id { get; set; }
        public string materialName { get; set; }
    }
}
