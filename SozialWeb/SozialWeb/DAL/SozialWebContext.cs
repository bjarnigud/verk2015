using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SozialWeb.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SozialWeb.DAL
{
    public class SozialWebContext : DbContext
    {
         public SozialWebContext() : base("SozialWebContext")
        {
        }
        
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupPost> GroupPosts { get; set; }
        public DbSet<Post>  Posts { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}