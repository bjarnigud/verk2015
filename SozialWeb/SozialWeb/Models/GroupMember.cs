using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SozialWeb.Models
{
    public class GroupMember
    {
        public int id { get; set; }
        public virtual ApplicationUser groupMember { get; set; }
        public virtual Group group { get; set; }
    }
}