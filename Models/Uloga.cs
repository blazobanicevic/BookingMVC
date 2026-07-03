using System.ComponentModel.DataAnnotations;

namespace BookingMVC.Models
{
    public class Uloga
    {
        [Key]
        public int IdUloga { get; set; }

        [Required]
        [StringLength(20)]
        public string Naziv { get; set; }
    }
}
