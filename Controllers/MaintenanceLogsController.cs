using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ATMMonitoringSystem.Models;

namespace ATMMonitoringSystem.Controllers
{
    public class MaintenanceLogsController : Controller
    {
        private ATMContext db = new ATMContext();

        // GET: MaintenanceLogs
        public ActionResult Index()
        {
            var maintenanceLogs = db.MaintenanceLogs.Include(m => m.ATM);
            return View(maintenanceLogs.ToList());
        }

        // GET: MaintenanceLogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintenanceLog maintenanceLog = db.MaintenanceLogs.Find(id);
            if (maintenanceLog == null)
            {
                return HttpNotFound();
            }
            return View(maintenanceLog);
        }

        // GET: MaintenanceLogs/Create
        public ActionResult Create()
        {
            ViewBag.ATMId = new SelectList(db.ATMs, "ATMId", "Location");
            return View();
        }

        // POST: MaintenanceLogs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LogId,ATMId,TechnicianName,Description,MaintenanceDate")] MaintenanceLog maintenanceLog)
        {
            // 🧠 Validation: Prevent assigning same technician to same ATM
            var isAlreadyAssigned = db.MaintenanceLogs
                .Any(m => m.ATMId == maintenanceLog.ATMId && m.TechnicianName == maintenanceLog.TechnicianName);

            if (isAlreadyAssigned)
            {
                TempData["Notice"] = $"Technician '{maintenanceLog.TechnicianName}' is already assigned to ATM #{maintenanceLog.ATMId}.";
                ViewBag.ATMId = new SelectList(db.ATMs, "ATMId", "Location", maintenanceLog.ATMId);
                return View(maintenanceLog);
            }

            if (ModelState.IsValid)
            {
                db.MaintenanceLogs.Add(maintenanceLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ATMId = new SelectList(db.ATMs, "ATMId", "Location", maintenanceLog.ATMId);
            return View(maintenanceLog);
        }

        // GET: MaintenanceLogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintenanceLog maintenanceLog = db.MaintenanceLogs.Find(id);
            if (maintenanceLog == null)
            {
                return HttpNotFound();
            }
            ViewBag.ATMId = new SelectList(db.ATMs, "ATMId", "Location", maintenanceLog.ATMId);
            return View(maintenanceLog);
        }

        // POST: MaintenanceLogs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LogId,ATMId,TechnicianName,Description,MaintenanceDate")] MaintenanceLog maintenanceLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maintenanceLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ATMId = new SelectList(db.ATMs, "ATMId", "Location", maintenanceLog.ATMId);
            return View(maintenanceLog);
        }

        // GET: MaintenanceLogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintenanceLog maintenanceLog = db.MaintenanceLogs.Find(id);
            if (maintenanceLog == null)
            {
                return HttpNotFound();
            }
            return View(maintenanceLog);
        }

        // POST: MaintenanceLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MaintenanceLog maintenanceLog = db.MaintenanceLogs.Find(id);
            db.MaintenanceLogs.Remove(maintenanceLog);
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
