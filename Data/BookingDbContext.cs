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

            modelBuilder.Entity<Uloga>().HasData(
    new Uloga { IdUloga = 1, Naziv = "Korisnik" },
    new Uloga { IdUloga = 2, Naziv = "Admin" }
);

            modelBuilder.Entity<StatusRezervacije>().HasData(
                new StatusRezervacije { IdStatus = 1, Naziv = "Na cekanju" },
                new StatusRezervacije { IdStatus = 2, Naziv = "Potvrdjena" },
                new StatusRezervacije { IdStatus = 3, Naziv = "Odbijena" },
                new StatusRezervacije { IdStatus = 4, Naziv = "Otkazana" }
            );

            modelBuilder.Entity<TipSmjestaja>().HasData(
                new TipSmjestaja { IdTipSmjestaja = 1, Naziv = "Apartman" },
                new TipSmjestaja { IdTipSmjestaja = 2, Naziv = "Studio" },
                new TipSmjestaja { IdTipSmjestaja = 3, Naziv = "Soba" },
                new TipSmjestaja { IdTipSmjestaja = 4, Naziv = "Kuca" }
            );

            modelBuilder.Entity<Sadrzaj>().HasData(
                new Sadrzaj { IdSadrzaj = 1, Naziv = "Wi-Fi" },
                new Sadrzaj { IdSadrzaj = 2, Naziv = "Parking" },
                new Sadrzaj { IdSadrzaj = 3, Naziv = "Klima" },
                new Sadrzaj { IdSadrzaj = 4, Naziv = "Kuhinja" },
                new Sadrzaj { IdSadrzaj = 5, Naziv = "Terasa" },
                new Sadrzaj { IdSadrzaj = 6, Naziv = "Bazen" }
            );

            modelBuilder.Entity<Grad>().HasData(
                new Grad { IdGrad = 1, Naziv = "Podgorica", Drzava = "Crna Gora" },
                new Grad { IdGrad = 2, Naziv = "Budva", Drzava = "Crna Gora" },
                new Grad { IdGrad = 3, Naziv = "Kotor", Drzava = "Crna Gora" },
                new Grad { IdGrad = 4, Naziv = "Herceg Novi", Drzava = "Crna Gora" },
                new Grad { IdGrad = 5, Naziv = "Bar", Drzava = "Crna Gora" }
            );

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                         .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}