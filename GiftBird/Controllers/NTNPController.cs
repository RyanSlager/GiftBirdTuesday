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
    public class NTNPController : Controller
    {
        private GiftBird_DB_v1Entities1 db = new GiftBird_DB_v1Entities1();

        // GET: NTNP
        public ActionResult Index()
        {
            return View(db.NanTheNonProfits.ToList());
        }

        // GET: NTNP/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NanTheNonProfit nanTheNonProfit = db.NanTheNonProfits.Find(id);
            if (nanTheNonProfit == null)
            {
                return HttpNotFound();
            }
            return View(nanTheNonProfit);
        }

        // GET: NTNP/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NTNP/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NonProfitName,NonProfitWebsite,ContactName,Address,City,State,Zip,UserID,Password,C501_C_3,CategoryOfCare")] NanTheNonProfit nanTheNonProfit)
        {
            if (ModelState.IsValid)
            {
                db.NanTheNonProfits.Add(nanTheNonProfit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nanTheNonProfit);
        }

        // GET: NTNP/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NanTheNonProfit nanTheNonProfit = db.NanTheNonProfits.Find(id);
            if (nanTheNonProfit == null)
            {
                return HttpNotFound();
            }
            return View(nanTheNonProfit);
        }

        // POST: NTNP/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NonProfitName,NonProfitWebsite,ContactName,Address,City,State,Zip,UserID,Password,C501_C_3,CategoryOfCare")] NanTheNonProfit nanTheNonProfit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nanTheNonProfit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nanTheNonProfit);
        }

        // GET: NTNP/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NanTheNonProfit nanTheNonProfit = db.NanTheNonProfits.Find(id);
            if (nanTheNonProfit == null)
            {
                return HttpNotFound();
            }
            return View(nanTheNonProfit);
        }

        // POST: NTNP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NanTheNonProfit nanTheNonProfit = db.NanTheNonProfits.Find(id);
            db.NanTheNonProfits.Remove(nanTheNonProfit);
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
