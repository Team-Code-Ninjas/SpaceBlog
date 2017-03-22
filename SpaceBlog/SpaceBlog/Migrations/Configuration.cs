namespace SpaceBlog.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BlogDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BlogDBContext db)
        {
            if (!db.Users.Any())
            {
                CreateUser(db, "a@a.com", "Gosho", "123");
                db.SaveChanges();
            }

            if (!db.Articles.Any())
            {
                CreateArticle(db, "My new article be", "this is the content", DateTime.Now, db.Users.First());
            }

            db.SaveChanges();
        }

        private void CreateArticle(BlogDBContext db, string title, string content, DateTime date, ApplicationUser author)
        {
            var article = new Article()
            {
                Title = title,
                Content = content,
                Date = date,
                Author = author
            };

            db.Articles.Add(article);
        }

        private void CreateUser(BlogDBContext db, string email, string fullName, string password)
        {
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(db))
            {
                PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 1,
                    RequireNonLetterOrDigit = false,
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireUppercase = false,
                }
            };

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = fullName
            };

            var userCreateResult = userManager.Create(user, password);
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userCreateResult.Errors));
            }
        }
    }
}
