using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MainSite.Utils;
using Microsoft.AspNetCore.Http;
using MainSite.Service;
using Vetapp.Client.ProxyCore;
using MainSite.Extensions;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MainSite.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppSettings _settings;

        public AccountController(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }

        private void InitializeSession(UserProxy userProxy)
        {
            HttpContext.Session.Clear();
            HttpContext.Session.SetObjectAsJson(Constants.sessionKeyUser, userProxy);
        }

        public JsonResult CheckUsername(string username)
        {
            bool bResult = false;
            bResult = doCheckUsername(username);
            return Json(bResult);
        }

        public JsonResult Authenticate(string username, string password)
        {
            bool bResult = false;
            UserProxy userProxy = doAuthenticate(username, password);
            if ((userProxy != null) && (userProxy.UserID > 0))
            {
                bResult = true;
            }
            return Json(bResult);
        }
        [HttpGet]
        public ActionResult Login(string username, string password)
        {
            try
            {
                UserProxy userProxy = doAuthenticate(username, password);
                if ((userProxy != null) && (userProxy.UserID > 0))
                {
                    InitializeSession(userProxy);
                    return Json(new { ok = true, newurl = "/Dashboard" });
                }
            }
            catch {}
            return Json(new { ok = false, newurl = "" });
        }
        [HttpGet]
        public ActionResult Register(string username, string password)
        {
            bool bExist = doCheckUsername(username);
            if (!bExist)
            {
                UserProxy userProxy = doRegister(username, password);
                if ((userProxy != null) && (userProxy.UserID > 0))
                {
                    // successfull created user
                    return Login(username, password);
                }
            }
            else
            {
                return Json(new { ok = false, newurl = "" });
            }
            return Json(new { ok = false, newurl = "" });
        }

        public JsonResult RegisterEvaluation(string username, string password, bool isfirsttimefiling, bool hasclaimwithva, bool hasactiveappeal, bool hasratingval, int slidervalue)
        {
            bool bResult = false;
            UserProxy userProxy = doRegister(username, password);
            if ((userProxy != null) && (userProxy.UserID > 0))
            {
                // successfull created user
                // track evaluation with user
                SaveEvaluation(userProxy, isfirsttimefiling, hasclaimwithva, hasactiveappeal, hasratingval, slidervalue);
            }

            return Json(bResult);
        }

        public IActionResult LogOut()
        {
            try
            {
                HttpContext.Session.Clear();
            }
            catch { }
            return RedirectToAction("Index", "Home");
        }
        public JsonResult SaveEvaluation(UserProxy userProxy, bool isfirsttimefiling, bool hasclaimwithva, bool hasactiveappeal, bool hasratingval, int slidervalue)
        {
            bool bResult = false;
            UsersService userService = new UsersService(_settings.DefaultService, _settings.ClientKey);
            return Json(bResult);
        }

        private UserProxy doRegister(string username, string password)
        {
            UsersService userService = new UsersService(_settings.DefaultService, _settings.ClientKey);
            UserProxy userProxy = new UserProxy() { Username = username, Passwd = password };
            userProxy = userService.Create(userProxy).Result;
            return userProxy;

        }

        private UserProxy doAuthenticate(string username, string password)
        {
            UsersService userService = new UsersService(_settings.DefaultService, _settings.ClientKey);
            UserProxy userProxy = new UserProxy() { Username = username, Passwd = password };
            userProxy = userService.Authenticate(userProxy).Result;
            return userProxy;

        }

        private bool doCheckUsername(string username)
        {
            bool bResult = false;
            UsersService userService = new UsersService(_settings.DefaultService, _settings.ClientKey);
            var bUserExist = userService.ExistByUsername(username);
            bResult = bUserExist.Result;
            return bResult;
        }

    }
}
