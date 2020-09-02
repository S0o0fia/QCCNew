using AspnetMvcDemo.Models;
using AspnetMvcDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspnetMvcDemo.Controllers
{
    public class OtherMessionController : Controller
    {
        QCEntities db = new QCEntities();
        // GET: BringMaterial
        public ActionResult Index()
        {

            othertask othertask = new othertask();
            othertask.factories = db.Factory11.ToList();
         
            othertask.users = db.Users.Where(u => u.JobTitle == "Monitor").ToList();
            return View(othertask);

        }


        public ActionResult ShowTasks ()
        {
            

          
            return View();
        }
       

    }
}