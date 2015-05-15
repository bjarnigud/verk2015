using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SozialWeb.Models
{
    public class GroupPostImage
    {
        public int ID { get; set; }
        public string PicUrl { get; set; }
        public virtual ApplicationUser author { get; set; }
        public DateTime timeOfPost { get; set; }
        public virtual Group reciver { get; set; }
    }
}