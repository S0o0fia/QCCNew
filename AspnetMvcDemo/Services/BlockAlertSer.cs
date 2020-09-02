using AspnetMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspnetMvcDemo.Services
{
    public class BlockAlertSer
    {
        QCEntities db = new QCEntities();
        public List<BlockAlerts> GetAlerts()
        {
            var FactoryName = (from a in db.BlockAlerts
                               join f in db.BlockFactories
                               on a.FactoryID equals f.Id
                               select new BlockAlerts
                               { AlertID = a.id ,
                               FactoryName= f.Name ,
                               Admin = a.AdminApproved , 
                               Monitor1Approve = a.MonitorApproved , 
                               Monitor2Approve = a.Monitor2Approved
                               }).ToList();

            return FactoryName;
        }

        public BlockInfractionAlert AlertDetails(int AlertId)
        {
            BlockInfractionAlert alert = new BlockInfractionAlert();
            alert.Infractions = (from inf in db.BlockInfractions
                          where inf.Alert_id == AlertId
                          select inf).ToList();

            alert.AlertId = AlertId;
            var alerts = db.BlockAlerts.Where(a => a.id == AlertId).FirstOrDefault();

            alert.Monitor1Approve = alerts.MonitorApproved;
            alert.Monitor2Approve = alerts.Monitor2Approved;
            alert.AdminApprove = alerts.AdminApproved;
            return  alert;
        }



    }
}