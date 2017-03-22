using SpaceBlog.Models;
using System.Linq;
using System.Web.Mvc;

namespace SpaceBlog.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Index()
        {
            var db = new BlogDBContext();

            var articles = db.Articles.Include("Author").ToArray();

            return View(articles);
        }
    }
}