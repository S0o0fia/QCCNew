using AspnetMvcDemo.Models;
using AspnetMvcDemo.Services;
using AspnetMvcDemo.ViewModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using RazorPDF;
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
    public class PledgeController : Controller
    {
        PledgeVM PledgeVm = new PledgeVM();
        QCEntities db = new QCEntities();
        // GET: Pledge
        public ActionResult Index()
        {
            PledgeM p = new PledgeM();
            p.locations = db.Locations.ToList();
            p.Factories = new List<Factory11>();

            return View(p);
        }
        [HttpPost]
        public ActionResult Index(PledgeM rev)
        {
            Pledge review = new Pledge();
            review.FactoryID = rev.FactoryId;
            review.LocationID = rev.LocationId;
            review.Date = DateTime.Now;
            db.Pledges.Add(review);
            db.SaveChanges();
            TempData["AlertMessage"] = "Done";
            return RedirectToAction("Index", "Pledge");
        }

        public string GetFactories(int id)
        {
            List<FactoryTemp> TempF = new List<FactoryTemp>();
            var alert = db.Alerts.ToList();
            List<Alert> Temp = new List<Alert>();
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
            List<ViewPledgeM> Temp = new List<ViewPledgeM>();
            var query = db.Pledges.ToList();
            foreach (var item in query)
            {
                if(item.Path == null)
                {
                    ViewPledgeM ViewPledgeM = new ViewPledgeM();
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
        public ActionResult ViewPledge(List<ViewPledgeM> rev)
        {
            foreach (var item in rev)
            {

            
            Pledge p = db.Pledges.Where(pp=>pp.ID == item.ID).FirstOrDefault();


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
                return RedirectToAction("ViewPledge", "Pledge");
            }
            public ActionResult Templete(int id)
        {
            var fac = db.Pledges.Where(p => p.ID == id).Select(f => f.FactoryID).FirstOrDefault();
            var alert = db.Alerts.Where(a => a.FactoryID == fac).ToList();
            Alert alt = new Alert();
            foreach (var item in alert)
            {
                if (item.Date.Value.Month == DateTime.Now.Month)
                    alt = item;
            }

            ViewPledgeAlert al = new ViewPledgeAlert();
            al.Date = alt.Date;
            al.FactoryName = db.Factory11.Where(l => l.Id == alt.FactoryID).Select(ll => ll.Name).FirstOrDefault();
            al.ID = alt.ID;
            return new PartialViewAsPdf("Templete", al)
            {
                FileName = "TestPartialViewAsPdf.pdf"
            };
        }
        //[HttpPost]
        //[ValidateInput(false)]
        //public FileResult Export(string GridHtml)
        //{
        //    using (MemoryStream stream = new System.IO.MemoryStream())
        //    {
        //        StringReader sr = new StringReader(GridHtml);
        //        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
        //        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
        //        pdfDoc.Open();
        //        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                
        //        pdfDoc.Close();
        //        return File(stream.ToArray(), "application/pdf", "Grid.pdf");
        //    }
        //}
    }
}