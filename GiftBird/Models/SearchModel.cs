using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiftBird.Models
{
    public class SearchModel
    {
        public string searchParams { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string categoryOfCare { get; set; }

        public string CreateURL()
        {
            searchParams = HttpUtility.UrlEncode(searchParams);
            //state = HttpUtility.UrlEncode(state);
            zip = HttpUtility.UrlEncode(zip);
            categoryOfCare = HttpUtility.UrlEncode(categoryOfCare);
            string url = "https://projects.propublica.org/nonprofits/api/v2/search.json?q=utf8=✓&q="+searchParams+ "state%5Bid%5D="+state+;



        }
    }


}