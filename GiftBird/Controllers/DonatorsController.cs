﻿using System;
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

        public ActionResult SearchPage(Models.SearchModel s)
        {
            ViewBag.items = MakeList();
            ViewBag.URL = CreateURL(s);
            ViewBag.State = s.state;

            return GetData(s);
        }

        public string CreateURL(Models.SearchModel s)
        {
            string searchParams = HttpUtility.UrlEncode(s.searchParams);
            string state = HttpUtility.UrlEncode(s.state);
            string city = HttpUtility.UrlEncode(s.city);
            string zip = HttpUtility.UrlEncode(s.zip);
            int categoryOfCare = s.categoryOfCare;

            string url = "https://projects.propublica.org/nonprofits/api/v2/search.json?q=utf8=✓&q=" + searchParams + city + "&state%5Bid%5D=" + state + "&ntee%5Bid%5D=" + categoryOfCare + "&c_code%5Bid%5D=";

            return url;
        }

        public ActionResult GetData(Models.SearchModel s)
        {
            System.Net.HttpWebRequest request = System.Net.WebRequest.CreateHttp(CreateURL(s));
            request.UserAgent = @"User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.116 Safari/537.36";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader rd = new StreamReader(response.GetResponseStream());
            string ApiText = rd.ReadToEnd();
            JObject o = JObject.Parse(ApiText);
            ViewBag.Object = o;

            for (int i = 0; i < o["organizations"].Count(); i++)
            {
                ViewBag.Posts += o["organizations"][i]["name"] + "   " + o["organizations"][i]["state"] + "<br>";
            }
            return View("SearchPage");
        }


        public List<SelectListItem> MakeList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Arts, Culture & Humanities", Value = "0", Selected = true });
            items.Add(new SelectListItem { Text = "Education", Value = "1" });
            items.Add(new SelectListItem { Text = "Environment and Animals", Value = "2" });
            items.Add(new SelectListItem { Text = "Health", Value = "3" });
            items.Add(new SelectListItem { Text = "Human Services", Value = "4" });
            items.Add(new SelectListItem { Text = "International, Foreign Affairs", Value = "5" });
            items.Add(new SelectListItem { Text = "Public, Societal Benefit", Value = "6" });
            items.Add(new SelectListItem { Text = "Religion Related", Value = "7" });
            items.Add(new SelectListItem { Text = "Mutual/Membership Benefit", Value = "8" });

            return items;

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
        //public ActionResult DonProfile(Models.GiftBird2 r)
        //{
        //    ViewBag.Message = "Hello," + r.FirstName + "!";
        //    ViewBag.Name = r.FirstName;
        //    return View(r);
        //}
    }
}
