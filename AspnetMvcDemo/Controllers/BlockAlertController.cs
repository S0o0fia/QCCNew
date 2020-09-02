using AspnetMvcDemo.Models;
using AspnetMvcDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspnetMvcDemo.Controllers
{
    public class BlockAlertController : Controller
    {
        BlockAlertSer blockAlertSer = new BlockAlertSer();
        QCEntities db = new QCEntities();
        // GET: BlockAlert
        public ActionResult Index()
        {
            var show = blockAlertSer.GetAlerts();
            return View(show);
        }

        public ActionResult Details(int id)
        {
            var show = blockAlertSer.AlertDetails(id);
            return PartialView(show);
        }


        public ActionResult ApproveAdmin (int id)
        {
            BlockAlert alert1 = new BlockAlert();
            alert1 = db.BlockAlerts.Where(a => a.id == id).FirstOrDefault();
            alert1.AdminApproved = true;
            try
            {
                db.SaveChanges();

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index" , "BlockAlert");
        }


        public ActionResult ApproveMonitor1(int id)
        {
            BlockAlert alert1 = new BlockAlert();
            alert1 = db.BlockAlerts.Where(a => a.id == id).FirstOrDefault();
            alert1.MonitorApproved = true;
            try
            {
                db.SaveChanges();

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index" , "BlockAlert");
        }


        public ActionResult ApproveMonitor2(int id)
        {
            BlockAlert alert1 = new BlockAlert();
            alert1 = db.BlockAlerts.Where(a => a.id == id).FirstOrDefault();
            alert1.Monitor2Approved = true;
            try
            {
                db.SaveChanges();

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index" , "BlockAlert");
        }
    }
}