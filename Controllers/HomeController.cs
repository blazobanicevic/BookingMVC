using System.Diagnostics;
using BookingMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingMVC.Controllers
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
            var uloga = HttpContext.Session.GetString("Uloga");

            if (uloga == "Admin")
            {
                return RedirectToAction("AdminDashboard");
            }

            if (uloga == "Korisnik")
            {
                return RedirectToAction("UserDashboard");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult UserDashboard()
        {
            if (HttpContext.Session.GetString("Uloga") != "Korisnik")
            {
                return RedirectToAction("AdminDashboard");
            }

            return View();
        }

        public IActionResult AdminDashboard()
        {
            if (HttpContext.Session.GetString("Uloga") != "Admin")
            {
                return RedirectToAction("UserDashboard");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
