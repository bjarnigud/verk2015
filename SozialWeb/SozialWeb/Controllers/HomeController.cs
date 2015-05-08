using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SozialWeb.Service;

namespace SozialWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string status)
        {
            PostService s = new PostService();

            s.addStatus("TODO: sækja notandaID", status);

            return RedirectToAction("...")
        }
 
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            //Yolo fyrir bernie

            return View();
        }
      
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}