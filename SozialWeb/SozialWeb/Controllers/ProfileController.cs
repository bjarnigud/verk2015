using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SozialWeb.Service;
using SozialWeb.Models;


namespace SozialWeb.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult ProfileView()
        {
            return View();
        }

        public ActionResult ProfileTestView(string id)
        {
            //id = "452ef5e8-2c90-4845-aeb3-66842e6a6469";
            ProfileService p = new ProfileService();
            ApplicationUser user = new ApplicationUser();
            user = p.getUser(id);
            return View(user);
        }

    }
}