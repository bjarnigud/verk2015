using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SozialWeb.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public int U_ID { get; set; }
        public int P_ID { get; set; }
        public bool Like { get; set; }
    }
}