using System.ComponentModel.DataAnnotations;

namespace BookingMVC.Models
{
    public class Sadrzaj
    {
        [Key]
        public int IdSadrzaj { get; set; }

        [Required]
        [StringLength(50)]
        public string Naziv { get; set; }
    }
}
