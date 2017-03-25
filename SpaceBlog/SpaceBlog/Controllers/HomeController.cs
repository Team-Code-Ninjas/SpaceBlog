using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Linq;
using SpaceBlog.Models;

namespace SpaceBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlogDBContext _dbContext =
            new BlogDBContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Contact(Contact contact)
        {
            if (!ModelState.IsValid)
                return View(contact);

            _dbContext.Contacts.Add(contact);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Team()
        {
            ViewBag.Message = "Our Team";

            return View();
        }

        public ActionResult Create()
        {
            ViewBag.Message = "Create";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }

    }
}