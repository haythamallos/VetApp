using MainSite.Core;
using System.Web.Mvc;
using MainSite.ViewModels;

namespace MainSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Who We Are";

            return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Contact";

            return View();
        }

        public ActionResult GetStarted()
        {
            //ViewBag.Message = "Get Started";

            return View();
        }

        public ActionResult LearnMore()
        {
            //ViewBag.Message = "Learn More";

            return View();
        }

        public ActionResult Problem()
        {
            ViewBag.Message = "Encountered a Problem";

            return View();
        }
        [HttpPost]
        public ActionResult Evaluator(string chkFirstTimeFiling )
        {
            DataManager dm = new DataManager();

            return View();
        }
    }
}