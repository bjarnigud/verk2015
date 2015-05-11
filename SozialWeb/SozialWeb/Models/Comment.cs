using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SozialWeb.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string text { get; set; }
        public virtual ApplicationUser user { get; set; }
        public Post commentPost { get; set; }
        public bool like { get; set; }
        public DateTime timeOfComment { get; set; }
    }
}