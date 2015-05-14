using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SozialWeb.Service;
using SozialWeb.Models;
using Microsoft.AspNet.Identity;

namespace SozialWeb.Controllers
{   [Authorize]
    public class MessageController : Controller
    {

        public ActionResult Index()
        {
            MessageService m = new MessageService();
            var userId = User.Identity.GetUserId();
            var list = m.GetAllMessagesByUserId(userId);
            
            return View(list);
        }

        public ActionResult SendMessage (string message, string reciverId, string returnurl)
        {
            MessageService m = new MessageService();
            var userId = User.Identity.GetUserId();

            m.SendMessage(userId, reciverId, message);

            //return RedirectToAction("SearchView", "Search");
            return Redirect(returnurl);
            
        }

        public ActionResult DeleteMessage(int? messageId, string returnUrl)
        {
            MessageService m = new MessageService();
            int id;
            if(messageId == null)
            {
                return RedirectToAction("TestError", "Test", new { errorMessage = "Can't delete message" });
            }
            id = (int)messageId;
            m.DeleteMessage(id);
            return RedirectToAction(returnUrl);
        }
	}
}