using BookingMVC.Data;
using BookingMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingMVC.Controllers
{
    public class RecenzijaController : BaseController
    {
        private readonly BookingDbContext _context;

        public RecenzijaController(BookingDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(int idSmjestaj, int ocjena, string komentar)
        {
            int idKorisnik = HttpContext.Session.GetInt32("IdKorisnik").Value;

            // Provjera da li korisnik ima zavrsenu i potvrdjenu rezervaciju
            bool imaRezervaciju = _context.Rezervacije.Any(r =>
                r.IdKorisnik == idKorisnik &&
                r.IdSmjestaj == idSmjestaj &&
                r.IdStatus == 2 &&
                r.DatumOdjave.Date < DateTime.Today);

            if (!imaRezervaciju)
            {
                TempData["ReviewError"] =
                    "Recenziju možete ostaviti samo nakon završene potvrđene rezervacije.";

                return RedirectToAction("Details", "Smjestaj", new { id = idSmjestaj });
            }

            // Jedna recenzija po korisniku i smjestaju
            bool vecPostoji = _context.Recenzije.Any(r =>
                r.IdKorisnik == idKorisnik &&
                r.IdSmjestaj == idSmjestaj);

            if (vecPostoji)
            {
                TempData["ReviewError"] =
                    "Već ste ostavili recenziju za ovaj smještaj.";

                return RedirectToAction("Details", "Smjestaj", new { id = idSmjestaj });
            }

            var recenzija = new Recenzija
            {
                IdKorisnik = idKorisnik,
                IdSmjestaj = idSmjestaj,
                Ocjena = ocjena,
                Komentar = komentar
            };

            _context.Recenzije.Add(recenzija);
            _context.SaveChanges();

            TempData["ReviewSuccess"] =
                "Recenzija je uspješno dodata.";

            return RedirectToAction("Details", "Smjestaj", new { id = idSmjestaj });
        }

        public IActionResult Index()
        {
            int idAdmin = HttpContext.Session.GetInt32("IdKorisnik").Value;

            var recenzije = _context.Recenzije
                .Include(r => r.Korisnik)
                .Include(r => r.Smjestaj)
                    .ThenInclude(s => s.Grad)
                .Where(r => r.Smjestaj.IdAdmin == idAdmin)
                .OrderBy(r => r.Smjestaj.Naziv)
                .ToList();

            return View(recenzije);
        }
    }
}
