using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GiftBird.Models;

namespace GiftBird.Controllers
{
    public class GtGController : Controller
    {
        private GiftBird_DB_v1Entities1 db = new GiftBird_DB_v1Entities1();

        // GET: GtG
        public ActionResult Index()
        {
            return View(db.GrantTheGivers.ToList());
        }

        // GET: GtG/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrantTheGiver grantTheGiver = db.GrantTheGivers.Find(id);
            if (grantTheGiver == null)
            {
                return HttpNotFound();
            }
            return View(grantTheGiver);
        }

        // GET: GtG/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GtG/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Email,Address,City,State,Zip,County,UserID,Password,CategoriesOfCare")] GrantTheGiver grantTheGiver)
        {
            if (ModelState.IsValid)
            {
                db.GrantTheGivers.Add(grantTheGiver);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(grantTheGiver);
        }

        // GET: GtG/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrantTheGiver grantTheGiver = db.GrantTheGivers.Find(id);
            if (grantTheGiver == null)
            {
                return HttpNotFound();
            }
            return View(grantTheGiver);
        }

        // POST: GtG/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Email,Address,City,State,Zip,County,UserID,Password,CategoriesOfCare")] GrantTheGiver grantTheGiver)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grantTheGiver).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grantTheGiver);
        }

        // GET: GtG/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrantTheGiver grantTheGiver = db.GrantTheGivers.Find(id);
            if (grantTheGiver == null)
            {
                return HttpNotFound();
            }
            return View(grantTheGiver);
        }

        // POST: GtG/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GrantTheGiver grantTheGiver = db.GrantTheGivers.Find(id);
            db.GrantTheGivers.Remove(grantTheGiver);
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
