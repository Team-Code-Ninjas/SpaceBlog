namespace SpaceBlog.Models
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    public class ArticleController : Controller
    {
        private BlogDBContext db = new BlogDBContext();

        // GET: Articles
        public ActionResult Index()
        {
            return View(db.Articles.Include(a => a.Author).ToList().Select(x => new Article
            {
                Id = x.Id,
                Content = HttpUtility.HtmlDecode(x.Content),
                Date = x.Date,
                Title = x.Title,
                Author = x.Author
            }));
        }

        // GET: Articles/Details/5
        [ValidateInput(false)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Article article = db
                .Articles
                .Include(a => a.Author)
                .Include(a => a.Comments)
                .Include("Comments.Author")
                .FirstOrDefault(a => a.Id == id);

            article.Content = HttpUtility.HtmlDecode(article.Content);

            if (article == null)
            {
                return HttpNotFound();
            }

            ViewBag.ArticleId = id.Value;

            var comments = db
                .Comments
                .Where(d => d.Article.Id.Equals(id.Value))
                .ToList();

            ViewBag.Comments = comments;

            return View(article);
        }

        // GET: Article/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Article/Create
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Title,Content")] Article article)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = HttpContext
                    .User
                    .Identity
                    .GetUserId();

                article.Content = HttpUtility.HtmlEncode(article.Content);

                article.Author = db
                    .Users
                    .Find(currentUserId);

                db.Articles.Add(article);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: Articles/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var currentUserId = HttpContext
                .User
                .Identity
                .GetUserId();


            var currentArticle = db
                .Articles
                .Include(a => a.Author)
                .FirstOrDefault(a => a.Id == id);

            var isAuthenticatedForTheAction = currentUserId == 
                currentArticle.Author.Id ||
                User.IsInRole("Administrators") ||
                User.IsInRole("Moderators");

            if (currentArticle == null)
            {
                return HttpNotFound();
            }

            if (!isAuthenticatedForTheAction)
            {
                return RedirectToAction("Index"); // should display "You are not authorized to do that" view
            }

            currentArticle.Content = HttpUtility.HtmlDecode(currentArticle.Content);
            return View(currentArticle);
        }

        // POST: Articles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,AuthorId")] Article article)
        {
            if (ModelState.IsValid)
            {
                HttpUtility.HtmlEncode(article.Content);
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction($"Details/{article.Id}");
            }

            return View(article);
        }

        // GET: Articles/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var currentUserId = HttpContext
                .User.Identity
                .GetUserId();

            var currentArticle = db
                .Articles
                .Include(a => a.Author)
                .FirstOrDefault(a => a.Id == id);

            var isAuthenticatedForTheAction = currentUserId ==
                currentArticle.Author.Id ||
                User.IsInRole("Administrators") ||
                User.IsInRole("Moderators");

            if (currentArticle == null)
            {
                return HttpNotFound();
            }

            if (!isAuthenticatedForTheAction)
            {
                return RedirectToAction("Index"); // should display "You are not authorized to do that" view
            }

            currentArticle.Content = HttpUtility.HtmlDecode(currentArticle.Content);
            return View(currentArticle);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();

            return RedirectToAction("Index");
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