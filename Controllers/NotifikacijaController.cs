using BookingMVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingMVC.Controllers
{
    public class NotifikacijaController : BaseController
    {
        private readonly BookingDbContext _context;

        public NotifikacijaController(BookingDbContext context)
        {
            _context = context;
        }

        public IActionResult NotificationPanel()
        {
            int idKorisnik = HttpContext.Session.GetInt32("IdKorisnik").Value;

            var notifikacije = _context.Notifikacije
                .Where(n => n.IdKorisnik == idKorisnik)
                .OrderByDescending(n => n.IdNotifikacija)
                .ToList();

            return PartialView("_NotificationPanel", notifikacije);
        }

        [HttpPost]
        public IActionResult ClearAll()
        {
            int idKorisnik = HttpContext.Session.GetInt32("IdKorisnik").Value;

            var notifikacije = _context.Notifikacije
                .Where(n => n.IdKorisnik == idKorisnik)
                .ToList();

            _context.Notifikacije.RemoveRange(notifikacije);
            _context.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
