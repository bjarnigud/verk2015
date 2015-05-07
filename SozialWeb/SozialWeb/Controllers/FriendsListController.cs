using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SozialWeb.Controllers
{
    public class FriendsListController : Controller
    {
        // GET: FriendsList
        public ActionResult Index()
        {
            return View();
        }
    }
}