using AspnetMvcDemo.Models;
using Microsoft.Ajax.Utilities;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspnetMvcDemo.Controllers
{
    public class DailyReportPrintOutController : Controller
    {
        QCEntities context = new QCEntities();
        // GET: DailyReportPrintOut
        public ActionResult Index()
        {
            var koloYaWaled = context.ConcreteSample1.Where(c =>  DbFunctions.TruncateTime(c.ReportDate) == DbFunctions.TruncateTime(DateTime.Today)).ToList();
            foreach (var item in koloYaWaled)
            {
                if (item.IsBasicUsage == null) item.IsBasicUsage = false;
                if (item.IsCleanLocation == null) item.IsCleanLocation = false;
                if (item.IsCleanUsage == null) item.IsCleanUsage = false;
                if (item.IsColumnUsage == null) item.IsColumnUsage = false;
                if (item.IsDustControlInStation == null) item.IsDustControlInStation = false;
                if (item.IsLabEngineer == null) item.IsLabEngineer = false;
                if (item.IsMoldanatInTrucks == null) item.IsMoldanatInTrucks = false;
                if (item.IsOtherUsage == null) item.IsOtherUsage = false;
                if (item.IsPeopleSafty == null) item.IsPeopleSafty = false;
                if (item.IsRokamSummer == null) item.IsRokamSummer = false;
                if (item.IsRoofUsage == null) item.IsRoofUsage = false;
            }

            return View(koloYaWaled);
        }


       
        public ActionResult PrintDailyReport ()
        {
            var koloYaWaled = (from s in context.ConcreteSample1
                               join f in context.Factory11 on s.FactoryName equals f.Name
                               join v in context.VisitDetails on f.Id equals v.FactoryId
                               join u in context.Users on v.MonitorId equals u.Id
                               where DbFunctions.TruncateTime(s.ReportDate) == DbFunctions.TruncateTime(DateTime.Now)
                               orderby s.ReportDate
                               select new Printout
                               {
                                   AdditionAmount = s.AdditionAmount,
                                   CementSource = s.CementSource,
                                   AdditionType = s.AdditionType,
                                   CementType = s.CementType,
                                   CementWeight = s.CementWeight,
                                   CleanDocNote = s.CleanDocNote,
                                   CleanDocPath = s.CleanDocPath,
                                   ClientName = s.ClientName,
                                   ConcreteRank = s.ConcreteRank,
                                   ConcreteTemperture = s.ConcreteTemperture,
                                   DownAmount = s.DownAmount,
                                   DustDocNote = s.DustDocNote,
                                   DustDocPath = s.DustDocPath,
                                   FactoryLocation = s.FactoryLocation,
                                   FactoryName = s.FactoryName,
                                   InvoiceNumber = s.InvoiceNumber,
                                   InvoicePhoto = s.InvoicePhoto,
                                   IsBasicUsage = s.IsBasicUsage,
                                   IsCleanLocation = s.IsCleanLocation,
                                   IsCleanUsage = s.IsCleanUsage,
                                   IsColumnUsage = s.IsColumnUsage,
                                   IsDustControlInStation = s.IsDustControlInStation,
                                   IsLabEngineer = s.IsLabEngineer,
                                   IsMoldanatInTrucks = s.IsMoldanatInTrucks,
                                   IsOtherUsage = s.IsOtherUsage,
                                   IsPeopleSafty = s.IsPeopleSafty,
                                   IsRokamSummer = s.IsRokamSummer,
                                   IsRoofUsage = s.IsRoofUsage,
                                   LabDocNote = s.LabDocNote,
                                   LabDocPath = s.LabDocPath,
                                   Latitude = s.Latitude,
                                   Longitude = s.Longitude,
                                   MixerNumber = s.MixerNumber,
                                   MonitorName = u.FullName,
                                   OtherReason = s.OtherReason,
                                   ReportDate = s.ReportDate,
                                   ReportNo = s.ReportNo,
                                   Rubble3by4 = s.Rubble3by4,
                                   Rubble3by8 = s.Rubble3by8,
                                   SafteyDocNote = s.SafteyDocNote,
                                   SafteyDocPath = s.SafteyDocPath,
                                   SampleNumber = s.SampleNumber,
                                   SummerDocNote = s.SummerDocNote,
                                   SummerDocPath = s.SummerDocPath,
                                   TruckDocNote = s.TruckDocNote,
                                   TruckDocPath = s.TruckDocPath,
                                   TruckNumber = s.TruckNumber,
                                   VisitLocation = s.VisitLocation,
                                   VisitNumber = s.VisitNumber,
                                   WashedSandWeight = s.WashedSandWeight,
                                   WaterTemperature = s.WaterTemperature,
                                   WhiteSandWeight = s.WhiteSandWeight,
                                   WaterWieght = s.WaterWieght,
                                   WeatherTemperture = s.WeatherTemperture


                               }
                            ).DistinctBy(cs => cs.SampleNumber).ToList();



            foreach (var item in koloYaWaled)
            {
                if (item.IsBasicUsage == null) item.IsBasicUsage = false;
                if (item.IsCleanLocation == null) item.IsCleanLocation = false;
                if (item.IsCleanUsage == null) item.IsCleanUsage = false;
                if (item.IsColumnUsage == null) item.IsColumnUsage = false;
                if (item.IsDustControlInStation == null) item.IsDustControlInStation = false;
                if (item.IsLabEngineer == null) item.IsLabEngineer = false;
                if (item.IsMoldanatInTrucks == null) item.IsMoldanatInTrucks = false;
                if (item.IsOtherUsage == null) item.IsOtherUsage = false;
                if (item.IsPeopleSafty == null) item.IsPeopleSafty = false;
                if (item.IsRokamSummer == null) item.IsRokamSummer = false;
                if (item.IsRoofUsage == null) item.IsRoofUsage = false;
            }


            return new PartialViewAsPdf("ShowDailYReport" , koloYaWaled)
            {
                FileName = "التقرير اليومي -"+DateTime.Today.ToShortDateString()+".pdf"
            };
        }
   


    public ActionResult PrintBlockDailyReport()
    {
        var koloYaWaled = (from s in context.BlockFactoryReports
                           join v in context.BlockVisitDetails on DbFunctions.TruncateTime(s.ReportDate) equals DbFunctions.TruncateTime(v.VisitDate)
                           join u in context.BlockUsers on v.MonitorId equals u.Id
                           where DbFunctions.TruncateTime(s.ReportDate) == DbFunctions.TruncateTime(DateTime.Now)
                           orderby s.ReportDate
                           select new BlockPrintout
                           {
                            AdditionalWeight = s.AdditionalWeight , 
                            CementWeight = s.CementWeight , 
                            CleanDocPath = s.CleanDocPath , 
                            CleanNote = s.CleanNote , 
                            Dim = s.Dim , 
                            
                            EngineerNote = s.EngineerNote , 
                            EngineerPathDoc = s.EngineerPathDoc , 
                            FactoryName = s.FactoryName ,
                            FatoryLocation = s.FatoryLocation , 
                            IsCleanLocation = s.IsCleanLocation , 
                            IsEngineerLab =s.IsEngineerLab , 
                            IsLabExist = s.IsLabExist , 
                            IsMaskSafty = s.IsMaskSafty , 
                            LabExistDocPath =s.LabExistDocPath , 
                            LabExistNote = s.LabExistNote , 
                            Latitude = s.Longitude ,
                            Longitude = s.Longitude , 
                            MaskSaftyNote = s.MaskSaftyNote , 
                            MaskSaftyPathDoc = s.MaskSaftyPathDoc , 
                            MonitorName = u.FullName , 
                            ReportDate = s.ReportDate , 
                            ReportNumber = s.ReportNumber , 
                            Rubble38 = s.Rubble38 , 
                            SandWeight = s.SandWeight , 
                            ShapeNumber = s.ShapeNumber , 
                            VisitNumber = s.VisitNumber , 
                            waterWeight = s.waterWeight
                           }
                        ).DistinctBy(s=>s.ReportDate).ToList();



        foreach (var item in koloYaWaled)
        {
           
            if (item.IsCleanLocation == null) item.IsCleanLocation = false;
            if (item.IsEngineerLab == null) item.IsEngineerLab = false;
            if (item.IsLabExist == null) item.IsLabExist = false;
            if (item.IsMaskSafty == null) item.IsMaskSafty = false;
           
        }


        return new PartialViewAsPdf("PrintBlockDailyReport", koloYaWaled)
        {
            FileName = "التقرير اليومي للبلوك -" + DateTime.Today.ToShortDateString() + ".pdf"
        };
    }
}
}

