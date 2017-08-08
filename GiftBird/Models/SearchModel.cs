using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GiftBird.Models
{
    public class SearchModel
    {   
        [Key]
        public string searchParams { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string categoryOfCare { get; set; }

        public string CreateURL()
        {
            searchParams = HttpUtility.UrlEncode(searchParams);
            //state = HttpUtility.UrlEncode(state);
            city = HttpUtility.UrlEncode(city);
            zip = HttpUtility.UrlEncode(zip);
            Dictionary<string, int> category = new Dictionary<string, int>();
            category.Add("Arts, Culture & Humanities", 1);
            category.Add("Education", 2);
            category.Add("Environment and Animals", 3);
            category.Add("Health", 4);
            category.Add("Human Services", 5);
            category.Add("International, Foreign Affairs", 6);
            category.Add("Public, Societal Benefit", 7);
            category.Add("Religion Related", 8);
            category.Add("Mutual/Membership Benefit", 9);

            int careCategory = category[categoryOfCare];

            string url = "https://projects.propublica.org/nonprofits/api/v2/search.json?q=utf8=✓&q=" + searchParams + "%2B" + city + "state%5Bid%5D=" + state + "ntee%5Bid%5D=" + careCategory;

            return url;

        }
    }


}