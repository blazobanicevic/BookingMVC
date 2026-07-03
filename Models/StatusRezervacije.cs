using System.ComponentModel.DataAnnotations;

namespace BookingMVC.Models
{
    public class StatusRezervacije
    {
        [Key]
        public int IdStatus { get; set; }

        [Required]
        [StringLength(30)]
        public string Naziv { get; set; }
    }
}
