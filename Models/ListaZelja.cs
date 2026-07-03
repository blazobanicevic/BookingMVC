using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingMVC.Models
{
    public class ListaZelja
    {
        [Key]
        public int IdListaZelja { get; set; }

        [Required]
        public int IdKorisnik { get; set; }

        [Required]
        public int IdSmjestaj { get; set; }

        [ForeignKey("IdKorisnik")]
        public Korisnik Korisnik { get; set; }

        [ForeignKey("IdSmjestaj")]
        public Smjestaj Smjestaj { get; set; }
    }
}
