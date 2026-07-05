using System.Diagnostics;
using BookingMVC.Data;
using BookingMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookingDbContext _context;

        public HomeController(ILogger<HomeController> logger, BookingDbContext context)
        {
            _logger = logger;
            _context = context;
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

        public IActionResult UserDashboard()
        {
            if (HttpContext.Session.GetString("Uloga") != "Korisnik")
            {
                return RedirectToAction("Index");
            }

            var smjestaji = _context.Smjestaji
                .Include(s => s.Grad)
                .Include(s => s.TipSmjestaja)
                .Where(s => s.Aktivan)
                .Take(6)
                .ToList();

            return View(smjestaji);
        }

        public IActionResult AdminDashboard()
        {
            if (HttpContext.Session.GetString("Uloga") != "Admin")
            {
                return RedirectToAction("Index");
            }

            int idAdmin = HttpContext.Session.GetInt32("IdKorisnik").Value;

            var smjestaji = _context.Smjestaji
                .Include(s => s.Grad)
                .Include(s => s.TipSmjestaja)
                .Where(s => s.IdAdmin == idAdmin)
                .Take(6)
                .ToList();

            return View(smjestaji);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}