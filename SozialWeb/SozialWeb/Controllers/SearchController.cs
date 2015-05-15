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

        [Authorize]
        public ActionResult SearchView(string searchString)
        {
            SearchService s = new SearchService();
            List<ApplicationUser> users = new List<ApplicationUser>();
            users = s.FindUsers(searchString);

            return View(users);
        }
    }
}