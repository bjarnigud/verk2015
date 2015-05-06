using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SozialWeb.Models
{
    public class Comment
    {
        int ID {get; set;}
        string Text { get; set;}
        int U_ID { get; set; }
        int P_ID { get; set; }
        bool Like { get; set; }
    }
}