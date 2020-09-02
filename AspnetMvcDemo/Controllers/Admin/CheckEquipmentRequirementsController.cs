using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AspnetMvcDemo.Models;

namespace AspnetMvcDemo.Controllers.Admin
{
    public class CheckEquipmentRequirementsController : Controller
    {
        private QCEntities db = new QCEntities();

        // GET: CheckEquipmentRequirements
        public ActionResult Index()
        {
            var checkEquipmentRequirements = db.CheckEquipmentRequirements.Include(c => c.EquipmentRequirementsType);
            return View(checkEquipmentRequirements.ToList());
        }

        // GET: CheckEquipmentRequirements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckEquipmentRequirement checkEquipmentRequirement = db.CheckEquipmentRequirements.Find(id);
            if (checkEquipmentRequirement == null)
            {
                return HttpNotFound();
            }
            return View(checkEquipmentRequirement);
        }

        // GET: CheckEquipmentRequirements/Create
        public ActionResult Create()
        {
            ViewBag.EquipmentRequirementsTypeId = new SelectList(db.EquipmentRequirementsTypes, "ID", "Title");
            return View();
        }

        // POST: CheckEquipmentRequirements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SubType_Arabic,SubType_English,InputFieldType,Description,EquipmentRequirementsTypeId")] CheckEquipmentRequirement checkEquipmentRequirement)
        {
            if (ModelState.IsValid)
            {
                db.CheckEquipmentRequirements.Add(checkEquipmentRequirement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EquipmentRequirementsTypeId = new SelectList(db.EquipmentRequirementsTypes, "ID", "Title", checkEquipmentRequirement.EquipmentRequirementsTypeId);
            return View(checkEquipmentRequirement);
        }

        // GET: CheckEquipmentRequirements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckEquipmentRequirement checkEquipmentRequirement = db.CheckEquipmentRequirements.Find(id);
            if (checkEquipmentRequirement == null)
            {
                return HttpNotFound();
            }
            ViewBag.EquipmentRequirementsTypeId = new SelectList(db.EquipmentRequirementsTypes, "ID", "Title", checkEquipmentRequirement.EquipmentRequirementsTypeId);
            return View(checkEquipmentRequirement);
        }

        // POST: CheckEquipmentRequirements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SubType_Arabic,SubType_English,InputFieldType,Description,EquipmentRequirementsTypeId")] CheckEquipmentRequirement checkEquipmentRequirement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(checkEquipmentRequirement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EquipmentRequirementsTypeId = new SelectList(db.EquipmentRequirementsTypes, "ID", "Title", checkEquipmentRequirement.EquipmentRequirementsTypeId);
            return View(checkEquipmentRequirement);
        }

        // GET: CheckEquipmentRequirements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckEquipmentRequirement checkEquipmentRequirement = db.CheckEquipmentRequirements.Find(id);
            if (checkEquipmentRequirement == null)
            {
                return HttpNotFound();
            }
            return View(checkEquipmentRequirement);
        }

        // POST: CheckEquipmentRequirements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CheckEquipmentRequirement checkEquipmentRequirement = db.CheckEquipmentRequirements.Find(id);
            db.CheckEquipmentRequirements.Remove(checkEquipmentRequirement);
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
