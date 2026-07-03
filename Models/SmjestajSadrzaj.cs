using System.ComponentModel.DataAnnotations.Schema;

namespace BookingMVC.Models
{
    public class SmjestajSadrzaj
    {
        public int IdSmjestaj { get; set; }

        public int IdSadrzaj { get; set; }

        [ForeignKey("IdSmjestaj")]
        public Smjestaj Smjestaj { get; set; }

        [ForeignKey("IdSadrzaj")]
        public Sadrzaj Sadrzaj { get; set; }
    }
}
