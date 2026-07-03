using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingMVC.Models
{
    public class Notifikacija
    {
        [Key]
        public int IdNotifikacija { get; set; }

        [Required]
        public int IdKorisnik { get; set; }

        [Required]
        [StringLength(100)]
        public string Naslov { get; set; }

        [Required]
        public string Tekst { get; set; }

        [ForeignKey("IdKorisnik")]
        public Korisnik Korisnik { get; set; }
    }
}
