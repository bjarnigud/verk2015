using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SozialWeb.Models;
using Microsoft.AspNet.Identity;

namespace SozialWeb.Controllers
{
    public class FriendsListController : Controller
    {
        // GET: FriendsList
        public ActionResult FriendsListView()
        {
            return View();
        }

        public ActionResult FriendsListTestView()
        {
            
            var userId = User.Identity.GetUserId();
            List<ApplicationUser> friends = new List<ApplicationUser>();
            ApplicationDbContext db = new ApplicationDbContext();

            var friendsList = from n in db.Users
                                  select n;
            var variabel = "HELLO";
            return View(friendsList);
        }
    }
}