using SozialWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SozialWeb.Service
{
    public class SearchService
    {
        public List<ApplicationUser> getUsers(string userId)
        {
            List<ApplicationUser> allUserList = new List<ApplicationUser>();
            ApplicationDbContext db = new ApplicationDbContext();
            var allUsers = from user in db.Users
                           where user.Id != userId
                           select user;

            foreach (ApplicationUser user in allUsers)
            {
                allUserList.Add(user);                 //ekki góð leið til að gera þetta
            }

            return allUserList;
        }
    }
}