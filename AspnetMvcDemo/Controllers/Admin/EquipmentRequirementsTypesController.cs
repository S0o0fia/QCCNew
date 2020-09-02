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
    public class EquipmentRequirementsTypesController : Controller
    {
        private QCEntities db = new QCEntities();

        // GET: EquipmentRequirementsTypes
        public ActionResult Index()
        {
            return View(db.EquipmentRequirementsTypes.ToList());
        }

        // GET: EquipmentRequirementsTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentRequirementsType equipmentRequirementsType = db.EquipmentRequirementsTypes.Find(id);
            if (equipmentRequirementsType == null)
            {
                return HttpNotFound();
            }
            return View(equipmentRequirementsType);
        }

        // GET: EquipmentRequirementsTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EquipmentRequirementsTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,QualityLab,LevelOfImportanc")] EquipmentRequirementsType equipmentRequirementsType)
        {
            if (ModelState.IsValid)
            {
                db.EquipmentRequirementsTypes.Add(equipmentRequirementsType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(equipmentRequirementsType);
        }

        // GET: EquipmentRequirementsTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentRequirementsType equipmentRequirementsType = db.EquipmentRequirementsTypes.Find(id);
            if (equipmentRequirementsType == null)
            {
                return HttpNotFound();
            }
            return View(equipmentRequirementsType);
        }

        // POST: EquipmentRequirementsTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,QualityLab")] EquipmentRequirementsType equipmentRequirementsType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipmentRequirementsType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(equipmentRequirementsType);
        }

        // GET: EquipmentRequirementsTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentRequirementsType equipmentRequirementsType = db.EquipmentRequirementsTypes.Find(id);
            if (equipmentRequirementsType == null)
            {
                return HttpNotFound();
            }
            return View(equipmentRequirementsType);
        }

        // POST: EquipmentRequirementsTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EquipmentRequirementsType equipmentRequirementsType = db.EquipmentRequirementsTypes.Find(id);
            db.EquipmentRequirementsTypes.Remove(equipmentRequirementsType);
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
