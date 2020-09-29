using AspnetMvcDemo.Models;
using AspnetMvcDemo.Services;
using AspnetMvcDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;

namespace AspnetMvcDemo.Controllers
{
    public class ReportsController : Controller
    {
        QCEntities context = new QCEntities();


        // get: All Visits with Report For Admin
        public ActionResult VisitsReport()
        {
            VisitService visitService = new VisitService();
            List<VisitDetailsForReports> visitDetailsForReports = new List<VisitDetailsForReports>();
            visitDetailsForReports = visitService.getVisitsReport();
           
            return View(visitDetailsForReports);
        }


        // GET: Details Of Visits With Report For Admin
        public ActionResult ReportShowVisits(string factoryname, DateTime visitDate)
        {
            var koloYaWaled = context.ConcreteSample1.Where(c => c.FactoryName == factoryname && DbFunctions.TruncateTime(c.ReportDate) == visitDate).FirstOrDefault();
            if (koloYaWaled.IsBasicUsage == null) koloYaWaled.IsBasicUsage = false;
            if (koloYaWaled.IsCleanLocation == null) koloYaWaled.IsCleanLocation = false;
            if (koloYaWaled.IsCleanUsage == null) koloYaWaled.IsCleanUsage = false;
            if (koloYaWaled.IsColumnUsage == null) koloYaWaled.IsColumnUsage = false;
            if (koloYaWaled.IsDustControlInStation == null) koloYaWaled.IsDustControlInStation = false;
            if (koloYaWaled.IsLabEngineer == null) koloYaWaled.IsLabEngineer = false;
            if (koloYaWaled.IsMoldanatInTrucks == null) koloYaWaled.IsMoldanatInTrucks = false;
            if (koloYaWaled.IsOtherUsage == null) koloYaWaled.IsOtherUsage = false;
            if (koloYaWaled.IsPeopleSafty == null) koloYaWaled.IsPeopleSafty = false;
            if (koloYaWaled.IsRokamSummer == null) koloYaWaled.IsRokamSummer = false;
            if (koloYaWaled.IsRoofUsage == null) koloYaWaled.IsRoofUsage = false;
            return View(koloYaWaled);
        }


        // get: Daily Visits with Report For Admin
        public ActionResult dailyVisitsReport()
        {
            VisitService visitService = new VisitService();
            datePickerDailyReport visitDetailsForReports = new datePickerDailyReport();
            visitDetailsForReports.DailyReportViews = new List<VisitDetailsForReports>();
            visitDetailsForReports.DailyReportViews = visitService.getDailyVisitsReport();
            visitDetailsForReports.fromDate = DateTime.Today.ToString("dd/MM/yyyy");
            return View(visitDetailsForReports);
        }

        [HttpPost]
        public ActionResult dailyVisitsReport(datePickerDailyReport model)
        {
            var visitDate = DateTime.ParseExact(model.fromDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            VisitService visitService = new VisitService();
            datePickerDailyReport visitDetailsForReports = new datePickerDailyReport();
            visitDetailsForReports.DailyReportViews = visitService.getDailyVisitsReport(visitDate);
           if (visitDetailsForReports.DailyReportViews.Count == 0)
            {
                TempData["msg"] = "Failed";
            }

            return View(visitDetailsForReports);

        }

        // GET: Details Of Daily Visits With Report For Admin
        public ActionResult ReportShowDailyVisits(string factoryname, DateTime visitDate)
        {
            var koloYaWaled = context.ConcreteSample1.Where(c => c.FactoryName == factoryname && DbFunctions.TruncateTime(c.ReportDate) == visitDate).FirstOrDefault();
            if (koloYaWaled.IsBasicUsage == null) koloYaWaled.IsBasicUsage = false;
            if (koloYaWaled.IsCleanLocation == null) koloYaWaled.IsCleanLocation = false;
            if (koloYaWaled.IsCleanUsage == null) koloYaWaled.IsCleanUsage = false;
            if (koloYaWaled.IsColumnUsage == null) koloYaWaled.IsColumnUsage = false;
            if (koloYaWaled.IsDustControlInStation == null) koloYaWaled.IsDustControlInStation = false;
            if (koloYaWaled.IsLabEngineer == null) koloYaWaled.IsLabEngineer = false;
            if (koloYaWaled.IsMoldanatInTrucks == null) koloYaWaled.IsMoldanatInTrucks = false;
            if (koloYaWaled.IsOtherUsage == null) koloYaWaled.IsOtherUsage = false;
            if (koloYaWaled.IsPeopleSafty == null) koloYaWaled.IsPeopleSafty = false;
            if (koloYaWaled.IsRokamSummer == null) koloYaWaled.IsRokamSummer = false;
            if (koloYaWaled.IsRoofUsage == null) koloYaWaled.IsRoofUsage = false;

            return View(koloYaWaled);
        }


        public ActionResult UpdateReport(string id)
        {
            int SampleNumber = int.Parse(id);
            var model = context.ConcreteSample1.Where(s => s.SampleNumber == SampleNumber).FirstOrDefault();
            ConcreteSamplesWithPath sample = new ConcreteSamplesWithPath();
            sample.AdditionAmount = model.AdditionAmount;
            sample.AdditionType = model.AdditionType;
            sample.CementSource = model.CementSource;
            sample.CementType = model.CementType;
            sample.CementWeight = model.CementWeight;
            sample.CleanDocNote = model.CleanDocNote;
            sample.CleanDocPath = model.CleanDocPath;
            sample.ClientName = model.ClientName;
            sample.concretemixPath = model.concretemixPath;
            sample.ConcreteRank = model.ConcreteRank;
            sample.ConcreteTemperture = model.ConcreteTemperture;
            sample.CreatedDate = model.CreatedDate;
            sample.DownAmount = model.DownAmount;
            sample.DownAmountDocPath = model.DownAmountDocPath;
            sample.DustDocNote = model.DustDocNote;
            sample.DustDocPath = model.DustDocPath;
            sample.FactoryLocation = model.FactoryLocation;
            sample.FactoryName = model.FactoryName;
            sample.InvoiceNumber = model.InvoiceNumber;
            sample.InvoicePhoto = model.InvoicePhoto;
            sample.IsBasicUsage = model.IsBasicUsage;
            sample.IsCleanLocation = model.IsCleanLocation;
            sample.IsCleanUsage = model.IsCleanUsage;
            sample.IsColumnUsage = model.IsColumnUsage;
            sample.IsDustControlInStation = model.IsDustControlInStation;
            sample.IsLabEngineer = model.IsLabEngineer;
            sample.IsMoldanatInTrucks = model.IsMoldanatInTrucks;
            sample.IsOtherUsage = model.IsOtherUsage;
            sample.IsPeopleSafty = model.IsPeopleSafty;
            sample.IsReceived = model.IsReceived;
            sample.IsRokamSummer = model.IsRokamSummer;
            sample.IsRoofUsage = model.IsRoofUsage;
            sample.labdeliver = model.labdeliver;
            sample.LabDocNote = model.LabDocNote;
            sample.LabDocPath = model.LabDocPath;
            sample.Latitude = model.Latitude;
            sample.Longitude = model.Longitude;
            sample.MixerNumber = model.MixerNumber;
            sample.OtherReason = model.OtherReason;
            sample.ReportDate = model.ReportDate;
            sample.ReportNo = model.ReportNo;
            sample.Rubble3by4 = model.Rubble3by4;
            sample.Rubble3by8 = model.Rubble3by8;
            sample.SafteyDocNote = model.SafteyDocNote;
            sample.SafteyDocPath = model.SafteyDocPath;
            sample.SampledBy = model.SampledBy;
            sample.SampleNumber = model.SampleNumber;
            sample.SummerDocNote = model.SummerDocNote;
            sample.SummerDocPath = model.SummerDocPath;
            sample.TempretureDocPath = model.TempretureDocPath;
            sample.TruckDocNote = model.TruckDocNote;
            sample.TruckDocPath = model.TruckDocPath;
            sample.TruckNumber = model.TruckNumber;
            sample.VisitLocation = model.VisitLocation;
            sample.VisitNumber = model.VisitNumber;
            sample.WashedSandWeight = model.WashedSandWeight;
            sample.WaterTemperature = model.WaterTemperature;
            sample.WaterWieght = model.WaterWieght;
            sample.WeatherTemperture = model.WeatherTemperture;
            sample.WhiteSandWeight = model.WhiteSandWeight;
            return View(sample);
        }
        [HttpPost]
        public ActionResult UpdateReport(ConcreteSamplesWithPath model)
        {

            if (model.ConcreteTemperture >= 34 || model.DownAmount <= 100)
            {
                bool Flag = false;
                Infraction infra = context.Infractions.Where(s => s.SampleNo == model.SampleNumber).FirstOrDefault();
                if (infra == null)
                {
                    Flag = true;
                    infra = new Infraction();
                    infra.AdminApproved = false;
                    infra.MonitorApproved = false;
                    infra.VisitDate = DateTime.Now;

                }
                //infra.FactoryId = sample.FactoryId;
                infra.SampleNo = model.SampleNumber;
                if (model.ConcreteTemperture >= 34)
                    infra.Temperature = true;
                if (model.DownAmount <= 100)
                    infra.Landing = true;
                if (model.IsCleanLocation == false)
                    infra.IsCleanLocation = false;
                if (Flag == true)
                    context.Infractions.Add(infra);
                context.SaveChanges();
            }

            foreach (var file in model.files)
            {
                if (file != null)
                {
                    Random random = new Random();
                    FileInfo fi = new FileInfo(file.FileName);
                    DateTime d = DateTime.Now;
                    var InputFileName = d.Ticks + random.Next(100, 100000000).ToString();
                    var ServerSavePath = System.IO.Path.Combine(Server.MapPath("~/Content"), InputFileName + fi.Extension);
                    //Save file to server folder  
                    file.SaveAs(ServerSavePath);
                    model.InvoicePhoto = InputFileName + fi.Extension;
                }
            }
            foreach (var file in model.CleanDocfiles)
            {
                if (file != null)
                {
                    Random random = new Random();
                    FileInfo fi = new FileInfo(file.FileName);

                    DateTime d = DateTime.Now;
                    var InputFileName = d.Ticks + random.Next(100, 100000000).ToString();
                    var ServerSavePath = System.IO.Path.Combine(Server.MapPath("~/Content"), InputFileName + fi.Extension);
                    //Save file to server folder  
                    file.SaveAs(ServerSavePath);
                    model.CleanDocPath = InputFileName + fi.Extension;
                }
            }
            foreach (var file in model.DustDocfiles)
            {
                if (file != null)
                {
                    Random random = new Random();
                    FileInfo fi = new FileInfo(file.FileName);
                    DateTime d = DateTime.Now;
                    var InputFileName = d.Ticks + random.Next(100, 100000000).ToString();
                    var ServerSavePath = System.IO.Path.Combine(Server.MapPath("~/Content"), InputFileName + fi.Extension);
                    //Save file to server folder  
                    file.SaveAs(ServerSavePath);
                    model.DustDocPath = InputFileName + fi.Extension;
                }
            }
            foreach (var file in model.LabDocfiles)
            {
                if (file != null)
                {
                    Random random = new Random();
                    FileInfo fi = new FileInfo(file.FileName);
                    DateTime d = DateTime.Now;
                    var InputFileName = d.Ticks + random.Next(100, 100000000).ToString();
                    var ServerSavePath = System.IO.Path.Combine(Server.MapPath("~/Content"), InputFileName + fi.Extension);
                    //Save file to server folder  
                    file.SaveAs(ServerSavePath);
                    model.LabDocPath = InputFileName + fi.Extension;
                }
            }
            foreach (var file in model.SafteyDocfiles)
            {
                if (file != null)
                {
                    Random random = new Random();
                    FileInfo fi = new FileInfo(file.FileName);
                    DateTime d = DateTime.Now;
                    var InputFileName = d.Ticks + random.Next(100, 100000000).ToString();
                    var ServerSavePath = System.IO.Path.Combine(Server.MapPath("~/Content"), InputFileName + fi.Extension);
                    //Save file to server folder  
                    file.SaveAs(ServerSavePath);
                    model.SafteyDocPath = InputFileName + fi.Extension;
                }
            }
            foreach (var file in model.SummerDocfiles)
            {
                if (file != null)
                {
                    Random random = new Random();
                    FileInfo fi = new FileInfo(file.FileName);
                    DateTime d = DateTime.Now;
                    var InputFileName = d.Ticks + random.Next(100, 100000000).ToString();
                    var ServerSavePath = System.IO.Path.Combine(Server.MapPath("~/Content"), InputFileName + fi.Extension);
                    //Save file to server folder  
                    file.SaveAs(ServerSavePath);
                    model.SummerDocPath = InputFileName + fi.Extension;
                }
            }
            foreach (var file in model.TruckDocfiles)
            {
                if (file != null)
                {
                    Random random = new Random();
                    FileInfo fi = new FileInfo(file.FileName);
                    DateTime d = DateTime.Now;
                    var InputFileName = d.Ticks + random.Next(100, 100000000).ToString();
                    var ServerSavePath = System.IO.Path.Combine(Server.MapPath("~/Content"), InputFileName + fi.Extension);
                    //Save file to server folder  
                    file.SaveAs(ServerSavePath);
                    model.TruckDocPath = InputFileName + fi.Extension;
                }
            }
            foreach (var file in model.TempretureDocFiles)
            {
                if (file != null)
                {
                    Random random = new Random();
                    FileInfo fi = new FileInfo(file.FileName);
                    DateTime d = DateTime.Now;
                    var InputFileName = d.Ticks + random.Next(100, 100000000).ToString();
                    var ServerSavePath = System.IO.Path.Combine(Server.MapPath("~/Content"), InputFileName + fi.Extension);
                    //Save file to server folder  
                    file.SaveAs(ServerSavePath);
                    model.TempretureDocPath = InputFileName + fi.Extension;
                }
            }
            foreach (var file in model.DownAmountDocFiles)
            {
                if (file != null)
                {
                    Random random = new Random();
                    FileInfo fi = new FileInfo(file.FileName);
                    DateTime d = DateTime.Now;
                    var InputFileName = d.Ticks + random.Next(100, 100000000).ToString();
                    var ServerSavePath = System.IO.Path.Combine(Server.MapPath("~/Content"), InputFileName + fi.Extension);
                    //Save file to server folder  
                    file.SaveAs(ServerSavePath);
                    model.DownAmountDocPath = InputFileName + fi.Extension;
                }
            }
            foreach (var file in model.ConcreteMixDocFiles)
            {
                if (file != null)
                {
                    Random random = new Random();
                    FileInfo fi = new FileInfo(file.FileName);
                    DateTime d = DateTime.Now;
                    var InputFileName = d.Ticks + random.Next(100, 100000000).ToString();
                    var ServerSavePath = System.IO.Path.Combine(Server.MapPath("~/Content"), InputFileName + fi.Extension);
                    //Save file to server folder  
                    file.SaveAs(ServerSavePath);
                    model.concretemixPath = InputFileName + fi.Extension;
                }
            }
            if (model.Reason1 == null)
                model.OtherReason = model.Reason2;
            else
                model.OtherReason = model.Reason1;

            var uId = Convert.ToInt32(Session["UserId"].ToString());
            var sample = context.ConcreteSample1.Where(s => s.SampleNumber == model.SampleNumber).FirstOrDefault();

            if (model.IsCleanUsage != null || model.IsBasicUsage != null || model.IsColumnUsage != null || model.IsOtherUsage != null || model.IsRoofUsage != null)
            {
                sample.IsBasicUsage = model.IsBasicUsage;
                sample.IsCleanUsage = model.IsCleanUsage;
                sample.IsColumnUsage = model.IsColumnUsage;
                sample.IsOtherUsage = model.IsOtherUsage;
                sample.IsRoofUsage = model.IsRoofUsage;
            }

            //Update History 
            ReportUpdateHistory reportUpdate = new ReportUpdateHistory();
            reportUpdate.AdditionAmount = sample.AdditionAmount != model.AdditionAmount ? true : false;
            reportUpdate.AdditionType = sample.AdditionType != model.AdditionType ? true : false;
            reportUpdate.CementSource = sample.CementSource != model.CementSource ? true : false;
            reportUpdate.CementType = sample.CementType != model.CementType ? true : false;
            reportUpdate.CementWeight = sample.CementWeight != model.CementWeight ? true : false;
            reportUpdate.CleanDocNote = sample.CleanDocNote != model.CleanDocNote ? true : false;
            reportUpdate.ClientName = sample.ClientName != model.ClientName ? true : false;
            reportUpdate.ConcreteRank = sample.ConcreteRank != model.ConcreteRank ? true : false;
            reportUpdate.ConcreteTemperture = sample.ConcreteTemperture != model.ConcreteTemperture ? true : false;
            reportUpdate.DownAmount = sample.DownAmount != model.DownAmount ? true : false;
         
            reportUpdate.DustDocNote = sample.DustDocNote != model.DustDocNote ? true : false;
            reportUpdate.InvoiceNumber = sample.InvoiceNumber != model.InvoiceNumber ? true : false;
            reportUpdate.IsBasicUsage = sample.IsBasicUsage != model.IsBasicUsage ? true : false;
            reportUpdate.IsCleanLocation = sample.IsCleanLocation != model.IsCleanLocation ? true : false;
            reportUpdate.IsCleanUsage = sample.IsCleanUsage != model.IsCleanUsage ? true : false;
            reportUpdate.IsColumnUsage = sample.IsColumnUsage != model.IsColumnUsage ? true : false;
            reportUpdate.IsDustControlInStation = sample.IsDustControlInStation != model.IsDustControlInStation ? true : false;
            reportUpdate.IsMoldanatInTrucks = sample.IsMoldanatInTrucks != model.IsMoldanatInTrucks ? true : false;
            reportUpdate.IsOtherUsage = sample.IsOtherUsage != sample.IsOtherUsage ? true : false;
            reportUpdate.IsPeopleSafty = sample.IsPeopleSafty != sample.IsPeopleSafty ? true : false;
            reportUpdate.IsRokamSummer = sample.IsRokamSummer != model.IsRokamSummer ? true : false;
            reportUpdate.IsRoofUsage = sample.IsRoofUsage != model.IsRoofUsage ? true : false;
            reportUpdate.LabDocNote = sample.LabDocNote != model.LabDocNote ? true : false;
            reportUpdate.MixerNumber = sample.MixerNumber != model.MixerNumber ? true : false;
            reportUpdate.OtherReason = sample.OtherReason != model.MixerNumber ? true : false;
            reportUpdate.Rubble3by4 = sample.Rubble3by4 != model.Rubble3by4 ? true : false;
            reportUpdate.Rubble3by8 = sample.Rubble3by8 != model.Rubble3by8 ? true : false;
            reportUpdate.SafteyDocNote = sample.SafteyDocNote != model.SafteyDocNote ? true : false;
            reportUpdate.SummerDocNote = sample.SummerDocNote != model.SummerDocNote ? true : false;
            reportUpdate.SummerDocNote = sample.SummerDocNote != model.SummerDocNote ? true : false;
            reportUpdate.TruckDocNote = sample.TruckDocNote != model.TruckDocNote ? true : false;
            reportUpdate.TruckNumber = sample.TruckNumber != model.TruckNumber ? true : false;
            reportUpdate.WashedSandWeight = sample.WashedSandWeight != model.WashedSandWeight ? true : false;
            reportUpdate.WaterTemperature = sample.WaterTemperature != model.WaterTemperature ? true : false;
            reportUpdate.WaterWieght = sample.WaterWieght != model.WaterWieght ? true : false;
            reportUpdate.WeatherTemperture = sample.WeatherTemperture != model.WeatherTemperture ? true : false;
            reportUpdate.WhiteSandWeight = sample.WhiteSandWeight != model.WhiteSandWeight ? true : false;
            reportUpdate.SampleNumber = int.Parse(sample.SampleNumber.ToString());
            reportUpdate.userId = uId;
            reportUpdate.MonitorName = context.Users.Where(u => u.Id == uId).Select(u => u.FullName).FirstOrDefault();
            reportUpdate.EditDate = DateTime.Today;
            if(model.CleanDocPath != null)
            {
                reportUpdate.CleanDocPath = sample.CleanDocPath != model.CleanDocPath ? true : false;
            }
            if (model.concretemixPath != null)
            {
                reportUpdate.concretemixPath = sample.concretemixPath != model.concretemixPath ? true : false;
            }
            if (model.InvoicePhoto != null)
            {
                reportUpdate.InvoicePhoto = sample.InvoicePhoto != model.InvoicePhoto ? true : false;
            }
            if (model.DustDocPath != null)
            {
                reportUpdate.DustDocPath = sample.DustDocPath != model.DustDocPath ? true : false;
            }
            if (model.LabDocPath != null)
            {
                reportUpdate.LabDocPath = sample.LabDocPath != model.LabDocPath ? true : false;
            }
            if (model.SafteyDocPath != null)
            {
                reportUpdate.SafteyDocPath = sample.SafteyDocPath != model.SafteyDocPath ? true : false;
            }
            if (model.SummerDocPath != null)
            {
                reportUpdate.SummerDocPath = sample.SummerDocPath != model.SummerDocPath ? true : false;
            }
            if (model.TempretureDocPath != null)
            {
                reportUpdate.TempretureDocPath = sample.TempretureDocPath != model.TempretureDocPath ? true : false;
            }
            if (model.DownAmountDocPath != null)
            {
                reportUpdate.DownAmountDocPath = sample.DownAmountDocPath != model.DownAmountDocPath ? true : false;
            }
            if (model.TruckDocPath != null)
            {
                reportUpdate.TruckDocPath = sample.TruckDocPath != model.TruckDocPath ? true : false;
            }

            //add old values 
            reportUpdate.OldAdditionAmount = sample.AdditionAmount;
            reportUpdate.OldAdditionType = sample.AdditionType;
            reportUpdate.OldCementSource = sample.CementSource;
            reportUpdate.OldCementType = sample.CementType;
            reportUpdate.OldCementWeight = sample.CementWeight;
            reportUpdate.OldCleanDocNote = sample.CleanDocNote;
            reportUpdate.OldCleanDocPath = sample.CleanDocPath;
            reportUpdate.OldClientName = sample.ClientName;
            reportUpdate.OldconcretemixPath = sample.concretemixPath;
            reportUpdate.OldConcreteRank = sample.ConcreteRank;
            reportUpdate.OldConcreteTemperture = sample.ConcreteTemperture;
            reportUpdate.OldDownAmount = sample.DownAmount;
            reportUpdate.OldDownAmountDocPath = sample.DownAmountDocPath;
            reportUpdate.OldDustDocNote = sample.DustDocNote;
            reportUpdate.OldDustDocPath = sample.DustDocPath;
             reportUpdate.OldInvoiceNumber = sample.InvoiceNumber;
            reportUpdate.OldInvoicePhoto = sample.InvoicePhoto;
            reportUpdate.OldIsBasicUsage = sample.IsBasicUsage;
            reportUpdate.OldIsCleanLocation = sample.IsCleanLocation;
            reportUpdate.OldIsCleanUsage = sample.IsCleanUsage;
            reportUpdate.OldIsColumnUsage = sample.IsColumnUsage;
            reportUpdate.OldIsDustControlInStation = sample.IsDustControlInStation;
            reportUpdate.OldIsLabEngineer = sample.IsLabEngineer;
            reportUpdate.OldIsMoldanatInTrucks = sample.IsMoldanatInTrucks;
            reportUpdate.OldIsOtherUsage = sample.IsOtherUsage;
            reportUpdate.OldIsPeopleSafty = sample.IsPeopleSafty;
            reportUpdate.OldIsRokamSummer = sample.IsRokamSummer;
            reportUpdate.OldIsRoofUsage = sample.IsRoofUsage;
            reportUpdate.OldLabDocNote = sample.LabDocNote;
            reportUpdate.OldLabDocPath = sample.LabDocPath;
             reportUpdate.OldMixerNumber = sample.MixerNumber;
            reportUpdate.OldOtherReason = sample.OtherReason;
            reportUpdate.OldRubble3by4 = sample.Rubble3by4;
            reportUpdate.OldRubble3by8 = sample.Rubble3by8;
            reportUpdate.OldSafteyDocNote = sample.SafteyDocNote;
            reportUpdate.OldSafteyDocPath = sample.SafteyDocPath;
            reportUpdate.OldSummerDocNote = sample.SummerDocNote;
            reportUpdate.OldSummerDocPath = sample.SummerDocPath;
            reportUpdate.OldTempretureDocPath = sample.TempretureDocPath;
            reportUpdate.OldTruckDocNote = sample.TruckDocNote;
            reportUpdate.OldTruckDocPath = sample.TruckDocPath;
            reportUpdate.OldTruckNumber = sample.TruckNumber;
            reportUpdate.OldWashedSandWeight = sample.WashedSandWeight;
            reportUpdate.OldWaterTemperature = sample.WaterTemperature;
            reportUpdate.OldWaterWieght = sample.WaterWieght;
            reportUpdate.OldWeatherTemperture = sample.WaterWieght;
            reportUpdate.OldWhiteSandWeight = sample.WhiteSandWeight;
            //add new Values
            reportUpdate.NewAdditionAmount = model.AdditionAmount;
            reportUpdate.NewAdditionType = model.AdditionType;
            reportUpdate.NewCementSource = model.CementSource;
            reportUpdate.NewCementType = model.CementType;
            reportUpdate.NewCementWeight = model.CementWeight;
            reportUpdate.NewCleanDocNote = model.CleanDocNote;
            reportUpdate.NewCleanDocPath = model.CleanDocPath;
            reportUpdate.NewClientName = model.ClientName;
            reportUpdate.NewconcretemixPath = model.concretemixPath;
            reportUpdate.NewConcreteRank = model.ConcreteRank;
            reportUpdate.NewConcreteTemperture = model.ConcreteTemperture;
            reportUpdate.NewDownAmount = model.DownAmount;
            reportUpdate.NewDownAmountDocPath = model.DownAmountDocPath;
            reportUpdate.NewDustDocNote = model.DustDocNote;
            reportUpdate.NewDustDocPath = model.DustDocPath;
            reportUpdate.NewInvoiceNumber = model.InvoiceNumber;
            reportUpdate.NewInvoicePhoto = model.InvoicePhoto;
            reportUpdate.NewIsBasicUsage = model.IsBasicUsage;
            reportUpdate.NewIsCleanLocation = model.IsCleanLocation;
            reportUpdate.NewIsCleanUsage = model.IsCleanUsage;
            reportUpdate.NewIsColumnUsage = model.IsColumnUsage;
            reportUpdate.NewIsDustControlInStation = model.IsDustControlInStation;
            reportUpdate.NewIsLabEngineer = model.IsLabEngineer;
            reportUpdate.NewIsMoldanatInTrucks = model.IsMoldanatInTrucks;
            reportUpdate.NewIsOtherUsage = model.IsOtherUsage;
            reportUpdate.NewIsPeopleSafty = model.IsPeopleSafty;
            reportUpdate.NewIsRokamSummer = model.IsRokamSummer;
            reportUpdate.NewIsRoofUsage = model.IsRoofUsage;
            reportUpdate.NewLabDocNote = model.LabDocNote;
            reportUpdate.NewLabDocPath = model.LabDocPath;
            reportUpdate.NewMixerNumber = model.MixerNumber;
            reportUpdate.NewOtherReason = model.OtherReason;
            reportUpdate.NewRubble3by4 = model.Rubble3by4;
            reportUpdate.NewRubble3by8 = model.Rubble3by8;
            reportUpdate.NewSafteyDocNote = model.SafteyDocNote;
            reportUpdate.NewSafteyDocPath = model.SafteyDocPath;
            reportUpdate.NewSummerDocNote = model.SummerDocNote;
            reportUpdate.NewSummerDocPath = model.SummerDocPath;
            reportUpdate.NewTempretureDocPath = model.TempretureDocPath;
            reportUpdate.NewTruckDocNote = model.TruckDocNote;
            reportUpdate.NewTruckDocPath = model.TruckDocPath;
            reportUpdate.NewTruckNumber = model.TruckNumber;
            reportUpdate.NewWashedSandWeight = model.WashedSandWeight;
            reportUpdate.NewWaterTemperature = model.WaterTemperature;
            reportUpdate.NewWaterWieght = model.WaterWieght;
            reportUpdate.NewWeatherTemperture = model.WaterWieght;
            reportUpdate.NewWhiteSandWeight = model.WhiteSandWeight;
            reportUpdate.NewWhiteSandWeight = model.WhiteSandWeight;

            //Update All Sample
            sample.AdditionAmount = model.AdditionAmount == null  ? sample.AdditionAmount : model.AdditionAmount;
            sample.AdditionType = model.AdditionType == null ? sample.AdditionType : model.AdditionType;
            sample.CementSource = model.CementSource == null ? sample.CementSource : model.CementSource;
            sample.CementType = model.CementType == null ? sample.CementType : model.CementType;
            sample.CementWeight = model.CementWeight == null ?  sample.CementWeight :  model.CementWeight;
            sample.CleanDocNote = model.CleanDocNote == null ? sample.CleanDocNote : model.CleanDocNote;
            sample.CleanDocPath = model.CleanDocPath == null ? sample.CleanDocPath : model.CleanDocPath;
            sample.ClientName = model.ClientName == null ? sample.ClientName : model.ClientName;
            sample.concretemixPath = model.concretemixPath == null ? sample.concretemixPath  : model.concretemixPath;
            sample.ConcreteRank = model.ConcreteRank == null ? sample.ConcreteRank  : model.ConcreteRank;
            sample.ConcreteTemperture = model.ConcreteTemperture == null ? sample.ConcreteTemperture : model.ConcreteTemperture;
            sample.CreatedDate = model.CreatedDate == null ? sample.CreatedDate : model.CreatedDate;
            sample.DownAmount = model.DownAmount == null ? sample.DownAmount : model.DownAmount;
            sample.DownAmountDocPath = model.DownAmountDocPath == null ? sample.DownAmountDocPath : model.DownAmountDocPath;
            sample.DustDocNote = model.DustDocNote == null ? sample.DustDocNote : model.DustDocNote;
            sample.DustDocPath = model.DustDocPath == null ? sample.DustDocPath  : model.DustDocPath;
            sample.FactoryLocation = model.FactoryLocation == null ? sample.FactoryLocation : model.FactoryLocation;
            sample.FactoryName = model.FactoryName == null ? sample.FactoryName  : model.FactoryName;
            sample.InvoiceNumber = model.InvoiceNumber == null ? sample.InvoiceNumber : model.InvoiceNumber;
            sample.InvoicePhoto = model.InvoicePhoto == null ? sample.InvoicePhoto : model.InvoicePhoto;
            //sample.IsBasicUsage = model.IsBasicUsage == null ? sample.IsBasicUsage : model.IsBasicUsage;
            sample.IsCleanLocation = model.IsCleanLocation == null ? sample.IsCleanLocation : model.IsCleanLocation;
            //sample.IsCleanUsage = model.IsCleanUsage == null ? sample.IsCleanUsage : model.IsCleanUsage;
            //sample.IsColumnUsage = model.IsColumnUsage == null ? sample.IsColumnUsage : model.IsColumnUsage;
            sample.IsDustControlInStation = model.IsDustControlInStation == null ? sample.IsDustControlInStation :  model.IsDustControlInStation;
            sample.IsLabEngineer = model.IsLabEngineer == null ? sample.IsLabEngineer : model.IsLabEngineer;
            sample.IsMoldanatInTrucks = model.IsMoldanatInTrucks == null ? sample.IsMoldanatInTrucks : model.IsMoldanatInTrucks;
            //sample.IsOtherUsage = model.IsOtherUsage == null ? sample.IsOtherUsage  : model.IsOtherUsage;
            sample.IsPeopleSafty = model.IsPeopleSafty == null ? sample.IsPeopleSafty : model.IsPeopleSafty;
            sample.IsReceived = model.IsReceived == null ? sample.IsReceived : model.IsReceived;
            sample.IsRokamSummer = model.IsRokamSummer == null ? sample.IsRokamSummer : model.IsRokamSummer;
            //sample.IsRoofUsage = model.IsRoofUsage == null ? sample.IsRoofUsage  : model.IsRoofUsage;
            sample.labdeliver = model.labdeliver == null ? sample.labdeliver : model.labdeliver;
            sample.LabDocNote = model.LabDocNote == null ? sample.LabDocNote : model.LabDocNote;
            sample.LabDocPath = model.LabDocPath == null ? sample.LabDocPath  : model.LabDocPath;
            sample.Latitude = model.Latitude == null ? sample.Latitude : model.Latitude;
            sample.Longitude = model.Longitude == null ? sample.Longitude : model.Longitude;
            sample.MixerNumber = model.MixerNumber == null ? sample.MixerNumber : model.MixerNumber;
            sample.OtherReason = model.OtherReason == null ? sample.OtherReason : model.OtherReason;
            sample.ReportDate = sample.ReportDate;
            sample.ReportNo = sample.ReportNo ;
            sample.Rubble3by4 = model.Rubble3by4 == null ? sample.Rubble3by4  : model.Rubble3by4;
            sample.Rubble3by8 = model.Rubble3by8 == null ? sample.Rubble3by8 : model.Rubble3by8;
            sample.SafteyDocNote = model.SafteyDocNote == null ? sample.SafteyDocNote : model.SafteyDocNote;
            sample.SafteyDocPath = model.SafteyDocPath == null ? sample.SafteyDocPath : model.SafteyDocPath;
            sample.SampledBy = model.SampledBy == null ? sample.SampledBy : model.SampledBy;
            sample.SampleNumber = model.SampleNumber == null ? sample.SampleNumber : model.SampleNumber;
            sample.SummerDocNote = model.SummerDocNote == null ? sample.SummerDocNote : model.SummerDocNote;
            sample.SummerDocPath = model.SummerDocPath == null ? sample.SummerDocPath : model.SummerDocPath;
            sample.TempretureDocPath = model.TempretureDocPath == null ? sample.TempretureDocPath  : model.TempretureDocPath;
            sample.TruckDocNote = model.TruckDocNote == null ? sample.TruckDocNote : model.TruckDocNote;
            sample.TruckDocPath = model.TruckDocPath== null ? sample.TruckDocPath : model.TruckDocPath;
            sample.TruckNumber = model.TruckNumber == null ? sample.TruckNumber : model.TruckNumber;
            sample.VisitLocation = model.VisitLocation == null ? sample.VisitLocation : model.VisitLocation;
            sample.VisitNumber = model.VisitNumber == null ? sample.VisitNumber : model.VisitNumber;
            sample.WashedSandWeight = model.WashedSandWeight == null ? sample.WashedSandWeight : model.WashedSandWeight;
            sample.WaterTemperature = model.WaterTemperature == null ? sample.WaterTemperature : model.WaterTemperature;
            sample.WaterWieght = model.WaterWieght == null ? sample.WaterWieght : model.WaterWieght;
            sample.WeatherTemperture = model.WeatherTemperture == null ? sample.WeatherTemperture : model.WeatherTemperture;
            sample.WhiteSandWeight = model.WhiteSandWeight == null ? sample.WhiteSandWeight : model.WhiteSandWeight;

            context.ReportUpdateHistories.Add(reportUpdate);
            context.SaveChanges();
            return RedirectToAction("VisitsReport");
        }

        public ActionResult showMonthlyReport(datePickerMonthlyReport datePickerMonthly = null)
        {
             datePickerMonthly.monthlyReportViews = new List<MonthlyReportViewModel>();
             return View(datePickerMonthly);
        }

        [HttpPost]
        public ActionResult MonthlyReport(datePickerMonthlyReport datePickerMonthly)
        {
            datePickerMonthly.monthlyReportViews = new List<MonthlyReportViewModel>();
            var date = datePickerMonthly.toDate.Trim();
            var date2 = date.Split(new string[] { "to"} ,StringSplitOptions.None);
            var fromdate = date2[0];
            var todate = date2[1];
            VisitService visitService = new VisitService();
            var list = visitService.monthlyReport();
            string[] itemdate;
            foreach (var item in list)
            {
                itemdate = item.visitDate.Split(new string[] { " " }, StringSplitOptions.None);
                if (DateTime.Parse(itemdate[0].ToString()) >= DateTime.Parse(fromdate) &&
                     DateTime.Parse(itemdate[0].ToString()) <= DateTime.Parse(todate))
                {
                        datePickerMonthly.monthlyReportViews.Add(item);
                }
            }
            TempData["object"] = datePickerMonthly;
            return RedirectToAction("MonthReport");
        }

        public ActionResult MonthReport()
        {
            datePickerMonthlyReport date = (datePickerMonthlyReport)TempData["object"];
            return View(date);
        }


        public ActionResult TestReport()
        {
            concreteSampleSer concreteSampleSer = new concreteSampleSer();
            var testList = concreteSampleSer.getTestReport();
            return View(testList);
        }
        
        public ActionResult testReportDetails(int sampleNumber)
        {
            DetailsTestResultViewModel detailsTestResultViewModel = new DetailsTestResultViewModel();
            concreteSampleSer concreteSampleSer = new concreteSampleSer();
            detailsTestResultViewModel.ConcreteSample1 = concreteSampleSer.getTestReportDetailsConcreteSample(sampleNumber);
            
            DateTime ReportDate = (DateTime)detailsTestResultViewModel.ConcreteSample1.ReportDate;
            detailsTestResultViewModel.ConcreteSample1.ReportDate = ReportDate.AddDays(1);
            
            detailsTestResultViewModel.sevenDaysResults = concreteSampleSer.getTestReportDetailsSevenDaysResults(sampleNumber);
            detailsTestResultViewModel.monthlyResults = concreteSampleSer.getTestReportDetailsMonthlyResults(sampleNumber);

            return View(detailsTestResultViewModel);

        }

        //for yealry Report 
        public ActionResult YearlyReport ()
        {
            return View();
        }


        public ActionResult UpdateHistory (string id)
        {
            int SampleNumber = int.Parse(id);
            
          
              var Report = context.ReportUpdateHistories.Where(h => h.SampleNumber == SampleNumber).ToList();
           
            return View(Report);
        }


        public ActionResult UpdateHistoryDetail(string id)
        {
            int ID = int.Parse(id);
            var UpdateHistory = context.ReportUpdateHistories.Where(h => h.SampleNumber == ID).FirstOrDefault();
            return PartialView(UpdateHistory);
        }
    }
}