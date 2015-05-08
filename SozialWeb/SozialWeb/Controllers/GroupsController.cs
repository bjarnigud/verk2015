using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SozialWeb.Controllers
{
    public class GroupsController : Controller
    {
        // GET: Groups
        public ActionResult GroupsView()
        {
            return View();
        }
    }
}