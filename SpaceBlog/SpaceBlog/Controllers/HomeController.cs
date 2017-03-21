using System.Web.Mvc;

namespace SpaceBlog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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
    }
}