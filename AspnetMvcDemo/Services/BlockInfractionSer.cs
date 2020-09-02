using AspnetMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AspnetMvcDemo.Services
{
    public class BlockInfractionSer
    {
        QCEntities db = new QCEntities();
        public void ApproveAlert(int InfId, int num)
        {
            var Inf = db.BlockInfractions.Where(al => al.id == InfId).FirstOrDefault();

            if (num == 0)
            {
                Inf.AdminApproved = true;
            }
            else if (num == 1)
            {
                Inf.MonitorApproved = true;

            }
            else if (num == 2)
            {
                Inf.Monitor2Approved = true;

            }
            db.SaveChanges();
        }
     
        public List<BlockInfractionDetails> GetInfractions ()
        {
            var infraction = (from inf in db.BlockInfractions
                             join f in db.BlockFactories
                             on inf.factory_id equals f.Id
                             where DbFunctions.DiffDays(inf.testDate, DateTime.Today) < 30
                             select new BlockInfractionDetails
                             {
                                 AdminApproved = inf.AdminApproved,
                                 MonitorApproved = inf.MonitorApproved,
                                 Monitor2Approved = inf.Monitor2Approved,
                                 FactoryName = f.Name,
                                 Location = f.Location,
                                 factory_id = f.Id,
                                 SampleNo = inf.SampleNo,
                                 testDate = inf.testDate
                             }).ToList();
            return infraction;
        }


        public BlockInfractionDetail infractionDetail (int id , BlockUser user)
        {
            var query = db.BlockInfractions.Where(f => f.SampleNo == id).FirstOrDefault();
            BlockInfractionDetail Details = new BlockInfractionDetail();
            Details.InfractionCount = 0;
            Details.infraction = query;
            Details.User = user;
            //var sMonth = DateTime.Now.AddMonths(-1).Month.ToString();
            var qq = db.BlockVisitDetails.Where(v => v.FactoryId == query.factory_id && DbFunctions.DiffMonths(v.VisitDate , DateTime.Today) <= 1).ToList();
            Details.VisitsCount = qq.Count;
            //foreach (var item in qq)
            //{
            //    if (item.VisitDate.Month.ToString() == sMonth)
            //    {
            //        Details.VisitsCount++;
            //    }

            //}
            var Factory = db.BlockFactories.Where(f => f.Id == query.factory_id).Select(ff => new { ff.Name, ff.Location }).FirstOrDefault();
            var Samples = db.BlockFactoryReports.Where(v => v.FactoryName == Factory.Name  && DbFunctions.DiffMonths(v.ReportDate, DateTime.Today) <= 1).ToList();
            List<BlockFactoryReport> samples = new List<BlockFactoryReport>();
            //foreach (var item in Samples)
            //{
            //    if (item.ReportDate.Value.Month.ToString() == sMonth)
            //    {
            //        samples.Add(item);
            //    }
            //}
            Details.infractions = samples;
            Details.FactoryName = Factory.Name;
            Details.Location = Factory.Location;


            return Details;

        }
    }
}