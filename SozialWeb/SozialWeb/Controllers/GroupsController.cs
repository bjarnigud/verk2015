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
                return View(groups);                            //birtir alla notendur ef það er engin leitar strengur settur inn(kannski óþarfi)
            }
            var groups2 = g.findGroups(searchString);           //finnur alla hópa sem hafa leitarstrenginn í nafninu
            var userId = User.Identity.GetUserId();
            ViewBag.Groups = g.getUserGroups(userId);           //tekur alla hópa sem notandi er skráður í og setur í viewbag til að birta í view
        
            return View(groups2);
        }
      
        //Býr til nýjan hóp í groupView í gegnum group service. Innskráður notandi verður stofnandi hópsins og fyrsti meðlimur
        [HttpPost]
        public ActionResult CreateNewGroup(string name, string description, string picture)
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

        /* Profile view fyrir hópa, tekur inn id á hóp og birtir profile fyrir þann hóp
         * */
        public ActionResult GroupProfile (int groupId)
        {
            GroupService g = new GroupService();
            Group group = new Group();
            var userId = User.Identity.GetUserId();
            if(!g.IsAMember(userId, groupId))
            {
                return RedirectToAction("TestError", "Test", new { errorMessage = "Can't view profiles of groups when your not a member" });
            }

            List<ApplicationUser> members = new List<ApplicationUser>();
            members = g.getGroupMembers(groupId);                           //finnur alla meðlimi hópsins to setur í viewbag til að loopa í gegn í viewinu
            ViewBag.Posts = g.getGroupPosts(groupId);                       //finnur alla pósta á svæði hópsins og setur í viewbag til að sýna
           
            ViewBag.Members = members;

            group = g.GetGroupById(groupId);                                //finnur hópinn
            return View(group);
        }
    
        //Kallað í þetta í GroupsView til að ganga í hóp    
        public ActionResult JoinGroup(int groupId)
        {
            
            GroupService g = new GroupService();
            var userId = User.Identity.GetUserId();
            if(g.IsAMember(userId, groupId) == true)
            {
                return RedirectToAction("TestError", "Test", new { errorMessage = "Can't join the group more than once" });
                //sendir notanda á villu síðu ef hann er nú þegar hópmeðlimur
            }
            g.JoinGroup(userId, groupId);
            return RedirectToAction("GroupsView");      //fer aftur á GroupsView eftir að hópur er stofnaður
        }

        // Kallað á þetta þegar notandi velur leave group í groupsview
        public ActionResult LeaveGroup(int groupId)
        {
            GroupService g = new GroupService();
            var userId = User.Identity.GetUserId();
            if (g.IsAMember(userId, groupId) == false)
            {
                return RedirectToAction("TestError", "Test", new { errorMessage = "Can't leave a group if you're not a member" });
                //Ef notandi reynir að hætta í hóp sem hann er ekki meðlimur í fer hann á villu síðu
            }
            g.LeaveGroup(userId, groupId);                  //hættir í hóp
            return RedirectToAction("GroupsView");
        }

        //Athuga hvort meigi eyða!!!!!!!!!!!!!!!!!!!!!
        public ActionResult SearchView(string searchString)
        {
            GroupService g = new GroupService();
            List<Group> groups = new List<Group>();
            var groupPosts = g.findGroups(searchString);

            return View(groups);
        }
        

        // ATH HVORT MEIGI EYÐA????????????????
        public ActionResult GroupMembers(int ID)
        {
            GroupService g = new GroupService();
            var members = g.getGroupMembers(ID);
            return View(members);
        }
        

        //Býr til groupPost
        [HttpPost]
        public ActionResult AddPost(GroupPost model, int groupId)
        {
            
          
                GroupService g = new GroupService();
                var userId = User.Identity.GetUserId();
                if(model == null)
                {
                    return RedirectToAction("TestError", "Test", new { errorMessage = "There was an error, probably our fault" });
                }
                g.addGroupPost(model.text, groupId, userId);

            return RedirectToAction("GroupProfile", new { groupId = groupId});      //fær aftur á groupProfile og setur inn groupId sem færibreytu
        }

        [HttpPost]
        public ActionResult AddGroupPostImage(string url, int groupId)
        {

            PostImageService p = new PostImageService();
            var userId = User.Identity.GetUserId();
            //k.addPic(userId, model.PicUrl);
            p.addGroupImage(url, userId, groupId);
            //}
            //return View(model);
            return RedirectToAction("GroupProfile", new { id = groupId });
        }
    }
}