using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SozialWeb.Service;


namespace SozialWeb.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult ProfileView()
        {
            return View();
        }

        public ActionResult ProfileTestView()
        {
            return View();
        }

    }
}