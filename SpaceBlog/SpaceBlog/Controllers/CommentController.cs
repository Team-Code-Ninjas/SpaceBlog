using Microsoft.AspNet.Identity;
using SpaceBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SpaceBlog.Controllers
{
    public class CommentController : Controller
    {
        private readonly BlogDBContext _db = new BlogDBContext();

        [HttpPost]
        [Authorize]
        public ActionResult Create(CommentViewModel commentViewModel)
        {
            if (!ModelState.IsValid)
                //return PartialView("_CommentBox", commentViewModel);
                return RedirectToAction("Details", "Article", new { id = commentViewModel.ArticleId });

            var article = _db.Articles.Find(commentViewModel.ArticleId);
            if (article == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest,
                    $"Invalid article specified.");

            var author = _db.Users.Find(User.Identity.GetUserId());
            if (author ==  null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest,
                    $"Invalid comment author specified.");

            var comment = new Comment
            {
                Article = article,
                Author = author,
                Date = DateTime.Now,
                Content = commentViewModel.Content,
            };

            _db.Comments.Add(comment);
            _db.SaveChanges();

            return RedirectToAction("Details", "Article", new { id = commentViewModel.ArticleId });
        }

        [HttpDelete]
        public ActionResult Delete(int? id)
        {
            var commentToDelete = _db.Comments.Find(id);
            if (commentToDelete == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest,
                    $"Comment with id '{id}' does not exist");

            _db.Comments.Remove(commentToDelete);
            _db.SaveChanges();

            return RedirectToAction("Details", "Article");
        }
    }
}