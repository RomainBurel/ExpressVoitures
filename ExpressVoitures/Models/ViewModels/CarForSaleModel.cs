using ExpressVoitures.Models.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models.ViewModels
{
    public class CarForSaleModel: IValidatableObject
    {
        public int IdCarForSale { get; set; }

        [DisplayName("Voiture")]
        public int IdCar { get; set; }

        [Required(ErrorMessage = "La sélection d'une date de disponibilité est obligatoire.")]
        [DisplayName("Date de disponibilité")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfAvailabilityForSale { get; set; }

        [DisplayName("Prix de vente")]
        public double SellingPrice { get; set; }

        public string? Description { get; set; }

        [DisplayName("Photo")]
        public string? ImagePath { get; set; }

        [DisplayName("Date de vente")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateOfSale { get; set; }

        public List<Car>? Cars { get; set; }

        [DisplayName("Voiture")]
        public string? CarLabel { get; set; }

        public Car? Car { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var validationResults = new List<ValidationResult>();

			if (Car != null && DateOfAvailabilityForSale < Car.DateOfBuy)
			{
				validationResults.Add(new ValidationResult("La date de disponibilité à la vente ne peut pas être antérieure à la date d'achat.", new[] { nameof(DateOfAvailabilityForSale) }));
			}

			if (DateOfSale.HasValue && DateOfSale.Value < DateOfAvailabilityForSale)
			{
				validationResults.Add(new ValidationResult("La date de vente ne peut pas être antérieure à la date de disponibilité à la vente.", new[] { nameof(DateOfSale) }));
			}

			return validationResults;
		}
	}
}
