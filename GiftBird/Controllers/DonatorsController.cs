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
    public class DonatorsController : Controller
    {
        private GiftBird_DB_v1Entities2 db = new GiftBird_DB_v1Entities2();

        // GET: Donators
        public ActionResult Index()
        {
            return View(db.Donators.ToList());
        }

        // GET: Donators/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donator donator = db.Donators.Find(id);
            if (donator == null)
            {
                return HttpNotFound();
            }
            return View(donator);
        }

        // GET: Donators/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Donators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Email,Address,City,State,Zip,UserID,Password,Catagories")] Donator donator)
        {
            if (ModelState.IsValid)
            {
                db.Donators.Add(donator);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(donator);
        }

        // GET: Donators/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donator donator = db.Donators.Find(id);
            if (donator == null)
            {
                return HttpNotFound();
            }
            return View(donator);
        }

        // POST: Donators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Email,Address,City,State,Zip,UserID,Password,Catagories")] Donator donator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donator);
        }

        // GET: Donators/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donator donator = db.Donators.Find(id);
            if (donator == null)
            {
                return HttpNotFound();
            }
            return View(donator);
        }

        // POST: Donators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Donator donator = db.Donators.Find(id);
            db.Donators.Remove(donator);
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
