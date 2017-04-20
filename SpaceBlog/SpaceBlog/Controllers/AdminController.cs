namespace SpaceBlog.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using SpaceBlog.Models;

    [Authorize(Roles = "Administrators")]
    public class AdminController : Controller
    {
        private BlogDBContext db = new BlogDBContext();

        // GET: Admin/Index
        public ActionResult Index()
        {
            var userViewModels = db.Users
                .Select(UserToUserViewModel)
                .ToArray();

            return View(userViewModels);
        }

        // GET: Admin/Suspend/5
        public ActionResult Suspend(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = db.Users.Find(id);

            /*
            The Administartor should not be able to suspend himself
            if (user.Roles.Contains("Administrators"))
            {
                return RedirectToAction("Index");
            }
            */

            return View(user);
        }

        // POST: Admin/Suspend/5
        [HttpPost, ActionName("Suspend")]
        public ActionResult SuspendConfirmed(string id)
        {
            var user = db.Users.Find(id);
            user.Suspended = true;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Admin/Activate/5
        public ActionResult Activate(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = db.Users.Find(id);

            return View(user);
        }

        // POST: Admin/Activate/5
        [HttpPost, ActionName("Activate")]
        public ActionResult ActivateConfirmed(string id)
        {
            var user = db.Users.Find(id);
            user.Suspended = false;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Details(string id)
        {
            var user = db.Users.Find(id);

            var userModel = new UserViewModel();

            var allArticles = db.Articles.Include(a => a.Author).ToList();
            var allComments = db.Comments.Include(a => a.Author).ToList();

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

        private UserViewModel UserToUserViewModel(ApplicationUser user)
        {
            var db = new BlogDBContext();

            var userViewModel = new UserViewModel();

            userViewModel.Id = user.Id;
            userViewModel.UserName = user.UserName;
            userViewModel.FullName = user.FullName;

            userViewModel.ArticlesCreated = db
                .Articles
                .Include(a => a.Author)
                .Count(a => a.Author.Id == user.Id);

            userViewModel.CommentsMade = db
                .Comments
                .Include(a => a.Author)
                .Count(a => a.Author.Id == user.Id);

            userViewModel.DateRegistered = user.DateRegistered;

            /*
            var userRoles = user.Roles;
            
            if (userRoles.Contains("Moderators"))
            {
                userViewModel.UserType = "Moderator";
            }
            else if (userRoles.Contains("Administrators"))
            {
                userViewModel.UserType = "Admin";
            }
            else
            {
                userViewModel.UserType = "User";
            }
            */

            userViewModel.Status = user.Suspended ? Status.Suspended : Status.Active;

            return userViewModel;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}