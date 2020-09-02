using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspnetMvcDemo.Controllers
{
    public class BlockFactoryReportsController : Controller
    {
        // GET: BlockFactoryReports
        public ActionResult BlockFactoryReports()
        {
            return View("~/Views/Reports/BlockFactoryReports.cshtml");
        }
    }
}