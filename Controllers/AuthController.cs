using BookingMVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly BookingDbContext _context;

        public AuthController(BookingDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string lozinka)
        {
            var korisnik = _context.Korisnici
                .Include(k => k.Uloga)
                .FirstOrDefault(k =>
                    k.Email == email &&
                    k.Lozinka == lozinka);

            if (korisnik == null)
            {
                ViewBag.Error = "Pogrešan email ili lozinka.";
                return View();
            }

            HttpContext.Session.SetInt32("IdKorisnik", korisnik.IdKorisnik);
            HttpContext.Session.SetString("Ime", korisnik.Ime);
            HttpContext.Session.SetString("Prezime", korisnik.Prezime);
            HttpContext.Session.SetString("Uloga", korisnik.Uloga.Naziv);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
}