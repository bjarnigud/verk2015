using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SozialWeb.Models
{
    public class FriendList
    {
        public int ID { get; set; }
        public ApplicationUser friend1 { get; set; }
        public ApplicationUser friend2 { get; set; }
    }
}