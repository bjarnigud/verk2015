using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SozialWeb.Models
{
    public class Group
    {
        public int ID { get; set; }
        public string name { get; set; }
        public List<ApplicationUser> posts { get; set; }        //bæta við virtual???????
        public List<ApplicationUser> members { get; set; }
 
    }
}