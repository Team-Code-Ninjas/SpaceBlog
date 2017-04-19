namespace SpaceBlog.Controllers
{
    using System.Web.Mvc;
    using SpaceBlog.Models;

    public class RatingController : Controller
    {
        private readonly BlogDBContext db = new BlogDBContext();
        public ActionResult Index()
        {
            return View();
        }
    }
}