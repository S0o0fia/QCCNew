using AspnetMvcDemo.Models;
using AspnetMvcDemo.Services;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AspnetMvcDemo.Controllers
{
    public class TestingResultsController : Controller
    {
        concreteSampleSer ConSer = new concreteSampleSer();

        QCEntities context = new QCEntities();
        public ActionResult Index(int id)
        {
            Session["Choice"] = id == 1 ? "Concrete" : "Block";


            var Concrete = ConSer.ViewIndex();
            return View(Concrete);
        }

        public ActionResult ConcreteTestingResult(int id)
        {
            var qu = ConSer.TestForCompany(id);
            return View(qu);
        }
        [HttpPost]
        public ActionResult ConcreteTestingResult(List<PressureResistanceTestforFactorySamplePhoto> PressList)
        {
            ConSer.SaveAll(PressList);
            return RedirectToAction("Index", new
            {
                id = 1
            });
        }

        public ActionResult SevenDaysResult(string id)
        {
            QCEntities qc = new QCEntities();
            var sapmleNumber = int.Parse(id);
            var sample = qc.sevenDaysResults.Where(s => s.sampleNumber == sapmleNumber).FirstOrDefault();
            if (sample != null)
            {
                return RedirectToAction("AddedSevenDaysResult", new { id = sapmleNumber });
            }
            else
            {
                threeCubeOfSevenDaysSample cubeOfOneSample = new threeCubeOfSevenDaysSample();
                cubeOfOneSample.SamplePart1 = new sevenDaysResult();
                cubeOfOneSample.SamplePart1.sampleNumber = int.Parse(id);
                return View(cubeOfOneSample);
            }
        }

        public ActionResult AddedSevenDaysResult (string id)
        {
            QCEntities qc = new QCEntities();
            var sample = int.Parse(id);
            var testResult = qc.sevenDaysResults.Where(s => s.sampleNumber == sample).ToArray();
            threeCubeOfSevenDaysSample cubeOfOneSample = new threeCubeOfSevenDaysSample();
            cubeOfOneSample.SamplePart1 = testResult[0];
            cubeOfOneSample.SamplePart2 = testResult[1];
            cubeOfOneSample.SamplePart3 = testResult[2];
            return View(cubeOfOneSample);
        }
        [HttpPost]
        public ActionResult SevenDaysResult(threeCubeOfSevenDaysSample cubeOfOneSample)
        {
            QCEntities qc = new QCEntities();
            
            cubeOfOneSample.SamplePart1.sampleNumber = int.Parse(RouteData.Values["id"].ToString());
            cubeOfOneSample.SamplePart1.volume = 5302;
            cubeOfOneSample.SamplePart1.areaLoaded = 17674;
            cubeOfOneSample.SamplePart1.testDate = DateTime.Now;
            cubeOfOneSample.SamplePart1.age = 7;

            cubeOfOneSample.SamplePart2.sampleNumber = int.Parse(RouteData.Values["id"].ToString());
            cubeOfOneSample.SamplePart2.volume = 5302;
            cubeOfOneSample.SamplePart2.areaLoaded = 17674;
            cubeOfOneSample.SamplePart2.testDate = DateTime.Now;
            cubeOfOneSample.SamplePart2.age = 7;

            cubeOfOneSample.SamplePart3.sampleNumber = int.Parse(RouteData.Values["id"].ToString());
            cubeOfOneSample.SamplePart3.volume = 5302;
            cubeOfOneSample.SamplePart3.areaLoaded = 17674;
            cubeOfOneSample.SamplePart3.testDate = DateTime.Now;
            cubeOfOneSample.SamplePart3.age = 7;
            double? avg = (cubeOfOneSample.SamplePart1.CompressiveStrength + cubeOfOneSample.SamplePart2.CompressiveStrength + cubeOfOneSample.SamplePart3.CompressiveStrength) / 3;

            cubeOfOneSample.SamplePart1.averageCompressiveStrength = avg;
            cubeOfOneSample.SamplePart2.averageCompressiveStrength = avg;
            cubeOfOneSample.SamplePart3.averageCompressiveStrength = avg;

            qc.sevenDaysResults.Add(cubeOfOneSample.SamplePart1);
            qc.sevenDaysResults.Add(cubeOfOneSample.SamplePart2);
            qc.sevenDaysResults.Add(cubeOfOneSample.SamplePart3);
            try
            {
                qc.SaveChanges();
                TempData["message"] = "Your Save is Sucess";

                return RedirectToAction("AddedSevenDaysResult" , new { id= cubeOfOneSample.SamplePart1.sampleNumber});

            }
            catch (Exception ex)
            {

                return RedirectToAction("SepvenDaysTest", new { id=1});
            }

        }

        public ActionResult MonthlyResult(string id)
        {

            QCEntities qc = new QCEntities();
            var sapmleNumber = int.Parse(id);
            var sample = qc.monthlyResults.Where(s => s.sampleNumber == sapmleNumber).FirstOrDefault();
            if (sample != null)
            {
                return RedirectToAction("AddedMonthlyResult", new { id = sapmleNumber });
            }
            else
            {
                threeCubeOfMonthlySample cubeOfOneSample = new threeCubeOfMonthlySample();
                cubeOfOneSample.SamplePart1 = new monthlyResult();
                cubeOfOneSample.SamplePart1.sampleNumber = int.Parse(id);
                return View(cubeOfOneSample);
            }

      }

        public ActionResult AddedMonthlyResult (string id)
        {
            QCEntities qc = new QCEntities();
            var sample = int.Parse(id);
            var testResult = qc.monthlyResults.Where(s => s.sampleNumber == sample).ToArray();
            threeCubeOfMonthlySample cubeOfOneSample = new threeCubeOfMonthlySample();
            cubeOfOneSample.SamplePart1 = testResult[0];
            cubeOfOneSample.SamplePart2 = testResult[1];
            cubeOfOneSample.SamplePart3 = testResult[2];
            return View(cubeOfOneSample);
        }

        [HttpPost]
        public ActionResult MonthlyResult(threeCubeOfMonthlySample cubeOfOneSample)
        {
            QCEntities qc = new QCEntities();
            int SampleNumberFromUrl = int.Parse(RouteData.Values["id"].ToString());
            cubeOfOneSample.SamplePart1.sampleNumber = SampleNumberFromUrl;
            cubeOfOneSample.SamplePart1.volume = 5302;
            cubeOfOneSample.SamplePart1.areaLoaded = 17674;
            cubeOfOneSample.SamplePart1.testDate = DateTime.Now;
            cubeOfOneSample.SamplePart1.age = 28;

            cubeOfOneSample.SamplePart2.sampleNumber = SampleNumberFromUrl;
            cubeOfOneSample.SamplePart2.volume = 5302;
            cubeOfOneSample.SamplePart2.areaLoaded = 17674;
            cubeOfOneSample.SamplePart2.testDate = DateTime.Now;
            cubeOfOneSample.SamplePart2.age = 28;

            cubeOfOneSample.SamplePart3.sampleNumber = SampleNumberFromUrl;
            cubeOfOneSample.SamplePart3.volume = 5302;
            cubeOfOneSample.SamplePart3.areaLoaded = 17674;
            cubeOfOneSample.SamplePart3.testDate = DateTime.Now;
            cubeOfOneSample.SamplePart3.age = 28;
            double? avg = (cubeOfOneSample.SamplePart1.CompressiveStrength + cubeOfOneSample.SamplePart2.CompressiveStrength + cubeOfOneSample.SamplePart3.CompressiveStrength) / 3;

            cubeOfOneSample.SamplePart1.averageCompressiveStrength = Math.Round(Double.Parse(avg.ToString()), 2);
            cubeOfOneSample.SamplePart2.averageCompressiveStrength = Math.Round(Double.Parse(avg.ToString()), 2);
            cubeOfOneSample.SamplePart3.averageCompressiveStrength = Math.Round(Double.Parse(avg.ToString()), 2);

            qc.monthlyResults.Add(cubeOfOneSample.SamplePart1);
            qc.monthlyResults.Add(cubeOfOneSample.SamplePart2);
            qc.monthlyResults.Add(cubeOfOneSample.SamplePart3);

            var cRank = qc.ConcreteSample1.Where(s => s.SampleNumber == SampleNumberFromUrl).Select(c => c.ConcreteRank).FirstOrDefault();
            int concRanck = int.Parse((cRank.Split('-'))[1]);

            var qInfrac = qc.Infractions.Where(inf => inf.SampleNo == SampleNumberFromUrl).FirstOrDefault();

            var fact = (from conSamp in qc.ConcreteSample1
                        join fac in qc.Factory11
                        on conSamp.FactoryName equals fac.Name
                        where conSamp.SampleNumber == SampleNumberFromUrl
                        select new
                        {
                            FactoryId = fac.Id,
                            createdDate = conSamp.CreatedDate
                        }).FirstOrDefault();

            switch (concRanck)
            {
                case 35:
                    {
                        if (cubeOfOneSample.SamplePart1.averageCompressiveStrength < 34.5)
                        {
                            
                             if(qInfrac == null)
                            {
                                Infraction infraction = new Infraction();
                                {
                                    infraction.FactoryId = fact.FactoryId;
                                    infraction.Temperature = false;
                                    infraction.Landing = false;
                                    infraction.C8Day = true;
                                    infraction.VisitDate = fact.createdDate;
                                    infraction.SampleNo = SampleNumberFromUrl;
                                    infraction.AdminApproved = false;
                                    infraction.MonitorApproved = false;
                                    infraction.IsCleanLocation = false;
                                    infraction.NotUsingMixtureofClass = false;
                                    infraction.AbsenceofDevice = false;
                                    infraction.HardwareNotCalibrated = false;
                                    infraction.InsufficientRecords = false;
                                }
                                qc.Infractions.Add(infraction);
                                qc.SaveChanges();
                            }
                             else
                            {
                                qInfrac.C8Day = true;
                                qc.SaveChanges();


                            }
                        }
                            break;
                    }
                case 30:
                    {
                        if (cubeOfOneSample.SamplePart1.averageCompressiveStrength < 29.5)
                        {

                            if (qInfrac == null)
                            {
                                Infraction infraction = new Infraction();
                                {
                                    infraction.FactoryId = fact.FactoryId;
                                    infraction.Temperature = false;
                                    infraction.Landing = false;
                                    infraction.C8Day = true;
                                    infraction.VisitDate = fact.createdDate;
                                    infraction.SampleNo = SampleNumberFromUrl;
                                    infraction.AdminApproved = false;
                                    infraction.MonitorApproved = false;
                                    infraction.IsCleanLocation = false;
                                    infraction.NotUsingMixtureofClass = false;
                                    infraction.AbsenceofDevice = false;
                                    infraction.HardwareNotCalibrated = false;
                                    infraction.InsufficientRecords = false;
                                }
                                qc.Infractions.Add(infraction);
                                qc.SaveChanges();
                            }
                            else
                            {
                                qInfrac.C8Day = true;
                                qc.SaveChanges();


                            }
                        }
                        break;
                    }
                case 20:
                    {
                        if (cubeOfOneSample.SamplePart1.averageCompressiveStrength < 19.5)
                        {

                            if (qInfrac == null)
                            {
                                Infraction infraction = new Infraction();
                                {
                                    infraction.FactoryId = fact.FactoryId;
                                    infraction.Temperature = false;
                                    infraction.Landing = false;
                                    infraction.C8Day = true;
                                    infraction.VisitDate = fact.createdDate;
                                    infraction.SampleNo = SampleNumberFromUrl;
                                    infraction.AdminApproved = false;
                                    infraction.MonitorApproved = false;
                                    infraction.IsCleanLocation = false;
                                    infraction.NotUsingMixtureofClass = false;
                                    infraction.AbsenceofDevice = false;
                                    infraction.HardwareNotCalibrated = false;
                                    infraction.InsufficientRecords = false;
                                }
                                qc.Infractions.Add(infraction);
                                qc.SaveChanges();
                            }
                            else
                            {
                                qInfrac.C8Day = true;
                                qc.SaveChanges();


                            }
                        }
                        break;
                    }
                case 15:
                    {
                        if (cubeOfOneSample.SamplePart1.averageCompressiveStrength < 14.5)
                        {

                            if (qInfrac == null)
                            {
                                Infraction infraction = new Infraction();
                                {
                                    infraction.FactoryId = fact.FactoryId;
                                    infraction.Temperature = false;
                                    infraction.Landing = false;
                                    infraction.C8Day = true;
                                    infraction.VisitDate = fact.createdDate;
                                    infraction.SampleNo = SampleNumberFromUrl;
                                    infraction.AdminApproved = false;
                                    infraction.MonitorApproved = false;
                                    infraction.IsCleanLocation = false;
                                    infraction.NotUsingMixtureofClass = false;
                                    infraction.AbsenceofDevice = false;
                                    infraction.HardwareNotCalibrated = false;
                                    infraction.InsufficientRecords = false;
                                }
                                qc.Infractions.Add(infraction);
                                cubeOfOneSample.isSaved = true;
                                qc.SaveChanges();
                            }
                            else
                            {
                                qInfrac.C8Day = true;
                                cubeOfOneSample.isSaved = true;
                                qc.SaveChanges();


                            }
                        }
                        break;
                    }
                default:
                    break;
            }

            qc.SaveChanges();
            TempData["message"] = "Your Save is Sucess";
            return RedirectToAction("AddedMonthlyResult", new { id = cubeOfOneSample.SamplePart1.sampleNumber });
        }





        public ActionResult BlockTestingResult(string id)
        {
            BlockTestResultCubes cubes = new BlockTestResultCubes() ;
            cubes.Cube1 = new BlockTestResult();
            cubes.Cube1.sampleNumber = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            return View(cubes);
        }

        [HttpPost]
       public ActionResult BlockTestingResult(BlockTestResultCubes model)
        {
            BlockTestResult cube1 = new BlockTestResult();
            BlockTestResult cube2 = new BlockTestResult();
            BlockTestResult cube3 = new BlockTestResult();

            //for first cube
            cube1.weight = model.Cube1.weight;
            cube1.length = model.Cube1.length;
            cube1.width = model.Cube1.width;
            cube1.height = model.Cube1.height;
            cube1.Minfaceshell = model.Cube1.Minfaceshell;
            cube1.NetArea = model.Cube1.NetArea;
            cube1.GrossArea = model.Cube1.GrossArea;
            cube1.load = model.Cube1.load;
            cube1.compbetarea = model.Cube1.compbetarea;
            cube1.compgrossarea = model.Cube1.compgrossarea;
            cube1.strengthrequirement = 3.45;
            cube1.AvgNetArea = model.Cube1.AvgNetArea;
            cube1.AvgGrossArea = model.Cube1.AvgGrossArea;
            cube1.sampleNumber = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            cube1.testDate = DateTime.Today;
            
            //for seconed Cube
            cube2.weight = model.Cube2.weight;
            cube2.length = model.Cube2.length;
            cube2.width = model.Cube2.width;
            cube2.height = model.Cube2.height;
            cube2.Minfaceshell = model.Cube2.Minfaceshell;
            cube2.NetArea = model.Cube2.NetArea;
            cube2.GrossArea = model.Cube2.GrossArea;
            cube2.load = model.Cube2.load;
            cube2.compbetarea = model.Cube2.compbetarea;
            cube2.compgrossarea = model.Cube2.compgrossarea;
            cube2.strengthrequirement = 3.45;
            cube2.AvgNetArea = model.Cube1.AvgNetArea;
            cube2.AvgGrossArea = model.Cube1.AvgGrossArea;
            cube2.sampleNumber = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            cube2.testDate = DateTime.Today;

            //for third Cube
            cube3.weight = model.Cube3.weight;
            cube3.length = model.Cube3.length;
            cube3.width = model.Cube3.width;
            cube3.height = model.Cube3.height;
            cube3.Minfaceshell = model.Cube3.Minfaceshell;
            cube3.NetArea = model.Cube3.NetArea;
            cube3.GrossArea = model.Cube3.GrossArea;
            cube3.load = model.Cube3.load;
            cube3.compbetarea = model.Cube3.compbetarea;
            cube3.compgrossarea = model.Cube3.compgrossarea;
            cube3.strengthrequirement = 3.45;
            cube3.AvgNetArea = model.Cube1.AvgNetArea;
            cube3.AvgGrossArea = model.Cube1.AvgGrossArea;
            cube3.sampleNumber = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            cube3.testDate = DateTime.Today;
           try
            {  // modify the isTested 
                int sample = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
                var testedsapmle = context.BlockFactoryReports.Where(s => s.SampleNumber == sample).FirstOrDefault();
                var factoryId = (from fr in context.BlockFactoryReports
                                 join bf in context.BlockFactories
                                 on fr.FactoryName equals bf.Name
                                 where fr.SampleNumber == sample
                                 select bf.Id).FirstOrDefault();
                testedsapmle.isTested = true;
                if(model.Cube1.AvgNetArea < 4.14)
                {
                     
                    BlockInfraction blockInfraction = new BlockInfraction()
                    {
                        testDate = DateTime.Today,
                        factory_id = factoryId,
                        AdminApproved = false,
                        MonitorApproved = false,
                        Monitor2Approved = false
                    };
                    context.BlockInfractions.Add(blockInfraction);
                    context.SaveChanges();

                    //Add Alert if it Exist 
                    var totalInfraction = (from infb in context.BlockInfractions
                                           where infb.factory_id == factoryId && infb.Alert_id == 0
                                           select infb).ToList();
                    if (totalInfraction.Count >= 3)
                    {
                        BlockAlert blockAlert = new BlockAlert()
                        {
                            FactoryID = factoryId,
                            Date = DateTime.Today,
                            Approved = false,
                        };
                    }
                }
                context.BlockTestResults.Add(cube1);
                context.BlockTestResults.Add(cube2);
                context.BlockTestResults.Add(cube3);
                context.SaveChanges();
                @TempData["AlertMessage2"] = "Sucess";
            }
            catch(Exception ex)
            {

            }
            return View(model);
        }
    }
}