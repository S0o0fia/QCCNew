using AspnetMvcDemo.Models;
using AspnetMvcDemo.Services;
using AspnetMvcDemo.ViewModel;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspnetMvcDemo.Services
{
    public class concreteSampleSer
    {
        QCEntities context = new QCEntities();


        public List<TestReportViewModel> getTestReport()
        {
            var query = (from c in context.ConcreteSample1
                         join mt in context.monthlyResults
                         on c.SampleNumber equals mt.sampleNumber
                         select new TestReportViewModel
                         {
                             factoryName = c.FactoryName,
                             sampleNumber = c.SampleNumber,
                             location = c.FactoryLocation,
                             concreteRank = c.ConcreteRank,
                             testDate = mt.testDate,
                             testResult = mt.averageCompressiveStrength
                         }).DistinctBy(c => c.sampleNumber).ToList();

            return query;
        }

        public ConcreteSample1 getTestReportDetailsConcreteSample(int sampleNumber)
        {
            var q = context.ConcreteSample1.Where(c => c.SampleNumber == sampleNumber).FirstOrDefault();
            return q;
        }
        public List<sevenDaysResult> getTestReportDetailsSevenDaysResults(int sampleNumber)
        {
            var q = context.sevenDaysResults.Where(s => s.sampleNumber == sampleNumber).ToList();
            return q;
        }
        public List<monthlyResult> getTestReportDetailsMonthlyResults(int sampleNumber)
        {
            var q = context.monthlyResults.Where(m => m.sampleNumber == sampleNumber).ToList();
            return q;
        }

        public void SaveAll(List<PressureResistanceTestforFactorySamplePhoto> press)
        {
            foreach (var item in press)
            {
                PressureResistanceTestforFactorySample p = new PressureResistanceTestforFactorySample()
                {
                    Age = item.Age,
                    AreaLoaded = item.AreaLoaded,
                    AvgCompressiveStrength = item.AvgCompressiveStrength,
                    CompressiveStrength = item.CompressiveStrength,
                    ConcreteRank = item.ConcreteRank,
                    CrushingLoad = item.CrushingLoad,
                    Result = item.Result,
                    SampleNo = item.SampleNo,
                    TestDate = item.TestDate,
                    UnitWeight = item.UnitWeight,
                    Volume = item.Volume,
                    Weight = item.Weight
                };
                context.PressureResistanceTestforFactorySamples.Add(p);
            }
            context.SaveChanges();

        }
        public CubeSamplesWithPath concreteSample(int id)
        {
            // var factoryId = context.VisitDetails.Where(f => f.FactoryId == id && DbFunctions.DiffDays(f.VisitDate, DateTime.Now) == 28).Select(v => v.FactoryId).FirstOrDefault();
            var FactoryName = context.Factory11.Where(f => f.Id == id).Select(fa => fa.Name).FirstOrDefault();

            var query = context.ConcreteSample1.Where(v => v.FactoryName == FactoryName && DbFunctions.DiffDays(v.ReportDate, DateTime.Now) == 28).FirstOrDefault();
            CubeSamplesWithPath p = new CubeSamplesWithPath();
            p.SampleNumber = query.SampleNumber;
            p.ConcreteRank = int.Parse((query.ConcreteRank.Split('-'))[1]);
            return p;

        }
        public List<PressureResistanceTestforFactorySamplePhoto> ViewIndex()
        {


            var samples = context.ConcreteSample1.Where(v => DbFunctions.DiffDays(v.ReportDate, DateTime.Now) == 28);
            List<PressureResistanceTestforFactorySamplePhoto> pList = new List<PressureResistanceTestforFactorySamplePhoto>();
            foreach (var item in samples)
            {
                SampleCubePath paths = new SampleCubePath();
                if (samples != null)
                    paths = context.SampleCubePaths.Where(s => s.SampleNumber == item.SampleNumber).FirstOrDefault();

                if (paths != null)
                {
                    PressureResistanceTestforFactorySamplePhoto p = new PressureResistanceTestforFactorySamplePhoto();
                    p.SampleNo = item.SampleNumber;
                    pList.Add(p);
                }

            }
            return pList;



        }
        public List<PressureResistanceTestforFactorySamplePhoto> TestForCompany(int id)
        {
            var query = context.ConcreteSample1.Where(c => c.SampleNumber == id).Select(sa => sa.ConcreteRank).FirstOrDefault();
            var Photos = context.SampleCubePaths.Where(s => s.SampleNumber == id).FirstOrDefault();
            PressureResistanceTestforFactorySamplePhoto p = new PressureResistanceTestforFactorySamplePhoto();
            p.SampleNo = id;
            p.ConcreteRank = int.Parse((query.Split('-'))[1]);
            p.Age = 28;
            p.AreaLoaded = 17674;
            p.Volume = 5302;
            p.TestDate = DateTime.Now;
            p.Photo1 = Photos.Cube1P1;
            p.Photo2 = Photos.Cube1P2;
            PressureResistanceTestforFactorySamplePhoto p2 = new PressureResistanceTestforFactorySamplePhoto();
            p2.SampleNo = id;
            p2.ConcreteRank = int.Parse((query.Split('-'))[1]);
            p2.Age = 28;
            p2.AreaLoaded = 17674;
            p2.Volume = 5302;
            p2.TestDate = DateTime.Now;
            p2.Photo1 = Photos.Cube2P1;
            p2.Photo2 = Photos.Cube2P2;
            PressureResistanceTestforFactorySamplePhoto p3 = new PressureResistanceTestforFactorySamplePhoto();
            p3.SampleNo = id;
            p3.ConcreteRank = int.Parse((query.Split('-'))[1]);
            p3.Age = 28;
            p3.AreaLoaded = 17674;
            p3.Volume = 5302;
            p3.TestDate = DateTime.Now;
            p3.Photo1 = Photos.Cube3P1;
            p3.Photo2 = Photos.Cube3P2;
            List<PressureResistanceTestforFactorySamplePhoto> PressureList = new List<PressureResistanceTestforFactorySamplePhoto>();
            PressureList.Add(p);
            PressureList.Add(p2);
            PressureList.Add(p3);
            return PressureList;
        }
      
      
    }
}