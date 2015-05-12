using SozialWeb.Models;
using SozialWeb.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace SozialWeb.Controllers
{
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
            return RedirectToAction("TestHome");
        }

        public ActionResult SendFriendRequest(string reciverId)
        {
            FriendListService f = new FriendListService();
            var senderId = User.Identity.GetUserId();

            f.sendFriendRequest(senderId, reciverId);

            return View();
        }

        public ActionResult SeeFriendRequests()
        {
            FriendListService fr = new FriendListService();
            List <FriendRequest> userRequests = new List <FriendRequest>();
            var userId = User.Identity.GetUserId();
            userRequests =  fr.getAllFriendRequestsReciver(userId);
            return View(userRequests);
        }

        public ActionResult AcceptFriendRequest(int id)
        {
            FriendListService fr = new FriendListService();
            FriendRequest f = fr.getFriendListById(id);
            fr.addFriend(f.requestReciver.Id, f.requestSender.Id);
            fr.deleteFriendRequest(id);
            return View();
        }
    }
}