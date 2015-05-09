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

        public bool addFriend(string user1, string user2)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ApplicationUser firstUser = new ApplicationUser();
            ApplicationUser secondUser = new ApplicationUser();

            firstUser = db.Users.Where(u => u.Id == user1).SingleOrDefault();
            secondUser = db.Users.Where(u => u.Id == user2).SingleOrDefault();

            if(firstUser == null || secondUser == null)
            {
                return false;
            }
            var friendConnection = new FriendList
            {
                friend1 = firstUser,
                friend2 = secondUser

            };

            db.FriendLists.Add(friendConnection);
            db.SaveChanges();

            return true;
        }

        public bool removeFriend(string user1, string user2)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            FriendList friendList = new FriendList();
            friendList = db.FriendLists.Where(f => f.friend1.Id == user1 && f.friend2.Id == user2).SingleOrDefault();

            if(friendList == null)
            {
                return false;
            }
            db.FriendLists.Remove(friendList);
            db.SaveChanges();

            return true;
        }

        public List<ApplicationUser> getNotFriends(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var allUsers = (from user in db.Users
                            select user);
            var allFriends = (from friend in db.FriendLists
                             where userId == friend.friend1.Id
                             select friend.friend2);
            
            List<ApplicationUser> currentUserFriendsList = new List<ApplicationUser>();

            foreach (ApplicationUser user in allUsers)
            {
                var user2 = from f in db.FriendLists
                            where userId == f.friend1.Id && user.Id == f.friend2.Id
                            select f;
                if(user2 != null)
                {
                    currentUserFriendsList.Add(user);                 //ekki góð leið til að gera þetta
                }

                int a = currentUserFriendsList.Count();
            }


            return currentUserFriendsList;
        }
    }
}