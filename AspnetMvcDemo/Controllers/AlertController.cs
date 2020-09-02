using AspnetMvcDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspnetMvcDemo.Controllers
{
    public class AlertController : Controller
    {
        AlertSer AlertSer = new AlertSer();
        UserService user = new UserService();
        // GET: Alert
        public ActionResult Add()
        {
             AlertSer.GenerateAlerts();
            return  RedirectToAction("Index", "Alert");

        }
        public ActionResult Index()
        {
            var Alert = AlertSer.GetAll();
            return View(Alert);
        }
        public ActionResult AlertDetails(int id)
        {
            var userId = Convert.ToInt32(Session["UserId"].ToString());
            var Cuser = user.UserDetails(userId);

            var Alert = AlertSer.GetDetails(id,Cuser);
            return PartialView(Alert);
        }
        public ActionResult ReportIndex()
        {
            return View();
        }
         public ActionResult ReporttIndex(int id)
        {
            var Alert = AlertSer.GetAllForReport(id);
            return PartialView(Alert);
        }
       

        public ActionResult ApproveAdmin(int id)
        {
            AlertSer.ApproveAlert(id, 0);
            return RedirectToAction("Index", "Alert");
        }
        public ActionResult ApproveMonitor1(int id)
        {
            AlertSer.ApproveAlert(id, 1);
            return RedirectToAction("Index", "Alert");
        }
        public ActionResult ApproveMonitor2(int id)
        {
            AlertSer.ApproveAlert(id, 2);
            return RedirectToAction("Index", "Alert");
        }

    }
}