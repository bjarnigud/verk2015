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

            var firstUser = db.Users.Where(u => u.Id == user1).SingleOrDefault();
            var secondUser = db.Users.Where(u => u.Id == user2).SingleOrDefault();
            
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

            db.Messages.Add(message);
           
            db.SaveChanges();

            return true;
        }

        public List<Message> GetAllMessagesByUserId(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var messageList = (from m in db.Messages
                            where m.reciver.Id == userId
                            orderby m.timeOfMessage ascending
                            select m).ToList();

            return messageList;
        }

        public bool DeleteMessage(int messageId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var message = db.Messages.Where(m => m.ID == messageId).SingleOrDefault();

            if(message == null)
            {
                return false;
            }
            db.Messages.Remove(message);
            db.SaveChanges();

            return true;
        }
    }
}