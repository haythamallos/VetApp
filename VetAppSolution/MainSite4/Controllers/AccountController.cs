using System;
using Microsoft.AspNetCore.Mvc;
using MainSite.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using MainSite.Utils;
using System.Dynamic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using MainSite.Service;
using Vetapp.Client.ProxyCore;
using MainSite.Models;
using Microsoft.AspNetCore.Identity;

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

        private bool InitializeSession(string pStrConnection)
        {
            bool bSuccess = true;
            try
            {
                HttpContext.Session.Clear();
                DateTime dateFirstSeen;
                dateFirstSeen = DateTime.Now;
                var serializedDate = JsonConvert.SerializeObject(dateFirstSeen);
                HttpContext.Session.SetString(Constants.sessionKeyStartDate, serializedDate);
                HttpContext.Session.SetString(Constants.sessionKeyConnection, pStrConnection);
            }
            catch { bSuccess = false; }
            return bSuccess;
        }

        public JsonResult CheckUsername(string username)
        {
            bool bResult = false;
            bResult = doCheckUsername(username);
            return Json(bResult);
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
                SetLoggedIn(userProxy);
            }

            return Json(bResult);
        }

        public JsonResult Register(string username, string password)
        {
            bool bResult = false;
            UserProxy userProxy = doRegister(username, password);
            if ((userProxy != null) && (userProxy.UserID > 0))
            {
                // successfull created user
                SetLoggedIn(userProxy);
            }

            return Json(bResult);
        }

        public JsonResult Login(string username, string password)
        {
            bool bResult = false;
            UserProxy userProxy = doAuthenticate(username, password);
            if ((userProxy != null) && (userProxy.UserID > 0))
            {
                // successfull created user
                SetLoggedIn(userProxy);
            }

            return Json(bResult);
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

        private void SetLoggedIn(UserProxy userProxy)
        {

        }
    }
}


//private readonly Auth0Settings _auth0Settings;

//public AccountController(IOptions<Auth0Settings> auth0Settings, IOptions<AppSettings> settings)
//{
//    //_auth0Settings = auth0Settings.Value;
//    _settings = settings.Value;
//}

//[HttpGet]
//public IActionResult Login(string returnUrl = "/")
//{
//    ViewData["ReturnUrl"] = returnUrl;
//    return View();
//}

//[HttpPost]
//public IActionResult Register(CombinedLoginRegisterViewModel model)
//{
//    //string url = $"https://{_auth0Settings.Domain}/dbconnections/signup";
//    //RESTUtil apiutil = new RESTUtil();
//    //dynamic dynamicJson = new ExpandoObject();
//    //dynamicJson.email = model.Register.Email;
//    //dynamicJson.password = model.Register.Password;
//    //apiutil.POSTreq(url, dynamicJson);

//    return View(model);
//}



//[HttpPost]
//public async Task<IActionResult> Login(CombinedLoginRegisterViewModel model, string returnUrl = null)
//{
//    if (ModelState.IsValid)
//    {
//        try
//        {
//            AuthenticationApiClient client = new AuthenticationApiClient(new Uri($"https://{_auth0Settings.Domain}/"));

//            var result = await client.AuthenticateAsync(new AuthenticationRequest
//            {
//                ClientId = _auth0Settings.ClientId,
//                Scope = "openid",
//                Connection = "Username-Password-Authentication", // Specify the correct name of your DB connection
//                Username = model.Login.Username,
//                Password = model.Login.Password
//            });

//            // Get user info from token
//            var user = await client.GetTokenInfoAsync(result.IdToken);

//            // Create claims principal
//            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
//            {
//                  new Claim(ClaimTypes.NameIdentifier, user.UserId),
//                  new Claim(ClaimTypes.Name, user.FullName)

//            }, CookieAuthenticationDefaults.AuthenticationScheme));

//            // Sign user into cookie middleware
//            await HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

//            return RedirectToLocal(returnUrl);
//        }
//        catch (Exception e)
//        {
//            ModelState.AddModelError("", e.Message);
//        }
//    }

//    return View(model);
//}

//[Authorize]
//public IActionResult Logout()
//{
//    HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

//    return RedirectToAction("Index", "Home");
//}

//[HttpGet]
//public IActionResult LoginExternal(string connection, string returnUrl = "/")
//{
//    var properties = new AuthenticationProperties() { RedirectUri = returnUrl };
//    if (!string.IsNullOrEmpty(connection))
//    {
//        properties.Items.Add("connection", connection);
//        InitializeSession(connection);
//    }

//    return new ChallengeResult("Auth0", properties);
//}

//private IActionResult RedirectToLocal(string returnUrl)
//{
//    if (Url.IsLocalUrl(returnUrl))
//    {
//        return Redirect(returnUrl);
//    }
//    else
//    {
//        return RedirectToAction(nameof(HomeController.Index), "Home");
//    }
//}

//[HttpPost]
//public PartialViewResult  Evaluation(CombinedLoginRegisterViewModel model)
//{
//    // check if user exists
//    UsersService userService = new UsersService(_settings.DefaultService, _settings.ClientKey);
//    var bUserExist = userService.ExistByUsername(model.Register.Email);
//    if (bUserExist.Result)
//    {
//        // user exists already
//    }
//    else
//    {
//        // proceed with creating the user
//        //UserProxy userProxy = new UserProxy() { EmailAddress = model.Register.Email };
//    }

//    //return View(model);
//    return PartialView(model);
//}

//[HttpPost]
//public IActionResult Recover(CombinedLoginRegisterViewModel model)
//{
//    return RedirectToAction(nameof(HomeController.Index), "Index");
//}

//[HttpPost]
//public IActionResult Login(CombinedLoginRegisterViewModel model)
//{
//    return RedirectToAction(nameof(HomeController.Index), "Index");
//}