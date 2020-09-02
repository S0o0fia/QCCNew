using AspnetMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AspnetMvcDemo.Services
{
    public class InfractionSer
    {
        QCEntities db = new QCEntities();
        public void ApproveAlert(int InfId,int num)
        {
            var Inf = db.Infractions.Where(al => al.ID == InfId).FirstOrDefault();

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
        public ConcreteSample1 ReportData(int id)
        {
            return db.ConcreteSample1.Where(s => s.SampleNumber == id).FirstOrDefault();


        }

        public List<sevenDaysResult> TestResultSeven(int id)
        {
            return db.sevenDaysResults.Where(s => s.sampleNumber == id).ToList();


        }
        public List<InfractionDetails> GetAll()
        {
            List<Infraction> AllInfra = new List<Infraction>();
            var FacIds = db.Factory11.Select(f => f.Id).ToList();
            var sMonth = DateTime.Now.AddMonths(-1).Month.ToString();
            foreach (var ID in FacIds)
            {
                var qq = db.VisitDetails.Where(f => f.FactoryId == ID);
                List<VisitDetail> TempV = new List<VisitDetail>();
                foreach (var item in qq)
                {
                    if (item.VisitDate.Month.ToString() == sMonth)
                    {
                        TempV.Add(item);
                    }

                }
                var Last = TempV.OrderByDescending(d => d.VisitDate).FirstOrDefault();
                if (Last != null)
                {
                    if ((DateTime.Now - Last.VisitDate).TotalDays >= 29)
                    {
                        var FaInfra = db.Infractions.Where(infra => infra.FactoryId == Last.FactoryId).ToList();
                        AllInfra.AddRange(FaInfra);
                    }
                }

            }

            List<InfractionDetails> InfraList = new List<InfractionDetails>();
            foreach (var item in AllInfra)
            {
                InfractionDetails f = new InfractionDetails();
                f.AdminApproved = item.AdminApproved;
                f.C8Day = item.C8Day;
                f.FactoryId = item.FactoryId;
                f.Landing = item.Landing;
                f.MonitorApproved = item.MonitorApproved;
                f.SampleNo = item.SampleNo;
                f.Temperature = item.Temperature;
                f.VisitDate = item.VisitDate;
                f.ID = item.ID;
                f.FactoryName = db.Factory11.Where(fac => fac.Id == item.FactoryId).Select(fa => fa.Name).FirstOrDefault();
                f.Location = db.Factory11.Where(fac => fac.Id == item.FactoryId).Select(fa => fa.Location).FirstOrDefault();
                InfraList.Add(f);
            }
            return InfraList;
        }
        public InfractionDetail Infraction1(int id,User user)
        {

            var query = db.Infractions.Where(f => f.SampleNo == id).FirstOrDefault();
            InfractionDetail Details = new InfractionDetail();
            Details.InfractionCount = 0;
            Details.infraction = query;
            Details.User = user;
            var sMonth = DateTime.Now.AddMonths(-1).Month.ToString();
            var qq = db.VisitDetails.Where(v => v.FactoryId == query.FactoryId);
            foreach (var item in qq)
            {
                if (item.VisitDate.Month.ToString() == sMonth)
                {
                    Details.VisitsCount++;
                }

            }
            var Factory = db.Factory11.Where(f => f.Id == query.FactoryId).Select(ff => new { ff.Name, ff.Location }).FirstOrDefault();
            var Samples = db.ConcreteSample1.Where(v => v.FactoryName == Factory.Name).ToList();
            List<ConcreteSample1> samples = new List<ConcreteSample1>();
            foreach (var item in Samples)
            {
                if (item.ReportDate.Value.Month.ToString() == sMonth)
                {
                    samples.Add(item);
                }
            }
            Details.infractions = samples;
            Details.FactoryName = Factory.Name;
            Details.Location = Factory.Location;

            if (query.AbsenceofDevice == true)
            {
                Details.InfractionCount++;
            }
            if (query.C8Day == true)
            {
                Details.InfractionCount++;
            }
            if (query.HardwareNotCalibrated == true)
            {
                Details.InfractionCount++;
            }
            if (query.InsufficientRecords == true)
            {
                Details.InfractionCount++;
            }
            if (query.IsCleanLocation == true)
            {
                Details.InfractionCount++;
            }
            if (query.Landing == true)
            {
                Details.InfractionCount++;
            }
            if (query.NotUsingMixtureofClass == true)
            {
                Details.InfractionCount++;
            }
            if (query.Temperature == true)
            {
                Details.InfractionCount++;

            }

            return Details;
        }

    }
}