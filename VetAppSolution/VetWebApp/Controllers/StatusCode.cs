using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace VetWebApp.Controllers
{
 
    public class StatusCode : Controller
    {
        private ILogger<HomeController> _logger;

        public StatusCode(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet("/StatusCode/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            var reExecuteFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            _logger.LogInformation("Unexpected StatusCode:  {statusCode}, OriginalPath:   {OriginalPath}", statusCode, reExecuteFeature.OriginalPath);
            return View(statusCode);
        }
    }
}
