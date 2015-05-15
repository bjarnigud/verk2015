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
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult ProfileView(string id)
        {
            PostService p = new PostService();
            var userId = User.Identity.GetUserId();
            //IEnumerable<Post> model = p.getPosts(userId);
           // IEnumerable<Post> model = p.getPosts(id);
            return RedirectToAction("ProfileTestView", new { id = userId });
            //return View(model);
        }

        public ActionResult ProfileTestView(string id, string url)
        {
            ProfileService p = new ProfileService();
            ApplicationUser user = new ApplicationUser();
            FriendListService f = new FriendListService();
            PostService ps = new PostService();
            user = p.getUser(id);
            var userId = User.Identity.GetUserId();
            if(!f.alreadyFriends(id, userId) && userId != id)
            {
                return RedirectToAction("TestError", "Test", new { errorMessage = "You can only view profiles of users that you are friends with" });
            }
            var posts = ps.getPosts(id);
            ViewBag.Posts = posts;
            ViewBag.Images = ps.getPics(id);
            return View(user);
        }

        public ActionResult TestHome()
        {
            PostService p = new PostService();
            var userId = User.Identity.GetUserId();
            IEnumerable<Post> model = p.getPosts(userId);

            return View(model);
        }

        [HttpPost]
        public ActionResult AddPost(Post model, string reciverId)
        {
            var test = model;
            if (ModelState.IsValid)
            {
                PostService p = new PostService();
                var userId = User.Identity.GetUserId();
                p.addStatus(userId, model.text, reciverId);
            }
            //return RedirectToAction("ProfileView", new { id = reciverId });
            return RedirectToAction("ProfileTestView", new { id = reciverId });
        }
        
        [HttpPost]
        public ActionResult AddImage(PostImage model)
        {
            var image = model;
            if(ModelState.IsValid)
            {
                PostService k = new PostService();
                var userId = User.Identity.GetUserId();
                k.addPic(userId, model.PicUrl);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AddPostImage(string url, string reciverId)
        {
            //var image = model;
            //if (ModelState.IsValid)
            //{
                //PostService k = new PostService();
                PostImageService p = new PostImageService();
                var userId = User.Identity.GetUserId();
                //k.addPic(userId, model.PicUrl);
                p.addImage(url, userId, reciverId);
            //}
            //return View(model);
            return RedirectToAction("ProfileTestView", new { id = reciverId });
        }

    }
}