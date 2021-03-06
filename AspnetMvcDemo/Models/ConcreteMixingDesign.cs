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
    
    public partial class ConcreteMixingDesign
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ConcreteMixingDesign()
        {
            this.MaterialResultsPdfs = new HashSet<MaterialResultsPdf>();
            this.MixingDesignTests = new HashSet<MixingDesignTest>();
        }
    
        public long Id { get; set; }
        public Nullable<int> FactoryId { get; set; }
        public string ConcreteRank { get; set; }
        public Nullable<double> CementWeight { get; set; }
        public Nullable<double> WaterWeight { get; set; }
        public Nullable<double> WashedSand { get; set; }
        public Nullable<double> Rubble3by4 { get; set; }
        public Nullable<double> Rubble3by8 { get; set; }
        public Nullable<double> WhiteSand { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string Comments { get; set; }
        public string Cement { get; set; }
        public string Water { get; set; }
        public string Rubble3by42 { get; set; }
        public string WashedSand2 { get; set; }
        public string Rubble3by82 { get; set; }
        public string WhiteSand2 { get; set; }
        public Nullable<double> C28Day { get; set; }
        public Nullable<double> Landing { get; set; }
    
        public virtual Factory11 Factory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MaterialResultsPdf> MaterialResultsPdfs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MixingDesignTest> MixingDesignTests { get; set; }
    }
}
