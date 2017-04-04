using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SpaceBlog.Models
{
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
            Article article = db.Articles.Include(a => a.Author).ToList().FirstOrDefault(a=>a.Id == id);
            article.Content = HttpUtility.HtmlDecode(article.Content);

            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Article/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Article/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Title,Content")] Article article)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = HttpContext.User.Identity.GetUserId();
                article.Content = HttpUtility.HtmlEncode(article.Content);
                article.Author = db.Users.Find(currentUserId);
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: Articles/Edit/5
        [Authorize(Roles = "Administrators")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            

            Article article = db.Articles.Find(id);

            article.Content =  HttpUtility.HtmlDecode(article.Content);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,Title,Content")] Article article)
        {
            if (ModelState.IsValid)
            {
                HttpUtility.HtmlEncode(article.Content);
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: Articles/Delete/5
        [Authorize(Roles = "Administrators")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
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

        [Authorize]
        public ActionResult Comment(int id)
        {
            var articleCommentViewModel = new ArticleCommentViewModel
            {
                ArticleId = id
            };

            return View(articleCommentViewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Comment(ArticleCommentViewModel articleComment)
        {
            var article = db.Articles.SingleOrDefault(a => a.Id == articleComment.ArticleId);
            if (article == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var comment = new Comment
            {
                Author = User.Identity.Name,
                Content = articleComment.Comment,
                DateTimeComment = DateTime.Now
            };

            article.Comments.Add(comment);
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
