using SozialWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SozialWeb.Service
{
    public class MessageService
    {
        public bool SendMessage(string user1, string user2, string textMessage)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ApplicationUser firstUser = new ApplicationUser();
            ApplicationUser secondUser = new ApplicationUser();

            firstUser = db.Users.Where(u => u.Id == user1).SingleOrDefault();
            secondUser = db.Users.Where(u => u.Id == user2).SingleOrDefault();

            if (firstUser == null || secondUser == null)
            {
                return false;
            }
            var message = new Message
            {
                author = firstUser,
                reciver = secondUser,
                timeOfMessage = DateTime.Now,
                text = textMessage

            };


            //db.Message.Add(message);
            db.SaveChanges();

            return true;
        }
    }
}