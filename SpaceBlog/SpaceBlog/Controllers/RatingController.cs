using SpaceBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpaceBlog.Controllers
{
    public class RatingController : Controller
    {
        private readonly BlogDBContext db = new BlogDBContext();
        public ActionResult Index()
        {
            return View();
        }
    }
}