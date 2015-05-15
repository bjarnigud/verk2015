using SozialWeb.Models;
using SozialWeb.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace SozialWeb.Controllers
{   [Authorize]
    public class TestController : Controller
    {
        public string Index() 
        {
            return "This is an index page";
        }

        public string TestPage()
        {
            return "This is a test page";
        }

        public ActionResult TestImage()
        {
            return View();
        }

        public ActionResult TestHome()
        {
            PostService p = new PostService();
            var userId = User.Identity.GetUserId();
            IEnumerable<Post> model = p.GetPosts(userId);
         
            return View(model);
        }

        public ActionResult TestError(string errorMessage)
        {
            ViewBag.message = errorMessage;

            return View();
        }
    }
}