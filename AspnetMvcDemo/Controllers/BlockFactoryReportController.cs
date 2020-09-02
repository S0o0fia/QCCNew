using AspnetMvcDemo.Models;
using AspnetMvcDemo.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspnetMvcDemo.Controllers
{
    public class BlockFactoryReportController : Controller
    {
        QCEntities db = new QCEntities();

        public ActionResult Index(int id)
        {
            var factory = db.BlockFactories.Where(f => f.Id == id).FirstOrDefault();
            var ReportNumber =
            db.BlockFactoryReports.Where(s => s.FactoryName.Equals(factory.Name) == true && DbFunctions.TruncateTime(s.ReportDate) == DbFunctions.TruncateTime(DateTime.Now)).Select(s => s.ReportNumber).FirstOrDefault();

            if (ReportNumber == null)
            {
                var samples = db.BlockFactoryReports.OrderByDescending(s => s.ReportNumber).ToList();
                BlockSamplesWithPath sample = new BlockSamplesWithPath();
                var lastSample = samples.Count() == 0 ? null : samples.FirstOrDefault();

                sample.ReportNumber = lastSample == null ? 1 : lastSample.ReportNumber + 1;
                sample.VisitNumber = lastSample == null ? 1 : lastSample.VisitNumber + 1;
                sample.FactoryName = factory.Name;
                sample.FatoryLocation = factory.Location;
                //sample.ClientName = factory.OwnerName;
                return View(sample);
            }
            else
            {
                return RedirectToAction("ShowAddedData", "BlockFactoryReport", new { id = ReportNumber });
            }


        }

        public ActionResult ShowAddedData (string id)
        {
            int Number = int.Parse(id);
            var sample = db.BlockFactoryReports.Where(s => s.ReportNumber == Number).FirstOrDefault();
            return View(sample);
        }

        [HttpPost]
        public ActionResult AddBlockSample(BlockSamplesWithPath model)
        {
            foreach (var file in model.CleanLocation)
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
                    model.EngineerPathDoc = InputFileName + fi.Extension;
                }
            }

            foreach (var file in model.LabExist)
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
                    model.LabExistDocPath = InputFileName + fi.Extension;
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
                    model.MaskSaftyPathDoc = InputFileName + fi.Extension;
                }
            }

            var samples = db.BlockFactoryReports.OrderByDescending(s => s.ReportNumber).ToList();
            var lastSample = samples.Count() == 0 ? null : samples.FirstOrDefault();
            BlockFactoryReport sample = new BlockFactoryReport() ;
            DateTime date = DateTime.Today;
            sample.LabExistNote = model.LabExistNote == null ? "" : model.LabExistNote;
            sample.AdditionalWeight = model.AdditionalWeight;
            sample.CementWeight = model.CementWeight;
            sample.CleanDocPath = model.CleanDocPath == null ? "" :  model.CleanDocPath; 
            sample.CleanNote = model.CleanNote == null ? "" : model.CleanNote;
            sample.Dim = model.Dim;
            sample.EngineerNote = model.EngineerNote == null ? "" : model.EngineerNote;
            sample.EngineerPathDoc = model.EngineerPathDoc == null ? "" : model.EngineerPathDoc;
            sample.FactoryName = model.FactoryName;
            sample.FatoryLocation = model.FatoryLocation;
            sample.IsCleanLocation = model.IsCleanLocation == null ? false : model.IsCleanLocation;
            sample.IsEngineerLab = model.IsEngineerLab == null ? false : model.IsEngineerLab;
            sample.IsLabExist = model.IsLabExist == null ? false : model.IsLabExist;
            sample.IsMaskSafty = model.IsMaskSafty == null ? false : model.IsMaskSafty;
            sample.LabExistDocPath = model.LabExistDocPath == null ? "" : model.LabExistDocPath;
            sample.LabExistNote = model.LabExistNote == null ? "" : model.LabExistNote;
            sample.Latitude = model.Latitude;
            sample.Longitude = model.Longitude;
            sample.MaskSaftyNote = model.MaskSaftyNote;
            sample.MaskSaftyPathDoc = model.MaskSaftyPathDoc;
            sample.ReportDate = date;
            sample.Rubble38 = model.Rubble38;
            sample.ReportNumber = lastSample == null ? 1 : lastSample.ReportNumber + 1;
            sample.SandWeight = model.SandWeight;
            sample.ShapeNumber = model.ShapeNumber;
            sample.VisitNumber = lastSample == null ? 1 : lastSample.VisitNumber + 1;
            sample.waterWeight = model.waterWeight;

            
                db.BlockFactoryReports.Add(sample);
                db.SaveChanges();
                TempData["AlertMessage1"] = "Success";
                return RedirectToAction("ShowAddedData", "BlockFactoryReport", new { id = sample.ReportNumber });
            
          
           
        }



        //for Cancel Visit 
        [HttpGet]
        public ActionResult CancleFactoryReport(string id)
        {
            int factid = int.Parse(id);
            BlockVisitDetail visitDetail = new BlockVisitDetail();
             visitDetail = db.BlockVisitDetails.Where(v => v.FactoryId == factid &&  DbFunctions.TruncateTime(v.VisitDate) == DbFunctions.TruncateTime(DateTime.Today)).FirstOrDefault();
            return View(visitDetail);
        }

        [HttpPost]
        public ActionResult CancleFactoryReport(string id , string userid)
        {
            BlockVisitDetail visit = new BlockVisitDetail();
            visit.FactoryId = int.Parse(id);
            VisitService visitService = new VisitService();
            var result = visitService.CancleBlockVisit(visit);
            if (result == true)
                return RedirectToAction("BlockVisit", "Home");
            else
                return RedirectToAction("Index", "BlockFactoryReport", new { id = visit.FactoryId });
        }

    }
}