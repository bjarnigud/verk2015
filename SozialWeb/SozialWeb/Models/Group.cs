using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SozialWeb.Models
{
    public class Group
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<ApplicationUser> Posts { get; set; }
        public List<ApplicationUser> Members { get; set; }
 
    }
}