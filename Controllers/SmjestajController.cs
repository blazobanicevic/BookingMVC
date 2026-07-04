using BookingMVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingMVC.Controllers
{
    public class SmjestajController : BaseController
    {
        private readonly BookingDbContext _context;

        public SmjestajController(BookingDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Uloga") != "Admin")
            {
                return RedirectToAction("UserDashboard", "Home");
            }

            int idAdmin = HttpContext.Session.GetInt32("IdKorisnik").Value;

            var smjestaji = _context.Smjestaji
                .Include(s => s.Grad)
                .Include(s => s.TipSmjestaja)
                .Include(s => s.Admin)
                .Where(s => s.IdAdmin == idAdmin)
                .ToList();

            return View(smjestaji);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Uloga") != "Admin")
            {
                return RedirectToAction("UserDashboard", "Home");
            }

            ViewBag.Tipovi = _context.TipoviSmjestaja.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Models.Smjestaj smjestaj, string gradNaziv)
        {
            if (HttpContext.Session.GetString("Uloga") != "Admin")
            {
                return RedirectToAction("UserDashboard", "Home");
            }

            var grad = _context.Gradovi.FirstOrDefault(g => g.Naziv == gradNaziv);

            if (grad == null)
            {
                grad = new Models.Grad
                {
                    Naziv = gradNaziv,
                    Drzava = "Crna Gora"
                };

                _context.Gradovi.Add(grad);
                _context.SaveChanges();
            }

            smjestaj.IdGrad = grad.IdGrad;
            smjestaj.IdAdmin = HttpContext.Session.GetInt32("IdKorisnik").Value;
            smjestaj.Aktivan = true;

            _context.Smjestaji.Add(smjestaj);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("Uloga") != "Admin")
            {
                return RedirectToAction("UserDashboard", "Home");
            }

            var smjestaj = _context.Smjestaji
                .Include(s => s.Grad)
                .FirstOrDefault(s => s.IdSmjestaj == id);

            if (smjestaj == null)
            {
                return NotFound();
            }

            ViewBag.Tipovi = _context.TipoviSmjestaja.ToList();

            return View(smjestaj);
        }

        [HttpPost]
        public IActionResult Edit(Models.Smjestaj smjestaj, string gradNaziv)
        {
            if (HttpContext.Session.GetString("Uloga") != "Admin")
            {
                return RedirectToAction("UserDashboard", "Home");
            }

            var grad = _context.Gradovi.FirstOrDefault(g => g.Naziv == gradNaziv);

            if (grad == null)
            {
                grad = new Models.Grad
                {
                    Naziv = gradNaziv,
                    Drzava = "Crna Gora"
                };

                _context.Gradovi.Add(grad);
                _context.SaveChanges();
            }

            smjestaj.IdGrad = grad.IdGrad;
            smjestaj.IdAdmin = HttpContext.Session.GetInt32("IdKorisnik").Value;

            _context.Smjestaji.Update(smjestaj);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Uloga") != "Admin")
            {
                return RedirectToAction("UserDashboard", "Home");
            }

            var smjestaj = _context.Smjestaji.Find(id);

            if (smjestaj != null)
            {
                _context.Smjestaji.Remove(smjestaj);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Search(string search)
        {
            var query = _context.Smjestaji
                .Include(s => s.Grad)
                .Include(s => s.TipSmjestaja)
                .Include(s => s.Admin)
                .Where(s => s.Aktivan);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s =>
                    s.Naziv.Contains(search) ||
                    s.Grad.Naziv.Contains(search) ||
                    s.TipSmjestaja.Naziv.Contains(search));
            }

            var smjestaji = query.ToList();

            return View(smjestaji);
        }

        [HttpGet]
        public IActionResult AdvancedSearch()
        {
            ViewBag.Tipovi = _context.TipoviSmjestaja.ToList();
            return View();
        }

        [HttpGet]
        public IActionResult AdvancedSearchResults(string grad, int? idTip, int? brojOsoba, decimal? minCijena, decimal? maxCijena, DateTime? datumPrijave, DateTime? datumOdjave)
        {
            var query = _context.Smjestaji
                .Include(s => s.Grad)
                .Include(s => s.TipSmjestaja)
                .Include(s => s.Admin)
                .Where(s => s.Aktivan)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(grad))
            {
                query = query.Where(s => s.Grad.Naziv.Contains(grad));
            }

            if (idTip.HasValue)
            {
                query = query.Where(s => s.IdTip == idTip.Value);
            }

            if (brojOsoba.HasValue)
            {
                query = query.Where(s => s.BrojOsoba >= brojOsoba.Value);
            }

            if (minCijena.HasValue)
            {
                query = query.Where(s => s.CijenaPoNoci >= minCijena.Value);
            }

            if (maxCijena.HasValue)
            {
                query = query.Where(s => s.CijenaPoNoci <= maxCijena.Value);
            }

            return View("Search", query.ToList());
        }
    }
}
