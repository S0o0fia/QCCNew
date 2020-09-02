using AspnetMvcDemo.Enums;
using AspnetMvcDemo.Models;
using AspnetMvcDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AspnetMvcDemo.Controllers
{
    public class ChooseFactoryController : Controller
    {
        QCEntities db = new QCEntities();
        // GET: ChooseFactory/Details/5
        public ActionResult Details(string targetControllerName,string targetActionName)
        {
            ViewBag.Locations = db.Locations.ToList();
            ChooseFactoryVM factoryvm = new ChooseFactoryVM();
            factoryvm.TargetControllerName = targetControllerName;
            factoryvm.TargetActionName = targetActionName;
            return View(factoryvm);
        }
        public ActionResult StartFactory()
        {
            ViewBag.Locations = db.Locations.ToList();
            ReportResultVM factoryvm = new ReportResultVM();
            return View(factoryvm);
        }
        [HttpPost]
        public ActionResult StartFactory(ReportResultVM vm)
        {
            try
            {
                var factory = db.Factory11.FirstOrDefault(p => p.Id == vm.FactoryId);
                db.OpenCloseFactories.Add(new OpenCloseFactory() { Comment = vm.Comment, FactoryId = factory.Id, Opened = true });
                factory.Status = "Opened";
                factory.IsActive = true;
                db.SaveChanges();
                return RedirectToAction("Home", "Home", new { Id = 1 });
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public ActionResult CloseFactory()
        {
            ViewBag.Locations = db.Locations.ToList();
            ReportResultVM factoryvm = new ReportResultVM();
            return View(factoryvm);
        }
        [HttpPost]
        public ActionResult CloseFactory(ReportResultVM vm)
        {
            try
            {
                var factory = db.Factory11.FirstOrDefault(p => p.Id == vm.FactoryId);
                db.OpenCloseFactories.Add(new OpenCloseFactory() { Comment = vm.Comment, FactoryId = factory.Id, Opened = false });
                factory.Status = "Closed";
                factory.IsActive = false;
                db.SaveChanges();
                return RedirectToAction("Home", "Home", new { Id = 1 });
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public ActionResult CreateTechnicalReport(string targetControllerName, string targetActionName)
        {
            ViewBag.Locations = db.Locations.ToList();
            ChooseFactoryVM factoryvm = new ChooseFactoryVM();
            factoryvm.TargetControllerName = targetControllerName;
            factoryvm.TargetActionName = targetActionName;
            return View(factoryvm);
        }
        public ActionResult ShowTechnicalReports(string targetControllerName, string targetActionName)
        {
            ViewBag.Locations = db.Locations.ToList();
            ChooseFactoryVM factoryvm = new ChooseFactoryVM();
            factoryvm.TargetControllerName = targetControllerName;
            factoryvm.TargetActionName = targetActionName;
            return View(factoryvm);
        }
        [HttpPost]
        public ActionResult Details(ChooseFactoryVM chooseFactoryVM)
        {
            try
            {
                var factory = db.Factory11.FirstOrDefault(p => p.Id == chooseFactoryVM.FactoryId);
                if(factory != null)
                {
                    return RedirectToAction(chooseFactoryVM.TargetActionName, chooseFactoryVM.TargetControllerName, new { Id = factory.Id });
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
        public PartialViewResult GetFactories(int Id,bool? Opened)
        {
            var factories = db.Factory11.Where(p => p.Location_Id == Id).ToList();
            ViewBag.Factories = Opened != null ? factories.Where(p => p.IsActive == Opened).ToList() : factories;
            return PartialView("_GetFactories");
        }
    }
}
