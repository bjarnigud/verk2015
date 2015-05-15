using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SozialWeb.Models;
using Microsoft.AspNet.Identity;
using SozialWeb.Service;

namespace SozialWeb.Controllers
{   [Authorize]
    public class FriendsListController : Controller
    {
        public ActionResult FriendsListView()
        {           
            var userId = User.Identity.GetUserId();                     //Finds ID from signed in user
            ApplicationDbContext db = new ApplicationDbContext();       

            FriendListService f = new FriendListService();
            var friendsList = f.getFriends(userId);                     //Finds list of users friend and sends it as a model to the view
            return View(friendsList);
        }
        
        public ActionResult RemoveFriend(string user2, string returnurl)        //returnurl is the site that the user came from
        {
            var userId = User.Identity.GetUserId();
            FriendListService f = new FriendListService();

            f.RemoveFriend(userId, user2);
            return View();

        }

        public ActionResult SendFriendRequest(string reciverId)
        {
            FriendListService f = new FriendListService();
            var senderId = User.Identity.GetUserId();

            bool test = f.AlreadyFriends(reciverId, senderId);          //Checks if it is sending a friend request to friend if so goes to a error side
            if (test == true)
            {
                return RedirectToAction("TestError", "Test", new { errorMessage = "You are already friend with that user silly" });
            }

            if (senderId == reciverId)                                  //Checks if it is sending a refuest to it self if so it goes to a error side, because it is sad to send your self a friend request  
            {                                                          // fer notandi á villusíðu vegna þess að það er sorglegt að senda sjálfum sér vinabeiðni
                return RedirectToAction("TestError", "Test", new { errorMessage = "Can't send a friend request to yourself" });
            }
            bool already;
            already = f.AlreadyFriendRequest(senderId, reciverId);
            if (f.AlreadyFriendRequest(senderId, reciverId))
            {
                return RedirectToAction("TestError", "Test", new { errorMessage = "Friend request already sent, be patient" });
            }

            f.SendFriendRequest(senderId, reciverId);

            return View();
        }

        public ActionResult SeeFriendRequests()
        {
            FriendListService fr = new FriendListService();
            List<FriendRequest> userRequests = new List<FriendRequest>();
            var userId = User.Identity.GetUserId();
            userRequests = fr.GetAllFriendRequestsReciver(userId);
            return View(userRequests);
        }

        public ActionResult AcceptFriendRequest(int id)
        {
            FriendListService fr = new FriendListService();
            FriendRequest f = fr.GetFriendListById(id);
            fr.addFriend(f.requestReciver.Id, f.requestSender.Id);
            fr.DeleteFriendRequest(id);
            return View();
        }
    }
}