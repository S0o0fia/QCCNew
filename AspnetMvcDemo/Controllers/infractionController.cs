using AspnetMvcDemo.Services;
using System;
using System.Web.Mvc;

namespace AspnetMvcDemo.Controllers
{
    public class infractionController : Controller
    {
        InfractionSer ser = new InfractionSer();
        UserService user = new UserService();
        // GET: infraction
        public ActionResult Index()
        {

            return View(ser.GetAll());
        }
        public ActionResult Infraction1(int id)
        {
            var userId = Convert.ToInt32(Session["UserId"].ToString());
            var Cuser = user.UserDetails(userId);


            var infraction = ser.Infraction1(id,Cuser);
            return View(infraction);
        }
        public ActionResult ConcreteFactoryReports(int id)
        {
            var query = ser.ReportData(id);
            return PartialView("ReportDetails", query);
        }

        public ActionResult TestResultSeven(int id)
        {
            var query = ser.TestResultSeven(id);
            return PartialView("TestResultSeven", query);
        }

        public ActionResult ApproveAdmin(int id)
        {
            ser.ApproveAlert(id,0);
            return RedirectToAction("Index", "Infraction");
        }
        public ActionResult ApproveMonitor1(int id)
        {
            ser.ApproveAlert(id, 1);
            return RedirectToAction("Index", "Infraction");
        }
        public ActionResult ApproveMonitor2(int id)
        {
            ser.ApproveAlert(id, 2);
            return RedirectToAction("Index", "Infraction");
        }
    }
}