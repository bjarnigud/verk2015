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

            var firstUser = db.Users.Where(u => u.Id == user1).SingleOrDefault();           //finds sender and reciver
            var secondUser = db.Users.Where(u => u.Id == user2).SingleOrDefault();
            
            if (firstUser == null || secondUser == null)                                    //if either user dosen't exist then return false
            {
                return false;
            }
             
            var message = new Message                                                       //Creates message
            {
                author = firstUser,
                reciver = secondUser,
                timeOfMessage = DateTime.Now,                                               //insert the time when the message was sent
                text = textMessage

            };

            db.Messages.Add(message);
           
            db.SaveChanges();

            return true;
        }

        public List<Message> GetAllMessagesByUserId(string userId)          //finds all messages user has
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var messageList = (from m in db.Messages
                            where m.reciver.Id == userId
                            orderby m.timeOfMessage ascending               //orders the messages in order by time
                            select m).ToList();

            return messageList;
        }

        public bool DeleteMessage(int messageId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var message = db.Messages.Where(m => m.ID == messageId).SingleOrDefault();          //find the message after it id

            if(message == null)                                                                 //if no message is found return false
            {
                return false;
            }
            db.Messages.Remove(message);
            db.SaveChanges();

            return true;
        }


    }
}