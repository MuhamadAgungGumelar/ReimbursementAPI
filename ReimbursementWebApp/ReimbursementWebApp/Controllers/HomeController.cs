using Microsoft.AspNetCore.Mvc;
using ReimbursementWebApp.Models;
using System.Diagnostics;

namespace ReimbursementWebApp.Controllers
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
            return View();
        }

        public IActionResult LandingPage()
        {
            return View();
        }

        public IActionResult Reimbursement()
        {
            return View();
        }

        public IActionResult ReimbursementForm()
        {
            return View();
        }

        public IActionResult Manager()
        {
            return View();
        }

        public IActionResult Finance()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}