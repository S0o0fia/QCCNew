using AspnetMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspnetMvcDemo.Controllers
{
    public class FactorySampleBreakController : Controller
    {
        QCEntities context = new QCEntities();


        // GET: FactorySampleBreak
        public ActionResult BreakSample(string  id)
        {
            FactoryBreakSamplePath Sample = new FactoryBreakSamplePath();
            Sample.SampleNumber = int.Parse(id);
            return View(Sample);
        }

       [HttpPost]
        public ActionResult BreakSample(FactoryBreakSamplePath model)
        {
            foreach (var file in model.cube1img1File)
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
                    model.Cube1Img1 = InputFileName + fi.Extension;
                }
            }

            foreach (var file in model.cube1img2File)
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
                    model.Cube1Img2 = InputFileName + fi.Extension;
                }
            }

            foreach (var file in model.cube2img1File)
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
                    model.Cube2Img1 = InputFileName + fi.Extension;
                }
            }

            foreach (var file in model.cube2img2File)
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
                    model.Cube2Img2 = InputFileName + fi.Extension;
                }
            }

            foreach (var file in model.cube3img1File)
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
                    model.Cube2Img1 = InputFileName + fi.Extension;
                }
            }

            foreach (var file in model.cube3img2File)
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
                    model.Cube2Img2 = InputFileName + fi.Extension;
                }
            }

            BreakSampleFactory breakSampleFactory = new BreakSampleFactory();

            breakSampleFactory.Cube1Img1 = model.Cube1Img1;
            breakSampleFactory.Cube1Img2 = model.Cube1Img2;
            breakSampleFactory.Cube2Img1 = model.Cube2Img1;
            breakSampleFactory.Cube2Img2 = model.Cube2Img2;
            breakSampleFactory.Cube3Img1 = model.Cube3Img1;
            breakSampleFactory.Cube3Img2 = model.Cube3Img2;
            breakSampleFactory.Date = DateTime.Today;
            breakSampleFactory.SampleNumber = model.SampleNumber;

            context.BreakSampleFactories.Add(breakSampleFactory);
            context.SaveChanges();
            return RedirectToAction("7maSsa");
        }
    }
}