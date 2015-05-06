using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SozialWeb.Controllers
{
    public class TestController : Controller
    {
        public string Index() 
        {
            return "This is an index page";
        }

        public string TestPage()
        {
            return "This is a test page";
        }
    }
}