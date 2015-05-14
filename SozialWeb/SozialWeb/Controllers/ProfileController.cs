﻿using System;
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
        public ActionResult ProfileView()
        {
            PostService p = new PostService();
            var userId = User.Identity.GetUserId();
            IEnumerable<Post> model = p.getPosts(userId);

            return View(model);
        }

        public ActionResult ProfileTestView(string id)
        {
            ProfileService p = new ProfileService();
            ApplicationUser user = new ApplicationUser();
            FriendListService f = new FriendListService();
            user = p.getUser(id);
            var userId = User.Identity.GetUserId();
            if(!f.alreadyFriends(id, userId) && userId != id)
            {
                return RedirectToAction("TestError", "Test", new { errorMessage = "You can only view profiles of users that are friends with" });
            }
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
        public ActionResult AddPost(Post model)
        {
            var test = model;
            if (ModelState.IsValid)
            {
                //model.UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                //CommentsRepository.Instance.AddComment(model);
                PostService p = new PostService();
                var userId = User.Identity.GetUserId();
                p.addStatus(userId, model.text);
            }
            return RedirectToAction("ProfileView");
        }

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

    }
}