using BookingMVC.Data;
using BookingMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingMVC.Controllers
{
    public class PorukaController : BaseController
    {
        private readonly BookingDbContext _context;

        public PorukaController(BookingDbContext context)
        {
            _context = context;
        }
        public IActionResult ChatPanel(int? idSagovornik)
        {
            int idKorisnik = HttpContext.Session.GetInt32("IdKorisnik").Value;

            var svePoruke = _context.Poruke
                .Include(p => p.Posiljalac)
                .Include(p => p.Primalac)
                .Where(p => p.IdPosiljalac == idKorisnik || p.IdPrimalac == idKorisnik)
                .OrderBy(p => p.VrijemeSlanja)
                .ToList();

            var sagovornikIds = svePoruke
                .Select(p => p.IdPosiljalac == idKorisnik ? p.IdPrimalac : p.IdPosiljalac)
                .Distinct()
                .ToList();

            var sagovornici = _context.Korisnici
                .Where(k => sagovornikIds.Contains(k.IdKorisnik))
                .ToList();

            if (idSagovornik.HasValue && !sagovornici.Any(k => k.IdKorisnik == idSagovornik.Value))
            {
                var noviSagovornik = _context.Korisnici.Find(idSagovornik.Value);

                if (noviSagovornik != null)
                {
                    sagovornici.Add(noviSagovornik);
                }
            }

            int? aktivniSagovornikId = idSagovornik ?? sagovornikIds.FirstOrDefault();

            var porukeSaSagovornikom = svePoruke
                .Where(p =>
                    aktivniSagovornikId != null &&
                    (
                        (p.IdPosiljalac == idKorisnik && p.IdPrimalac == aktivniSagovornikId) ||
                        (p.IdPosiljalac == aktivniSagovornikId && p.IdPrimalac == idKorisnik)
                    ))
                .ToList();

            ViewBag.Sagovornici = sagovornici;
            ViewBag.AktivniSagovornikId = aktivniSagovornikId;
            ViewBag.IdKorisnik = idKorisnik;

            return PartialView("_ChatPanel", porukeSaSagovornikom);
        }

        [HttpPost]
        public IActionResult Reply(int idPrimalac, string sadrzaj)
        {
            int idPosiljalac = HttpContext.Session.GetInt32("IdKorisnik").Value;

            var poruka = new Poruka
            {
                IdPosiljalac = idPosiljalac,
                IdPrimalac = idPrimalac,
                Sadrzaj = sadrzaj,
                VrijemeSlanja = DateTime.Now,
                Procitana = false
            };

            _context.Poruke.Add(poruka);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
