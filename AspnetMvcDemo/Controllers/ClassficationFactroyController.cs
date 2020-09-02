using AspnetMvcDemo.Models;
using AspnetMvcDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspnetMvcDemo.Controllers
{
    public class ClassficationFactroyController : Controller
    {
        QCEntities db = new QCEntities();
       

        // GET: ClassficationFactroy
        public ActionResult Index(string targetControllerName, string targetActionName)
        {

            ViewBag.Locations = db.Locations.ToList();
            ChooseFactoryVM factoryvm = new ChooseFactoryVM();
            factoryvm.TargetControllerName = targetControllerName;
            factoryvm.TargetActionName = targetActionName;
            return View(factoryvm);
           
        }

        [HttpPost]
        public ActionResult Index(ChooseFactoryVM chooseFactoryVM)
        {

            try
            {
                var factory = db.Factory11.FirstOrDefault(p => p.Id == chooseFactoryVM.FactoryId);
                if (factory != null)
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

        public PartialViewResult GetFactories(int Id)
        {
            var factories = db.Factory11.Where(p => p.Location_Id == Id).ToList();
            ViewBag.Factories = factories.Where(p => p.IsActive == true).ToList();
            return PartialView("_GetFactories");
        }


        public ActionResult classficationvisit()
        {
            return View();
        }


    }
}