using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MVCIntroDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Message"] = "Hello world!";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            ViewData["Message"] = "This is an ASP.NET Core MVC app.";
            return View();
        }
        public IActionResult Numbers()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NumbersToN()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NumbersToN(int count)
        {
            ViewData["Count"] = count;
            return View();
        }
    }
}
