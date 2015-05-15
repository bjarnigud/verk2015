using SozialWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SozialWeb.Service
{
    public class SearchService
    {
        public List<ApplicationUser> GetUsers(string userId)                 // gets all users
        {
            List<ApplicationUser> allUserList = new List<ApplicationUser>(); // gets acces to database
            ApplicationDbContext db = new ApplicationDbContext();
            var allUsers = from user in db.Users
                           where user.Id != userId
                           select user;

            foreach (ApplicationUser user in allUsers)
            {
                allUserList.Add(user);                 
            }

            return allUserList;
        }

        public List<ApplicationUser> FindUsers(string searchString)         // finds uers
        {
            ApplicationDbContext db = new ApplicationDbContext();           // gets acces to database
            List<ApplicationUser> usersFound = new List<ApplicationUser>(); 
            var users = from u in db.Users                          
                         select u;

            if (!String.IsNullOrEmpty(searchString))                        // checks if string is empty
            {
                users = users.Where(u => u.Name.Contains(searchString));    // Finds user
            }

            foreach (ApplicationUser user in users)
            {
                usersFound.Add(user);    
            
            }                                                           // adds the users found
                    
            return usersFound;      
                                                                    // returns the user thats was found
        }

        // laga til nöfn á föllum
        public ApplicationUser FindUser(string id)                          // finds user
        {
            ApplicationDbContext db = new ApplicationDbContext();           // gets acces to database
            ApplicationUser userReturned = new ApplicationUser();
            var user = from u in db.Users
                       where u.Id == id                                     // finds user in database
                       select u;
        

        foreach (ApplicationUser u in user)                                 //  loops in users
            {
                userReturned = u;                                           // returns user      
            }

        return userReturned;                                                // returns user
        }
        

    }
}