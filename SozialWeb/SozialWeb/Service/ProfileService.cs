using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SozialWeb.Models;

namespace SozialWeb.Service
{
    public class ProfileService
    {
        public ApplicationUser GetUser(string id)                   // Finds the profile user
        {
            ApplicationDbContext db = new ApplicationDbContext();   // gets acces to database
            ApplicationUser userToReturn = new ApplicationUser();
           
            var userFromLinq = from user in db.Users                // find users in database
                       where user.Id == id
                       select user;

            foreach (ApplicationUser user in userFromLinq)          // loops to find profile user
            {
                userToReturn = user;                 
            }

            return userToReturn;                                    // retruns the user
               
        }
    }
}