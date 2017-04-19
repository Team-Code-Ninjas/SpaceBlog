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
        public ActionResult Create(CommentViewModel commentViewModel)
        {
            if (!ModelState.IsValid)
                return PartialView("_CommentBox", commentViewModel);

            var article = _db.Articles.Find(commentViewModel.ArticleId);
            if (article == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest,
                    $"Article with id '{commentViewModel.ArticleId}' does not exist");
            var author = _db.Users.Find(commentViewModel.AuthorId);
            if (author ==  null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest,
                    $"User with id '{commentViewModel.AuthorId}' does not exist");

            var comment = new Comment
            {
                Article = article,
                Author = author,
                Date = DateTime.Now,
                Content = commentViewModel.Content,
            };

            _db.Comments.Add(comment);
            _db.SaveChanges();

            return RedirectToAction("Index", "Article");
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