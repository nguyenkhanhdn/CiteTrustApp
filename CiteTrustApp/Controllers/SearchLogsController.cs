using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CiteTrustApp.Models;

namespace CiteTrustApp.Controllers
{
    public class SearchLogsController : Controller
    {
        private AcademicEvidenceModel db = new AcademicEvidenceModel();

        // GET: SearchLogs
        public ActionResult Index()
        {
            var searchLogs = db.SearchLogs.Include(s => s.User);
            return View(searchLogs.ToList());
        }

        // GET: SearchLogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SearchLog searchLog = db.SearchLogs.Find(id);
            if (searchLog == null)
            {
                return HttpNotFound();
            }
            return View(searchLog);
        }

        // GET: SearchLogs/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName");
            return View();
        }

        // POST: SearchLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Keyword,SearchTime")] SearchLog searchLog)
        {
            if (ModelState.IsValid)
            {
                db.SearchLogs.Add(searchLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName", searchLog.UserId);
            return View(searchLog);
        }

        // GET: SearchLogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SearchLog searchLog = db.SearchLogs.Find(id);
            if (searchLog == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName", searchLog.UserId);
            return View(searchLog);
        }

        // POST: SearchLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Keyword,SearchTime")] SearchLog searchLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(searchLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName", searchLog.UserId);
            return View(searchLog);
        }

        // GET: SearchLogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SearchLog searchLog = db.SearchLogs.Find(id);
            if (searchLog == null)
            {
                return HttpNotFound();
            }
            return View(searchLog);
        }

        // POST: SearchLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SearchLog searchLog = db.SearchLogs.Find(id);
            db.SearchLogs.Remove(searchLog);
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
