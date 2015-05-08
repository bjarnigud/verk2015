using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SozialWeb.Models;

namespace SozialWeb.Service
{
    public class FriendListService
    {
        public List<ApplicationUser> getFriends(string userId)
        {
            /*
            List<ApplicationUser> friendList = new List<ApplicationUser>();
            ApplicationDbContext db = new ApplicationDbContext();
            var allUsers = from user in db.Users
                           select user;
            */
            ApplicationDbContext db = new ApplicationDbContext();
            var allFriends = from friend in db.FriendLists
                             where userId == friend.friend1.Id
                             select friend.friend2;
            List<ApplicationUser> currentUserFriendsList = new List<ApplicationUser>();

            foreach (ApplicationUser friend in allFriends)
            {
                currentUserFriendsList.Add(friend);                 //ekki góð leið til að gera þetta
            }


            return currentUserFriendsList;
        }

        public void addFriend(string user1, string user2)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ApplicationUser firstUser = new ApplicationUser();
            ApplicationUser secondUser = new ApplicationUser();

            firstUser = db.Users.Where(u => u.Id == user1).SingleOrDefault();
            secondUser = db.Users.Where(u => u.Id == user2).SingleOrDefault();
            var friendConnection = new FriendList
            {
                friend1 = firstUser,
                friend2 = secondUser

            };

            db.FriendLists.Add(friendConnection);
            db.SaveChanges();
            /* for reference
            var user = db.Users.Where(u => u.Id == userId).SingleOrDefault();
            if (user != null)
            {
                var post = new Post
                {
                    text = status,
                    author = user
                };

                db.Posts.Add(post);
                db.SaveChanges();
            }
             * */ 
        }
    }
}