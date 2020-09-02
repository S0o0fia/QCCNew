using AspnetMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspnetMvcDemo.Controllers
{
    public class LaboratoryController : Controller
    {
        QCEntities db = new QCEntities();

        // GET: Laboratory
        public ActionResult Laboratory(int id)
        {
            Session["Choice"] = id == 1 ? "Concrete" : "Block";

            LaboratoryModel model = new LaboratoryModel();

            model.TodaySamples = db.ConcreteSample1.Where(s => s.IsReceived == true && DbFunctions.DiffDays(s.ReportDate, DateTime.Today) == 1 && s.labdeliver == false).ToList();
            return View(model);
        }


        [HttpPost]
        public ActionResult Laboratory(string id)
        {
            int number = int.Parse(id);
            var updated = db.ConcreteSample1.Where(s => s.SampleNumber == number).FirstOrDefault();
            updated.labdeliver = true;
            db.SaveChanges();
            LaboratoryModel model = new LaboratoryModel();
            model.TodaySamples = db.ConcreteSample1.Where(s => s.IsReceived == true && DbFunctions.DiffDays(s.ReportDate, DateTime.Today) == 1 && s.labdeliver == false).ToList();
            return RedirectToAction("Laboratory", new {id = 1 });

        }
      
        //Recieve Block Sample 
        public ActionResult BlockLaboratory()
        {
            BlockLab model = new BlockLab();

            model.TodaySamples = db.BlockFactoryReports.Where(s => s.isRecieved == true ).ToList();

            return View(model);

        }

        [HttpPost]
        public ActionResult BlockLaboratory(string id)

        {
            int number = int.Parse(id);
            var updated = db.BlockFactoryReports.Where(s => s.SampleNumber == number).FirstOrDefault();
            updated.isRecieved = true;
            db.SaveChanges();

            BlockLab model = new BlockLab();
      
            return RedirectToAction("BlockLaboratory");
            
        }
            //Get The 7 Days

            public ActionResult SevenDaysTest(int id)
        {
            Session["Choice"] = id == 1 ? "Concrete" : "Block";

            LaboratoryModel model = new LaboratoryModel();
            model.SevenDaysSamples = db.ConcreteSample1.Where(s => s.labdeliver == true && DbFunctions.DiffDays(s.ReportDate, DateTime.Today) == 7).ToList();


            return View(model);

        }


        public ActionResult MonthTest(int id)
        {
            Session["Choice"] = id == 1 ? "Concrete" : "Block";

            LaboratoryModel model = new LaboratoryModel();
            Nullable<int> month = 28;
            model.TestedSamples = db.ConcreteSample1.Where(s => s.IsReceived == true && DbFunctions.DiffDays(s.ReportDate, DateTime.Today) == month ).ToList();
            return View(model);

        }


        public ActionResult BlockTest ()
        {
            BlockLab model = new BlockLab();
            model.TodaySamples = db.BlockFactoryReports.Where(s => s.isRecieved == true && s.isTested != true ).ToList();
            return View(model);
        }

    }
}