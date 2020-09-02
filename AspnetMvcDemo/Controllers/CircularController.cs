using AspnetMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspnetMvcDemo.Controllers
{
    public class CircularController : Controller
    {
        QCEntities context = new QCEntities();

        // GET: Circular
        public ActionResult attachCircular()
        { 
           var q = context.Factory11.Select(x=>new SelectListItem
           {
                Text =x.Name,
                Value = x.Id.ToString()
           }).ToList();
            CircularsPath circulars = new CircularsPath();
            circulars.facts = q;
            return View(circulars);
        }

        [HttpPost]
        public ActionResult attachCircular(CircularsPath circularsPath)
        {
            foreach (var file in circularsPath.circularFileName)
            {
                if (file != null)
                {
                    Random random = new Random();
                    FileInfo fi = new FileInfo(file.FileName);
                    DateTime d = DateTime.Now;
                    var InputFileName = fi.Name;
                    var ServerSavePath = System.IO.Path.Combine(Server.MapPath("~/Content"), InputFileName + fi.Extension);
                    //Save file to server folder  
                    file.SaveAs(ServerSavePath);
                    circularsPath.fileName = InputFileName;
                }
            }

            Circular circular = new Circular()
            {
                fileName = circularsPath.fileName,
                fileDate = DateTime.Today,
                factoryId = int.Parse(circularsPath.factId),
                download = false
            };
            context.Circulars.Add(circular);
            context.SaveChanges();
            return RedirectToAction("Home", "Home", new { id = 1 });
        }

        public ActionResult showCricularFiles()
        {
            String Username = Session["Username"].ToString();
            var user = context.Users.Where(x => x.Username == Username).Select(s => s.FullName).FirstOrDefault();
            var fact = context.Factory11.Where(f => f.Name == user).Select(fa => fa.Id).FirstOrDefault();
            var show = context.Circulars.Where(c => c.factoryId == fact).FirstOrDefault();
           
            return View(show);
        }

        [HttpPost]
        public ActionResult showCricularFiles(Circular circular)
        {
            var cir = context.Circulars.Where(c => c.id == circular.id).FirstOrDefault();
            cir.download = true;
            context.SaveChanges();
            return RedirectToAction( "Home", "Home", new { id = 1});
        }

        public ActionResult displayAllCircular()
        {
            List<allCircularsDisplay> circularsDisplays = new List<allCircularsDisplay>();
            
            circularsDisplays = (from f in context.Factory11
                     join c in context.Circulars
                     on f.Id equals c.factoryId
                     select new allCircularsDisplay
                     {
                         fileName = c.fileName,
                         factoryName = f.Name,
                         location = f.Location,
                         date = c.fileDate,
                         downloadState = c.download
                     }).ToList();
            return View(circularsDisplays);
        }
    }
}