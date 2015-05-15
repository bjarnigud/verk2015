using SozialWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SozialWeb.Service
{
    public class PostImageService
    {
        public void addImage(string imageUrl, string userId, string reciverId)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var user = db.Users.Where(u => u.Id == userId).SingleOrDefault();
            var reciver = db.Users.Where(r => r.Id == reciverId).SingleOrDefault();

            if(user != null)
            {
                var image = new PostImage
                {
                    author = user,
                    PicUrl = imageUrl,
                    reciver = reciver,
                    timeOfPost = DateTime.Now
                };
                db.PostImages.Add(image);
                db.SaveChanges();
            }

        }

        public List<PostImage> getImages(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var user = db.Users.Where(u => u.Id == userId).SingleOrDefault();

            var images = (from i in db.PostImages
                         where i.reciver.Id == userId
                         select i).ToList();

            return images;

        }
 
    }
}