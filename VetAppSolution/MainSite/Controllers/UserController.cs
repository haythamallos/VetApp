using System.Web.Mvc;
using System.Security.Claims;
using Vetapp.Client.Proxy;
using MainSite.Extensions;
using MainSite.Core;

namespace MainSite.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [Authorize]
        public ActionResult Index()
        {
            //DataManager dm = new DataManager(User.Identity as ClaimsIdentity);
            //dm.SaveUserIfNotExist();
            //if (dm.HasError)
            //{
            //    return RedirectToAction("Problem", "Home");
            //}
            return View();

        }
 
    }
}