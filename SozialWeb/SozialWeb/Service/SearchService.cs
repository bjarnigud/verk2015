﻿using SozialWeb.Models;
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

        public List<ApplicationUser> findUser(string searchString)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            List<ApplicationUser> usersFound = new List<ApplicationUser>();
            var users = from u in db.Users
                         select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.Name.Contains(searchString));
            }
            //usersFound = (List<ApplicationUser>)users;


            foreach (ApplicationUser user in users)
            {
                usersFound.Add(user);                 //ekki góð leið til að gera þetta
            }

            return usersFound;
        }
    }
}