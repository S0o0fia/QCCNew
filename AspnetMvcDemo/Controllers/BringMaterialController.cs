using AspnetMvcDemo.Models;
using AspnetMvcDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AspnetMvcDemo.Controllers
{
    public class BringMaterialController : Controller
    {
        QCEntities db = new QCEntities();
        // GET: BringMaterial
        public ActionResult Index()
        {
            BringMaterialVM createVisit = new BringMaterialVM();
            createVisit.locations = db.Locations.ToList();
            createVisit.factories = db.Factory11.Where(f => f.Location_Id == 1).ToList();
            return View(createVisit);

        }

        public string GetFactories(int id)
        {
            List<FactoryTemp> TempF = new List<FactoryTemp>();
            if (id > 3)
            {
                id = 3;
            }
            var query = db.Factory11.Where(f => f.Location_Id == id).ToList();

            JavaScriptSerializer jss = new JavaScriptSerializer();
            foreach (var item in query)
            {

                FactoryTemp f = new FactoryTemp();
                f.Id = item.Id;
                f.Name = item.Name;
                TempF.Add(f);

            }
            string output = jss.Serialize(TempF);
            Response.Write(output);
            Response.Flush();
            Response.End();

            return output;
        }
       
        public ActionResult ViewMaterialtoMonitor ()
        {
            return View();
        }

    }
}