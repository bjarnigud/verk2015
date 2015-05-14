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
        
    /*
     *          EYÐA??????????????????
     * */
        public ActionResult FriendsListTestView()
        {
            var userId = User.Identity.GetUserId();
            ApplicationDbContext db = new ApplicationDbContext();

            FriendListService f = new FriendListService();
            var friendsList = f.getNotFriends(userId);
            return View(friendsList);
        }

        /*Kallað í þetta ActionResult þegar er klikkað remove friend í FriendListView
         * */
        public ActionResult RemoveFriend(string user2, string returnurl)        //returnurl er síðan sem notandi kom frá
        {
            var userId = User.Identity.GetUserId();
            FriendListService f = new FriendListService();

            f.removeFriend(userId, user2);
            return RedirectToAction(returnurl);                                 //notandi sendur aftur þaðan sem hann kom

        }
    }
}