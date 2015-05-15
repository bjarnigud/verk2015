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
        public List<ApplicationUser> getFriends(string userId)      //gets all friends of particular user
        {
 
            ApplicationDbContext db = new ApplicationDbContext();   //gets access to database
            var allFriends = (from friend in db.FriendLists
                             where userId == friend.friend1.Id
                             orderby friend.friend2.Name            //get users in alphabetical order
                              select friend.friend2).ToList() ;


            return allFriends;
        }

        public bool addFriend(string user1, string user2)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var firstUser = db.Users.Where(u => u.Id == user1).SingleOrDefault();       //finding first user
            var secondUser = db.Users.Where(u => u.Id == user2).SingleOrDefault();      //finding second user

            if(firstUser == null || secondUser == null)                                 //if either user does not exit then return false
            {
                return false;
            }
            var friendConnection = new FriendList                                       // user 1 is a friend of user 2
            {
                friend1 = firstUser,
                friend2 = secondUser

            };

            var friendConnection2 = new FriendList                                      // user 2 is a friend of user 1
            {
                friend1 = secondUser,
                friend2 = firstUser

            };

            db.FriendLists.Add(friendConnection);                                       //writeing to database
            db.FriendLists.Add(friendConnection2);
            db.SaveChanges();

            return true;
        }

        public void SendFriendRequest(string sender, string reciver)        //sending friend request to other user
        {
           
            ApplicationDbContext db = new ApplicationDbContext();
            SearchService s = new SearchService();

             
            var user1 = db.Users.Where(u => u.Id == sender).SingleOrDefault();      //finding users
            var user2 = db.Users.Where(u => u.Id == reciver).SingleOrDefault();
          

            var friendRequest = new FriendRequest                                  //creating requests
            {
                requestSender = user1,
                requestReciver = user2
            };

            db.FriendRequests.Add(friendRequest);
            db.SaveChanges();
        }

        public List<FriendRequest> GetAllFriendRequestsReciver(string userId)       //Finding all requests user has recived
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var allRequests = (from f in db.FriendRequests
                              where userId == f.requestReciver.Id
                              orderby f.requestReciver.Name
                              select f).ToList();

            return allRequests;
        }

        public List<FriendRequest> GetAllFriendRequestsSender(string userId)         //Finding all friend requests user has sent
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var allRequests = (from f in db.FriendRequests
                              where userId == f.requestSender.Id
                              orderby f.requestSender.Name
                              select f).ToList();

            return allRequests;
        }

        public FriendRequest GetFriendListById(int id) //?id ????'
        {
             ApplicationDbContext db = new ApplicationDbContext();
            var friendRequest = db.FriendRequests.Where(f => f.ID == id).SingleOrDefault();

            return friendRequest;

        }

        public void DeleteFriendRequest(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var friendRequest = db.FriendRequests.Where(f => f.ID == id).SingleOrDefault();

            db.FriendRequests.Remove(friendRequest);
            db.SaveChanges();
            
        }

        public bool AlreadyFriends(string user1id, string user2id)
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

        public bool AlreadyFriendRequest(string user1, string user2)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var find = (from f in db.FriendRequests
                       where user2 == f.requestReciver.Id && user1 == f.requestSender.Id
                       select f).ToList();

            if (find.Count() == 0)
            {
                return false;
            }

            return true;
        }

        public bool RemoveFriend(string user1, string user2)
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