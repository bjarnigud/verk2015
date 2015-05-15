using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SozialWeb.Models;

namespace SozialWeb.Service
{
    public class ProfileService
    {
        public ApplicationUser getUser(string id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ApplicationUser userToReturn = new ApplicationUser();
           
            var userFromLinq = from user in db.Users
                       where user.Id == id
                       select user;

            foreach (ApplicationUser user in userFromLinq)
            {
                userToReturn = user;                 
            }

            return userToReturn;
               
        }
    }
}