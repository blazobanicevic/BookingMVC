using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingMVC.Models
{
    public class Rezervacija
    {
        [Key]
        public int IdRezervacija { get; set; }

        [Required]
        public int IdSmjestaj { get; set; }

        [Required]
        public int IdKorisnik { get; set; }

        [Required]
        public int IdStatus { get; set; }

        [Required]
        public DateTime DatumPrijave { get; set; }

        [Required]
        public DateTime DatumOdjave { get; set; }

        [Required]
        public int BrojOsoba { get; set; }

        [ForeignKey("IdSmjestaj")]
        public Smjestaj Smjestaj { get; set; }

        [ForeignKey("IdKorisnik")]
        public Korisnik Korisnik { get; set; }

        [ForeignKey("IdStatus")]
        public StatusRezervacije StatusRezervacije { get; set; }
    }
}
