using ExpressVoitures.Models.Entities;
using ExpressVoitures.Models.Repositories;
using ExpressVoitures.Services;
using Microsoft.AspNetCore.Hosting;
using Moq;

namespace Test
{
	public class CarForSaleServiceTest
	{
		private List<Car> GetAllCars()
		{
			return new List<Car>()
			{
				new Car
				{
					IdCar = 1,
					VIN = "123AB56",
					Brand = "Ford",
					Model = "Fiesta",
					Finish = "Trend",
					Year = 2018,
					DateOfBuy = new DateTime(2024, 1, 1),
					Price = 8000d,
					DateOfRepair = new DateTime(2024, 2, 1),
					RepairCost = 1500d,
					RepairDescription = "Révision + carrosserie",
					NoMoreAvailable = false
				},
				new Car
				{
					IdCar = 2,
					VIN = "234BC67",
					Brand = "Peugeot",
					Model = "208",
					Finish = "Active",
					Year = 2020,
					DateOfBuy = new DateTime(2024, 3, 1),
					Price = 7000d,
					DateOfRepair = new DateTime(2024, 4, 1),
					RepairCost = 100d,
					RepairDescription = "Révision",
					NoMoreAvailable = false
				}
			};
		}

		private Car GetCar(int idCar)
		{
			return this.GetAllCars().FirstOrDefault(c => c.IdCar == idCar);
		}

		[Fact]
		public void CalculatedSellingPriceIsCorrect()
		{
			// Arrange
			var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
			var mockCarForSaleRepository = new Mock<ICarForSaleRepository>();
			var mockCarRepository = new Mock<ICarRepository>();
			mockCarRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(this.GetCar(1));
			var carForSaleService = new CarForSaleService(mockWebHostEnvironment.Object, mockCarForSaleRepository.Object, mockCarRepository.Object);
			var car = this.GetCar(1);

			// Act
			var calculatedSellingPrice = carForSaleService.GetSellingPrice(1);

			// Assert
			Assert.Equal(car.Price + car.RepairCost + 500d, calculatedSellingPrice);
		}
	}
}
