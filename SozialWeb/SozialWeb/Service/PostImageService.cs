using SozialWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SozialWeb.Service
{
    public class PostImageService
    {
        public void AddImage(string imageUrl, string userId, string reciverId)
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

        public List<PostImage> GetImages(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var user = db.Users.Where(u => u.Id == userId).SingleOrDefault();

            var images = (from i in db.PostImages
                         where i.reciver.Id == userId
                         select i).ToList();

            return images;

        }

        public void AddGroupImage(string imageUrl, string userId, int groupId)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var user = db.Users.Where(u => u.Id == userId).SingleOrDefault();
            var reciver = db.Groups.Where(r => r.ID == groupId).SingleOrDefault();

            if (user != null || reciver != null)
            {
                var image = new GroupPostImage
                {
                    author = user,
                    PicUrl = imageUrl,
                    reciver = reciver,
                    timeOfPost = DateTime.Now
                };
                db.GroupPostImages.Add(image);
                db.SaveChanges();
            }

        }

        public List<GroupPostImage> GetGroupImages(int groupId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var user = db.Groups.Where(g => g.ID == groupId).SingleOrDefault();

            var images = (from i in db.GroupPostImages
                          where i.reciver.ID == groupId
                          select i).ToList();

            return images;

        }
    }
}