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
                CreateUser(db, "alex@gmail.com", "Alexander", "123");
                CreateUser(db, "admin@gmail.com", "Admin Adminov", "123456");

                db.SaveChanges();
            }

            if (!db.Roles.Any())
            {
                CreateRole(db, "Administrators");
                CreateRole(db, "Moderators");

                AddUserToRole(db, "admin@gmail.com", "Administrators");

                db.SaveChanges();
            }

            if (!db.Articles.Any())
            {
                CreateArticle(db,
                    "Mars Volcano Died at Same Time As Dinosaurs", // Title
                    "Around the same time that the dinosaurs became extinct on Earth, a volcano on Mars went dormant, NASA researchers have learned. Arsia Mons is the southernmost volcano in a group of three massive Martian volcanoes known collectively as Tharsis Montes. Until now, the volcano's history has remained a mystery. But thanks to a new computer model, scientists were finally able to figure out when Arsia Mons stopped spewing out lava. According to the model, volcanic activity at Arsia Mons came to a halt about 50 million years ago. Around that same time, Earth experienced the Cretaceous - Paleogene extinction event, which wiped out three-quarters of its animal and plant species, including the dinosaurs.", // Content
                    DateTime.Now, // Date
                    db.Users.FirstOrDefault(u => u.UserName == "alex@gmail.com")); // Author

                db.SaveChanges();
            }

            if (!db.Ratings.Any())
            {
                var ratingAuthorId = db.Users.First().Id;
                var ratingValue = 4;
                var articleId = db.Articles.First().Id;

                CreateRating(db, ratingAuthorId, ratingValue, articleId);
            }

            db.SaveChanges();
        }

        private void CreateRating(BlogDBContext db, string userID, decimal ratingValue, int articleId)
        {
            var rating = new Rating()
            {
                AuthorId = userID,
                Value = ratingValue,
                ArticleId = articleId
            };

            db.Ratings.Add(rating);

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
                FullName = fullName,
                Suspended = false,
                DateRegistered = DateTime.Now
            };

            var userCreateResult = userManager.Create(user, password);
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userCreateResult.Errors));
            }
        }

        private void CreateRole(BlogDBContext db, string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(db));
            var roleCreateResult = roleManager.Create(new IdentityRole(roleName));
            if (!roleCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", roleCreateResult.Errors));
            }
        }

        private static void AddUserToRole(BlogDBContext db, string userName, string roleName)
        {
            var user = db.Users.First(u => u.UserName == userName);
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(db));
            var addAdminRoleResult = userManager.AddToRole(user.Id, roleName);

            if (!addAdminRoleResult.Succeeded)
            {
                throw new Exception(string.Join("; ", addAdminRoleResult.Errors));
            }
        }
    }
}
