using ExpressVoitures.Models.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models.ViewModels
{
    public class CarForSaleModel
    {
        public int IdCarForSale { get; set; }

        [DisplayName("Voiture")]
        public int IdCar { get; set; }

        [Required(ErrorMessage = "La sélection d'une date de disponibilité est obligatoire.")]
        [DisplayName("Date de disponibilité")]
        public DateTime DateOfAvailabilityForSale { get; set; }

        [DisplayName("Prix de vente")]
        public double SellingPrice { get; set; }

        public string? Description { get; set; }

        [DisplayName("Photo")]
        public string? ImagePath { get; set; }

        [DisplayName("Date de vente")]
        public DateTime? DateOfSale { get; set; }

        public List<Car>? Cars { get; set; }

        [DisplayName("Voiture")]
        public string? CarLabel { get; set; }
    }
}
