using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SozialWeb.Models;
using Microsoft.AspNet.Identity;

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

            var friendConnection2 = new FriendList
            {
                friend1 = secondUser,
                friend2 = firstUser

            };

            db.FriendLists.Add(friendConnection);
            db.FriendLists.Add(friendConnection2);
            db.SaveChanges();

            return true;
        }

        //?????????????? - þarf kannski ekki og virkar líklegast ekki
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

        public void sendFriendRequest(string sender, string reciver)
        {
           
            ApplicationDbContext db = new ApplicationDbContext();
            SearchService s = new SearchService();

             
            var user1 = db.Users.Where(u => u.Id == sender).SingleOrDefault();
            var user2 = db.Users.Where(u => u.Id == reciver).SingleOrDefault();

            string user1id = user1.Id;
            string user2id = user2.Id;
          

            var friendRequest = new FriendRequest
            {
                requestSender = user1,
                requestReciver = user2
            };

            db.FriendRequests.Add(friendRequest);
            db.SaveChanges();
        }

        public List<FriendRequest> getAllFriendRequestsReciver(string userId)
        {
            List<FriendRequest> fr = new List<FriendRequest>();
            ApplicationDbContext db = new ApplicationDbContext();

            var allRequests = from f in db.FriendRequests
                              where userId == f.requestReciver.Id
                              select f;

            foreach (FriendRequest requests in allRequests)
            {
                fr.Add(requests);                 
            }

            return fr;
        }

        public List<FriendRequest> getAllFriendRequestsSender(string userId)
        {
            List<FriendRequest> fr = new List<FriendRequest>();
            ApplicationDbContext db = new ApplicationDbContext();

            var allRequests = from f in db.FriendRequests
                              where userId == f.requestSender.Id
                              select f;

            foreach (FriendRequest requests in allRequests)
            {
                fr.Add(requests);            
            }

            return fr;
        }

        public FriendRequest getFriendListById(int id) //?id ????'
        {
             ApplicationDbContext db = new ApplicationDbContext();
            var friendRequest = db.FriendRequests.Where(f => f.ID == id).SingleOrDefault();

            return friendRequest;

        }

        public void deleteFriendRequest(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var friendRequest = db.FriendRequests.Where(f => f.ID == id).SingleOrDefault();

            db.FriendRequests.Remove(friendRequest);
            db.SaveChanges();
            
        }

        public bool alreadyFriends(string user1id, string user2id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var user1 = db.Users.Where(u => u.Id == user1id).SingleOrDefault();
            var user2 = db.Users.Where(u => u.Id == user2id).SingleOrDefault();

            var find = from f in db.FriendLists
                        where user1id == f.friend1.Id 
                        && user2id == f.friend2.Id
                            select f;
            if(find.Count() == 0)
            {
                return false;
            }
            return true;
        }

        public bool alreadyFriendRequest(string user1, string user2)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var find = from f in db.FriendRequests
                       where user1 == f.requestReciver.Id && user2 == f.requestSender.Id
                       select f;

            if (find.Count() == 0)
            {
                return false;
            }

            return true;
        }

        public bool removeFriend(string user1, string user2)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var friend = (from f in db.FriendLists
                         where f.friend1.Id == user1 && f.friend2.Id == user2
                         select f).SingleOrDefault();

            var friend2 = (from f in db.FriendLists
                         where f.friend1.Id == user1 && f.friend2.Id == user2
                         select f).SingleOrDefault();

            if(friend == null || friend2 == null)
            {
                return false;
            }

            db.FriendLists.Remove(friend);
            db.FriendLists.Remove(friend2);
            db.SaveChanges();

            return true;
        }
    }
}