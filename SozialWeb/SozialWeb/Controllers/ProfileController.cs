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
            //id = "452ef5e8-2c90-4845-aeb3-66842e6a6469";
            ProfileService p = new ProfileService();
            ApplicationUser user = new ApplicationUser();
            user = p.getUser(id);
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

    }
}