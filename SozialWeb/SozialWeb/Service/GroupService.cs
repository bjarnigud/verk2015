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
        public void CreateGroup(string groupName, string description, string picture, string userId)    //when user creates a new group
        {
            ApplicationDbContext db = new ApplicationDbContext();
           
            var user = db.Users.Where(u => u.Id == userId).SingleOrDefault();
            var group = new Group
            {
                name = groupName,
                creator = user,
                descriptionOfGroup = description,
                groupPicLocation = picture
            };

            db.Groups.Add(group);
            db.SaveChanges();
            JoinGroup(userId, group.ID);                                //so the creator becomes joins group automatically
        }

        public List<Group> GetAllGroups()                               //getting all groups in alphabetical order
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var groups = (from g in db.Groups
                          orderby g.name
                         select g).ToList();

            return groups;
        }

        public Group GetGroupById(int id)                               //Getting group by group id
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var group = db.Groups.Where(g => g.ID == id).SingleOrDefault();

            return group;
        }

        public bool JoinGroup(string userId, int groupId)   
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var user = db.Users.Where(u => u.Id == userId).SingleOrDefault();           //finding user
            var groupToJoin = db.Groups.Where(g => g.ID == groupId).SingleOrDefault();  //finding group to join

            var groupMember = new GroupMember{                                          //creating group member
                                  groupMember = user,
                                  group = groupToJoin
                              };

            db.GroupMembers.Add(groupMember);
            db.SaveChanges();
            return true;
        }

        public bool LeaveGroup(string userId, int groupId)                              //when user leaves group
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var user = db.Users.Where(u => u.Id == userId).SingleOrDefault();
            var groupMember = db.GroupMembers.Where(g => g.group.ID == groupId          //finding user and group
                && g.groupMember.Id == userId).SingleOrDefault();


            db.GroupMembers.Remove(groupMember);                                        //removes from database
            db.SaveChanges();                                                           //save changes to database
            return true;
        }

        public bool IsAMember(string userId, int groupId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var groupMember = db.GroupMembers.Where(g => g.group.ID == groupId          //finding user and group
                && g.groupMember.Id == userId).SingleOrDefault();
            if(groupMember == null)                                                     //if search returns empty then he is not a member and returns false
            {
                return false;
            }
            return true;
        }
        public List<Group> FindGroups(string searchString)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            List<Group> groupsFound = new List<Group>();
            var groups = from g in db.Groups
                        // orderby g.name
                         select g;

            if (!String.IsNullOrEmpty(searchString))
            {
                groups = groups.Where(g => g.name.Contains(searchString));
            }

            foreach (Group g in groups)
            {
                groupsFound.Add(g);                 //
            }
            return groupsFound;
        }

        public List<GroupPost> GetGroupPosts(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var groupPosts = (from g in db.GroupPosts
                             where id == g.groupReciver.ID
                             orderby g.timeOfPost descending                //return newest post at the top in the view
                             select g).ToList();
            return groupPosts;
        }

        public List<ApplicationUser> GetGroupMembers(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            List<ApplicationUser> gr = new List<ApplicationUser>();
            var groupMembers = (from g in db.Users
                                orderby g.Name
                               select g).ToList();

            foreach(ApplicationUser member in groupMembers)                 //finds all users
            {
               
                if (IsAMember(member.Id, id) == true)       //if the user is a member in the group then he is added to gr
                {
                    gr.Add(member);
                }
            }

            return gr;
        }

        public List<Group> GetUserGroups(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            List<Group> groups = new List<Group>();
            
           
            var allGroups = from g in db.Groups
                            select g;

            foreach (Group group in allGroups)                 //finnur alla notendur sem eru í þessum hóp
            {

                if (IsAMember(userId, group.ID) == true)       //gert svona vegna tæknilegra erfiðleika
                {
                    groups.Add(group);
                }
            }
            return groups;
        }

        public bool AddGroupPost(string status, int groupId, string userId)     
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var user = db.Users.Where(u => u.Id == userId).SingleOrDefault();       //finding user and group
            var group = db.Groups.Where(g => g.ID == groupId).SingleOrDefault();
            if (user != null)
            {
                var groupPost = new GroupPost                                       //creating new group post
                {
                    text = status,
                    author = user,
                    timeOfPost = DateTime.Now,
                    groupReciver = group
                    

                };
                db.GroupPosts.Add(groupPost);                                       //saving groupPost to database
                db.SaveChanges();
                return true;                                                        // returns true if success
            }
            return false;
        }
    }
}