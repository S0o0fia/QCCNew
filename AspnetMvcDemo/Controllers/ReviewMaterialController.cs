using AspnetMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
namespace AspnetMvcDemo.Controllers
{
    public class ReviewMaterialController : Controller
    {
        QCEntities db = new QCEntities();

        // GET: ReviewMaterial
        public ActionResult Index()
        {
            REviewMaterials rev = new REviewMaterials();
            rev.Locations = db.Locations.ToList();
            rev.factories = new List<Factory11>();
            return View(rev);
        }
        [HttpPost]
        public ActionResult Index(REviewMaterials rev)
        {
            ReviewMaterial review = new ReviewMaterial();
            review.FactoryID = rev.FactoryID;
            review.Date = rev.Date;
            review.LocationID = rev.LocationID;
            review.Message = rev.Message;
            db.ReviewMaterials.Add(review);
            db.SaveChanges();
            TempData["AlertMessage"] = "Done";
            return RedirectToAction("Index","ReviewMaterial");
        }
    
        public string GetFactories(int id)
        {
            List<Factory11> ff = new List<Factory11>();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var query = db.Factory11.Where(f => f.Location_Id == id).ToList();
            foreach (var item in query)
            {

                var alert = db.Alerts.Where(a => a.FactoryID == item.Id).FirstOrDefault();
                if (alert != null)
                {
                    item.Alerts = null;
                    item.Comments = null;
                    item.ConcreteMixingDesigns = null;
                    
                    ff.Add(item);
                }
                   
            }
            string output = jss.Serialize(ff);
            Response.Write(output);
            Response.Flush();
            Response.End();

            return output;
        }

    }
}