using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SozialWeb.Models;
using Microsoft.AspNet.Identity;
using SozialWeb.Service;

namespace SozialWeb.Controllers
{
    public class FriendsListController : Controller
    {
        // GET: FriendsList
        /*
        public ActionResult FriendsListView()
        {
            return View();
        }
        */
        public ActionResult FriendsListView()
        {
            
            var userId = User.Identity.GetUserId();
            ApplicationDbContext db = new ApplicationDbContext();

            FriendListService f = new FriendListService();
            var friendsList = f.getFriends(userId);
            return View(friendsList);
        }
    }
}