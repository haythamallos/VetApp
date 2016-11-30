﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MainSite.Utils;
using System.Security.Claims;
using System.Linq;
using Microsoft.Extensions.Options;
using Vetapp.Client.ProxyCore;
using MainSite.Service;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MainSite.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppSettings _settings;

        public DashboardController(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // get user claims if social connection
            //var claimsIdentity = User.Identity as ClaimsIdentity;
            //string UserID = claimsIdentity?.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;

            //UsersService userService = new UsersService(_settings.DefaultService, _settings.ClientKey);
            //var userServiceTask = userService.Load(UserID);
            //UserProxy userProxy = userServiceTask.Result;
            //userServiceTask = userService.Save(userProxy);



            //string sessionKey = Constants.sessionKeyIsFirstTime;
            //var connection = HttpContext.Session.GetString(Constants.sessionKeyConnection);
            //var isFirstTime = HttpContext.Session.GetString(Constants.sessionKeyIsFirstTime);
            //if (string.IsNullOrEmpty(isFirstTime))
            //{
            //    //First time login
            //    HttpContext.Session.SetString(Constants.sessionKeyIsFirstTime, "false");

            //    string claimType = null;
            //    string claimValue = null;
            //    foreach (var claim in User.Claims)
            //    {
            //        claimType = claim.Type;
            //        claimValue = claim.Value;
            //    }
            //}

            return View();
        }
        [HttpGet]
        public IActionResult Gallery()
        {
            return View();
        }
    }
}
