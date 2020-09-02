using AspnetMvcDemo.Models;
using AspnetMvcDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspnetMvcDemo.Controllers
{
    public class MixingDesignController : Controller
    {
        QCEntities db = new QCEntities();

        ConcreteMixingDesign addDesign = new ConcreteMixingDesign();

        AddMixDesign addMix = new AddMixDesign();

        List<ApproveMixingDesign> approveMix = new List<ApproveMixingDesign>();
        public ActionResult ApproveOrDeclineMixingDesign()
        {
            List<ApproveMixingDesign> result = new List<ApproveMixingDesign>();
            String Username = Session["Username"].ToString();
            var user = db.Users.Where(x => x.Username == Username).FirstOrDefault();
            if (user.JobTitle == "Admin")
            {
                result = (from m in db.ConcreteMixingDesigns
                          join f in db.Factory11 on m.FactoryId equals f.Id
                          select new ApproveMixingDesign
                          {
                              FactoryName = f.Name,
                              Latitude = f.Latitude,
                              Longitude = f.Longitude,
                              OwnerName = f.OwnerName,
                              Id = m.Id,
                              FactoryId = m.FactoryId,
                              ConcreteRank = m.ConcreteRank,
                              CementWeight = m.CementWeight,
                              WaterWeight = m.WaterWeight,
                              WashedSand = m.WashedSand,
                              WhiteSand = m.WhiteSand,
                              Rubble3by4 = m.Rubble3by4,
                              Rubble3by8 = m.Rubble3by8,
                              Status = m.Status,
                              Comments = m.Comments,
                              IsConcrete = true

                          }).Where(f => f.Status == "Waiting").ToList();
            }
            else
            {

                result = (from m in db.ConcreteMixingDesigns
                          join f in db.Factory11 on m.FactoryId equals f.Id
                          select new ApproveMixingDesign
                          {
                              FactoryName = f.Name,
                              Latitude = f.Latitude,
                              Longitude = f.Longitude,
                              OwnerName = f.OwnerName,
                              Id = m.Id,
                              FactoryId = m.FactoryId,
                              ConcreteRank = m.ConcreteRank,
                              CementWeight = m.CementWeight,
                              WaterWeight = m.WaterWeight,
                              WashedSand = m.WashedSand,
                              WhiteSand = m.WhiteSand,
                              Rubble3by4 = m.Rubble3by4,
                              Rubble3by8 = m.Rubble3by8,
                              Status = m.Status,
                              Comments = m.Comments,
                              IsConcrete = true
                          }).Where(f => f.Status == "Waiting" && f.CreatedBy == user.Id).ToList();
            }
            return View(result);

        }
        public ActionResult ApproveDetails(int id)
        {

            var result = (from m in db.ConcreteMixingDesigns
                          join f in db.Factory11 on m.FactoryId equals f.Id
                          select new ApproveMixingDesign
                          {
                              FactoryName = f.Name,
                              Latitude = f.Latitude,
                              Longitude = f.Longitude,
                              OwnerName = f.OwnerName,
                              Id = m.Id,
                              FactoryId = m.FactoryId,
                              ConcreteRank = m.ConcreteRank,
                              CementWeight = m.CementWeight,
                              WaterWeight = m.WaterWeight,
                              WashedSand = m.WashedSand,
                              WhiteSand = m.WhiteSand,
                              Rubble3by4 = m.Rubble3by4,
                              Rubble3by8 = m.Rubble3by8,
                              Cement = m.Cement,
                              Water = m.Water,
                              WashedSand2 = m.WashedSand2,
                              WhiteSand2 = m.WhiteSand2,
                              Rubble3by42 = m.Rubble3by42,
                              Rubble3by82 = m.Rubble3by82,
                              C28Day = m.C28Day,
                              Landing = m.Landing,
                              Status = m.Status,
                              Comments = m.Comments

                          }).Where(x => x.Id == id).FirstOrDefault();
            result.Paths = db.MaterialResultsPdfs.Where(r => r.MixingDesignId == result.Id).Select(p =>  new Models.Path { Filename = p.FileName,PathName=p.PdfPath}).ToList();
        result.ConcreatePaths = db.ConcretePaths.Where(r => r.MixingID == result.Id).Select(p => new Models.Path { Filename = p.FileName, PathName = p.Path }).ToList();
        return View(result);
    }
        // GET: MixingDesign
        public ActionResult MixingDesign(int id)
        {
            Session["Choice"] = id == 1 ? "Concrete" : "Block";
            String Username = Session["Username"].ToString();
            var user = db.Users.Where(x => x.Username == Username).FirstOrDefault();
            if (user.JobTitle == "Admin")
                return View(db.ConcreteMixingDesigns.ToList());
            else
            {
                var allCon = db.ConcreteMixingDesigns.Where(con => con.CreatedBy == user.Id).ToList();
                return View(allCon);


            }
        }
        public ActionResult ApproveMixingDesign()
        {
            var result = (from m in db.ConcreteMixingDesigns
                          join f in db.Factory11 on m.FactoryId equals f.Id
                          select new ApproveMixingDesign
                          {
                              FactoryName = f.Name,
                              Latitude = f.Latitude,
                              Longitude = f.Longitude,
                              OwnerName = f.OwnerName,
                              Id = m.Id,
                              FactoryId = m.FactoryId,
                              ConcreteRank = m.ConcreteRank,
                              CementWeight = m.CementWeight,
                              WaterWeight = m.WaterWeight,
                              WashedSand = m.WashedSand,
                              WhiteSand = m.WhiteSand,
                              Rubble3by4 = m.Rubble3by4,
                              Rubble3by8 = m.Rubble3by8,
                              Status = m.Status,
                              Comments = m.Comments
                          }).ToList();
            return View(result);
        }
        // Get: accept
        public ActionResult AddTestsMixingDesign()
        {
            List< ApproveMixingDesign> result = new List<ApproveMixingDesign>();
            String Username = Session["Username"].ToString();
            var user = db.Users.Where(x => x.Username == Username).FirstOrDefault();
            if (user.JobTitle == "Admin")
            {
                 result = (from m in db.ConcreteMixingDesigns
                              join f in db.Factory11 on m.FactoryId equals f.Id
                              select new ApproveMixingDesign
                              {
                                  FactoryName = f.Name,
                                  Latitude = f.Latitude,
                                  Longitude = f.Longitude,
                                  OwnerName = f.OwnerName,
                                  Id = m.Id,
                                  FactoryId = m.FactoryId,
                                  ConcreteRank = m.ConcreteRank,
                                  CementWeight = m.CementWeight,
                                  WaterWeight = m.WaterWeight,
                                  WashedSand = m.WashedSand,
                                  WhiteSand = m.WhiteSand,
                                  Rubble3by4 = m.Rubble3by4,
                                  Rubble3by8 = m.Rubble3by8,
                                  Status = m.Status,
                                  Comments = m.Comments,
                                  IsConcrete = true

                              }).Where(f => f.Status == "Waiting").ToList();
            }
            else
            {

                 result = (from m in db.ConcreteMixingDesigns
                              join f in db.Factory11 on m.FactoryId equals f.Id
                              select new ApproveMixingDesign
                              {
                                  FactoryName = f.Name,
                                  Latitude = f.Latitude,
                                  Longitude = f.Longitude,
                                  OwnerName = f.OwnerName,
                                  Id = m.Id,
                                  FactoryId = m.FactoryId,
                                  ConcreteRank = m.ConcreteRank,
                                  CementWeight = m.CementWeight,
                                  WaterWeight = m.WaterWeight,
                                  WashedSand = m.WashedSand,
                                  WhiteSand = m.WhiteSand,
                                  Rubble3by4 = m.Rubble3by4,
                                  Rubble3by8 = m.Rubble3by8,
                                  Status = m.Status,
                                  Comments = m.Comments,
                                  IsConcrete = true
                              }).Where(f => f.Status == "Waiting"&&f.CreatedBy == user.Id ).ToList();
            }
            return View(result);
        }       
        public ActionResult TestsDetails(int id)
        {
             var result = db.TestsMixingDesigns.ToList();
            List<TestsPathMixing> testsPaths = new List<TestsPathMixing>();
            foreach (var item in result)
            {
                TestsPathMixing t = new TestsPathMixing()
                {
                    ArName = item.ArName,
                    EnName = item.EnName,
                    ID = item.ID,
                    files = null
                };
                testsPaths.Add(t);
            }
         
            ConcreateTests con = new ConcreateTests()
            {
                MixingId = id,
                Tests = testsPaths,
                 IsConcrete = "SubmitConcreateTests"

            };
                return PartialView(con);
        }

        public ActionResult AddTestsMixingDesign2()
        {
            List<ApproveMixingDesign> result = new List<ApproveMixingDesign>();
            String Username = Session["Username"].ToString();
            var user = db.Users.Where(x => x.Username == Username).FirstOrDefault();
            if (user.JobTitle == "Admin")
            {
                result = (from m in db.ConcreteMixingDesigns
                          join f in db.Factory11 on m.FactoryId equals f.Id
                          select new ApproveMixingDesign
                          {
                              FactoryName = f.Name,
                              Latitude = f.Latitude,
                              Longitude = f.Longitude,
                              OwnerName = f.OwnerName,
                              Id = m.Id,
                              FactoryId = m.FactoryId,
                              ConcreteRank = m.ConcreteRank,
                              CementWeight = m.CementWeight,
                              WaterWeight = m.WaterWeight,
                              WashedSand = m.WashedSand,
                              WhiteSand = m.WhiteSand,
                              Rubble3by4 = m.Rubble3by4,
                              Rubble3by8 = m.Rubble3by8,
                              Status = m.Status,
                              Comments = m.Comments,
                              IsConcrete = false

                          }).Where(f => f.Status == "Waiting").ToList();
            }
            else
            {

                result = (from m in db.ConcreteMixingDesigns
                          join f in db.Factory11 on m.FactoryId equals f.Id
                          select new ApproveMixingDesign
                          {
                              FactoryName = f.Name,
                              Latitude = f.Latitude,
                              Longitude = f.Longitude,
                              OwnerName = f.OwnerName,
                              Id = m.Id,
                              FactoryId = m.FactoryId,
                              ConcreteRank = m.ConcreteRank,
                              CementWeight = m.CementWeight,
                              WaterWeight = m.WaterWeight,
                              WashedSand = m.WashedSand,
                              WhiteSand = m.WhiteSand,
                              Rubble3by4 = m.Rubble3by4,
                              Rubble3by8 = m.Rubble3by8,
                              Status = m.Status,
                              Comments = m.Comments,
                              IsConcrete = false

                          }).Where(f => f.Status == "Waiting" && f.CreatedBy == user.Id).ToList();
            }
            return View("AddTestsMixingDesign", result);
        }
        public ActionResult TestsDetails2(int id)
        {
            var result = db.ConcreteTests.ToList();
            List<TestsPathMixing> testsPaths = new List<TestsPathMixing>();
            foreach (var item in result)
            {
                TestsPathMixing t = new TestsPathMixing()
                {
                    ArName = item.ArName,
                    EnName = item.EnName,
                    ID = item.ID,
                    files = null
                };
                testsPaths.Add(t);
            }

            ConcreateTests con = new ConcreateTests()
            {
                MixingId = id,
                Tests = testsPaths,
                IsConcrete = "SubmitConcreateTests2"

            };
            return PartialView("TestsDetails",con);
        }

        public ActionResult SubmitConcreateTests2(ConcreateTests con)
        {
            foreach (var item in con.Tests)
            {

                foreach (var file in item.files)
                {
                    if (file != null)
                    {
                        Random random = new Random();
                        FileInfo fi = new FileInfo(file.FileName);
                        DateTime d = DateTime.Now;
                        var InputFileName = d.Ticks + random.Next(100, 100000000).ToString();
                        var ServerSavePath = System.IO.Path.Combine(Server.MapPath("~/Content"), InputFileName+fi.Extension);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);

                        ConcretePath t = new ConcretePath()
                        {
                            MixingID = con.MixingId,
                            ConcreateID = item.ID,
                            Path = InputFileName+ fi.Extension,
                            FileName = file.FileName
                        };
                        db.ConcretePaths.Add(t);
                        db.SaveChanges();

                    }
                    // 
                }
            }
            return RedirectToAction("AddTestsMixingDesign2", "MixingDesign", new
            {
                id = 1
            });

        }

        public ActionResult SubmitConcreateTests(ConcreateTests con)
        {
            foreach (var item in con.Tests)
            {
                
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
                        
                        MixingDesignTest t = new MixingDesignTest()
                        {
                            MixingID = con.MixingId,
                            TestID = item.ID,
                            FileName = InputFileName+fi.Extension,
                            CompleteFileName = file.FileName
                        };
                        db.MixingDesignTests.Add(t);
                        db.SaveChanges();

                    }
                    // 
                }
            }
            return RedirectToAction("AddTestsMixingDesign", "MixingDesign", new
            {
                id = 1
            });
    
}
        public ActionResult changeStatus(ApproveData approveData)

        {
            try
            {
                // TODO: Add insert logic here
                int uId = Convert.ToInt32(Session["UserId"].ToString());
                var query = db.ConcreteMixingDesigns.Where(c => c.Id == approveData.id).FirstOrDefault();
                query.Status = approveData.Status;
                query.UpdatedBy = uId;
                query.UpdatedDate = DateTime.Now;
                db.SaveChanges();
               
                return RedirectToAction("ApproveOrDeclineMixingDesign", "MixingDesign", new
                {
                    id = 1
                });
            }
            catch
            {
                return View("ApproveDetails");
            }

            
        }
        // GET: MixingDesign/Details/5
        public ActionResult UserDetails(int id)
        {
            var result = (from m in db.ConcreteMixingDesigns
                          join f in db.Factory11 on m.FactoryId equals f.Id
                          select new ApproveMixingDesign
                          {
                              FactoryName = f.Name,
                              Latitude = f.Latitude,
                              Longitude = f.Longitude,
                              OwnerName = f.OwnerName,
                              Id = m.Id,
                              FactoryId = m.FactoryId,
                              ConcreteRank = m.ConcreteRank,
                              CementWeight = m.CementWeight,
                              WaterWeight = m.WaterWeight,
                              WashedSand = m.WashedSand,
                              WhiteSand = m.WhiteSand,
                              Rubble3by4 = m.Rubble3by4,
                              Rubble3by8 = m.Rubble3by8,
                              Cement = m.Cement,
                              Water = m.Water,
                              WashedSand2 = m.WashedSand2,
                              WhiteSand2 = m.WhiteSand2,
                              Rubble3by42 = m.Rubble3by42,
                              Rubble3by82 = m.Rubble3by82,
                              C28Day = m.C28Day,
                              Landing = m.Landing,
                              Status = m.Status,
                              Comments = m.Comments
                              ,
                              IsApprove = false
                          }).Where(x => x.Id == id).FirstOrDefault();
            result.Paths = db.MaterialResultsPdfs.Where(r => r.Id == result.Id).Select(p=>new Models.Path { Filename = p.FileName, PathName = p.PdfPath }).ToList();
            return PartialView("Details",result);
        }
        public ActionResult Details(int id)
        {
            var result = (from m in db.ConcreteMixingDesigns
                          join f in db.Factory11 on m.FactoryId equals f.Id
                          select new ApproveMixingDesign
                          {
                              FactoryName = f.Name,
                              Latitude = f.Latitude,
                              Longitude = f.Longitude,
                              OwnerName = f.OwnerName,
                              Id = m.Id,
                              FactoryId = m.FactoryId,
                              ConcreteRank = m.ConcreteRank,
                              CementWeight = m.CementWeight,
                              WaterWeight = m.WaterWeight,
                              WashedSand = m.WashedSand,
                              WhiteSand = m.WhiteSand,
                              Rubble3by4 = m.Rubble3by4,
                              Rubble3by8 = m.Rubble3by8,
                              Cement = m.Cement,
                              Water = m.Water,
                              WashedSand2 = m.WashedSand2,
                              WhiteSand2 = m.WhiteSand2,
                              Rubble3by42 = m.Rubble3by42,
                              Rubble3by82 = m.Rubble3by82,
                              C28Day = m.C28Day,
                              Landing = m.Landing,
                              Status = m.Status,
                              Comments = m.Comments
                              ,
                              IsApprove = true
                          }).Where(x => x.Id == id).FirstOrDefault();
            result.Paths = db.MaterialResultsPdfs.Where(r => r.MixingDesignId == result.Id).Select(p => new Models.Path { Filename = p.FileName, PathName = p.PdfPath }).ToList();
            return PartialView(result);
        }
        // GET: MixingDesign/Create
        public ActionResult AddMixingDesign()
        {
            addMix.Factories = db.Factory11.Select(x => new Fact { FactoryId = x.Id, FactoryName = x.Name }).ToList();
            return PartialView(addMix);
        }

        // POST: MixingDesign/Create
        [HttpPost]
        public ActionResult AddMixingDesign(AddMixDesign mix)
        {
            try
            {
               
                // TODO: Add insert logic here
                int uId = Convert.ToInt32(Session["UserId"].ToString());
                ObjectParameter statusCode = new ObjectParameter("StatusCode", typeof(int));
                ObjectParameter statusMessage = new ObjectParameter("StatusMessage", typeof(string));
                ConcreteMixingDesign mixD = new ConcreteMixingDesign()
                {
                    C28Day = mix.C28Day,
                    Cement = mix.Cement,
                    CementWeight = mix.CementWeight,
                    Comments = mix.Comments,
                    ConcreteRank = mix.ConcreteRank,
                    CreatedBy = uId,
                    CreatedDate = DateTime.Now,
                    ExpiryDate = DateTime.Now,
                    FactoryId = mix.FactoryId,
                    Landing = mix.Landing,
                    Rubble3by4 = mix.Rubble3by4,
                    Rubble3by42 = mix.Rubble3by42,
                    Rubble3by8 = mix.Rubble3by8,
                    Rubble3by82 = mix.Rubble3by82,
                    Status = "Pending",
                    UpdatedBy = uId,
                    UpdatedDate = DateTime.Now,
                    WashedSand = mix.WashedSand,
                    WashedSand2 = mix.WashedSand2,
                    Water = mix.Water,
                    WaterWeight = mix.WaterWeight,
                    WhiteSand = mix.WhiteSand,
                    WhiteSand2 = mix.WhiteSand2

                };
                db.ConcreteMixingDesigns.Add(mixD);
                db.SaveChanges();
                foreach (HttpPostedFileBase file in mix.files)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        Random random = new Random();
                        FileInfo fi = new FileInfo(file.FileName);

                        DateTime d = DateTime.Now;
                        var InputFileName = d.Ticks + random.Next(100, 100000000).ToString();
                        var ServerSavePath = System.IO.Path.Combine(Server.MapPath("~/Content"), InputFileName + fi.Extension);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        MaterialResultsPdf mPdf = new MaterialResultsPdf()
                        {
                            MixingDesignId = mixD.Id,
                            PdfPath = InputFileName+fi.Extension,
                            FileName = file.FileName

                        };
                        db.MaterialResultsPdfs.Add(mPdf);
                        db.SaveChanges();
                    }
                }
                
                TempData["AlertMessage"] = "Design Added Successfully";
                return RedirectToAction("MixingDesign", "MixingDesign", new
                {
                    id = 1
                });
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult UpdateMixingDesign(AddMixDesign mix)
        {
            try
            {
                // TODO: Add insert logic here
                int uId = Convert.ToInt32(Session["UserId"].ToString());
                ObjectParameter statusCode = new ObjectParameter("StatusCode", typeof(int));
                ObjectParameter statusMessage = new ObjectParameter("StatusMessage", typeof(string));

                db.AddUpdateConcMixDesing(mix.Id, mix.FactoryId, mix.ConcreteRank, mix.CementWeight, mix.WaterWeight, mix.WashedSand,
                                            mix.Rubble3by4, mix.Rubble3by8, mix.WhiteSand, mix.Cement, mix.Water, mix.WashedSand2,
                                            mix.Rubble3by42, mix.Rubble3by82, mix.WhiteSand2,mix.C28Day,mix.Landing, mix.Status, null, mix.Comments,
                                            mix.CreatedBy, mix.CreatedDate, uId, null, statusCode, statusMessage);

                if (mix.Status == "Waiting")
                {
                   TempData["AlertMessage"] = "Waiting";
                }
                
                return RedirectToAction("ApproveMixingDesign", "MixingDesign", new
                {
                    id = 1
                });
            }
            catch
            {
                return View();
            }
        }

        // GET: MixingDesign/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MixingDesign/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MixingDesign/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MixingDesign/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
