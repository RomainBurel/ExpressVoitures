using ExpressVoitures.Models.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models.ViewModels
{
    public class CarModel: IValidatableObject
    {
        public int IdCar { get; set; }

        [DisplayName("Numéro d'identification du véhicule")]
        public string? VIN { get; set; }

        [Required(ErrorMessage = "La saisie de l'année est obligatoire.")]
        [DisplayName("Année de mise en circulation du véhicule")]
		[RangeUntilCurrentYear(1990, ErrorMessage = "L'année doit être comprise entre 1990 et aujourd'hui.")]
		public int Year { get; set; }

        [Required(ErrorMessage = "La saisie de la marque est obligatoire.")]
        [DisplayName("Marque du véhicule")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "La saisie du modèle est obligatoire.")]
        [DisplayName("Modèle du véhicule")]
        public string Model { get; set; }

        [DisplayName("Finition du véhicule")]
        public string? Finish { get; set; }

        [Required(ErrorMessage = "La saisie de la date d'achat est obligatoire.")]
        [DisplayName("Date d'achat du véhicule")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfBuy { get; set; }

        [Required(ErrorMessage = "La saisie du prix d'achat est obligatoire.")]
        [DisplayName("Prix d'achat du véhicule")]
        public double Price { get; set; }

        [DisplayName("Réparation(s) effectuée(s) sur le véhicule")]
        public string? RepairDescription { get; set; }

        [DisplayName("Date de la réparation")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateOfRepair { get; set; }

        [Required(ErrorMessage = "La saisie du coût de réparation est obligatoire (mettre 0 si aucun).")]
        [DisplayName("Coût de la réparation")]
        public double RepairCost { get; set; }

        [DisplayName("Véhicule plus disponible")]
        public bool NoMoreAvailable { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if (DateOfBuy.Year < Year)
            {
                validationResults.Add(new ValidationResult("La date d'achat ne peut pas être inférieure à l'année de mise en circulation.", new[] { nameof(DateOfBuy) }));
            }

            if (DateOfRepair.HasValue && DateOfRepair.Value < DateOfBuy)
            {
                validationResults.Add(new ValidationResult("La date de réparation ne peut pas être antérieure à la date d'achat.", new[] { nameof(DateOfRepair) }));
            }

            return validationResults;
        }
    }
}
