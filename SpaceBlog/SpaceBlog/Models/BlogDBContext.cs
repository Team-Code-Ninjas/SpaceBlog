namespace SpaceBlog.Models
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class BlogDBContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Article> Articles { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<Contact> Contacts { get; set; }

        public BlogDBContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static BlogDBContext Create()
        {
            return new BlogDBContext();
        }
    }
}