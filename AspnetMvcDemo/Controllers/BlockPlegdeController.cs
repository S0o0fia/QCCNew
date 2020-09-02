using AspnetMvcDemo.Models;
using AspnetMvcDemo.Services;
using AspnetMvcDemo.ViewModel;
using Rotativa;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AspnetMvcDemo.Controllers
{
    public class BlockPlegdeController : Controller
    {
        BlockPledgeVM PledgeVm = new BlockPledgeVM();
        QCEntities db = new QCEntities();
        // GET: Pledge
        public ActionResult Index()
        {
            BlockPledgeM p = new BlockPledgeM();
            p.locations = db.Locations.ToList();
            p.Factories = new List<BlockFactory>();

            return View(p);
        }
        [HttpPost]
        public ActionResult Index(BlockPledgeM rev)
        {
            BlockPledge review = new BlockPledge();
            review.FactoryID = rev.FactoryId;
            review.LocationID = rev.LocationId;
            review.Date = DateTime.Now;
            db.BlockPledges.Add(review);
            db.SaveChanges();
            TempData["AlertMessage"] = "Done";
            return RedirectToAction("Index", "BlockPledge");
        }

        public string GetFactories(int id)
        {
            List<FactoryTemp> TempF = new List<FactoryTemp>();
            var alert = db.BlockAlerts.ToList();
            List<BlockAlert> Temp = new List<BlockAlert>();
            var query = db.Factory11.Where(f => f.Location_Id == id).ToList();
            foreach (var item in alert)
            {
                if (item.Date.Value.Month == DateTime.Now.Month)

                {

                    Temp.Add(item);

                }
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            foreach (var item in query)
            {
                var ISFound = Temp.Where(f => f.FactoryID == item.Id).FirstOrDefault();
                if (ISFound != null)
                {
                    FactoryTemp f = new FactoryTemp();
                    f.Id = item.Id;
                    f.Name = item.Name;
                    TempF.Add(f);
                }
            }
            string output = jss.Serialize(TempF);
            Response.Write(output);
            Response.Flush();
            Response.End();

            return output;
        }
        public ActionResult ViewPledge()
        {
            List<BlockViewPledgeM> Temp = new List<BlockViewPledgeM>();
            var query = db.Pledges.ToList();
            foreach (var item in query)
            {
                if (item.Path == null)
                {
                    BlockViewPledgeM ViewPledgeM = new BlockViewPledgeM();
                    ViewPledgeM.ID = item.ID;
                    ViewPledgeM.Date = item.Date;
                    ViewPledgeM.Location = db.Locations.Where(l => l.Id == item.LocationID).Select(ll => ll.Location_Arabic).FirstOrDefault();
                    ViewPledgeM.FactoryName = db.Factory11.Where(l => l.Id == item.FactoryID).Select(ll => ll.Name).FirstOrDefault();
                    Temp.Add(ViewPledgeM);

                }
            }
            return View(Temp);

        }
        [HttpPost]
        public ActionResult ViewPledge(List<BlockViewPledgeM> rev)
        {
            foreach (var item in rev)
            {


                BlockPledge p = db.BlockPledges.Where(pp => pp.ID == item.ID).FirstOrDefault();


                foreach (var file in item.files)
                {
                    if (file != null)
                    {
                        Random random = new Random();
                        FileInfo fi = new FileInfo(file.FileName);
                        DateTime d = DateTime.Now;
                        var InputFileName = d.Ticks + random.Next(100, 100000000).ToString();
                        var ServerSavePath = System.IO.Path.Combine(Server.MapPath("~/Content"), InputFileName + fi.Extension);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);

                        p.Path = InputFileName + fi.Extension;
                        db.SaveChanges();

                    }
                }
            }
            return RedirectToAction("ViewPledge", "BlockPledge");
        }
        public ActionResult Templete(int id)
        {
            var fac = db.BlockPledges.Where(p => p.ID == id).Select(f => f.FactoryID).FirstOrDefault();
            var alert = db.BlockAlerts.Where(a => a.FactoryID == fac).ToList();
            BlockAlert alt = new BlockAlert();
            foreach (var item in alert)
            {
                if (item.Date.Value.Month == DateTime.Now.Month)
                    alt = item;
            }

            BlockViewPledgeAlert al = new BlockViewPledgeAlert();
            al.Date = alt.Date;
            al.FactoryName = db.BlockFactories.Where(l => l.Id == alt.FactoryID).Select(ll => ll.Name).FirstOrDefault();
            al.ID = alt.id;
            return new PartialViewAsPdf("Templete", al)
            {
                FileName = "تعهد مصنع" + al.FactoryName + DateTime.Today.ToShortDateString() + ".pdf"
            };
        }
    }
}