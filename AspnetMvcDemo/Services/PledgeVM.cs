using AspnetMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspnetMvcDemo.Services
{
    public class PledgeVM
    {
        QCEntities db = new QCEntities();
        public List<Factory11> GeneratePledge ()
        {
            var qu = db.Factory11.ToList();
            var alert = db.Alerts.ToList();
            List<Alert> Temp = new List<Alert>();
            List<Factory11> TempF = new List<Factory11>();
            foreach (var item in alert)
            {
                if (item.Date.Value.Month == DateTime.Now.Month)
                    Temp.Add(item);


            }
            foreach (var item in qu)
            {
                var ISFound = Temp.Where(f => f.FactoryID == item.Id).FirstOrDefault();
                if (ISFound != null)
                    TempF.Add(item);
            }
            return TempF;
        }
    }
}