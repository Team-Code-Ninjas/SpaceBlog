namespace SpaceBlog.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using SpaceBlog.Models;

    public class UserController : Controller
    {
        private BlogDBContext db = new BlogDBContext();

        // GET: User
        [ActionName("Profile")]
        public ActionResult Index()
        {
            var user = db.Users.Find(User.Identity.GetUserId());

            var userModel = new UserViewModel();

            var allArticles = db
                .Articles
                .Include(a => a.Author)
                .ToList();

            var allComments = db
                .Comments
                .Include(a => a.Author)
                .ToList();

            userModel.Id = user.Id;
            userModel.UserName = user.UserName;
            userModel.FullName = user.FullName;

            userModel.Articles = allArticles
                .Where(b => b.Author == user)
                .ToList();

            userModel.Comments = allComments
                .Where(b => b.Author == user)
                .ToList();

            userModel.DateRegistered = user.DateRegistered;

            return View(userModel);
        }
    }
}