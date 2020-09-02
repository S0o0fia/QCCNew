using AspnetMvcDemo.Models;
using AspnetMvcDemo.Services;
using AspnetMvcDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspnetMvcDemo.Controllers
{
    public class BlockReportController : Controller
    {
        QCEntities context = new QCEntities();
        // GET: BlockReport
        // get: Daily Visits with Report For Admin
        public ActionResult dailyVisitsReport()
        {
            VisitService visitService = new VisitService();
            List<VisitDetailsForReports> visitDetailsForReports = new List<VisitDetailsForReports>();
            visitDetailsForReports = visitService.getDailyBlockVisitsReport();
            return View(visitDetailsForReports);
        }


        // GET: Details Of Daily Visits With Report For Admin
        public ActionResult ReportShowDailyVisits(string factoryname, DateTime visitDate)
        {
            var koloYaWaled = context.BlockFactoryReports.Where(c => c.FactoryName == factoryname && DbFunctions.TruncateTime(c.ReportDate) == visitDate).FirstOrDefault();
           
            if (koloYaWaled.IsCleanLocation == null) koloYaWaled.IsCleanLocation = false;
            if (koloYaWaled.IsEngineerLab == null) koloYaWaled.IsEngineerLab = false;
            if (koloYaWaled.IsLabExist == null) koloYaWaled.IsLabExist = false;
            if (koloYaWaled.IsMaskSafty == null) koloYaWaled.IsMaskSafty = false;
            return View(koloYaWaled);
        }

    }
}