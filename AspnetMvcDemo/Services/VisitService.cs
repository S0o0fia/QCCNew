using AspnetMvcDemo.Models;
using AspnetMvcDemo.ViewModel;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace AspnetMvcDemo.Services
{
    public class VisitService
    {
        QCEntities db = new QCEntities();

        //function for getting visits from database 
        public List<Visitviewmodel> GetVisits()
        {
            var query = (from vd in db.VisitDetails
                         join user in db.Users
                         on vd.MonitorId equals user.Id
                         join fact in db.Factory11
                         on vd.FactoryId equals fact.Id
                         select new Visitviewmodel
                         {
                             factroyname = fact.Name,
                             monitorname = user.FullName,
                             visitDate = vd.VisitDate,
                             state = vd.Status
                         }).ToList();

            return query;
        }

        public List<Visitviewmodel> GetBlockVisits()
        {
            var query = (from vd in db.BlockVisitDetails
                         join user in db.BlockUsers
                         on vd.MonitorId equals user.Id
                         join fact in db.BlockFactories
                         on vd.FactoryId equals fact.Id
                         select new Visitviewmodel
                         {
                             factroyname = fact.Name,
                             monitorname = user.FullName,
                             visitDate = vd.VisitDate,
                             state = vd.Status
                         }).ToList();

            return query;
        }

        //Function for getting All visits from database For Reports
        public List<VisitDetailsForReports> getVisitsReport()
        {
            var totalVisits = (from cs in db.ConcreteSample1
                               join v in db.VisitDetails 
                               on DbFunctions.TruncateTime(cs.ReportDate) equals DbFunctions.TruncateTime(v.VisitDate) 
                               where DbFunctions.TruncateTime(cs.ReportDate) <= DbFunctions.TruncateTime(DateTime.Today)
                               orderby cs.ReportDate
                               select new VisitDetailsForReports 
                               {
                                   visitId = v.Id,
                                   Mid = v.MonitorId , 
                                   factroyname =cs.FactoryName,
                                   visitDate = DbFunctions.TruncateTime(cs.ReportDate),
                                   location = cs.FactoryLocation
                               }).DistinctBy(cs=> new { cs.factroyname , cs.visitDate}).ToList();


            List<VisitDetailsForReports> list = new List<VisitDetailsForReports>();
            foreach (var item in totalVisits)
            {
                item.monitorname = db.Users.Where(u=>u.Id == item.Mid).Select(u=>u.FullName).FirstOrDefault();
                list.Add(item);
            }

            return (list);
        }

        //Function For Getting Daily Visits From DataBase For Report
        public List<VisitDetailsForReports> getDailyVisitsReport()
        {
            var totalVisits = (from cs in db.ConcreteSample1
                               join v in db.VisitDetails
                               on DbFunctions.TruncateTime(cs.ReportDate) equals DbFunctions.TruncateTime(v.VisitDate)
                               join u in db.Users on v.MonitorId equals u.Id
                               where DbFunctions.TruncateTime(cs.ReportDate) == DbFunctions.TruncateTime(DateTime.Now)
                               orderby cs.ReportDate
                               select new VisitDetailsForReports 
                               {
                                   visitId = v.Id,
                                   factroyname = cs.FactoryName,
                                   visitDate = DbFunctions.TruncateTime(cs.ReportDate),
                                   monitorname = u.FullName,
                                   location = cs.FactoryLocation
                               }).DistinctBy(cs=>cs.factroyname).ToList();
                
            return (totalVisits);
        }

        //overload of the daily report
        public List<VisitDetailsForReports> getDailyVisitsReport(DateTime date)
        {
            var totalVisits = (from cs in db.ConcreteSample1
                               join f in db.Factory11 on cs.FactoryName equals f.Name
                               join v in db.VisitDetails on f.Id equals v.FactoryId
                               join u in db.Users on v.MonitorId equals u.Id
                               where DbFunctions.TruncateTime(cs.ReportDate) == DbFunctions.TruncateTime(date)
                               orderby cs.ReportDate
                               select new VisitDetailsForReports
                               {
                                   visitId = v.Id,
                                   factroyname = f.Name,
                                   visitDate = DbFunctions.TruncateTime(cs.ReportDate),
                                   monitorname = u.FullName,
                                   location = f.Location
                               }).DistinctBy(cs => cs.factroyname).ToList();

            return (totalVisits);
        }

        //Block Daily Report 
        public List<VisitDetailsForReports> getDailyBlockVisitsReport()
        {
            var totalVisits = (from cs in db.BlockFactoryReports
                               join v in db.BlockVisitDetails on DbFunctions.TruncateTime(cs.ReportDate) equals DbFunctions.TruncateTime(v.VisitDate)
                               join u in db.BlockUsers on v.MonitorId equals u.Id
                               where DbFunctions.TruncateTime(cs.ReportDate) == DbFunctions.TruncateTime(DateTime.Today)
                               orderby cs.ReportDate
                               select new VisitDetailsForReports
                               {
                                   visitId = v.Id,
                                   factroyname = cs.FactoryName ,
                                   visitDate = DbFunctions.TruncateTime(cs.ReportDate),
                                   monitorname = u.FullName,
                                   location = cs.FatoryLocation
                               }).DistinctBy(cs => cs.factroyname).ToList();

            return (totalVisits);
        }

        //Function For getting Monthly Visits For Report
        public List<MonthlyReportViewModel> monthlyReport ()
        {
            var monthlyReport = (from c in db.ConcreteSample1
                                 join s in db.sevenDaysResults
                                 on c.SampleNumber equals s.sampleNumber
                                 join m in db.monthlyResults
                                 on c.SampleNumber equals m.sampleNumber
                                 select new MonthlyReportViewModel
                                 {
                                     factoryName = c.FactoryName,
                                     conctreteRank = c.ConcreteRank,
                                     visitDate = DbFunctions.TruncateTime(c.ReportDate).ToString() ,
                                     SampleNumber = c.SampleNumber,
                                     ConcreteTemperture = c.ConcreteTemperture,
                                     DownAmount = c.DownAmount,
                                     CementType = c.CementType,
                                     IsCleanUsage = c.IsCleanUsage,
                                     IsBasicUsage = c.IsBasicUsage,
                                     IsColumnUsage = c.IsColumnUsage,
                                     IsRoofUsage = c.IsRoofUsage,
                                     IsOtherUsage = c.IsOtherUsage,
                                     OtherReason = c.OtherReason,
                                     SevenDaysAverageCompressiveStrength = s.averageCompressiveStrength,
                                     MonthlyAverageCompressiveStrength = m.averageCompressiveStrength
                                 }).DistinctBy(c=>c.SampleNumber).ToList();
            return (monthlyReport);
        }


      
       

        //function for create new  visit 
        public Boolean CreateVisits(VisitDetail model)
        {
            db.VisitDetails.Add(new VisitDetail
            {
                FactoryId = model.FactoryId,
                MonitorId = model.MonitorId,
                Status = model.Status,
                VisitDate = model.VisitDate
            });
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


         public Boolean CreateBlockVisits(BlockVisitDetail model)
        {
            db.BlockVisitDetails.Add(new BlockVisitDetail
            {
                FactoryId = model.FactoryId,
                MonitorId = model.MonitorId,
                Status = model.Status,
                VisitDate = model.VisitDate
            });
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool CancleVisit(VisitDetail visitDetail)
        {
          try { 
            DateTime dateTime = DateTime.Today;
                var visit = db.VisitDetails.Where(v => v.FactoryId == visitDetail.FactoryId && DbFunctions.TruncateTime(v.VisitDate) == DbFunctions.TruncateTime(DateTime.Today)).FirstOrDefault();
                var locationid = db.Factory11.Where(f => f.Id == visit.FactoryId).Select(f => f.Location_Id).FirstOrDefault();
                var dateOfNextDay = (
                    from vs in db.VisitDetails
                    join f in db.Factory11
                    on vs.FactoryId equals f.Id
                    where DbFunctions.TruncateTime(vs.VisitDate) > DbFunctions.TruncateTime(DateTime.Today) && 
                    f.Location_Id == locationid
                    select vs
                    ).FirstOrDefault();


              visit.VisitDate = dateOfNextDay.VisitDate;
                dateOfNextDay.VisitDate = DateTime.Today;
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Cancel Block Visit
        public bool CancleBlockVisit(BlockVisitDetail visitDetail)
        {

            DateTime dateTime = DateTime.Today;
            DateTime nextdate = DateTime.Today;
            try
            {
                if ((int)nextdate.DayOfWeek == 4)
                {
                    nextdate = nextdate.AddDays(2);
                }
                else if ((int)nextdate.DayOfWeek == 5)
                {
                    nextdate = nextdate.AddDays(1);
                }
                else if ((int)nextdate.DayOfWeek == 3)
                {
                    nextdate = nextdate.AddDays(3);
                }
                else
                {
                    nextdate = nextdate.AddDays(1);
                }

                var visit = db.VisitDetails.Where(v => v.FactoryId == visitDetail.FactoryId && DbFunctions.TruncateTime(v.VisitDate) == DbFunctions.TruncateTime(DateTime.Today)).FirstOrDefault();
                var locationid = db.Factory11.Where(f => f.Id == visit.FactoryId).Select(f => f.Location_Id).FirstOrDefault();
                var dateOfNextDay = (
                    from vs in db.VisitDetails
                    join f in db.Factory11
                    on vs.FactoryId equals f.Id
                    where DbFunctions.TruncateTime(vs.VisitDate) == DbFunctions.TruncateTime(nextdate) && f.Location_Id == locationid
                    select vs
                    ).FirstOrDefault();




                visit.VisitDate = dateOfNextDay.VisitDate;
                dateOfNextDay.VisitDate = DateTime.Today;
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        //function for edit the already stored visit
        public Boolean EditVisit(Visitviewmodel model)
        {
            //for getting user id 
            var userId = db.Users.Where(u => u.FullName == model.monitorname).Select(u => u.Id).FirstOrDefault();

            //for getting factory id 
            var factoryid = db.Factory11.Where(f => f.Name == model.factroyname).Select(f => f.Id).FirstOrDefault();

            //getting the recored from db
            VisitDetail vd = db.VisitDetails.Where(v => v.Id == model.visitId).FirstOrDefault();
            //change the old values with new one
            vd.MonitorId = userId;
            vd.FactoryId = factoryid;
            vd.VisitDate = model.visitDate;

            //saving 
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }

        //Function to Get Today Visits
        public List<VisitsWithPriority> getTodayVisits(int userId)
        {
            var todayVisits = (from f in db.Factory11
                               join v in db.VisitDetails on f.Id equals v.FactoryId
                               where v.MonitorId == userId && DbFunctions.TruncateTime(v.VisitDate) == DbFunctions.TruncateTime(DateTime.Today)
                               select new VisitsWithPriority
                               {
                                Name = f.Name , 
                                Id = f.Id , 
                                Location = f.Location ,
                                VisitPriority = v.visitpriority , 
                                VisitStatus = v.Status
                               }).ToList();
            return (todayVisits);
        }

        public List<BlockFactory> getTodayBlockVisits(string username)
        {
            int userId = db.BlockUsers.Where(u => u.Username == username).Select(u => u.Id).FirstOrDefault();

            var todayVisits = (from f in db.BlockFactories
                               join v in db.BlockVisitDetails on f.Id equals v.FactoryId
                               where v.MonitorId == userId && DbFunctions.TruncateTime(v.VisitDate) == DbFunctions.TruncateTime(DateTime.Today)
                               select f).ToList();
            return (todayVisits);
        }
        //Function to Get Total Visits
        public List<AdminVisit> getTotalVisits()
        {
            var totalVisits = (from v in db.VisitDetails
                               join f in db.Factory11 on v.FactoryId equals f.Id
                               join u in db.Users on v.MonitorId equals u.Id
                               orderby v.VisitDate
                               select new AdminVisit { Id = v.Id, Monitor = u.FullName, VisitDate = DbFunctions.TruncateTime(v.VisitDate), FactoryName = f.Name, FactoryLocation = f.Location }
                              ).ToList();
            List<AdminVisit> lists = new List<AdminVisit>();
            foreach (var item in totalVisits)
            {
                if(item.VisitDate.Value.Date.Month == DateTime.Today.Month)
                {
                    lists.Add(item);
                }
            }
            return (lists);
        }

        public List<AdminVisit> getMonitroLocation ()
        {
            var totalVisits = (from f in db.Factory11
                            join v in db.VisitDetails on f.Id equals v.FactoryId
                            join u in db.Users on v.MonitorId equals u.Id
                            where DbFunctions.TruncateTime(v.VisitDate) == DbFunctions.TruncateTime(DateTime.Today)
                            orderby v.MonitorId
                            select new AdminVisit { Id = v.Id, Monitor = u.FullName, VisitDate = DbFunctions.TruncateTime(v.VisitDate), FactoryName = f.Name, FactoryLocation = f.Location
                            }
                              ).DistinctBy(v=>v.MonitorId).ToList();
           List<AdminVisit> lists = new List<AdminVisit>();
            foreach (var item in totalVisits)
            {
                if(item.VisitDate.Value.Date.Month == DateTime.Today.Month)
                {
                    lists.Add(item);
                }
            }

            return (lists);
        }
        public List<AdminVisit> getTotalBlockVisits()
        {
            var totalVisits = (from f in db.BlockFactories
                               join v in db.BlockVisitDetails on f.Id equals v.FactoryId
                               join u in db.BlockUsers on v.MonitorId equals u.Id
                               orderby v.VisitDate
                               select new AdminVisit { Id = v.Id, Monitor = u.FullName, VisitDate = DbFunctions.TruncateTime(v.VisitDate), FactoryName = f.Name, FactoryLocation = f.Location }
                              ).ToList();
            List<AdminVisit> lists = new List<AdminVisit>();
            foreach (var item in totalVisits)
            {
                if (item.VisitDate.Value.Date.Month == DateTime.Today.Month)
                {
                    lists.Add(item);
                }
            }
            return (lists);
        }

        //Function to get Receive Concrete Sample
        public List<ConcreteSample1> ReceiveSample(int userId)
        {
            //get Monitor  location
            var location = (from v in db.VisitDetails
                            join f in db.Factory11
                            on v.FactoryId equals f.Id
                            where v.MonitorId == userId &&
                            DbFunctions.TruncateTime(v.VisitDate) == DbFunctions.TruncateTime(DateTime.Today)
                            select f.Location
                            ).FirstOrDefault();
          
            var receiveSamples = (from C in db.ConcreteSample1
                                  join f in db.Factory11 on C.FactoryName equals f.Name
                                  join vs in db.VisitDetails on f.Id equals vs.FactoryId
                                  where
                                  DbFunctions.DiffDays(C.ReportDate, DateTime.Today) == 1
                                  && C.IsReceived == false
                                  select C).Distinct().ToList();

           List < ConcreteSample1> result = new List<ConcreteSample1>();
            foreach (var item in receiveSamples)
            {
                if(item.FactoryLocation == location)
                {
                    result.Add(item);   
                }
            }
            return (result);
        }

        public List<ConcreteSample1> ReceiveSample()
        {

            var receiveSamples = (from C in db.ConcreteSample1
                                  where DbFunctions.DiffDays(C.ReportDate, DateTime.Today) == 1
                                  && C.IsReceived == false
                                  select C).Distinct().ToList();
            return (receiveSamples);
        }

        //Function to get Broken Sample
        public List<ConcreteSample1> BrokenSample(int userId)
        {
            var brokenSamples = ( from f in db.ConcreteSample1
                                 join v in db.VisitDetails on  DbFunctions.TruncateTime(f.ReportDate) equals DbFunctions.TruncateTime(v.VisitDate)
                                 where v.MonitorId == userId && DbFunctions.DiffDays(f.ReportDate, DateTime.Today) == 28
                                 select f).ToList();
            return (brokenSamples);
        }

        public List<ConcreteSample1> BrokenSample()
        {
            var brokenSamples = (from f in db.ConcreteSample1
                                 join v in db.VisitDetails on DbFunctions.TruncateTime(f.ReportDate) equals DbFunctions.TruncateTime(v.VisitDate)
                                 where DbFunctions.DiffDays(f.ReportDate, DateTime.Today) == 28
                                 select f).ToList();
            return (brokenSamples);
        }

        public void updatetheVisitState(string factoryname )
        {
            var date = DateTime.Today.Date.Add(new TimeSpan(0, 0, 0)); ;
            int factid = db.Factory11.Where(f => f.Name == factoryname).Select(f => f.Id).FirstOrDefault();
            var visit = db.VisitDetails.Where(v => v.FactoryId == factid  && v.VisitDate == date).FirstOrDefault();
            visit.Status = true;
            try
            {
                db.SaveChanges();
            }
            catch(Exception ex)
            {

            }
        }
    }
}