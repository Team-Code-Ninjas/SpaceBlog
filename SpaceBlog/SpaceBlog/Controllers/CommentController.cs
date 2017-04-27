namespace SpaceBlog.Models
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    public class CommentController : Controller
    {
        private BlogDBContext db = new BlogDBContext();

        // GET: Comments
        public ActionResult Index()
        {
            return View(db.Comments.ToList());
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(CommentViewModel commentViewModel)
        {
            if (!ModelState.IsValid)
            {
                /// return PartialView("_CommentBox", commentViewModel);
                return RedirectToAction("Details", "Article", new { id = commentViewModel.ArticleId });
            }

            var article = db
                .Articles
                .Find(commentViewModel.ArticleId);

            if (article == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"Invalid article specified.");
            }

            var author = db.Users.Find(User.Identity.GetUserId());

            if (author == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"Invalid comment author specified.");
            }

            var comment = new Comment
            {
                Article = article,
                Author = author,
                Date = DateTime.Now,
                Content = commentViewModel.Content,
            };

            db.Comments.Add(comment);
            db.SaveChanges();

            return RedirectToAction("Details", "Article", new { id = commentViewModel.ArticleId });
        }

        // GET: Comments/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = db
                .Comments
                .Include(m => m.Author)
                .Include(m => m.Article)
                .ToList()
                .SingleOrDefault(m => m.Id == id);
                
            if (comment == null)
            {
                return HttpNotFound();
            }

            var currentUserId = User.Identity.GetUserId();

            var isAuthenticatedForTheAction = currentUserId ==
                comment.Author.Id ||
                User.IsInRole("Administrators") ||
                User.IsInRole("Moderators");

            if (!isAuthenticatedForTheAction)
            {
               return RedirectToAction($"Details/{comment.Article.Id}", "Article"); // should display "You are not authorized to do that" view
            }

            var commentToDisplay = new CommentViewModel
            {
                ArticleId = comment.Article.Id,
                AuthorId = comment.Author.Id,
                Content = comment.Content
            };

            return View(commentToDisplay);
        }

        // POST: Comments/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CommentViewModel comment)
        {
            if (!ModelState.IsValid)
            {
                return View(comment);
            }

            var commentToUpdate = db
                .Comments
                .Include(m => m.Author)
                .Include(m => m.Article)
                .SingleOrDefault(m => m.Id == comment.Id);

            if (commentToUpdate == null)
            {
                return HttpNotFound();
            }
            
            commentToUpdate.Content = comment.Content;
            commentToUpdate.Date = DateTime.Now;
            db.Entry(commentToUpdate).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction($"../Article/Details/{comment.ArticleId}");

        }

        // POST: Comments/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            Comment comment = db
                .Comments
                .Include(c => c.Article)
                .Include(c => c.Author)
                .ToList()
                .SingleOrDefault(c => c.Id == id);

            var currentUserId = User.Identity.GetUserId();

            var isAuthenticatedForTheAction = currentUserId ==
                comment.Author.Id ||
                User.IsInRole("Administrators") ||
                User.IsInRole("Moderators");

            if (!isAuthenticatedForTheAction)
            {
                return RedirectToAction($"Details/{comment.Article.Id}", "Article"); // should display "You are not authorized to do that" view
            }

            var articleId = comment.Article.Id;
            db.Comments.Remove(comment);
            db.SaveChanges();

            return RedirectToAction($"../Article/Details/{articleId}");

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
