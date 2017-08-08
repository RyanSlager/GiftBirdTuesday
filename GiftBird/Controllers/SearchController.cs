using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GiftBird.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData(Models.SearchModel s)
        {
            string url = s.CreateURL();

            System.Net.HttpWebRequest request = System.Net.WebRequest.CreateHttp(url);
            request.UserAgent = @"User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.116 Safari/537.36";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader rd = new StreamReader(response.GetResponseStream());
            string ApiText = rd.ReadToEnd();
            JObject o = JObject.Parse(ApiText);
            ViewBag.Object = o;

            for (int i = 0; i < o["data"]["children"].Count(); i++)
            {
                ViewBag.Posts += "<li>"+o["name"][i]+"</li>";
            }

            return View("Data");
        }

        public ActionResult Data(Models.SearchModel s)
        {
            return GetData(s);
        }
    }
}