﻿using Microsoft.AspNet.Identity;
using SpaceBlog.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System;

namespace SpaceBlog.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class AdminController : Controller
    {

        private BlogDBContext db = new BlogDBContext();

        // GET: Admin/Index

        public ActionResult Index()
        {
            //var currentUserId = HttpContext.User.Identity.GetUserId();


            var userViewModels = db.Users
                .Select(UserToUserViewModel)
                .ToArray();

            //ViewBag.Articles = articles;
            //ViewBag.Comments = comments;

            //if (!User.IsInRole("Administrators"))
            //{
            //    return RedirectToAction("/../"); // should display "You are not authorized to do that" view
            //}

            return View(userViewModels);
        }

        private UserViewModel UserToUserViewModel(ApplicationUser user)
        {
            var db = new BlogDBContext();

            var userViewModel = new UserViewModel();

            userViewModel.Id = user.Id;
            userViewModel.UserName = user.UserName;
            userViewModel.FullName = user.FullName;
            userViewModel.ArticlesCreated = db.Articles.Include(a => a.Author).Count(a => a.Author.Id == user.Id);
            userViewModel.CommentsMade = db.Comments.Include(a => a.Author).Count(a => a.Author.Id == user.Id);

            var roles = db.Users.Include(a => a.Roles).FirstOrDefault(a => a.Id == user.Id).Roles.Select(a => a.ToString()).ToArray();

            if (roles.Contains("Moderators"))
            {
                userViewModel.UserType = "Moderator";
            }
            else if (roles.Contains("Administrators"))
            {
                userViewModel.UserType = "Admin";
            }
            else
            {
                userViewModel.UserType = "User";
            }


            userViewModel.Status = user.Suspended ? Status.Suspended : Status.Active;

            return userViewModel;
        }

        // GET: Admin/Suspend/5
        public ActionResult Suspend(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = db.Users.Find(id);

            // The Administartor should not be able to suspend himself
            //if (true) 
            //{
            //    return RedirectToAction("Index");
            //}

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