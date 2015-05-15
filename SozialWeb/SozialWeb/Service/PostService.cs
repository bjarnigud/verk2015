using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SozialWeb.Models;

namespace SozialWeb.Service
{
    public class PostService
    {
        public void AddStatus(string userId, string status)                      // Lets user add status
        {
            ApplicationDbContext db = new ApplicationDbContext();               // get acces to the database

            var user = db.Users.Where(u => u.Id == userId).SingleOrDefault();
            if (user != null)
            {
                var post = new Post                                              // creates the status
                {
                    text   = status,
                    author = user,
                    timeOfPost = DateTime.Now
                   
                };

                db.Posts.Add(post);
                db.SaveChanges();
            }
        }

        public List <Post> GetPosts(string userId)
        {
             ApplicationDbContext db = new ApplicationDbContext();
             List<Post> Posts = new List<Post>();

             var postList = (from p in db.Posts
                             where p.reciver.Id == userId
                             orderby p.timeOfPost descending
                             select p).ToList();

             return postList;
        }

        public void AddPic (string userId, string image)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var user = db.Users.Where(u => u.Id == userId).SingleOrDefault();
            if (user != null)
            {
                var postImage = new PostImage
                {
                    PicUrl = image,
                    author = user,
                    timeOfPost = DateTime.Now
                };

                db.PostImages.Add(postImage);
                db.SaveChanges();
            }
        }

        public List <PostImage> GetPics(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
             List<PostImage> ImagePosts = new List<PostImage>();

           var postImageList = (from p in db.PostImages
                             where p.author.Id == userId
                             orderby p.timeOfPost descending
                             select p).ToList();

           return postImageList;
        }

        public void addStatus(string UserId, string status, string reciverId)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var user = db.Users.Where(u => u.Id == UserId).SingleOrDefault();
            var reciver = db.Users.Where(r => r.Id == reciverId).SingleOrDefault();
            if (user != null)
            {
                var post = new Post
                {
                    text = status,
                    author = user,
                    timeOfPost = DateTime.Now,
                    reciver = reciver

                };

                db.Posts.Add(post);
                db.SaveChanges();
            }
        }

        public IEnumerable<Post> GetNewestPosts(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            FriendListService f = new FriendListService();
            List<Post> newestPost = new List<Post>();
            var allPost = (from p in db.Posts
                           orderby p.timeOfPost descending
                          select p).ToList();

            foreach(Post p in allPost)
            {
                if(f.AlreadyFriends(p.author.Id, userId))
                {
                    newestPost.Add(p);
                }
            }
           
            
            var tenPosts = newestPost.Take(10);
            return tenPosts;

        }

        public List<PostImage> GetImages(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var user = db.Users.Where(u => u.Id == userId).SingleOrDefault();

            var images = (from i in db.PostImages
                          where i.reciver.Id == userId
                          orderby i.timeOfPost descending
                          select i).ToList();

            return images;

        }
   
    }
}