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
            var userId = User.Identity.GetUserId();                     //finnur id innskráðs notanda
            ApplicationDbContext db = new ApplicationDbContext();       

            FriendListService f = new FriendListService();
            var friendsList = f.getFriends(userId);                     //finnur lista af vinum notanda og sendir sem model í viewið
            return View(friendsList);
        }
        

        /*Kallað í þetta ActionResult þegar er klikkað remove friend í FriendListView
         * */
        public ActionResult RemoveFriend(string user2, string returnurl)        //returnurl er síðan sem notandi kom frá
        {
            var userId = User.Identity.GetUserId();
            FriendListService f = new FriendListService();

            f.RemoveFriend(userId, user2);
            return RedirectToAction(returnurl);                                 //notandi sendur aftur þaðan sem hann kom

        }

        public ActionResult SendFriendRequest(string reciverId)
        {
            FriendListService f = new FriendListService();
            var senderId = User.Identity.GetUserId();

            bool test = f.AlreadyFriends(reciverId, senderId);          //Athugar hvort sé verið að senda vinabeiðni á vin sinn, ef svo fer á error síðu
            if (test == true)
            {
                return RedirectToAction("TestError", "Test", new { errorMessage = "You are already friend with that user silly" });
            }

            if (senderId == reciverId)                                  //Athugar hvort sé verið að senda sjálfum sér vinabeiðni og ef svo er  
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