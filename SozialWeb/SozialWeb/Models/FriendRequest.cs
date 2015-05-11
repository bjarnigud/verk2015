using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SozialWeb.Models
{
    public class FriendRequest
    {
        public int ID { get; set; }
        public virtual ApplicationUser requestSender { get; set; }
        public virtual ApplicationUser requestReciver { get; set; }
    }
}