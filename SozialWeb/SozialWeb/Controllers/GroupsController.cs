using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SozialWeb.Service;
using SozialWeb.Models;
using Microsoft.AspNet.Identity;

namespace SozialWeb.Controllers
{
    public class GroupsController : Controller
    {
        // GET: Groups
        public ActionResult GroupsView()
        {
            GroupService g = new GroupService();
            var groups = g.GetAllGroups();
            return View(groups);
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

        public ActionResult SeeGroupsList()
        {
            GroupService g = new GroupService();
            var groups = g.GetAllGroups();
            return View(groups);
        }

    }
}