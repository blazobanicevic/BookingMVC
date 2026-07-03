using System.ComponentModel.DataAnnotations;

namespace BookingMVC.Models
{
    public class TipSmjestaja
    {
        [Key]
        public int IdTipSmjestaja { get; set; }

        [Required]
        [StringLength(30)]
        public string Naziv { get; set; }
    }
}
