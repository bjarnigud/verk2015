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
    public class MessageController : Controller
    {
        //
        // GET: /Message/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendMessage (string message, string reciverId)
        {
            MessageService m = new MessageService();
            var userId = User.Identity.GetUserId();

            m.SendMessage(userId, reciverId, message);

            return RedirectToAction("SearchView", "Search");
            
        }
	}
}