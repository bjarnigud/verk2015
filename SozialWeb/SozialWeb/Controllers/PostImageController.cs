using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SozialWeb.Controllers
{
    public class PostImageController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PostImage(string reciverId, string url)
        {
            return View();
        }


	}
}