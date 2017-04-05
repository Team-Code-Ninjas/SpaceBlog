using Microsoft.AspNet.Identity.EntityFramework;
using SpaceBlog.Models;
using System.Data.Entity;

namespace SpaceBlog.Models
{
    public class BlogDBContext : IdentityDbContext<ApplicationUser>
    {
        public BlogDBContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static BlogDBContext Create()
        {
            return new BlogDBContext();
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Comment> Comments { get; set;  }
        public virtual DbSet<Contact> Contacts { get; set; }
    }
}