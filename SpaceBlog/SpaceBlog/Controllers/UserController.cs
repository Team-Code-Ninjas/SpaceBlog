using SpaceBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpaceBlog.Controllers
{
    public class UserController : Controller
    {
        private BlogDBContext db = new BlogDBContext();
        // GET: User
        [ActionName("Profile")]
        public ActionResult Index()
        {
            
            return View();
        }
    }
}