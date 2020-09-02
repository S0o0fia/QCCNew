using AspnetMvcDemo.Models;
using AspnetMvcDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace AspnetMvcDemo.Controllers
{
    public class EvaluationFactoryController : Controller
    {
        QCEntities db = new QCEntities();

        public ActionResult Reports(int Id)
        {
            // the general info about the factory
            var factoryy = db.Factory11.FirstOrDefault(p => p.Id == Id);
            if(factoryy != null)
            {
                FactoryGeneralInfoVM factoryGeneralInfovm = new FactoryGeneralInfoVM();
                var factory = db.Factory11.FirstOrDefault(p => p.Id == Id);
                factoryGeneralInfovm.FactoryId = factory.Id;
                factoryGeneralInfovm.DailyProductionRate = factory.DailyProductionRate;
                factoryGeneralInfovm.FactoryFaxNumber = factory.FaxNumber;
                factoryGeneralInfovm.FactoryName = factory.Name;
                factoryGeneralInfovm.FactoryPhoneNumber = factory.PhoneNumber;
                factoryGeneralInfovm.Location = db.Locations.FirstOrDefault(p => p.Id == factory.Location_Id).Location_Arabic;
                factoryGeneralInfovm.ManagerName = factory.ManagerName;
                factoryGeneralInfovm.ManagerQualification = factory.ManagerExperience;
                factoryGeneralInfovm.OwnerName = factory.OwnerName;
                factoryGeneralInfovm.ProductionCapacity = factory.ProductionCapacity;
                ViewBag.GeneralInfo = factoryGeneralInfovm;

                var factoryEvaluationReports = db.FactoryEvaluationReports.Where(p=>p.FactoryId == factoryy.Id).Include(f => f.Factory).ToList();
                return View(factoryEvaluationReports);
            }
            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FactoryEvaluationReport factoryEvaluationReport = db.FactoryEvaluationReports.Find(id);
            if (factoryEvaluationReport == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListOfQualifications = db.EquipmentRequirementsTypes.ToList();
            return View(factoryEvaluationReport);
        }
        public ActionResult ReportResult(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FactoryEvaluationReport factoryEvaluationReport = db.FactoryEvaluationReports.Find(id);
            if (factoryEvaluationReport == null)
            {
                return HttpNotFound();
            }
            ReportResultVM vm = new ReportResultVM() { ReportId = factoryEvaluationReport.ID } ;
            return View(vm);
        }
        [HttpPost]
        public ActionResult ReportResult(ReportResultVM vm)
        {
            try
            {
                if (vm.ReportId == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                FactoryEvaluationReport factoryEvaluationReport = db.FactoryEvaluationReports.Find(vm.ReportId);
                if (factoryEvaluationReport == null)
                {
                    return HttpNotFound();
                }
                db.ReportResults.Add(new ReportResult() { Comment = vm.Comment, Status = (int)vm.Status == 1, FactoryId = factoryEvaluationReport.FactoryId, FactoryReportId = vm.ReportId });
                var factory = db.Factory11.FirstOrDefault(p => p.Id == factoryEvaluationReport.FactoryId);
                if ((int)vm.Status != 1)
                {
                    factory.Status = "Closed";
                    factory.IsActive = false;
                }
                else
                {
                    factory.Status = "Opened";
                    factory.IsActive = true;
                }
                db.SaveChanges();
                return RedirectToAction("Home", "Home", new { Id = 1 });
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public ActionResult Create(int Id)
        {
            // the general info about the factory
            FactoryGeneralInfoVM factoryGeneralInfovm = new FactoryGeneralInfoVM();
            var factory = db.Factory11.FirstOrDefault(p => p.Id == Id);
            factoryGeneralInfovm.FactoryId = factory.Id;
            factoryGeneralInfovm.DailyProductionRate = factory.DailyProductionRate;
            factoryGeneralInfovm.FactoryFaxNumber = factory.FaxNumber;
            factoryGeneralInfovm.FactoryName = factory.Name;
            factoryGeneralInfovm.FactoryPhoneNumber = factory.PhoneNumber;
            factoryGeneralInfovm.Location = db.Locations.FirstOrDefault(p=>p.Id == factory.Location_Id).Location_Arabic;
            factoryGeneralInfovm.ManagerName = factory.ManagerName;
            factoryGeneralInfovm.ManagerQualification = factory.ManagerExperience;
            factoryGeneralInfovm.OwnerName = factory.OwnerName;
            factoryGeneralInfovm.ProductionCapacity = factory.ProductionCapacity;

            ViewBag.ListOfQualifications = db.EquipmentRequirementsTypes.ToList();

            ViewBag.GeneralInfo = factoryGeneralInfovm;
            return View();
        }
        [HttpPost]
        public ActionResult Create(EvaluationFactoryVM vm)
        {
            try
            {
                var newReport = new FactoryEvaluationReport { CreationDate = DateTime.Now, FactoryId = vm.FactoryId };
                db.FactoryEvaluationReports.Add(newReport);
                db.SaveChanges();
                foreach (var item in vm.option)
                {
                    if (item.QuestionId != null && item.Answer != null)
                        db.CheckEquipmentRequirementsOfFactories.Add(new CheckEquipmentRequirementsOfFactory { FactoryEvaluationReportId = newReport.ID ,  CheckEquipmentRequirementsId = int.Parse(item.QuestionId), InputValue = item.Answer });
                }
                db.SaveChanges();
                return RedirectToAction("Home","Home",new { Id = 1 });
            }
            catch(Exception ex)
            {
                    return View();
            }
        }
        [ChildActionOnly]
        public PartialViewResult NormalForm(List<EquipmentRequirementsType> ListOfTitles)
        {
            ViewBag.ListOfTitles = ListOfTitles;
            return PartialView("_NormalForm");
        }
        [ChildActionOnly]
        public PartialViewResult NormalSubmittedForm(List<EquipmentRequirementsType> ListOfTitles, List<CheckEquipmentRequirementsOfFactory> ListOfAnswers , int FactoryId)
        {
            ViewBag.ListOfTitles = ListOfTitles;
            ViewBag.ReportAnswersOfFactories = ListOfAnswers;
            var stationsOfFactory = db.MixingStationsAnswersOfFactories.Where(p => p.FactoryId == FactoryId).GroupBy(g => g.StationNumber);
            ViewBag.MixingStations = stationsOfFactory.Count();
            ViewBag.FactoryId = FactoryId;

            return PartialView("_NormalSubmittedForm");
        }
        public PartialViewResult AddMixiningStation(int FactoryId)
        {
            ViewBag.ListMixStationTitls = db.MixingStationsTypes.ToList();
            ViewBag.ListMixStationOptions = db.MixingStationsQuestions.ToList();
            MixingStationAnswersVM vm = new MixingStationAnswersVM() { FactoryId = FactoryId };
            return PartialView("_NewMixiningStation", vm);
        }
        public PartialViewResult ShowMixiningStation(int FactoryId, int StationNumber)
        {
            ViewBag.ListMixStationTitls = db.MixingStationsTypes.ToList();
            ViewBag.ListMixStationOptions = db.MixingStationsQuestions.ToList();
            ViewBag.MixingStationsAnswersOfFactories = db.MixingStationsAnswersOfFactories.Where(p => p.FactoryId == FactoryId).GroupBy(p => p.StationNumber).OrderBy(g => g.Key).Skip(StationNumber).FirstOrDefault().ToList();
            MixingStationAnswersVM vm = new MixingStationAnswersVM() { FactoryId = FactoryId };
            return PartialView("_SubmitNewMixingStationsAnswers", vm);
        }
        [HttpPost]
        public PartialViewResult SubmitNewMixingStationsAnswers(MixingStationAnswersVM vm)
        {
            int stationCount = default(int);
            try
            {
                var stationsOfFactory = db.MixingStationsAnswersOfFactories.Where(p => p.FactoryId == vm.FactoryId).GroupBy(g => g.StationNumber);
                if (stationsOfFactory != null)
                {
                    stationCount = stationsOfFactory.Count() + 1;
                }
                else
                {
                    stationCount = 1;
                }
            }
            catch
            {
                stationCount = 1;
            }
            foreach (var item in vm.mixstation)
            {
                if (item.QuestionId != null && item.Answer != null)
                    db.MixingStationsAnswersOfFactories.Add(new MixingStationsAnswersOfFactory { FactoryId = vm.FactoryId, MixingStationsQuestionsId = int.Parse(item.QuestionId), InputValue = item.Answer, StationNumber = stationCount });
            }
            db.SaveChanges();
            ViewBag.ListMixStationTitls = db.MixingStationsTypes.ToList();
            ViewBag.ListMixStationOptions = db.MixingStationsQuestions.ToList();
            ViewBag.MixingStationsAnswersOfFactories = db.MixingStationsAnswersOfFactories.Where(p => p.FactoryId == vm.FactoryId).GroupBy(p => p.StationNumber).OrderByDescending(g => g.Key).FirstOrDefault().ToList();
            return PartialView("_SubmitNewMixingStationsAnswers");
        }
    }
}
