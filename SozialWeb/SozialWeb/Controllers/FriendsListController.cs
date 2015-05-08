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
            ApplicationDbContext db = new ApplicationDbContext();
            /*
            var friendsList = from user in db.Users
                              join friend in db.FriendLists on user.Id equals friend.friend1.Id into something
                              where userId equals user.Id
                              select new { Cate = something };
          
            */
            var friendsList = from user in db.Users
                              select user;

            var variabel = "HELLO";
            return View(friendsList);
        }
    }
}