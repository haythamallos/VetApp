using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VetWebApp.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace VetWebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index(int id)
        {
            //Contact contact = new Contact() { FirstName = "Haytham", LastName = "Allos", Id = id };
            //return View(contact);

            return View();
        }
        public IActionResult GetStarted()
        {
            return View();
        }
    }
}
