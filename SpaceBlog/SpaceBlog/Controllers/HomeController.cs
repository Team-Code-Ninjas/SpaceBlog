using System.Web.Mvc;
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
            ViewBag.Title = "About";
            ViewBag.ContactInfo = "Contact info:";

            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Title = "Contact Us";
            ViewBag.Message = "Get in touch";

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
            ViewBag.Title = "Team";
            ViewBag.Message = "Our Team";
        
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Create";
            ViewBag.Message = "New Post";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }

    }
}