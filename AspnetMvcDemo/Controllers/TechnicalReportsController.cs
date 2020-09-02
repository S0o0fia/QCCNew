using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AspnetMvcDemo.Models;

namespace AspnetMvcDemo.Controllers
{
    public class TechnicalReportsController : Controller
    {
        private QCEntities db = new QCEntities();

        // GET: TechnicalReports
        public ActionResult Index()
        {
            var technicalReports = db.TechnicalReports.Include(t => t.Factory);
            return View(technicalReports.ToList());
        }

        // GET: TechnicalReports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechnicalReport technicalReport = db.TechnicalReports.Find(id);
            if (technicalReport == null)
            {
                return HttpNotFound();
            }
            return View(technicalReport);
        }

        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factory11 factory = db.Factory11.Find(id);
            if (factory == null)
            {
                return HttpNotFound();
            }
            ViewBag.FactoryId = factory.Id;
            TechnicalReport vm = new TechnicalReport() { FactoryId = factory.Id };
            return View(vm);
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "laboTechStaff,ProductQuality,FactoryWarnings,ProceduresRequired,FactoryId")] TechnicalReport technicalReport)
        {
            if (ModelState.IsValid)
            {
                Factory11 factory = db.Factory11.Find(technicalReport.FactoryId);
                technicalReport.FactoryName = factory.Name;
                db.TechnicalReports.Add(technicalReport);
                technicalReport.Approval = "Draft";
                technicalReport.CreationDate = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Home", "Home", new { id = 1 });
            }

            ViewBag.FactoryId = new SelectList(db.Factory11, "Id", "Name", technicalReport.FactoryId);
            return View(technicalReport);
        }

        // GET: TechnicalReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechnicalReport technicalReport = db.TechnicalReports.Find(id);
            if (technicalReport == null)
            {
                return HttpNotFound();
            }
            ViewBag.FactoryId = new SelectList(db.Factory11, "Id", "Name", technicalReport.FactoryId);
            return View(technicalReport);
        }

        // POST: TechnicalReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FactoryName,laboTechStaff,ProductQuality,FactoryWarnings,ProceduresRequired,FactoryId")] TechnicalReport technicalReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(technicalReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FactoryId = new SelectList(db.Factory11, "Id", "Name", technicalReport.FactoryId);
            return View(technicalReport);
        }

        // GET: TechnicalReports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechnicalReport technicalReport = db.TechnicalReports.Find(id);
            if (technicalReport == null)
            {
                return HttpNotFound();
            }
            return View(technicalReport);
        }

        // POST: TechnicalReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TechnicalReport technicalReport = db.TechnicalReports.Find(id);
            db.TechnicalReports.Remove(technicalReport);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
