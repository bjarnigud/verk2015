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
                    author = user
                };

                db.Posts.Add(post);
                db.SaveChanges();
            }
        }
    }
}