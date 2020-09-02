using AspnetMvcDemo.Models;
using AspnetMvcDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspnetMvcDemo.Controllers
{
    public class BringMaterialController : Controller
    {
        QCEntities db = new QCEntities();
        // GET: BringMaterial
        public ActionResult Index(string targetControllerName, string targetActionName)
        {

            ViewBag.Locations = db.Locations.ToList();
            ChooseFactoryVM factoryvm = new ChooseFactoryVM();
            factoryvm.TargetControllerName = targetControllerName;
            factoryvm.TargetActionName = targetActionName;
            return View(factoryvm);

        }
        public PartialViewResult GetFactories(int Id)
        {
            var factories = db.Factory11.Where(p => p.Location_Id == Id).ToList();
            ViewBag.Factories = factories.Where(p => p.IsActive == true).ToList();
            return PartialView("_GetFactories");
        }

        public ActionResult ViewMaterialtoMonitor ()
        {
            return View();
        }

    }
}