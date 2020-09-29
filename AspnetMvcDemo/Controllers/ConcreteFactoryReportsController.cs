using AspnetMvcDemo.Models;
using AspnetMvcDemo.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspnetMvcDemo.Controllers
{
    public class ConcreteFactoryReportsController : Controller
    {
        QCEntities db = new QCEntities();

        // GET: ConcreteFactoryReports
        public ActionResult ConcreteFactoryReports(int id)
        {
            var factory = db.Factory11.Where(f => f.Id == id).FirstOrDefault();
            var Sample =
            db.ConcreteSample1.Where(s => s.FactoryName.Equals(factory.Name) == true && DbFunctions.TruncateTime(s.ReportDate) == DbFunctions.TruncateTime(DateTime.Today)).Select(s => s.SampleNumber).FirstOrDefault();

            if(Sample == null)
            {
                var samples = db.ConcreteSample1.OrderByDescending(s => s.SampleNumber).ToList();
                var lastSample = samples.Count() == 0 ? null : samples.FirstOrDefault();

                ConcreteSamplesWithPath sample = new ConcreteSamplesWithPath();
                //sample.FactoryId = factory.Id;
                sample.SampleNumber = lastSample == null ? 1 : lastSample.SampleNumber + 1;
                sample.ReportNo = lastSample == null ? 1 : lastSample.ReportNo + 1;
                sample.VisitNumber = lastSample == null ? 1 : lastSample.VisitNumber + 1;
                sample.FactoryName = factory.Name;
                sample.FactoryLocation = factory.Location;
                //sample.ClientName = factory.OwnerName;
                return View(sample);
            }
            else
            {
               return RedirectToAction("ShowAddedDate", "ConcreteFactoryReports", new { id = Sample });
            }
        }

        [HttpPost]
        public ActionResult AddConcreteSample(ConcreteSamplesWithPath sample)
        {
          
            if (Request.Form["Submit2"] != null)
            {
                ObjectParameter statusCode = new ObjectParameter("StatusCode", typeof(int));
                ObjectParameter statusMessage = new ObjectParameter("StatusMessage", typeof(string));

                if (sample.ConcreteTemperture >= 34 || sample.DownAmount <= 100)
                {
                    bool Flag = false;
                    Infraction infra = db.Infractions.Where(s => s.SampleNo == sample.SampleNumber).FirstOrDefault();
                    if (infra == null)
                    {
                        Flag = true;
                        infra = new Infraction();
                        infra.AdminApproved = false;
                        infra.MonitorApproved = false;
                        infra.VisitDate = DateTime.Today;

                    }
                    //infra.FactoryId = sample.FactoryId;
                    infra.SampleNo = sample.SampleNumber;
                    if (sample.ConcreteTemperture >= 34)
                        infra.Temperature = true;
                    if (sample.DownAmount <= 100)
                        infra.Landing = true;
                    if (sample.IsCleanLocation == false)
                        infra.IsCleanLocation = false;
                    if (Flag == true)
                        db.Infractions.Add(infra);
                    db.SaveChanges();
                }

                
                foreach (var file in sample.CleanDocfiles)
                {
                    if (file != null)
                    {
                        Random random = new Random();
                        FileInfo fi = new FileInfo(file.FileName);

                        DateTime d = DateTime.Today;
                        var InputFileName = d.Ticks + random.Next(100, 100000000).ToString();
                        var ServerSavePath = System.IO.Path.Combine(Server.MapPath("~/Content"), InputFileName + fi.Extension);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        sample.CleanDocPath = InputFileName + fi.Extension;
                    }
                }
                foreach (var file in sample.DustDocfiles)
                {
                    if (file != null)
                    {
                        Random random = new Random();
                        FileInfo fi = new FileInfo(file.FileName);
                        DateTime d = DateTime.Today;
                        var InputFileName = d.Ticks + random.Next(100, 100000000).ToString();
                        var ServerSavePath = System.IO.Path.Combine(Server.MapPath("~/Content"), InputFileName + fi.Extension);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        sample.DustDocPath = InputFileName + fi.Extension;
                    }
                }
                foreach (var file in sample.LabDocfiles)
                {
                    if (file != null)
                    {
                        Random random = new Random();
                        FileInfo fi = new FileInfo(file.FileName);
                        DateTime d = DateTime.Today;
                        var InputFileName = d.Ticks + random.Next(100, 100000000).ToString();
                        var ServerSavePath = System.IO.Path.Combine(Server.MapPath("~/Content"), InputFileName + fi.Extension);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        sample.LabDocPath = InputFileName + fi.Extension;
                    }
                }
                foreach (var file in sample.SafteyDocfiles)
                {
                    if (file != null)
                    {
                        Random random = new Random();
                        FileInfo fi = new FileInfo(file.FileName);
                        DateTime d = DateTime.Today;
                        var InputFileName = d.Ticks + random.Next(100, 100000000).ToString();
                        var ServerSavePath = System.IO.Path.Combine(Server.MapPath("~/Content"), InputFileName + fi.Extension);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        sample.SafteyDocPath = InputFileName + fi.Extension;
                    }
                }
                foreach (var file in sample.SummerDocfiles)
                {
                    if (file != null)
                    {
                        Random random = new Random();
                        FileInfo fi = new FileInfo(file.FileName);
                        DateTime d = DateTime.Today;
                        var InputFileName = d.Ticks + random.Next(100, 100000000).ToString();
                        var ServerSavePath = System.IO.Path.Combine(Server.MapPath("~/Content"), InputFileName + fi.Extension);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        sample.SummerDocPath = InputFileName + fi.Extension;
                    }
                }
                foreach (var file in sample.TruckDocfiles)
                {
                    if (file != null)
                    {
                        Random random = new Random();
                        FileInfo fi = new FileInfo(file.FileName);
                        DateTime d = DateTime.Today;
                        var InputFileName = d.Ticks + random.Next(100, 100000000).ToString();
                        var ServerSavePath = System.IO.Path.Combine(Server.MapPath("~/Content"), InputFileName + fi.Extension);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        sample.TruckDocPath = InputFileName + fi.Extension;
                    }
                }
                foreach (var file in sample.TempretureDocFiles)
                {
                    if (file != null)
                    {
                        Random random = new Random();
                        FileInfo fi = new FileInfo(file.FileName);
                        DateTime d = DateTime.Today;
                        var InputFileName = d.Ticks + random.Next(100, 100000000).ToString();
                        var ServerSavePath = System.IO.Path.Combine(Server.MapPath("~/Content"), InputFileName + fi.Extension);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        sample.TempretureDocPath = InputFileName + fi.Extension;
                    }
                }
                foreach (var file in sample.DownAmountDocFiles)
                {
                    if (file != null)
                    {
                        Random random = new Random();
                        FileInfo fi = new FileInfo(file.FileName);
                        DateTime d = DateTime.Today;
                        var InputFileName = d.Ticks + random.Next(100, 100000000).ToString();
                        var ServerSavePath = System.IO.Path.Combine(Server.MapPath("~/Content"), InputFileName + fi.Extension);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        sample.DownAmountDocPath = InputFileName + fi.Extension;
                    }
                }
                foreach (var file in sample.ConcreteMixDocFiles)
                {
                    if (file != null)
                    {
                        Random random = new Random();
                        FileInfo fi = new FileInfo(file.FileName);
                        DateTime d = DateTime.Today;
                        var InputFileName = d.Ticks + random.Next(100, 100000000).ToString();
                        var ServerSavePath = System.IO.Path.Combine(Server.MapPath("~/Content"), InputFileName + fi.Extension);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        sample.concretemixPath = InputFileName + fi.Extension;
                    }
                }
                if (sample.Reason1 == null)
                    sample.OtherReason = sample.Reason2;
                else
                    sample.OtherReason = sample.Reason1;

                var uId = Convert.ToInt32(Session["UserId"].ToString());
               
                var date = DateTime.Today;
                ConcreteSample1 concreteSamp = new ConcreteSample1
                {
                    AdditionAmount = sample.AdditionAmount,
                    AdditionType = sample.AdditionType,
                    CementSource = sample.CementSource,
                    CementType = sample.CementType,
                    CementWeight = sample.CementWeight,
                    CleanDocNote = sample.CleanDocNote,
                    CleanDocPath = sample.CleanDocPath,
                    ClientName = sample.ClientName,
                    ConcreteRank = sample.ConcreteRank,
                    ConcreteTemperture = sample.ConcreteTemperture,
                    CreatedDate = sample.CreatedDate,
                    DownAmount = sample.DownAmount,
                    DownAmountDocPath = sample.DownAmountDocPath,
                    DustDocNote = sample.DustDocNote,
                    DustDocPath = sample.DustDocPath,
                    FactoryLocation = sample.FactoryLocation,
                    FactoryName = sample.FactoryName,
                    InvoiceNumber = sample.InvoiceNumber,
                    InvoicePhoto = sample.InvoicePhoto,
                    IsBasicUsage = sample.IsBasicUsage,
                    IsCleanLocation = sample.IsCleanLocation,
                    IsCleanUsage = sample.IsCleanUsage,
                    IsColumnUsage = sample.IsColumnUsage,
                    IsDustControlInStation = sample.IsDustControlInStation,
                    IsLabEngineer = sample.IsLabEngineer,
                    IsMoldanatInTrucks = sample.IsMoldanatInTrucks,
                    IsOtherUsage = sample.IsOtherUsage,
                    IsPeopleSafty = sample.IsPeopleSafty,
                    IsReceived = false,
                    IsRokamSummer = sample.IsRokamSummer,
                    IsRoofUsage = sample.IsRoofUsage,
                    labdeliver = false,
                    LabDocNote = sample.LabDocNote,
                    LabDocPath = sample.LabDocPath,
                    Latitude = sample.Latitude,
                    Longitude = sample.Longitude,
                    MixerNumber = sample.MixerNumber,
                    OtherReason = sample.OtherReason,
                    ReportDate = date,
                    ReportNo = sample.ReportNo,
                    Rubble3by4 = sample.Rubble3by4,
                    Rubble3by8 = sample.Rubble3by8,
                    SafteyDocNote = sample.SafteyDocNote,
                    SafteyDocPath = sample.SafteyDocPath,
                    SampledBy = sample.SampledBy,
                    SampleNumber = sample.SampleNumber,
                    SummerDocNote = sample.SummerDocNote,
                    SummerDocPath = sample.SummerDocPath,
                    TempretureDocPath = sample.TempretureDocPath,
                    TruckDocNote = sample.TruckDocNote,
                    TruckDocPath = sample.TruckDocPath,
                    TruckNumber = sample.TruckNumber,
                    VisitLocation = sample.VisitLocation,
                    VisitNumber = sample.VisitNumber,
                    WashedSandWeight = sample.WashedSandWeight,
                    WaterTemperature = sample.WaterTemperature,
                    WaterWieght = sample.WaterWieght,
                    WeatherTemperture = sample.WeatherTemperture,
                    WhiteSandWeight = sample.WhiteSandWeight,
                    concretemixPath = sample.concretemixPath
                };
                db.ConcreteSample1.Add(concreteSamp);
            }

            else if (Request.Form["Submit1"] != null)
            {
                foreach (var file in sample.files)
                {
                    if (file != null)
                    {
                        Random random = new Random();
                        FileInfo fi = new FileInfo(file.FileName);
                        DateTime d = DateTime.Today;
                        var InputFileName = d.Ticks + random.Next(100, 100000000).ToString();
                        var ServerSavePath = System.IO.Path.Combine(Server.MapPath("~/Content"), InputFileName + fi.Extension);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        sample.InvoicePhoto = InputFileName + fi.Extension;
                    }
                }

                if (sample.Reason1 == null)
                    sample.OtherReason = sample.Reason2;
                else
                    sample.OtherReason = sample.Reason1;

                var uId = Convert.ToInt32(Session["UserId"].ToString());
                var date = DateTime.Today;
                ConcreteSample1 concreteSamp = new ConcreteSample1
                {
                    ClientName = sample.ClientName,
                    ConcreteRank = sample.ConcreteRank,
                    CreatedDate = sample.CreatedDate,
                    FactoryLocation = sample.FactoryLocation,
                    FactoryName = sample.FactoryName,
                    InvoiceNumber = sample.InvoiceNumber,
                    InvoicePhoto = sample.InvoicePhoto,
                    IsBasicUsage = sample.IsBasicUsage,
                    IsCleanUsage = sample.IsCleanUsage,
                    IsColumnUsage = sample.IsColumnUsage,
                    IsLabEngineer = sample.IsLabEngineer,
                    IsOtherUsage = sample.IsOtherUsage,
                    IsReceived = false,
                    IsRoofUsage = sample.IsRoofUsage,
                    labdeliver = false,
                    Latitude = sample.Latitude,
                    Longitude = sample.Longitude,
                    MixerNumber = sample.MixerNumber,
                    OtherReason = sample.OtherReason,
                    ReportDate = date,
                    ReportNo = sample.ReportNo,
                    SampledBy = sample.SampledBy,
                    SampleNumber = sample.SampleNumber,
                    TruckNumber = sample.TruckNumber,
                    VisitLocation = sample.VisitLocation,
                    VisitNumber = sample.VisitNumber,
                };
                db.ConcreteSample1.Add(concreteSamp);
                
            }
            var factid = db.Factory11.Where(f => f.Name == sample.FactoryName).Select(f => f.Id).FirstOrDefault();
            var visit = db.VisitDetails.Where(v => v.FactoryId == factid &&
            DbFunctions.TruncateTime(v.VisitDate) == DbFunctions.TruncateTime(DateTime.Today)).FirstOrDefault();
           if(visit != null)
            {
                visit.Status = true;
            }
            try
            {
                db.SaveChanges();
                TempData["AlertMessage1"] = "Success";
                return RedirectToAction("ShowAddedDate", "ConcreteFactoryReports", new
                {
                    id = sample.SampleNumber
                });
            }
            catch (Exception ex)
            {
                TempData["AlertMessage2"] = ex.Data;
                return RedirectToAction("ConcreteFactoryReports", "ConcreteFactoryReports", new
                {
                    id = sample.FactoryId
                });
            }




        }

        public ActionResult ShowAddedDate(int id)
        {

            var sample = db.ConcreteSample1.Where(s => s.SampleNumber == id).FirstOrDefault();
            ConcreteSamplesWithPath model = new ConcreteSamplesWithPath();
            model.AdditionAmount = sample.AdditionAmount;
            model.AdditionType = sample.AdditionType;
            model.CementSource = sample.CementSource;
            model.CementType = sample.CementType;
            model.CementWeight = sample.CementWeight;
            model.CleanDocNote = sample.CleanDocNote;
            model.ClientName = sample.ClientName;
            model.concretemixPath = sample.concretemixPath;
            model.ConcreteRank = sample.ConcreteRank;
            model.ConcreteTemperture = sample.ConcreteTemperture;
            model.CreatedDate = sample.CreatedDate;
            model.DownAmount = sample.DownAmount;
            model.DownAmountDocPath = sample.DownAmountDocPath;
            model.DustDocPath = sample.DustDocPath;
            model.DustDocNote = sample.DustDocNote;
            model.FactoryId = db.Factory11.Where(f => f.Name == sample.FactoryName).Select(f => f.Id).FirstOrDefault();
            model.FactoryLocation = sample.FactoryLocation;
            model.FactoryName = sample.FactoryName;
            model.InvoiceNumber = sample.InvoiceNumber;
            model.InvoicePhoto = sample.InvoicePhoto;
            model.IsBasicUsage = sample.IsBasicUsage;
            model.IsCleanLocation = sample.IsCleanLocation;
            model.IsCleanUsage = sample.IsCleanUsage;
            model.IsColumnUsage = sample.IsColumnUsage;
            model.IsDustControlInStation = sample.IsDustControlInStation;
            model.IsLabEngineer = sample.IsLabEngineer;
            model.IsMoldanatInTrucks = sample.IsMoldanatInTrucks;
            model.IsOtherUsage = sample.IsOtherUsage;
            model.IsPeopleSafty = sample.IsPeopleSafty;
            model.IsReceived = sample.IsReceived;
            model.IsRokamSummer = sample.IsRokamSummer;
            model.IsRoofUsage = sample.IsRoofUsage;
            model.labdeliver = sample.labdeliver;
            model.LabDocPath = sample.LabDocPath;
            model.LabDocNote = sample.LabDocNote;
            model.Latitude = sample.Latitude;
            model.Longitude = sample.Longitude;
            model.MixerNumber = sample.MixerNumber;
            model.OtherReason = sample.OtherReason;
            model.ReportDate = sample.ReportDate;
            model.ReportNo = sample.ReportNo;
            model.Rubble3by4 = sample.Rubble3by4;
            model.Rubble3by8 = sample.Rubble3by8;
            model.SafteyDocPath = sample.SafteyDocPath;
            model.SafteyDocNote = sample.SafteyDocNote;
            model.SampledBy = sample.SampledBy;
            model.SampleNumber = sample.SampleNumber;
            model.SummerDocPath = sample.SummerDocPath;
            model.SummerDocNote = sample.SummerDocNote;
            model.TempretureDocPath = sample.TempretureDocPath;
            model.TruckDocNote = sample.TruckDocNote;
            model.TruckDocPath = sample.TruckDocPath;
            model.TruckNumber = sample.TruckNumber;
            model.VisitLocation = sample.VisitLocation;
            model.VisitNumber = sample.VisitNumber;
            model.WashedSandWeight = sample.WashedSandWeight;
            model.WaterTemperature = sample.WaterTemperature;
            model.WaterWieght = sample.WaterWieght;
            model.WeatherTemperture = sample.WeatherTemperture;
            model.WhiteSandWeight = sample.WhiteSandWeight;
            
            return View(model);
        }

        [HttpPost]
        public ActionResult ShowAddedDate (ConcreteSamplesWithPath model)

        {
            if (model.ConcreteTemperture >= 34 || model.DownAmount <= 100)
            {
                bool Flag = false;
                Infraction infra = db.Infractions.Where(s => s.SampleNo == model.SampleNumber).FirstOrDefault();
                if (infra == null)
                {
                    Flag = true;
                    infra = new Infraction();
                    infra.AdminApproved = false;
                    infra.MonitorApproved = false;
                    infra.VisitDate = DateTime.Today;

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
                    db.Infractions.Add(infra);
                db.SaveChanges();
            }


            foreach (var file in model.CleanDocfiles)
            {
                if (file != null)
                {
                    Random random = new Random();
                    FileInfo fi = new FileInfo(file.FileName);

                    DateTime d = DateTime.Today;
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
                    DateTime d = DateTime.Today;
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
                    DateTime d = DateTime.Today;
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
                    DateTime d = DateTime.Today;
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
                    DateTime d = DateTime.Today;
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
                    DateTime d = DateTime.Today;
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
                    DateTime d = DateTime.Today;
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
                    DateTime d = DateTime.Today;
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
                    DateTime d = DateTime.Today;
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
            var date = DateTime.Today;

            var sample = db.ConcreteSample1.Where(s => s.SampleNumber == model.SampleNumber).FirstOrDefault();
            sample.AdditionAmount = model.AdditionAmount;
            sample.AdditionType = model.AdditionType;
            sample.CementSource = model.CementSource;
            sample.CementType = model.CementType;
            sample.CementWeight = model.CementWeight;
            sample.CleanDocNote = model.CleanDocNote;
            sample.CleanDocPath = model.CleanDocPath;
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
            sample.ReportDate = date;
            sample.ReportNo = model.ReportNo;
            sample.Rubble3by4 = model.Rubble3by4;
            sample.Rubble3by8 = model.Rubble3by8;
            sample.SafteyDocNote = model.SafteyDocNote;
            sample.SafteyDocPath = model.SafteyDocPath;
            sample.SampledBy = model.SampledBy;
            sample.SampleNumber = model.SampleNumber;
            sample.SummerDocNote = model.SummerDocNote;
            sample.SummerDocPath = model.SummerDocPath ;
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
            
            db.SaveChanges();

            return View(model);
        }

        [HttpGet]
        public ActionResult CancleFactoryReport(string id)
        {
            int factid = int.Parse(id);
            ViewBag.factid = factid;
            VisitDetail visitDetail = db.VisitDetails.Where(v => v.FactoryId == factid && DbFunctions.TruncateTime(v.VisitDate) == DbFunctions.TruncateTime(DateTime.Today)).FirstOrDefault();
            return View(visitDetail);
        }

        [HttpPost]
        public ActionResult CancleFactoryReport(string id , string userid)
        {
            VisitDetail visit = new VisitDetail();
            visit.FactoryId = int.Parse(id);
            VisitService visitService = new VisitService();
            var result = visitService.CancleVisit(visit);
            if (result == true)
                return RedirectToAction("Home", "Home", new { id = 1 });
            else
                return RedirectToAction("ConcreteFactoryReports", "ConcreteFactoryReports", new { id = visit.FactoryId });
        }
 
        public ActionResult UpdateConcreteReport(int id)
        {
            ObjectParameter statusCode = new ObjectParameter("StatusCode", typeof(int));
            ObjectParameter statusMessage = new ObjectParameter("StatusMessage", typeof(string));

            var userId = Convert.ToInt32(Session["UserId"].ToString());

            var factoryName = db.Factory11.Where(f => f.Id == id).Select(x => x.Name).FirstOrDefault();

            var sampleNumber = from s in db.ConcreteSample1
                                 join v in db.VisitDetails on s.SampledBy equals v.MonitorId
                                 where v.MonitorId == userId && DbFunctions.DiffDays(v.VisitDate, DateTime.Today) == 1 && v.FactoryId==id && s.FactoryName== factoryName
                               select new List<long?> { s.SampleNumber};
            var lst = sampleNumber.ToList();

            var receivedSample = lst.Count()>0?db.UpdateConcreteReport(sampleNumber.FirstOrDefault().FirstOrDefault(), statusCode, statusMessage):0;

            return RedirectToAction("Home", "Home", new
            {
                id = 1
            });
        }


        public FileContentResult Docs()
        {
            // to get the user details to load user Image
            String Username = Session["Username"].ToString();

            var userImage = db.Users.Where(x => x.Username == Username).FirstOrDefault();
            if (userImage.Photo != null)
            {
                return new FileContentResult(userImage.Photo, "image/jpeg");
            }
            else
            {
                string fileName = HttpContext.Server.MapPath(@"~/Images/avatars/UserIcon.jpg");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);

                return File(imageData, "image/jpeg");
            }


        }

        public ActionResult ConcreteSampleReports()
        {
            var samples = db.ConcreteSample1.OrderByDescending(r => r.ReportDate).ToList();
            List<SampleReport> result = new List<SampleReport>();
            SampleReport s;
            if (samples.Count() > 0)
            {
                foreach (var sample in samples)
                {
                    s = new SampleReport
                    {
                        SampleNumber = sample.SampleNumber,
                        ReportedDate = sample.ReportDate,
                        FactoryName = sample.FactoryName,
                        MonitorName = db.Users.Where(u => u.Id == sample.SampledBy).FirstOrDefault().FullName,
                        CustomerName = sample.ClientName
                    };
                    result.Add(s);
                }
            }
            
            return View(result);
        }

        public ActionResult EditConcreteSampleReports(long Id)
        {
            ConcreteSample1 sample = db.ConcreteSample1.Where(s=>s.SampleNumber==Id).FirstOrDefault();
            
            return View(sample);
        }

        [HttpPost]
        public ActionResult UpdateConcreteSample(ConcreteSample1 sample)
        {
            ObjectParameter statusCode = new ObjectParameter("StatusCode", typeof(int));
            ObjectParameter statusMessage = new ObjectParameter("StatusMessage", typeof(string));


            if (Request.Files.Count > 0)
            {
                foreach (var file in Request.Files)
                {
                    byte[] docData = null;
                    HttpPostedFileBase docFile = Request.Files["Doc"];

                    using (var binary = new BinaryReader(docFile.InputStream))
                    {
                        docData = binary.ReadBytes(docFile.ContentLength);
                    }
                }

            }

            db.UpdateConcreteSample(sample.ReportNo, sample.ReportDate, sample.FactoryName, sample.FactoryLocation, sample.MixerNumber, sample.VisitNumber,
                                       sample.SampleNumber, sample.TruckNumber, sample.InvoiceNumber, sample.ClientName, sample.VisitLocation, sample.Latitude,
                                       sample.Longitude, sample.ConcreteRank, sample.ConcreteTemperture, sample.WaterTemperature, sample.WeatherTemperture,
                                       sample.DownAmount, sample.CementType, sample.CementSource, sample.AdditionType, sample.AdditionAmount, sample.CementWeight,
                                       sample.WaterWieght, sample.WashedSandWeight, sample.WhiteSandWeight, sample.Rubble3by4, sample.Rubble3by8, sample.IsCleanUsage,
                                       sample.IsBasicUsage, sample.IsColumnUsage, sample.IsRoofUsage, sample.IsOtherUsage, sample.IsCleanLocation, sample.CleanDocPath,
                                       sample.IsDustControlInStation, sample.DustDocPath, sample.IsRokamSummer, sample.SummerDocPath, sample.IsLabEngineer, sample.LabDocPath,
                                       sample.IsMoldanatInTrucks, sample.TruckDocPath, sample.IsPeopleSafty, sample.SafteyDocPath, 2, null, statusCode, statusMessage
                                         , sample.OtherReason, sample.CleanDocNote, sample.DustDocNote, sample.SummerDocNote, sample.LabDocNote, sample.TruckDocNote, sample.SafteyDocNote);


            return RedirectToAction("ConcreteSampleReports", "ConcreteFactoryReports");
        }
    }
}