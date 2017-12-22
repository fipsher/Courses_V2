using System.Web.Mvc;

namespace Courses_v2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() => View();

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}