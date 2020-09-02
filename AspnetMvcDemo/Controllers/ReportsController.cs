using AspnetMvcDemo.Models;
using AspnetMvcDemo.Services;
using AspnetMvcDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
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
            var koloYaWaled = context.ConcreteSample1.Where(c => c.FactoryName == factoryname && DbFunctions.TruncateTime(c.ReportDate) == visitDate) .FirstOrDefault();
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
            List<VisitDetailsForReports> visitDetailsForReports = new List<VisitDetailsForReports>();
            visitDetailsForReports = visitService.getDailyVisitsReport();
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
    }
}