using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SozialWeb.Service;
using Microsoft.AspNet.Identity;

namespace SozialWeb.Controllers
{   [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            PostService p = new PostService();
            ViewBag.Posts = p.GetNewestPosts(userId);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string status)
        {
            PostService s = new PostService();
            var userId = User.Identity.GetUserId();
            s.AddStatus(userId, status);

            return RedirectToAction("index");
        }

        public ActionResult TestingPost()
        {
            PostService s = new PostService();
            var userId = User.Identity.GetUserId();
            string status = "This is a status";
            s.AddStatus(userId, status);
            return View();
        }

        public ActionResult TestingPostList()
        {
            PostService p = new PostService();
            var userId = User.Identity.GetUserId();
            var posts = p.GetPosts(userId);
            return View(posts);
        }
 
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            //Yolo fyrir bernie

            return View();
        }
      
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}