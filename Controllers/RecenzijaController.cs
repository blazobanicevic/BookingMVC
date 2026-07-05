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

            bool mozeOstavitiRecenziju = _context.Database
                .SqlQueryRaw<bool>(
                    "SELECT CAST(dbo.fn_KorisnikMozeOstavitiRecenziju({0}, {1}) AS bit) AS Value",
                    idKorisnik,
                    idSmjestaj
                )
                .AsEnumerable()
                .First();

            if (!mozeOstavitiRecenziju)
            {
                TempData["ReviewError"] =
                    "Recenziju možete ostaviti samo nakon završene potvrđene rezervacije i samo jednom po smještaju.";

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