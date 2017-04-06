using Microsoft.AspNet.Identity;
using SpaceBlog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SpaceBlog.Controllers
{
    public class AdminController : Controller
    {

        private BlogDBContext db = new BlogDBContext();

        // GET: Admin/Index
        public ActionResult Index()
        {
            var currentUserId = HttpContext.User.Identity.GetUserId();

            var articles = db.Articles.Include(a=>a.Author).ToList();
            var comments = db.Comments.Include(a => a.Author).ToList();

            ViewBag.Articles = articles;
            ViewBag.Comments = comments;

            if (!User.IsInRole("Administrators"))
            {
                return RedirectToAction("/../"); // should display "You are not authorized to do that" view
            }

            return View(db.Users);
        }

        //// GET: Admin/Delete/5
        //[Authorize]
        //public ActionResult Delete(int? id)
        //{
        //    if (!User.IsInRole("Administrators"))
        //    {
        //        return RedirectToAction("/../"); // should display "You are not authorized to do that" view
        //    }

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    var user = db.Users.Find(id);

        //    return View(user);
        //}

        //// POST: Admin/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ApplicationUser user = db.Users.Find(id);
        //    db.Users.Remove(user);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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