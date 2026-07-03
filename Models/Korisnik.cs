using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingMVC.Models
{
    public class Korisnik
    {
        [Key]
        public int IdKorisnik { get; set; }

        [Required]
        [StringLength(50)]
        public string Ime { get; set; }

        [Required]
        [StringLength(50)]
        public string Prezime { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Lozinka { get; set; }

        [Required]
        public int IdUloga { get; set; }

        [ForeignKey("IdUloga")]
        public Uloga Uloga { get; set; }
    }
}
