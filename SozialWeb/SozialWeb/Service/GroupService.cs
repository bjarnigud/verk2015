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

        public Group GetGroupById(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var group = db.Groups.Where(g => g.ID == id).SingleOrDefault();

            return group;
        }

        public bool JoinGroup(string userId, int groupId)   
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var user = db.Users.Where(u => u.Id == userId).SingleOrDefault();
            var groupToJoin = db.Groups.Where(g => g.ID == groupId).SingleOrDefault();

            var groupMember = new GroupMember{
                                  groupMember = user,
                                  group = groupToJoin
                              };

            db.GroupMembers.Add(groupMember);
            db.SaveChanges();
            return true;
        }

        public bool LeaveGroup(string userId, int groupId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var user = db.Users.Where(u => u.Id == userId).SingleOrDefault();
            var groupMember = db.GroupMembers.Where(g => g.group.ID == groupId && g.groupMember.Id == userId).SingleOrDefault();


            db.GroupMembers.Remove(groupMember);
            db.SaveChanges();
            return true;
        }

        public bool IsAMember(string userId, int groupId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var groupMember = db.GroupMembers.Where(g => g.group.ID == groupId && g.groupMember.Id == userId).SingleOrDefault();
            if(groupMember == null)
            {
                return false;
            }
            return true;
        }
        public List<Group> findGroups(string searchString)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            List<Group> groupsFound = new List<Group>();
            var groups = from g in db.Groups
                         select g;

            if (!String.IsNullOrEmpty(searchString))
            {
                groups = groups.Where(g => g.name.Contains(searchString));
            }

            foreach (Group g in groups)
            {
                groupsFound.Add(g);                 //ekki góð leið til að gera þetta
            }
            return groupsFound;
        }

        public List<GroupPost> getGroupPosts(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var groupPosts = (from g in db.GroupPosts
                             where id == g.groupReciver.ID
                             select g).ToList();
            return groupPosts;
        }

        public List<GroupMember> getGroupMembers(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var groupMembers = (from g in db.GroupMembers
                              // where id == g.group
                               select g).ToList();

            return groupMembers;
        }
    }
}