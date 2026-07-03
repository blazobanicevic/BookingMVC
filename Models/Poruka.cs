using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingMVC.Models
{
    public class Poruka
    {
        [Key]
        public int IdPoruka { get; set; }

        [Required]
        public int IdPosiljalac { get; set; }

        [Required]
        public int IdPrimalac { get; set; }

        [Required]
        public string Sadrzaj { get; set; }

        [Required]
        public DateTime VrijemeSlanja { get; set; }

        [Required]
        public bool Procitana { get; set; }

        [ForeignKey("IdPosiljalac")]
        public Korisnik Posiljalac { get; set; }

        [ForeignKey("IdPrimalac")]
        public Korisnik Primalac { get; set; }
    }
}
