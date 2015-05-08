using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SozialWeb.Models
{
    public class Post
    {
        public int ID { get; set; }
        public string text { get; set; }
        public virtual ApplicationUser author { get; set; }
    }
}