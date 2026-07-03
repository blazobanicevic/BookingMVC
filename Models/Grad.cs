using System.ComponentModel.DataAnnotations;

namespace BookingMVC.Models
{
    public class Grad
    {
        [Key]
        public int IdGrad { get; set; }

        [Required]
        [StringLength(50)]
        public string Naziv { get; set; }

        [Required]
        [StringLength(50)]
        public string Drzava { get; set; }
    }
}
