using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingMVC.Models
{
    public class Recenzija
    {
        [Key]
        public int IdRecenzija { get; set; }

        [Required]
        public int IdKorisnik { get; set; }

        [Required]
        public int IdSmjestaj { get; set; }

        [Required]
        public int Ocjena { get; set; }

        [Required]
        public string Komentar { get; set; }

        [ForeignKey("IdKorisnik")]
        public Korisnik Korisnik { get; set; }

        [ForeignKey("IdSmjestaj")]
        public Smjestaj Smjestaj { get; set; }
    }
}
