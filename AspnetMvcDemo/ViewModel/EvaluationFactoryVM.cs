using AspnetMvcDemo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspnetMvcDemo.ViewModel
{
    public class EvaluationFactoryVM
    {
        public EvaluationFactoryVM()
        {
            option = new List<QuestionAndAnswersVM>();
        }
        public int FactoryId { get; set; }
        public List<QuestionAndAnswersVM> option { get; set; }
    }
    public class MixingStationAnswersVM
    {
        public MixingStationAnswersVM()
        {
            mixstation = new List<QuestionAndAnswersVM>();
        }
        public int FactoryId { get; set; }
        public List<QuestionAndAnswersVM> mixstation { get; set; }
    }
    public class QuestionAndAnswersVM
    {
        public string QuestionId { get; set; }
        public string Answer { get; set; }
    }
    public class ReportResultVM
    {
        public int ReportId { get; set; }
        public string Comment { get; set; }
        public int LocationId { get; set; }
        public int FactoryId { get; set; }
        public FactoryStatus Status { get; set; }
    }
    public class FactoryGeneralInfoVM
    {
        public int FactoryId { get; set; }
        public string FactoryName { get; set; }
        public string Location { get; set; }
        public string OwnerName { get; set; }
        public string ManagerName { get; set; }
        public string ManagerQualification { get; set; }
        public string TechnicalDirectorName { get; set; }
        public string TechnicalDirectorQualification { get; set; }
        public string FactoryPhoneNumber { get; set; }
        public string FactoryFaxNumber { get; set; }
        public double? ProductionCapacity { get; set; }
        public double? DailyProductionRate { get; set; }

    }
}