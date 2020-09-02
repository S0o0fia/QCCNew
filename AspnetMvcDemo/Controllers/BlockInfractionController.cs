using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspnetMvcDemo.Models;
using AspnetMvcDemo.Services;


namespace AspnetMvcDemo.Controllers
{
    public class BlockInfractionController : Controller
    {
        BlockInfractionSer ser = new BlockInfractionSer();
        UserService user = new UserService();
        QCEntities context = new QCEntities();
        // GET: infraction
        public ActionResult Index()
        {
            return View(ser.GetInfractions());
        }

        public ActionResult testDetails(string id)
        {
            var userId = Convert.ToInt32(Session["UserId"].ToString());
            var buser = user.BlockUserDetails(userId);

            var samplenumber = int.Parse(id);
            var infraction = ser.infractionDetail(samplenumber, buser);
            return View(infraction);

            
        }


        public ActionResult testReuslt (string id)
        {
            int samplenumber = int.Parse(id);
            var q = (from bts in context.BlockTestResults
                     where bts.sampleNumber == samplenumber
                     select bts).ToList();
            return PartialView("testReuslt", q);
        }

        public ActionResult ApproveAdmin(int id)
        {
            ser.ApproveAlert(id, 0);
            return RedirectToAction("Index", "BlockInfraction");
        }
        public ActionResult ApproveMonitor1(int id)
        {
            ser.ApproveAlert(id, 1);
            return RedirectToAction("Index", "BlockInfraction");
        }
        public ActionResult ApproveMonitor2(int id)
        {
            ser.ApproveAlert(id, 2);
            return RedirectToAction("Index", "BlockInfraction");
        }
    }
}