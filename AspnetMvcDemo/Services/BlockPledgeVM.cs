using AspnetMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspnetMvcDemo.Services
{
    public class BlockPledgeVM
    {
        QCEntities db = new QCEntities();
        public List<BlockFactory> GeneratePledge()
        {
            var qu = db.BlockFactories.ToList();
            var alert = db.BlockAlerts.ToList();
            List<BlockAlert> Temp = new List<BlockAlert>();
            List<BlockFactory> TempF = new List<BlockFactory>();
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