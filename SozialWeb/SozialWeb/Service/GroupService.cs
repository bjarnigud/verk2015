using SozialWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;


namespace SozialWeb.Service
{
    public class GroupService
    {
        public void CreateGroup(string groupName, string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
           
            var user = db.Users.Where(u => u.Id == userId).SingleOrDefault();
            var group = new Group
            {
                name = groupName,
                creator = user
            };

            db.Groups.Add(group);
            db.SaveChanges();
        }

        public List<Group> GetAllGroups()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var groups = (from g in db.Groups
                         select g).ToList();

            return groups;
        }
    }
}