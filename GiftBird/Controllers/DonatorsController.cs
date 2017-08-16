using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GiftBird.Models;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;

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

		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult LoginUser2([Bind(Include = "UserID,Password")] Donator donator, Models.Donator g)
		//{
		//	return Details(donator.ID);//this is the form-redirects to details action page.
		//}

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

        public ActionResult Verify(Models.LoginModels l)
        {
            List<Donator> Donators = db.Donators.ToList();
            Donator curUser = new Donator();
            ViewBag.Okay = "";
            ViewBag.Yay = "";
            ViewBag.U = l.uName;
            ViewBag.P = l.password;

            bool check = false;

            foreach (Donator x in Donators)
            {
                if (x.UserID == l.uName)
                {
                    ViewBag.Okay += "Okay";
                    curUser = x;
                    if (curUser.Password == l.password)
                    {
                        //ViewBag.CurPass = curUser.Password;
                        //ViewBag.LoginPass = l.password;
                        //ViewBag.CurName = curUser.UserID;
                        //ViewBag.LoginName = l.uName;

                        //check = true;
                        //ViewBag.Check = check;
                        return DetailsPage(curUser, l);
                    }
                 
                }
            }

            return LoginUser2();
        }

        public ActionResult DetailsPage(Donator curUser, Models.LoginModels l)
        {
            
            //ViewBag.Yay += "yay";
            //ViewBag.Name = curUser.FirstName + " " + curUser.LastName;
            //ViewBag.CurPass = curUser.Password;
            //ViewBag.LoginPass = l.password;
            return View("DetailsPage");
        }

        public ActionResult LoginUser2()
        {
            return View("LoginUser2");
        }

        public ActionResult SearchView(Models.SearchModel s)
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
            //int categoryOfCare = s.categoryOfCare;

            string url = "https://projects.propublica.org/nonprofits/api/v2/search.json?q=utf8=✓&q=" + searchParams + city + "&state%5Bid%5D=" + state + "&ntee%5Bid%5D=" + "&c_code%5Bid%5D=";

            ViewBag.URL = url;
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


            int uChoice = 3;
            List<string> match = new List<string>();
            List<string> noMatch = new List<string>();

            for (int i = 0; i < o["organizations"].Count(); i++)
            {

                string ntee = o["organizations"][i]["ntee_code"].Value<string>();
                string name = o["organizations"][i]["name"].Value<string>();
                string state = o["organizations"][i]["state"].Value<string>();

                if (ntee != null)
                {
                    int nteeInt = ConvertNtee(ntee);
                    if (nteeInt == uChoice)
                    {
                        match.Add($"{name}   {state}   {ntee}");
                    }
                    else
                    {
                        noMatch.Add($"{name}   {state}   {ntee}");
                    }
                }
            }

            ViewBag.Match = match;
            ViewBag.NoMatch = noMatch;

            return View("SearchView");
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

        public int ConvertNtee(string ntee)
        {
            int convNtee = 0;
            Dictionary<string, int> cats =
            new Dictionary<string, int>();
            cats.Add("A", 1);
            cats.Add("B", 2);
            cats.Add("CD", 3);
            cats.Add("EFGH", 4);
            cats.Add("IJKLMNOP", 5);
            cats.Add("Q", 6);
            cats.Add("RSTUVW", 7);
            cats.Add("X", 8);
            cats.Add("Y", 9);
            cats.Add("Z", 10);

            foreach (string key in cats.Keys)
            {
                if (key.Contains(ntee[0]))
                {
                    convNtee = cats[key];
                    return convNtee;
                }
            }

            return convNtee;
        }


        ////Megan added this on 81317 for the Details Pages.  The two - three methods below will provide details to a detail page that the user can select to add to their registry
        //public string CreateDetailsURL(Models.SearchModel s)
        //{
        //    string searchParams = HttpUtility.UrlEncode(s.searchParams);
        //    string state = HttpUtility.UrlEncode(s.state);
        //    string city = HttpUtility.UrlEncode(s.city);
        //    string zip = HttpUtility.UrlEncode(s.zip);
        //    //int categoryOfCare = s.categoryOfCare;

        //    string url = "https://projects.propublica.org/nonprofits/api/v2/search.json?q=utf8=✓&q=" + searchParams + city + "&state%5Bid%5D=" + state + "&ntee%5Bid%5D=" + "&c_code%5Bid%5D=";

        //    ViewBag.URL = url;
        //    return url;
        //}
        //public ActionResult GetDataDetails(Models.SearchModel s)
        //{
        //    System.Net.HttpWebRequest request = System.Net.WebRequest.CreateHttp(CreateURL(s));
        //    request.UserAgent = @"User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.116 Safari/537.36";
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //    StreamReader rd = new StreamReader(response.GetResponseStream());
        //    string ApiText = rd.ReadToEnd();
        //    JObject o = JObject.Parse(ApiText);
        //    ViewBag.Object = o;


        //    int uChoice = 3;
        //    string matches = "";
        //    string noMatch = "";

        //    for (int i = 0; i < o["organizations"].Count(); i++)
        //    {

        //        string ntee = o["organizations"][i]["ntee_code"].Value<string>();

        //        if (ntee != null)
        //        {
        //            int nteeInt = ConvertNtee(ntee);
        //            if (nteeInt == uChoice)
        //            {
        //                matches += "<li onclick=\"Select(event)\">" + o["organizations"][i]["name"] + "   " + o["organizations"][i]["state"] + "    " + o["organizations"][i]["ntee_code"] + "</li>" + "</br>";
        //            }
        //            else
        //            {
        //                noMatch += "<li onclick=\"Select(event)\">" + o["organizations"][i]["name"] + "   " + o["organizations"][i]["state"] + "    " + o["organizations"][i]["ntee_code"] + "</li>" + "</br>";
        //            }
        //        }
        //    }

        //    ViewBag.Match = matches;
        //    ViewBag.NoMatch = noMatch;

        //    return View("SearchView");
        //}

        //public ActionResult SaveDetails([Bind(Include = "Name,Password")] Donator donator)
        //{
        //    bool matchFound = false;

        //    List<Donator> Donators = db.Donators.ToList();

        //    foreach (Donator x in Donators)
        //    {
        //        if (x.UserID == donator.UserID)
        //        {
        //            matchFound = true;
        //        }
        //    }
        //    return View();
        //}

    }
}

