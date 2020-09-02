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
    
    public partial class BlockFactoryReport
    {
        public int Id { get; set; }
        public Nullable<int> ReportNumber { get; set; }
        public Nullable<System.DateTime> ReportDate { get; set; }
        public string FactoryName { get; set; }
        public string FatoryLocation { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }
        public Nullable<int> VisitNumber { get; set; }
        public string Dim { get; set; }
        public Nullable<int> ShapeNumber { get; set; }
        public Nullable<double> CementWeight { get; set; }
        public Nullable<double> waterWeight { get; set; }
        public Nullable<double> SandWeight { get; set; }
        public Nullable<double> AdditionalWeight { get; set; }
        public Nullable<bool> IsCleanLocation { get; set; }
        public string CleanNote { get; set; }
        public string CleanDocPath { get; set; }
        public Nullable<bool> IsLabExist { get; set; }
        public string LabExistNote { get; set; }
        public string LabExistDocPath { get; set; }
        public Nullable<bool> IsEngineerLab { get; set; }
        public string EngineerNote { get; set; }
        public string EngineerPathDoc { get; set; }
        public Nullable<bool> IsMaskSafty { get; set; }
        public string MaskSaftyNote { get; set; }
        public string MaskSaftyPathDoc { get; set; }
        public string Rubble38 { get; set; }
        public Nullable<int> SampleNumber { get; set; }
        public Nullable<bool> isRecieved { get; set; }
        public Nullable<bool> isTested { get; set; }
    }
}
