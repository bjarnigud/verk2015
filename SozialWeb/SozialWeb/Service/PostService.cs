using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SozialWeb.Models;

namespace SozialWeb.Service
{
    public class PostService
    {
        public void addStatus(string userId, string status)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var user = db.Users.Where(u => u.Id == userId).SingleOrDefault();
            if (user != null)
            {
                var post = new Post
                {
                    text   = status,
                    author = user,
                    timeOfPost = DateTime.Now
                   
                };

                db.Posts.Add(post);
                db.SaveChanges();
            }
        }

        public List <Post> getPosts(string userId)
        {
             ApplicationDbContext db = new ApplicationDbContext();
             List<Post> Posts = new List<Post>();

             var postList = (from p in db.Posts
                             where p.author.Id == userId
                             orderby p.timeOfPost descending
                             select p).ToList();

             return postList;
        }
    }
}