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
    
    public partial class ContAsphaltSample
    {
        public int ID { get; set; }
        public Nullable<int> AsphaltTypeId { get; set; }
        public Nullable<int> ProjectId { get; set; }
        public Nullable<double> StartHour { get; set; }
        public Nullable<double> EndHour { get; set; }
        public Nullable<bool> NoJobs { get; set; }
        public Nullable<int> PermitNumber { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<double> LayerThickness { get; set; }
        public Nullable<double> LayerWidth { get; set; }
        public Nullable<double> EvelCm { get; set; }
        public Nullable<double> LocationStartN { get; set; }
        public Nullable<double> LocationStartE { get; set; }
        public Nullable<double> LocationEN { get; set; }
        public Nullable<double> LocationEE { get; set; }
        public string Street { get; set; }
        public string MoreStreet2 { get; set; }
        public Nullable<double> LocationStartNStreet2 { get; set; }
        public Nullable<double> LocationStartEStreet2 { get; set; }
        public Nullable<double> LocationENStreet2 { get; set; }
        public Nullable<double> LocationEEStreet2 { get; set; }
        public string RequestNumber { get; set; }
        public Nullable<bool> IsSampled { get; set; }
        public string Source { get; set; }
        public Nullable<int> TeamId { get; set; }
        public Nullable<System.DateTime> ExecutionDate { get; set; }
        public Nullable<double> Quantity { get; set; }
        public Nullable<bool> InspectorReturned { get; set; }
        public string InspectorNotes { get; set; }
        public Nullable<double> InspectorLongitude { get; set; }
        public Nullable<double> InspectorMagnitude { get; set; }
        public Nullable<int> PlantLocationId { get; set; }
        public string ContSampleRequest { get; set; }
        public Nullable<bool> IsEmergency { get; set; }
        public Nullable<System.Guid> ContractorId { get; set; }
        public Nullable<System.Guid> SupplierId { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<System.Guid> InspectorBy { get; set; }
        public Nullable<int> MixDesignId { get; set; }
    }
}