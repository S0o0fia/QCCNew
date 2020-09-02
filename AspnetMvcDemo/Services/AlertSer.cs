using AspnetMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspnetMvcDemo.Services
{
    public class AlertSer
    {
        QCEntities db = new QCEntities();
        public int GenerateAlerts ()
        {
            foreach (var item in db.Factory11.ToList())
            {
                int counter = 0;
                List<int> Samples = new List<int>();
                int Range = 0;
                DateTime LastDate;
                var sMonth = DateTime.Now.Month;
                if (sMonth >= 7)
                    Range = sMonth - 6;
                else
                    Range = (sMonth - 6) + 12;
                var FirstDate = DateTime.Now;
                if (Range <= 6)
                    LastDate = new DateTime(DateTime.Now.Year, Range, 1);
                else
                    LastDate = new DateTime((DateTime.Now.Year) - 1, Range, 1);

                var infractions = db.Infractions.Where(i => i.FactoryId == item.Id).Where(inf => inf.VisitDate >= LastDate && inf.VisitDate <= FirstDate).ToList();
                if (infractions != null)
                {
                    foreach (var inf in infractions)
                    {
                        if (inf.C8Day == true)
                        {
                            Samples.Add(inf.ID);
                            counter++;
                        }
                    }
                    if (counter >= 3)
                    {
                        Alert alert = new Alert();
                        alert.FactoryID = item.Id;
                        alert.Date = DateTime.Now;
                        alert.Approved = false;
                        db.Alerts.Add(alert);
                        db.SaveChanges();
                        foreach (var sam in Samples)
                        {
                            AlertInfraction AltInf = new AlertInfraction();
                            AltInf.AlertID = alert.ID;
                            AltInf.InfractionID = sam;
                            db.AlertInfractions.Add(AltInf);

                        }
                        db.SaveChanges();
                    }
                    else
                    {
                        bool Flag = false;
                        foreach (var query in infractions)
                        {

                            if (query.AbsenceofDevice == true)
                            {
                                counter++;
                                Flag = true;
                            }
                            if (query.C8Day == true)
                            {
                                Flag = true;
                                counter++;
                            }
                            if (query.HardwareNotCalibrated == true)
                            {
                                Flag = true;
                                counter++;
                            }
                            if (query.InsufficientRecords == true)
                            {
                                Flag = true;
                                counter++;
                            }
                            if (query.IsCleanLocation == true)
                            {
                                Flag = true;
                                counter++;
                            }
                            if (query.Landing == true)
                            {
                                Flag = true;
                                counter++;
                            }
                            if (query.NotUsingMixtureofClass == true)
                            {
                                Flag = true;
                                counter++;
                            }
                            if (query.Temperature == true)
                            {
                                Flag = true;
                                counter++;

                            }
                            if (Flag == true)
                            {
                                Samples.Add(query.ID);
                            }
                            if (counter >= 5)
                                break;
                        }
                        if (counter >= 5)
                        {
                            Alert alert = new Alert();
                            alert.FactoryID = item.Id;
                            alert.Date = DateTime.Now;
                            alert.Approved = false;
                            db.Alerts.Add(alert);
                            db.SaveChanges();
                            foreach (var sam in Samples.Distinct())
                            {
                                AlertInfraction AltInf = new AlertInfraction();
                                AltInf.AlertID = alert.ID;
                                AltInf.InfractionID = sam;
                                db.AlertInfractions.Add(AltInf);

                            }
                            db.SaveChanges();
                        }
                    }
                }




            }
            return 0;
        }
        public List<Alerts> GetAll ()
        {

            var alerts= db.Alerts.ToList();
            List<Alerts> alertsList = new List<Alerts>();
            foreach (var item in alerts)
            {
                Alerts alert = new Alerts();
                alert.FactoryName = db.Factory11.Where(f => f.Id == item.FactoryID).Select(ff => ff.Name).FirstOrDefault();
                alert.AlertID = item.ID;
                alert.Status = item.Status;
                alertsList.Add(alert);

            }
            return alertsList;
        }
        public List<Alerts> GetAllForReport(int Month)
        {
            var alerts = db.Alerts.ToList();

            List<Alerts> alertsList = new List<Alerts>();
            foreach (var item in alerts)
            {
                if (item.Date.Value.Month == Month)
                {
                    Alerts alert = new Alerts();
                    alert.FactoryName = db.Factory11.Where(f => f.Id == item.FactoryID).Select(ff => ff.Name).FirstOrDefault();
                    alert.AlertID = item.ID;
                    alert.Status = item.Status;
                    alertsList.Add(alert);
                }
            }
            return alertsList;
        }

        public void ApproveAlert(int AlertId,int num)
        {
            var alert = db.Alerts.Where(al => al.ID == AlertId).FirstOrDefault();

            if (num == 0)
            {
                alert.Status = "Admin";
            }
            else if (num == 1)
            {
                alert.Status = "Monitor1";

            }
            else if (num == 2)
            {
                alert.Status = "Monitor2";

            }
            db.SaveChanges();
        }
        public InfractionAlert GetDetails (int alertID,User user)
        {
            var status= db.Alerts.Where(a => a.ID == alertID).Select(aa => aa.Status).FirstOrDefault();
            InfractionAlert infractionsList = new InfractionAlert();
            infractionsList.Infractions = new List<Infraction>();
            var Infras = db.AlertInfractions.Where(inf => inf.AlertID == alertID).Select(f => f.InfractionID).ToList();
            foreach (var item in Infras)
            {
                var Infractions = db.Infractions.Where(inf => inf.ID == item).FirstOrDefault();
                infractionsList.Infractions.Add(Infractions);
            }
            infractionsList.AlertId = alertID;
            infractionsList.Status = status;
            infractionsList.User = user;
            return infractionsList;
            
        }
    }
}