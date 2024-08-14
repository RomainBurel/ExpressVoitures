using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpressVoitures.Models.Entities
{
    public class CarForSale
    {
        [Key]
        public int IdCarForSale { get; set; }

        [ForeignKey("IdCar")]
        public int IdCar { get; set; }

        public DateTime DateOfAvailabilityForSale { get; set; }

        public double SellingPrice { get; set; }

        public string? Description { get; set; }

        public DateTime? DateOfSale { get; set; }

        public string? ImagePath { get; set; }
    }
}
