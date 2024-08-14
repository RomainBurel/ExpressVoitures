using ExpressVoitures.Models.Entities;
using ExpressVoitures.Models.Repositories;
using ExpressVoitures.ViewModels;

namespace ExpressVoitures.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            this._carRepository = carRepository;
        }

        public CarModel GetById(int id)
        {
            return this.GetCarModelFromCar(this._carRepository.GetById(id));
        }

        public IEnumerable<CarModel> GetAll()
        {
            return this._carRepository.GetAll().Select(c => this.GetCarModelFromCar(c)).ToList();
        }

        public void Add(CarModel carModel)
        {
            this._carRepository.Add(this.GetCarFromCarModel(carModel));
        }

        public void Update(CarModel carModel)
        {
            this._carRepository.Update(this.GetCarFromCarModel(carModel));
        }

        public void Delete(CarModel carModel)
        {
            Car car = this.GetCarFromCarModel(carModel);
            //TODO : Remove CarForSale ?
            this._carRepository.Remove(car);
        }

        public void SaveChanges()
        {
            this._carRepository.SaveChanges(); 
        }

        private CarModel GetCarModelFromCar(Car car)
        {
            return new CarModel
            {
                IdCar = car.IdCar,
                VIN = car.VIN,
                Year = car.Year,
                Brand = car.Brand,
                Model = car.Model,
                Finish = car.Finish,
                DateOfBuy = car.DateOfBuy,
                Price = car.Price,
                RepairDescription = car.RepairDescription,
                DateOfRepair = car.DateOfRepair,
                RepairCost = car.RepairCost,
                NoMoreAvailable = car.NoMoreAvailable
            };
        }

        private Car GetCarFromCarModel(CarModel carModel)
        {
            return new Car
            {
                IdCar = carModel.IdCar,
                VIN = carModel.VIN,
                Year = carModel.Year,
                Brand = carModel.Brand,
                Model = carModel.Model,
                Finish = carModel.Finish,
                DateOfBuy = carModel.DateOfBuy,
                Price = carModel.Price,
                RepairDescription = carModel.RepairDescription,
                DateOfRepair = carModel.DateOfRepair,
                RepairCost = carModel.RepairCost,
                NoMoreAvailable = carModel.NoMoreAvailable
            };
        }
    }
}
