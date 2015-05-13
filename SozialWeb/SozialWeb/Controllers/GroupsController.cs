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
            return View(groups2);
        }
      
        [HttpPost]
        public ActionResult CreateNewGroup(string name)
        {
            GroupService g = new GroupService();


                //model.UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                //CommentsRepository.Instance.AddComment(model);
                PostService p = new PostService();
                var userId = User.Identity.GetUserId();
                g.CreateGroup(name, userId);
       
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
            groups = g.findGroups(searchString);

            return View(groups);
        }
    }
}