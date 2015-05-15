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
        public string descriptionOfGroup { get; set; }
        public string groupPicLocation { get; set; }
        public virtual List<ApplicationUser> posts { get; set; }        
        public virtual List<ApplicationUser> members { get; set; }
        public virtual ApplicationUser creator { get; set; }
 
    }
}