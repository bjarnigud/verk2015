using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SozialWeb.Models
{
    public class FriendRequest
    {
        public int ID { get; set; }
        public ApplicationUser requestSender { get; set; }
        public ApplicationUser requestReciver { get; set; }
    }
}