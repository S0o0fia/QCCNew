 using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AspnetMvcDemo.Models;
using AspnetMvcDemo.Services;
using Microsoft.Ajax.Utilities;
using Resources;

namespace AspnetMvcDemo.Controllers
{
    public class HomeController : Controller
    {
        QCEntities db = new QCEntities();

        public ActionResult MakeChoice()
        {
            return View();
        }


        public ActionResult Home(int id)
        {
            if (id == 1)
            {
                Session["Choice"] = "Concrete";
                var dateTime = DateTime.Now;
                if ((int)dateTime.DayOfWeek == 4)
                {
                    dateTime = dateTime.AddDays(2);
                }
                //if day is Friday
                else if ((int)dateTime.DayOfWeek == 5)
                {
                    dateTime = dateTime.AddDays(1);
                }

                var date = db.VisitDetails.Where(vs => DbFunctions.TruncateTime(vs.VisitDate) == DbFunctions.TruncateTime(dateTime)).Select(vs => vs.VisitDate).FirstOrDefault();
                if (date.Date.Year == 1)
                {
                    CreateVisitService vs = new CreateVisitService();
                    vs.CreateVisit();
                }

            }
            var userId = Convert.ToInt32(Session["UserId"].ToString());

            VisitDetailsModel model = new VisitDetailsModel();

            VisitService visitService = new VisitService();
            model.TodayVisits = visitService.getTodayVisits(userId);

            model.TotalVisits = visitService.getTotalVisits();

           

            return View(model);
        }


        public ActionResult BlockVisit()
        {

            var dateTime = DateTime.Now;
            if ((int)dateTime.DayOfWeek == 4)
            {
                dateTime = dateTime.AddDays(2);
            }
            //if day is Friday
            else if ((int)dateTime.DayOfWeek == 5)
            {
                dateTime = dateTime.AddDays(1);
            }


            var date = db.BlockVisitDetails.Where(vs => DbFunctions.TruncateTime(vs.VisitDate) == DbFunctions.TruncateTime(dateTime)).Select(vs => vs.VisitDate).FirstOrDefault();
            if (date.Date.Year == 1)
            {
                BlockCreateVisitSer vs = new BlockCreateVisitSer();
                vs.CreateVisit();
            }
            var userId = Convert.ToInt32(Session["UserId"].ToString());
            var username = db.Users.Where(u => u.Id == userId).Select(u => u.Username).FirstOrDefault();
            BlockVisitDetailsModel model = new BlockVisitDetailsModel();
            VisitService visitService = new VisitService();
            model.TodayVisits = visitService.getTodayBlockVisits(username);

            model.TotalVisits = visitService.getTotalBlockVisits();

            return View(model);


        }

        public ActionResult ReceiveSample(int id)
        {
            Session["Choice"] = id == 1 ? "Concrete" : "Block";

            var userId = Convert.ToInt32(Session["UserId"].ToString());

            VisitDetailsModel model = new VisitDetailsModel();
            VisitService visitService = new VisitService();
            if (@Session["JobTitle"].ToString() == "Admin")
            {
                model.ReceiveSamples = visitService.ReceiveSample();
            }
            else
            {
                model.ReceiveSamples = visitService.ReceiveSample(userId);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult ReceiveSample(VisitDetailsModel fact, string id)
        {
            var yesterday = DateTime.Now;
            var factname = id;
            var report = db.ConcreteSample1.Where(s => s.FactoryName == factname && DbFunctions.DiffDays(s.ReportDate, yesterday) == 1).FirstOrDefault();
            report.IsReceived = true;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }

            var userId = Convert.ToInt32(Session["UserId"].ToString());

            VisitDetailsModel model = new VisitDetailsModel();
            VisitService visitService = new VisitService();

            if (@Session["JobTitle"].ToString() == "Admin")
            {
                model.ReceiveSamples = visitService.ReceiveSample();
            }
            else
            {
                model.ReceiveSamples = visitService.ReceiveSample(userId);
            }

            return View(model);
        }

        public ActionResult BrokenSample(int id)
        {
            Session["Choice"] = id == 1 ? "Concrete" : "Block";

            var userId = Convert.ToInt32(Session["UserId"].ToString());

            VisitDetailsModel model = new VisitDetailsModel();
            VisitService visitService = new VisitService();

            model.BrokenSamples = visitService.BrokenSample(userId);
            return View(model);
        }

        [HttpGet]
        public ActionResult VisitDetails()
        {
                  

        var result = (from f in db.Factory11
                      join v in db.VisitDetails on f.Id equals v.FactoryId
                      join u in db.Users on v.MonitorId equals u.Id
                      where DbFunctions.TruncateTime(v.VisitDate) == DbFunctions.TruncateTime(DateTime.Today)
                      orderby v.VisitDate
                      select new EditAdminVisit
                      {
                          OldMonitorId= u.Id,
                          FactoryId = f.Id,
                          Monitor = u.FullName,
                          VisitDate = DbFunctions.TruncateTime(v.VisitDate),
                          FactoryName = f.Name,
                          FactoryLocation = f.Location,
                          Id = v.Id,
                          Date = "",

                      }).DistinctBy(v => v.FactoryLocation).ToList();

           
         

            //to add the remaiing monitor to each visit 
            foreach (var item in result)
            {
                //to get the all monitors 
                var monitors = db.Users.Where(u => u.JobTitle == "Monitor" &&u.Id != item.OldMonitorId ).ToList();
                item.RemainingMonitors = monitors; 
            }

            return PartialView(result);
        }


        [HttpPost]
        public ActionResult VisitDetails(List<EditAdminVisit> model)
        {
            foreach (var item in model)
            {
                //check if this item has newMonitor id or not 
                if(item.NewMonitorId != null)
                {
                    //get the oldMonitorvisits
                    var oldMonitorVisits = db.VisitDetails.Where(v => v.MonitorId == item.OldMonitorId
                    && DbFunctions.TruncateTime(v.VisitDate) == DbFunctions.TruncateTime(item.VisitDate)).ToList();

                    //get newMonitor visits to check if he is main one or not
                    var newMonitorVisits = db.VisitDetails.Where(v => v.MonitorId == item.NewMonitorId 
                    && DbFunctions.TruncateTime(v.VisitDate) == DbFunctions.TruncateTime(item.VisitDate)).ToList();

                   
                    //if the newnewMonitorVisits == null that not Main Monitor
                    if(newMonitorVisits.Count == 0)
                    {
                        //just update the Monitorid with the new one 
                        foreach (var visit in oldMonitorVisits)
                        {
                            var location = db.Factory11.Where(f => f.Id == visit.FactoryId).FirstOrDefault();
                            if (location.Location == item.FactoryLocation)
                            {
                                visit.MonitorId = int.Parse(item.NewMonitorId.ToString());
                            }
                        }

                    }
                    //that reamain one 
                    else
                    {
                        //Start swaping 
                        //old one with new one
                        foreach (var visit1 in oldMonitorVisits)
                        {
                            var location = db.Factory11.Where(f => f.Id == visit1.FactoryId).FirstOrDefault();
                            if (location.Location == item.FactoryLocation)
                                visit1.MonitorId = int.Parse(item.NewMonitorId.ToString());
                        }
                        }

                    //new one with old one
                    foreach (var visit2 in newMonitorVisits)
                    {
                        var location = db.Factory11.Where(f => f.Id == visit2.FactoryId).FirstOrDefault();
                        if (location.Location == item.FactoryLocation)

                        {
                            visit2.MonitorId = item.OldMonitorId;
                  
                        }
                    }

                }
            }
            try
            {
                db.SaveChanges();

            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("Home", new { id = 1 });
        }

        public string GetLocarions(int id)
        {
            var query = (from f in db.Factory11
                         join v in db.VisitDetails
                         on f.Id equals v.FactoryId
                         where v.MonitorId != id
                         select new Location {
                         Id = int.Parse(f.Location_Id.ToString()),
                         Location_Arabic = f.Location}
                         ).DistinctBy(l=>l.Location_Arabic).ToList();


            JavaScriptSerializer jss = new JavaScriptSerializer();
            string output = jss.Serialize(query);
            Response.Write(output);
            Response.Flush();
            Response.End();

            return output;
        }

        [HttpGet]
        public ActionResult VisitDetailsDateEdit(int Id)
        {
            var visitDetails = from f in db.Factory11
                               join v in db.VisitDetails on f.Id equals v.FactoryId
                               join u in db.Users on v.MonitorId equals u.Id
                               where v.Id == Id
                               orderby v.VisitDate
                               select new AdminVisit { MonitorId = u.Id, FactoryId = f.Id, Monitor = u.FullName, VisitDate = DbFunctions.TruncateTime(v.VisitDate), FactoryName = f.Name, FactoryLocation = f.Location };

            var monitors = db.Users.Where(u => u.JobTitle == "Monitor").Select(x => new Mon { MonitorId = x.Id, MonitorName = x.FullName }).ToList().ToList();

            var visit = visitDetails.FirstOrDefault();

            AdminVisit result = new AdminVisit

            {
                Id = Id,
                MonitorId = visit.MonitorId,
                FactoryId = visit.FactoryId,

                Monitor = visit.Monitor,
                FactoryName = visit.FactoryName,
                FactoryLocation = visit.FactoryLocation,
                VisitDate = visit.VisitDate,
             
            };

            return PartialView(result);
        }

       

        [HttpPost]
        public ActionResult UpdateVisiteDate (AdminVisit adminVisit)
        {
            var visit = db.VisitDetails.Where(v => v.Id == adminVisit.Id).FirstOrDefault();
            var visitloaction = db.Factory11.Where(f => f.Id == visit.FactoryId).Select(f => f.Location_Id).FirstOrDefault();
            var visitDate = DateTime.ParseExact(adminVisit.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var newVisits = db.VisitDetails.Where(v => DbFunctions.TruncateTime(v.VisitDate) == DbFunctions.TruncateTime(visitDate)).ToList();
            //to createNew one 
            var ifFactoryExist = db.VisitDetails.Where(v => v.FactoryId == visit.FactoryId 
            && DbFunctions.TruncateTime(v.VisitDate) == DbFunctions.TruncateTime(visitDate)).FirstOrDefault();

            if(ifFactoryExist == null)
            {
                VisitDetail visitDetail = new VisitDetail();
                foreach (var item in newVisits)
                {
                    var location_id = db.Factory11.Where(f => f.Id == item.FactoryId).Select(f => f.Location_Id).FirstOrDefault();
                    if (location_id == visitloaction)
                    {
                        visitDetail.MonitorId = item.MonitorId;
                    }

                }
                visitDetail.VisitDate = DateTime.ParseExact(adminVisit.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                visitDetail.visitpriority = true;
                visitDetail.FactoryId = visit.FactoryId;
                db.VisitDetails.Add(visitDetail);
                db.SaveChanges();
            }
            else
            {
                TempData["message"] = "Your Save is Failed";
            }

            return RedirectToAction("Home", "Home", new { id = 1 });
        }

        //Edit Block Visit

        [HttpGet]
        public ActionResult BlockVisitDetails(int Id)
        {
            var visitDetails = from f in db.BlockFactories
                               join v in db.BlockVisitDetails on f.Id equals v.FactoryId
                               join u in db.BlockUsers on v.MonitorId equals u.Id
                               where v.Id == Id
                               orderby v.VisitDate
                               select new AdminVisit { MonitorId = u.Id, FactoryId = f.Id, Monitor = u.FullName, VisitDate = DbFunctions.TruncateTime(v.VisitDate), FactoryName = f.Name, FactoryLocation = f.Location };

            var monitors = db.BlockUsers.Where(u => u.JobTitle == "Monitor").Select(x => new Mon { MonitorId = x.Id, MonitorName = x.FullName }).ToList().ToList();

            var visit = visitDetails.FirstOrDefault();

            AdminVisit result = new AdminVisit
            {
                Id = Id,
                MonitorId = visit.MonitorId,
                FactoryId = visit.FactoryId,
                Monitor = visit.Monitor,
                FactoryName = visit.FactoryName,
                FactoryLocation = visit.FactoryLocation,
                VisitDate = visit.VisitDate,
             
            };

            return PartialView(result);
        }

        [HttpPost]
        public ActionResult UpdateBlockVisit(AdminVisit visit)
        {
            //get old visit from database 
            var oldvisit = db.VisitDetails.Where(v => v.Id == visit.Id).FirstOrDefault();
            //store Miontir
            var oldMonitor = oldvisit.MonitorId;
            //get the old Monitor main Visits
            var oldMonitorVisit = db.VisitDetails.Where(v => v.MonitorId == oldvisit.MonitorId && DbFunctions.TruncateTime(v.VisitDate) == DbFunctions.TruncateTime(visit.VisitDate)).ToList();
            //to check if the new Monitor is main one or not
            var location_id = db.Users.Where(u => u.Id == visit.MonitorId).Select(u => u.Location_Id).FirstOrDefault();

            //get the new Monitor main  visits 
            var newMonitorVisit = db.VisitDetails.Where(v => v.MonitorId == visit.MonitorId && DbFunctions.TruncateTime(v.VisitDate) == DbFunctions.TruncateTime(visit.VisitDate)).ToList();

            //if the location_id != null that is mean that this montior is main one 
            if (location_id != null)
            {
                //location id of the visit 

                var location_id_visit = db.Factory11.Where(f => f.Name == visit.FactoryName).Select(f => f.Location_Id).FirstOrDefault();

                if (oldMonitorVisit.Count > 2)
                {
                    foreach (var item in oldMonitorVisit)
                    {
                        var location = db.Factory11.Where(f => f.Id == item.FactoryId).Select(f => f.Location_Id).FirstOrDefault();
                        if (location == location_id_visit)
                        {
                            item.MonitorId = visit.MonitorId;
                        }
                    }
                }

                else
                {
                    foreach (var item in oldMonitorVisit)
                    {
                        item.MonitorId = visit.MonitorId;

                    }
                }


                if (newMonitorVisit.Count != 0)
                {
                    foreach (var item in newMonitorVisit)
                    {
                        item.MonitorId = oldMonitor;
                    }
                }

            }
            else
            {
                foreach (var item in oldMonitorVisit)
                {
                    item.MonitorId = visit.MonitorId;
                }
            }


            try
            {
                db.SaveChanges();

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("BlockVisit", "Home");
        }
    }
}