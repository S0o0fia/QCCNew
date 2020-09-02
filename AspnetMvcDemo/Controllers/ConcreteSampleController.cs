using AspnetMvcDemo.Models;
using AspnetMvcDemo.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspnetMvcDemo.Controllers
{
    public class ConcreteSampleController : Controller
    {
        // GET: ConcreteSample
        QCEntities context = new QCEntities();
        concreteSampleSer service = new concreteSampleSer();
        public ActionResult ConcreteSample(int id)
        {

            var Concrete = service.concreteSample(id);
            return View(Concrete);
        }

        [HttpPost]
        public ActionResult ConcreteSample(CubeSamplesWithPath sample)
        {
            foreach (var file in sample.Cube1P1Path)
            {
                if (file != null)
                {
                    Random random = new Random();
                    FileInfo fi = new FileInfo(file.FileName);
                    DateTime d = DateTime.Now;
                    var InputFileName = d.Ticks+ random.Next(100, 100000000).ToString();
                    var ServerSavePath = System.IO.Path.Combine(Server.MapPath("~/Content"), InputFileName + fi.Extension);
                    //Save file to server folder  
                    file.SaveAs(ServerSavePath);
                    sample.Cube1P1 = InputFileName + fi.Extension;
                }
            }
            foreach (var file in sample.Cube1P2Path)
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
                    sample.Cube1P2 = InputFileName + fi.Extension;
                }
            }
            foreach (var file in sample.Cube2P1Path)
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
                    sample.Cube2P1 = InputFileName + fi.Extension;
                }
            }
            foreach (var file in sample.Cube2P2Path)
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
                    sample.Cube2P2 = InputFileName + fi.Extension;
                }
            }
            foreach (var file in sample.Cube3P1Path)
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
                    sample.Cube3P1 = InputFileName + fi.Extension;
                }
            }
            foreach (var file in sample.Cube3P2Path)
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
                    sample.Cube3P2 = InputFileName + fi.Extension;
                }
            }
            SampleCubePath s = new SampleCubePath()
            {
                SampleNumber = sample.SampleNumber,
                
                Cube1P1 = sample.Cube1P1,
                Cube1P2 = sample.Cube1P2,
                Cube2P1 = sample.Cube2P1,
                Cube2P2 = sample.Cube2P2,
                Cube3P1 = sample.Cube3P1,
                Cube3P2 = sample.Cube3P2
            };
            context.SampleCubePaths.Add(s);
            context.SaveChanges();
            return RedirectToAction( "BrokenSample", "Home", new
            {
                id = 1
            });
        }
    }
}