using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SozialWeb.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SozialWeb.DAL
{
    public class SozialWebContext : DbContext
    {
        public SozialWebContext()
            : base("DefaultConnection")
        {

        }
        
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupPost> GroupPosts { get; set; }
        public DbSet<Post>  Posts { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}