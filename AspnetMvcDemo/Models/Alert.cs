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
    
    public partial class Alert
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Alert()
        {
            this.AlertInfractions = new HashSet<AlertInfraction>();
        }
    
        public int ID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<bool> Approved { get; set; }
        public string Status { get; set; }
    
        public virtual Factory11 Factory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlertInfraction> AlertInfractions { get; set; }
    }
}
