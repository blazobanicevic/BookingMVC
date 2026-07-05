using BookingMVC.Data;
using BookingMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingMVC.Controllers
{
    public class RezervacijaController : BaseController
    {
        private readonly BookingDbContext _context;

        public RezervacijaController(BookingDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(int idSmjestaj, DateTime datumPrijave, DateTime datumOdjave, int brojOsoba)
        {
            if (HttpContext.Session.GetString("Uloga") != "Korisnik")
            {
                return RedirectToAction("AdminDashboard", "Home");
            }

            int idKorisnik = HttpContext.Session.GetInt32("IdKorisnik").Value;

            bool dostupan = _context.Database
                .SqlQueryRaw<bool>(
                    "SELECT CAST(dbo.fn_SmjestajDostupan({0}, {1}, {2}) AS bit) AS Value",
                    idSmjestaj,
                    datumPrijave,
                    datumOdjave
                )
                .AsEnumerable()
                .First();

            if (!dostupan)
            {
                TempData["ReservationError"] = "Smještaj nije dostupan za odabrani period.";
                return RedirectToAction("Details", "Smjestaj", new { id = idSmjestaj });
            }

            var rezervacija = new Rezervacija
            {
                IdSmjestaj = idSmjestaj,
                IdKorisnik = idKorisnik,
                IdStatus = 1,
                DatumPrijave = datumPrijave,
                DatumOdjave = datumOdjave,
                BrojOsoba = brojOsoba
            };

            _context.Rezervacije.Add(rezervacija);
            _context.SaveChanges();

            var smjestaj = _context.Smjestaji
                .Include(s => s.Admin)
                .First(s => s.IdSmjestaj == idSmjestaj);

            _context.Notifikacije.Add(new Notifikacija
            {
                IdKorisnik = smjestaj.IdAdmin,
                Naslov = "Nova rezervacija",
                Tekst = $"{HttpContext.Session.GetString("Ime")} je poslao zahtjev za rezervaciju smještaja \"{smjestaj.Naziv}\"."
            });

            _context.SaveChanges();

            TempData["ReservationSuccess"] = "Rezervacija je uspješno poslata i čeka potvrdu.";

            return RedirectToAction("Details", "Smjestaj", new { id = idSmjestaj });
        }

        public IActionResult Index(int? mjesec, int? godina)
        {
            var istekleRezervacije = _context.Rezervacije
                .Where(r =>
                    r.IdStatus == 1 &&                // Na čekanju
                    r.DatumPrijave.Date < DateTime.Today)
                .ToList();

            foreach (var rezervacija in istekleRezervacije)
            {
                rezervacija.IdStatus = 5; // Istekla
            }

            _context.SaveChanges();

            int idKorisnik = HttpContext.Session.GetInt32("IdKorisnik").Value;
            string uloga = HttpContext.Session.GetString("Uloga");

            int trenutniMjesec = mjesec ?? DateTime.Today.Month;
            int trenutnaGodina = godina ?? DateTime.Today.Year;

            ViewBag.Mjesec = trenutniMjesec;
            ViewBag.Godina = trenutnaGodina;
            ViewBag.DaniUMjesecu = DateTime.DaysInMonth(trenutnaGodina, trenutniMjesec);

            var rezervacije = _context.Rezervacije
                .Include(r => r.Smjestaj)
                    .ThenInclude(s => s.Grad)

                .Include(r => r.Smjestaj)
                    .ThenInclude(s => s.Admin)

                .Include(r => r.Korisnik)
                .Include(r => r.StatusRezervacije)
                .AsQueryable();

            if (uloga == "Korisnik")
            {
                rezervacije = rezervacije.Where(r =>
                    r.IdKorisnik == idKorisnik &&
                    r.DatumOdjave >= DateTime.Today &&
                    (r.IdStatus == 1 || r.IdStatus == 2));
            }
            else if (uloga == "Admin")
            {
                rezervacije = rezervacije.Where(r =>
                    r.Smjestaj.IdAdmin == idKorisnik);
            }

            if (uloga == "Admin")
            {
                var smjestajiAdmina = _context.Smjestaji
                    .Include(s => s.Grad)
                    .Where(s => s.IdAdmin == idKorisnik)
                    .OrderBy(s => s.Naziv)
                    .ToList();

                var zauzetiTermini = _context.Rezervacije
                    .Include(r => r.Smjestaj)
                    .Include(r => r.StatusRezervacije)
                    .Where(r =>
                        r.Smjestaj.IdAdmin == idKorisnik &&
                        (r.IdStatus == 1 || r.IdStatus == 2) &&
                        r.DatumPrijave.Month == trenutniMjesec &&
                        r.DatumPrijave.Year == trenutnaGodina)
                    .ToList();

                ViewBag.SmjestajiAdmina = smjestajiAdmina;
                ViewBag.ZauzetiTermini = zauzetiTermini;
            }

            return View(rezervacije
                .OrderBy(r => r.DatumPrijave)
                .ToList());
        }

        //1-Na cekanju, 2-potvrdjena, 3-odbijena, 4-otkazana, 5-istekla

        [HttpPost]
        public IActionResult Confirm(int idRezervacija)
        {
            int idAdmin = HttpContext.Session.GetInt32("IdKorisnik").Value;

            var rezervacija = _context.Rezervacije
                .Include(r => r.Smjestaj)
                .FirstOrDefault(r =>
                    r.IdRezervacija == idRezervacija &&
                    r.Smjestaj.IdAdmin == idAdmin);

            if (rezervacija == null)
            {
                return NotFound();
            }

            rezervacija.IdStatus = 2;

            _context.Notifikacije.Add(new Notifikacija
            {
                IdKorisnik = rezervacija.IdKorisnik,
                Naslov = "Rezervacija potvrđena",
                Tekst = $"Vaša rezervacija za \"{rezervacija.Smjestaj.Naziv}\" je potvrđena."
            });

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Reject(int idRezervacija)
        {
            int idAdmin = HttpContext.Session.GetInt32("IdKorisnik").Value;

            var rezervacija = _context.Rezervacije
                .Include(r => r.Smjestaj)
                .FirstOrDefault(r =>
                    r.IdRezervacija == idRezervacija &&
                    r.Smjestaj.IdAdmin == idAdmin);

            if (rezervacija == null)
            {
                return NotFound();
            }

            rezervacija.IdStatus = 3;

            _context.Notifikacije.Add(new Notifikacija
            {
                IdKorisnik = rezervacija.IdKorisnik,
                Naslov = "Rezervacija odbijena",
                Tekst = $"Vaša rezervacija za \"{rezervacija.Smjestaj.Naziv}\" je odbijena."
            });

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Cancel(int idRezervacija)
        {
            int idKorisnik = HttpContext.Session.GetInt32("IdKorisnik").Value;

            var rezervacija = _context.Rezervacije
                .Include(r => r.Smjestaj)
                .FirstOrDefault(r =>
                    r.IdRezervacija == idRezervacija &&
                    r.IdKorisnik == idKorisnik);

            if (rezervacija == null)
            {
                return NotFound();
            }

            rezervacija.IdStatus = 4;

            _context.Notifikacije.Add(new Notifikacija
            {
                IdKorisnik = rezervacija.Smjestaj.IdAdmin,
                Naslov = "Rezervacija otkazana",
                Tekst = $"{HttpContext.Session.GetString("Ime")} je otkazao rezervaciju za \"{rezervacija.Smjestaj.Naziv}\"."
            });

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Availability(int idSmjestaj, int mjesec, int godina)
        {
            int daniUMjesecu = DateTime.DaysInMonth(godina, mjesec);

            var rezervacije = _context.Rezervacije
                .Where(r =>
                    r.IdSmjestaj == idSmjestaj &&
                    (r.IdStatus == 1 || r.IdStatus == 2) &&
                    r.DatumPrijave.Year == godina &&
                    r.DatumPrijave.Month == mjesec)
                .ToList();

            var dani = new List<object>();

            for (int dan = 1; dan <= daniUMjesecu; dan++)
            {
                var datum = new DateTime(godina, mjesec, dan);

                var rezervacijaDana = rezervacije.FirstOrDefault(r =>
                    datum >= r.DatumPrijave.Date &&
                    datum < r.DatumOdjave.Date);

                string status = "slobodno";

                if (rezervacijaDana != null)
                {
                    if (rezervacijaDana.IdStatus == 1)
                        status = "naCekanju";

                    if (rezervacijaDana.IdStatus == 2)
                        status = "zauzeto";
                }

                dani.Add(new
                {
                    Dan = dan,
                    Status = status
                });
            }

            return Json(dani);
        }
    }
}