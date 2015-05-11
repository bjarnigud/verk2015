using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SozialWeb.Service;
using SozialWeb.Models;
using Microsoft.AspNet.Identity;

namespace SozialWeb.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        /*
        public ActionResult SearchView()
        {
            var userId = User.Identity.GetUserId();
            SearchService s = new SearchService();
            List<ApplicationUser> users = new List<ApplicationUser>();
            users = s.getUsers(userId);             //laga til svo notandi komi líka???????
            return View(users);
        }
         * */
        /*
        public ActionResult SearchTestView()
        {
            SearchService s = new SearchService();
            List<ApplicationUser> users = new List<ApplicationUser>();
            users = s.findUser("Heimir");

            return View(users);
        }
        */
        public ActionResult SearchView(string searchString)
        {
            SearchService s = new SearchService();
            List<ApplicationUser> users = new List<ApplicationUser>();
            users = s.findUsers(searchString);

            return View(users);
        }
    }
}