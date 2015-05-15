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
            return RedirectToAction("ProfileTestView", new { id = userId });
        }

        public ActionResult ProfileTestView(string id, string url)
        {
            ProfileService p = new ProfileService();
            ApplicationUser user = new ApplicationUser();
            FriendListService f = new FriendListService();
            PostService ps = new PostService();
            user = p.GetUser(id);
            var userId = User.Identity.GetUserId();
            if(!f.AlreadyFriends(id, userId) && userId != id)
            {
                return RedirectToAction("TestError", "Test", new { errorMessage = "You can only view profiles of users that you are friends with" });
            }
            var posts = ps.GetPosts(id);
            ViewBag.Posts = posts;
            ViewBag.Images = ps.GetPics(id);
            return View(user);
        }

        public ActionResult TestHome()
        {
            PostService p = new PostService();
            var userId = User.Identity.GetUserId();
            IEnumerable<Post> model = p.GetPosts(userId);

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
                k.AddPic(userId, model.PicUrl);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AddPostImage(string url, string reciverId)
        {

                PostImageService p = new PostImageService();
                var userId = User.Identity.GetUserId();
                p.AddImage(url, reciverId, userId);
                return RedirectToAction("ProfileTestView", new { id = reciverId });
        }

    }
}