using Microsoft.EntityFrameworkCore;
using BookingMVC.Models;
using System.Linq;

namespace BookingMVC.Data
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Uloga> Uloge { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Grad> Gradovi { get; set; }
        public DbSet<TipSmjestaja> TipoviSmjestaja { get; set; }
        public DbSet<Smjestaj> Smjestaji { get; set; }
        public DbSet<Sadrzaj> Sadrzaji { get; set; }
        public DbSet<SmjestajSadrzaj> SmjestajSadrzaji { get; set; }
        public DbSet<StatusRezervacije> StatusiRezervacija { get; set; }
        public DbSet<Rezervacija> Rezervacije { get; set; }
        public DbSet<ListaZelja> ListaZelja { get; set; }
        public DbSet<Recenzija> Recenzije { get; set; }
        public DbSet<Poruka> Poruke { get; set; }
        public DbSet<Notifikacija> Notifikacije { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SmjestajSadrzaj>()
                .HasKey(ss => new { ss.IdSmjestaj, ss.IdSadrzaj });

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                         .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}