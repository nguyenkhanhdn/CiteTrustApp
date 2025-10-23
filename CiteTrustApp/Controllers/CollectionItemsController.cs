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
    public class CollectionItemsController : Controller
    {
        private AcademicEvidenceModel db = new AcademicEvidenceModel();

        // GET: CollectionItems
        public ActionResult Index()
        {
            var collectionItems = db.CollectionItems.Include(c => c.Collection).Include(c => c.Evidence);
            return View(collectionItems.ToList());
        }

        // GET: CollectionItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollectionItem collectionItem = db.CollectionItems.Find(id);
            if (collectionItem == null)
            {
                return HttpNotFound();
            }
            return View(collectionItem);
        }

        // GET: CollectionItems/Create
        public ActionResult Create()
        {
            ViewBag.CollectionId = new SelectList(db.Collections, "Id", "Name");
            ViewBag.EvidenceId = new SelectList(db.Evidences, "Id", "Content");
            return View();
        }

        // POST: CollectionItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CollectionId,EvidenceId,AddedAt")] CollectionItem collectionItem)
        {
            if (ModelState.IsValid)
            {
                db.CollectionItems.Add(collectionItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CollectionId = new SelectList(db.Collections, "Id", "Name", collectionItem.CollectionId);
            ViewBag.EvidenceId = new SelectList(db.Evidences, "Id", "Content", collectionItem.EvidenceId);
            return View(collectionItem);
        }

        // GET: CollectionItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollectionItem collectionItem = db.CollectionItems.Find(id);
            if (collectionItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.CollectionId = new SelectList(db.Collections, "Id", "Name", collectionItem.CollectionId);
            ViewBag.EvidenceId = new SelectList(db.Evidences, "Id", "Content", collectionItem.EvidenceId);
            return View(collectionItem);
        }

        // POST: CollectionItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CollectionId,EvidenceId,AddedAt")] CollectionItem collectionItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(collectionItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CollectionId = new SelectList(db.Collections, "Id", "Name", collectionItem.CollectionId);
            ViewBag.EvidenceId = new SelectList(db.Evidences, "Id", "Content", collectionItem.EvidenceId);
            return View(collectionItem);
        }

        // GET: CollectionItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollectionItem collectionItem = db.CollectionItems.Find(id);
            if (collectionItem == null)
            {
                return HttpNotFound();
            }
            return View(collectionItem);
        }

        // POST: CollectionItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CollectionItem collectionItem = db.CollectionItems.Find(id);
            db.CollectionItems.Remove(collectionItem);
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
