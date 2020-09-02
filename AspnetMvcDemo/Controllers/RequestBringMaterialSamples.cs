using AspnetMvcDemo.Models;
using AspnetMvcDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace AspnetMvcDemo.Controllers
{
    public class RequestBringMaterialSamples : Controller
    {
        QCEntities db = new QCEntities();

        [HttpGet]
        public ActionResult SelectMaterialSample()
        {
            //Session["Choice"] = id == 1 ? "Concrete" : "Block";
            MaterialSample materialSample = new MaterialSample();

                foreach (var loc in db.Locations)
                {
                    materialSample.factories.Add(new SelectListItem { Text = loc.Location_Arabic, Value = loc.Id.ToString() });
                }
            return View(materialSample);

        }

        [HttpPost]
        public ActionResult SelectMaterialSample(int? locationId)
        {
            MaterialSample materialSample = new MaterialSample();
            foreach (var loc in db.Locations)
            {
                materialSample.locations.Add(new SelectListItem { Text = loc.Location_Arabic, Value = loc.Id.ToString() });
            }
            if(locationId.HasValue)
            {
                var factories = (from fac in db.Factory11
                                 where (fac.Location_Id == locationId.Value)
                                 select fac).ToList();
                foreach (var fact in db.Factory11)
                {
                    materialSample.factories.Add(new SelectListItem { Text = fact.Name, Value = fact.Id.ToString() });
                }
            }
            return View(materialSample);
        }


    }
}