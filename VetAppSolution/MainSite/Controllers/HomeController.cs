using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}