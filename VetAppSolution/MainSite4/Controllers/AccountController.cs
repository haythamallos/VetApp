using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MainSite.Models;
using MainSite.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using MainSite.Utils;
using System.Dynamic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MainSite.Controllers
{
    public class AccountController : Controller
    {
        private readonly Auth0Settings _auth0Settings;

        public AccountController(IOptions<Auth0Settings> auth0Settings)
        {
            _auth0Settings = auth0Settings.Value;
        }

        //[HttpGet]
        //public IActionResult Login(string returnUrl = "/")
        //{
        //    ViewData["ReturnUrl"] = returnUrl;
        //    return View();
        //}

        [HttpPost]
        public IActionResult Register(CombinedLoginRegisterViewModel model)
        {
            string url = $"https://{_auth0Settings.Domain}/dbconnections/signup";
            RESTUtil apiutil = new RESTUtil();
            dynamic dynamicJson = new ExpandoObject();
            dynamicJson.email = model.Register.Email;
            dynamicJson.password = model.Register.Password;
            //apiutil.POSTreq(url, dynamicJson);

            return View(model);
        }



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

        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult LoginExternal(string connection, string returnUrl = "/")
        {
            var properties = new AuthenticationProperties() { RedirectUri = returnUrl };
            if (!string.IsNullOrEmpty(connection))
            {
                properties.Items.Add("connection", connection);
                InitializeSession(connection);
            }

            return new ChallengeResult("Auth0", properties);
        }

        private IActionResult RedirectToLocal(string returnUrl)
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

        [HttpPost]
        public IActionResult Evaluation(CombinedLoginRegisterViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(DashboardController.Index), "Dashboard");
            }
            return RedirectToAction(nameof(HomeController.Index), "Index");
        }
    }
}
