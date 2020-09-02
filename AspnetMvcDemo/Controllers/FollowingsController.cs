using AspnetMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspnetMvcDemo.Controllers
{
    public class FollowingsController : Controller
    {
        QCEntities db = new QCEntities();
        // GET: Followings
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Temprature()
        {
            tempretureClass tempreture = new tempretureClass();
            tempreture.temprutureChecks = db.temprutureChecks.ToList();
           
            return View(tempreture);
        }

      
        public ActionResult Addtemp ()
        {
            tempretureClass tempreture = new tempretureClass();
            return View(tempreture);
        }

        [HttpPost]
        public ActionResult Addtemp(tempretureClass temp)
        {
            try
            {
                temp.tempruture.date_time = DateTime.Now;
                db.temprutureChecks.Add(temp.tempruture);
                db.SaveChanges();
                return RedirectToAction("Temprature");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Addtemp");
            }
            
        }
        public ActionResult AddMachine ()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMachine(MachineClass model)
        {
            try
            {
                db.MachineChecks.Add(model.machine);
                db.SaveChanges();
                return RedirectToAction("ShowMachine");
            }
            catch (Exception ex)
            {
                return RedirectToAction("AddMachine");
            }
        }

        public ActionResult ShowMachine ()
        {
            MachineClass machineClass = new MachineClass();
             machineClass.machineChecks = db.MachineChecks.ToList();
            return View(machineClass);
            
        }
    }
}