using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SozialWeb.Service;
using Microsoft.AspNet.Identity;

namespace SozialWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string status)
        {
            PostService s = new PostService();
            var userId = User.Identity.GetUserId();
            s.addStatus(userId, status);

            return RedirectToAction("index");
        }

        public ActionResult TestingPost()
        {
            PostService s = new PostService();
            var userId = User.Identity.GetUserId();
            string status = "This is a status";
            s.addStatus(userId, status);
            return View();
        }

        public ActionResult TestingPostList()
        {
            PostService p = new PostService();
            var userId = User.Identity.GetUserId();
            var posts = p.getPosts(userId);
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