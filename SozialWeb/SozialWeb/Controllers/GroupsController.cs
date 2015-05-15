using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SozialWeb.Service;
using SozialWeb.Models;
using Microsoft.AspNet.Identity;

namespace SozialWeb.Controllers
{   [Authorize]
    public class GroupsController : Controller
    {
       
        //Aðalviewið fyrir groups
        public ActionResult GroupsView(string searchString)
        {
            GroupService g = new GroupService();
            if(searchString == "")
            {
                var groups = g.GetAllGroups();
                return View(groups);                            //Shows allt the user if no search string is put it
            }
            var groups2 = g.FindGroups(searchString);           //Finds allt the groups that have string in their name
            var userId = User.Identity.GetUserId();
            ViewBag.Groups = g.GetUserGroups(userId);           //Takes all the grouos that are sigends in and adds them to viewbag to show in the view
        
            return View(groups2);
        }
        [HttpPost]
        public ActionResult CreateNewGroup(string name, string description, string picture) //Makes a new group in the groupview trough group service. Signed in user has to be author and first member
        {
            GroupService g = new GroupService();

                var userId = User.Identity.GetUserId();
                g.CreateGroup(name, description, picture, userId);
                
            return RedirectToAction("GroupsView");
        }

        public ActionResult SeeGroupsList(int id)
        {
            GroupService g = new GroupService();
            var group = g.GetGroupById(id);
           
            return View(group);
        }

        public ActionResult GroupProfile (int groupId)  // Profile view for groups , takes in id for a group and shows a profile form that group
        {
            GroupService g = new GroupService();
            PostImageService p  = new PostImageService();
            //Group group = new Group();
            var userId = User.Identity.GetUserId();
            if(!g.IsAMember(userId, groupId))
            {
                return RedirectToAction("TestError", "Test", new { errorMessage = "Can't view profiles of groups when you're not a member" });
            }

            List<ApplicationUser> members = new List<ApplicationUser>();
            members = g.GetGroupMembers(groupId);                           //Finds all the members og the grouops and puts them in viewbag to loop trough the view
            ViewBag.Posts = g.GetGroupPosts(groupId);                       //Finds all the posts on the groups profile and puts them in viewbag to show them
           
            ViewBag.Members = members;
            ViewBag.Images = p.GetGroupImages(groupId);

            var group = g.GetGroupById(groupId);                                //Finds the group
            return View(group);
        }
    
        public ActionResult JoinGroup(int groupId) // This is called in groupview to join a group
        {
            
            GroupService g = new GroupService();
            var userId = User.Identity.GetUserId();
            if(g.IsAMember(userId, groupId) == true)
            {
                return RedirectToAction("TestError", "Test", new { errorMessage = "Can't join the group more than once" });
                //Sends a error to the user ef he is already a group member
            }
            g.JoinGroup(userId, groupId);
            return RedirectToAction("GroupsView");      //Goes back to groupview after the group has been created
        }

        public ActionResult LeaveGroup(int groupId) // this is called when the user leaves group in groupView
        {
            GroupService g = new GroupService();
            var userId = User.Identity.GetUserId();
            if (g.IsAMember(userId, groupId) == false)
            {
                return RedirectToAction("TestError", "Test", new { errorMessage = "Can't leave a group if you're not a member" });
                //If a user tries to leave a group he is not a member of he gets a error message
            }
            g.LeaveGroup(userId, groupId);                  //leaves the group
            return RedirectToAction("GroupsView");
        }

        
        public ActionResult SearchView(string searchString)
        {
            GroupService g = new GroupService();
            List<Group> groups = new List<Group>();
            var groupPosts = g.FindGroups(searchString);

            return View(groups);
        }

        public ActionResult GroupMembers(int ID)
        {
            GroupService g = new GroupService();
            var members = g.GetGroupMembers(ID);
            return View(members);
        }
        
        [HttpPost]
        public ActionResult AddPost(GroupPost model, int groupId) // makes a grouppost
        {
            
          
                GroupService g = new GroupService();
                var userId = User.Identity.GetUserId();
                if(model == null)
                {
                    return RedirectToAction("TestError", "Test", new { errorMessage = "There was an error, probably our fault" });
                }
                g.AddGroupPost(model.text, groupId, userId);

            return RedirectToAction("GroupProfile", new { groupId = groupId});      //Goes back to groupProfile and puts in groupId as member varible
        }

        [HttpPost]
        public ActionResult AddGroupPostImage(string url, int groupId)
        {

            PostImageService p = new PostImageService();
            var userId = User.Identity.GetUserId();
            p.AddGroupImage(url, userId, groupId);
          
            return RedirectToAction("GroupProfile", new { groupId = groupId });
        }
    }
}