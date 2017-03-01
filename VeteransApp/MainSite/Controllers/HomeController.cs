using System;
using System.Web;
using System.Web.Mvc;

using MainSite.Classes;
using Vetapp.Engine.BusinessFacadeLayer;
using Vetapp.Engine.DataAccessLayer.Data;
using Vetapp.Engine.Common;

namespace MainSite.Controllers
{
    public class HomeController : Controller
    {
        private Config _config = null;

        public HomeController()
        {
            _config = new Config();
            var CurrentRatingsList = new SelectList(new[] { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 });
            ViewBag.CurrentRatingsList = CurrentRatingsList;
         
        }
        public ActionResult Index()
        {
            bool bCookiesEnabled = SetInitialCookie();
            if (!bCookiesEnabled)
            {
                ViewData["CookiesEnabled"] = false;
            }
            return View();
        }
        public ActionResult Testimonials()
        {
            return View();
        }
        public ActionResult Benefits()
        {
            return View();
        }
        public ActionResult Login2()
        {
            return View();
        }
        public ActionResult RegisterCode()
        {
            ViewData["RegisterCode"] = true;
            return View();
        }
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Terms()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Register2()
        {
            return View();
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        public bool SetInitialCookie()
        {
            bool isSuccess = false;
            try
            {
                HttpCookie cookie = Request.Cookies[CookieManager.COOKIENAME];
                if (cookie == null)
                {
                    cookie = new HttpCookie(CookieManager.COOKIENAME);
                    cookie.Expires = DateTime.Now.AddYears(1);
                    cookie[CookieManager.COOKIE_FIELD_VISIT_COUNT] = "1";
                }
                else
                {
                    int nCount = Convert.ToInt32(cookie[CookieManager.COOKIE_FIELD_VISIT_COUNT]);
                    nCount++;
                    cookie[CookieManager.COOKIE_FIELD_VISIT_COUNT] = Convert.ToString(nCount);
                    string userguid = cookie[CookieManager.COOKIE_FIELD_USER_GUID];
                    if (!string.IsNullOrEmpty(userguid))
                    {
                        BusFacCore busFacCore = new BusFacCore();
                        User user = busFacCore.UserGet(userguid);
                        if ((user != null) && (user.UserID > 0))
                        {
                            user.NumberOfVisits = nCount;
                            user.LastVisitDate = DateTime.UtcNow;
                            busFacCore.UserCreateOrModify(user);
                        }
                    }
                }
                Response.Cookies.Add(cookie);
                isSuccess = true;
            }
            catch (Exception ex) { }

            return isSuccess;
        }
    }
}