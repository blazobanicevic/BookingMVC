using BookingMVC.Data;
using BookingMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingMVC.Controllers
{
    public class ListaZeljaController : BaseController
    {
        private readonly BookingDbContext _context;

        public ListaZeljaController(BookingDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Uloga") != "Korisnik")
            {
                return RedirectToAction("AdminDashboard", "Home");
            }

            int idKorisnik = HttpContext.Session.GetInt32("IdKorisnik").Value;

            var lista = _context.ListaZelja
                .Include(lz => lz.Smjestaj)
                    .ThenInclude(s => s.Grad)
                .Include(lz => lz.Smjestaj)
                    .ThenInclude(s => s.TipSmjestaja)
                .Where(lz => lz.IdKorisnik == idKorisnik)
                .ToList();

            return View(lista);
        }

        public IActionResult Add(int idSmjestaj)
        {
            if (HttpContext.Session.GetString("Uloga") != "Korisnik")
            {
                return RedirectToAction("AdminDashboard", "Home");
            }

            int idKorisnik = HttpContext.Session.GetInt32("IdKorisnik").Value;

            bool vecPostoji = _context.ListaZelja.Any(lz =>
                lz.IdKorisnik == idKorisnik &&
                lz.IdSmjestaj == idSmjestaj);

            if (!vecPostoji)
            {
                var stavka = new ListaZelja
                {
                    IdKorisnik = idKorisnik,
                    IdSmjestaj = idSmjestaj
                };

                _context.ListaZelja.Add(stavka);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            if (HttpContext.Session.GetString("Uloga") != "Korisnik")
            {
                return RedirectToAction("AdminDashboard", "Home");
            }

            var stavka = _context.ListaZelja.Find(id);

            if (stavka != null)
            {
                _context.ListaZelja.Remove(stavka);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Toggle(int idSmjestaj)
        {
            if (HttpContext.Session.GetString("Uloga") != "Korisnik")
            {
                return RedirectToAction("AdminDashboard", "Home");
            }

            int idKorisnik = HttpContext.Session.GetInt32("IdKorisnik").Value;

            var stavka = _context.ListaZelja.FirstOrDefault(lz =>
                lz.IdKorisnik == idKorisnik &&
                lz.IdSmjestaj == idSmjestaj);

            if (stavka == null)
            {
                _context.ListaZelja.Add(new ListaZelja
                {
                    IdKorisnik = idKorisnik,
                    IdSmjestaj = idSmjestaj
                });
            }
            else
            {
                _context.ListaZelja.Remove(stavka);
            }

            _context.SaveChanges();

            return RedirectToAction("Details", "Smjestaj", new { id = idSmjestaj });
        }
    }
}