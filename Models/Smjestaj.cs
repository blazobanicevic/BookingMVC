using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingMVC.Models
{
    public class Smjestaj
    {
        [Key]
        public int IdSmjestaj { get; set; }

        [Required]
        public int IdAdmin { get; set; }

        [Required]
        public int IdGrad { get; set; }

        [Required]
        public int IdTip { get; set; }

        [Required]
        [StringLength(100)]
        public string Naziv { get; set; }

        [Required]
        public string Opis { get; set; }

        [Required]
        [StringLength(150)]
        public string Adresa { get; set; }

        [Required]
        public int BrojOsoba { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal CijenaPoNoci { get; set; }

        [Required]
        public bool Aktivan { get; set; }

        [ForeignKey("IdAdmin")]
        public Korisnik Admin { get; set; }

        [ForeignKey("IdGrad")]
        public Grad Grad { get; set; }

        [ForeignKey("IdTip")]
        public TipSmjestaja TipSmjestaja { get; set; }
    }
}
