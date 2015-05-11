using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SozialWeb.Models
{
    public class FriendList
    {
        public int ID { get; set; }
        public virtual ApplicationUser friend1 { get; set; }
        public virtual ApplicationUser friend2 { get; set; }
    }
}