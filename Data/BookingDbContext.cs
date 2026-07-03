using Microsoft.EntityFrameworkCore;
using BookingMVC.Models;

namespace BookingMVC.Data
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options)
            : base(options)
        {
        }
    }
}
