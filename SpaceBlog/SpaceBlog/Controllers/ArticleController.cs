using SpaceBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpaceBlog.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Index()
        {
            Article[] articles = new Article[]
            {
                new Article {title = "exampleTitle", Author = "authorName"}
            };
            return View(articles);
        }
    }
}