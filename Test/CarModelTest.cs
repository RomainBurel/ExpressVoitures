using ExpressVoitures.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Test
{
    public class CarModelTest
    {
        [Fact]
        public void CarShoudHaveYear()
        {
            // Arrange
            var carModel = new CarModel
            {
                IdCar = 1,
                VIN = "123AB56",
                Brand = "Ford",
                Model = "Fiesta",
                Finish = "Trend",
                DateOfBuy = new DateTime(2024, 1, 1),
                Price = 8000d,
                DateOfRepair = new DateTime(2024, 2, 1),
                RepairCost = 1500d,
                RepairDescription = "Révision + carrosserie",
                NoMoreAvailable = false
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(carModel, new ValidationContext(carModel), validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Year"));
        }
    }
}
