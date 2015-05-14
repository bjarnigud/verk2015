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
        // GET: Groups
        public ActionResult GroupsView(string searchString)
        {
            GroupService g = new GroupService();
            if(searchString == "")
            {
                var groups = g.GetAllGroups();
                return View(groups);
            }
            var groups2 = g.findGroups(searchString);
            var userId = User.Identity.GetUserId();
            ViewBag.Groups = g.getUserGroups(userId);
        
            return View(groups2);
        }
      
        [HttpPost]
        public ActionResult CreateNewGroup(string name, string description)
        {
            GroupService g = new GroupService();


                //model.UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                //CommentsRepository.Instance.AddComment(model);

                var userId = User.Identity.GetUserId();
                g.CreateGroup(name, description, userId);

                
       
            return RedirectToAction("GroupsView");
        }

        public ActionResult SeeGroupsList(int id)
        {
            GroupService g = new GroupService();
            var group = g.GetGroupById(id);
           
            return View(group);
        }

        public ActionResult GroupProfile (int groupId)
        {
            GroupService g = new GroupService();
            Group group = new Group();
            List<ApplicationUser> members = new List<ApplicationUser>();
            members = g.getGroupMembers(groupId);
            ViewBag.Posts = g.getGroupPosts(groupId);
            //ViewData["Members"] = members;
            ViewBag.Members = members;
           
   
            group = g.GetGroupById(groupId);
            return View(group);
        }

        public ActionResult JoinGroup(int groupId)
        {
            
            GroupService g = new GroupService();
            var userId = User.Identity.GetUserId();
            if(g.IsAMember(userId, groupId) == true)
            {
                return RedirectToAction("TestError", "Test", new { errorMessage = "Can't join the group more than once" });
            }
            g.JoinGroup(userId, groupId);
            return RedirectToAction("GroupsView");
        }

        public ActionResult LeaveGroup(int groupId)
        {
            GroupService g = new GroupService();
            var userId = User.Identity.GetUserId();
            if (g.IsAMember(userId, groupId) == false)
            {
                return RedirectToAction("TestError", "Test", new { errorMessage = "Can't leave a group if you're not a member" });
            }
            g.LeaveGroup(userId, groupId);
            return RedirectToAction("GroupsView");
        }
        public ActionResult SearchView(string searchString)
        {
            GroupService g = new GroupService();
            List<Group> groups = new List<Group>();
            var groupPosts = g.findGroups(searchString);

            return View(groups);
        }

        public ActionResult GroupMembers(int ID)
        {
            GroupService g = new GroupService();
            var members = g.getGroupMembers(ID);
            return View(members);
        }

        [HttpPost]
        public ActionResult AddPost(GroupPost model, int groupId)
        {
            
           // if (ModelState.IsValid)
           // {
                //model.UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                //CommentsRepository.Instance.AddComment(model);
               // PostService p = new PostService();
                GroupService g = new GroupService();
                var userId = User.Identity.GetUserId();
               // g.addGroupPost(model.text, model.groupReciver.ID, userId);
                g.addGroupPost(model.text, groupId, userId);
                //p.addStatus(userId, model.text);
           // }
            return RedirectToAction("GroupProfile", new { groupId = groupId});
        }
    }
}