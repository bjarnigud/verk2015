using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SozialWeb.Models
{
    public class Message
    {
        public int ID { get; set; }
        public string text { get; set; }
        public virtual ApplicationUser author { get; set; }
        public virtual ApplicationUser reciver { get; set; }
        public DateTime timeOfMessage { get; set; }
       
    }
}