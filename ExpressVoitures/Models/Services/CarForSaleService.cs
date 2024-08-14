using ExpressVoitures.Models.Entities;
using ExpressVoitures.Models.Repositories;
using ExpressVoitures.ViewModels;

namespace ExpressVoitures.Services
{
    public class CarForSaleService : ICarForSaleService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICarForSaleRepository _carForSaleRepository;
        private readonly ICarRepository _carRepository;

        public CarForSaleService(IWebHostEnvironment webHostEnvironment, ICarForSaleRepository carForSaleRepository, ICarRepository carRepository)
        {
            this._webHostEnvironment = webHostEnvironment;
            this._carForSaleRepository = carForSaleRepository;
            this._carRepository = carRepository;
        }

        public CarForSaleModel GetById(int id)
        {
            return this.GetCarForSaleModelFromCarForSale(this._carForSaleRepository.GetById(id));
        }

        public IEnumerable<CarForSaleModel> GetAll()
        {
            return this._carForSaleRepository.GetAll().Select(c => this.GetCarForSaleModelFromCarForSale(c)).ToList();
        }

        public void Add(CarForSaleModel carForSaleModel, IFormFile image)
        {
            var carForSale = this.GetCarForSaleFromCarForSaleModel(carForSaleModel);

            if (image != null)
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                string voiturePath = Path.Combine(webRootPath, @"img\cars");

                if (!string.IsNullOrEmpty(carForSale.ImagePath))
                {
                    var oldImagePath = Path.Combine(webRootPath, carForSale.ImagePath.TrimStart('\\'));
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }
                }

                using (var fileStream = new FileStream(Path.Combine(voiturePath, fileName), FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }

                carForSale.ImagePath = @"img\cars\" + fileName;
            }

            this._carForSaleRepository.Add(carForSale);
        }

        public void Update(CarForSaleModel carForSaleModel)
        {
            this._carForSaleRepository.Update(this.GetCarForSaleFromCarForSaleModel(carForSaleModel));
        }

        public void Delete(CarForSaleModel carForSaleModel)
        {
            CarForSale carForSale = this.GetCarForSaleFromCarForSaleModel(carForSaleModel);
            //TODO : Remove image
            this._carForSaleRepository.Remove(carForSale);
        }

        public void SaveChanges()
        {
            this._carForSaleRepository.SaveChanges();
        }

        public List<Car> GetCarsAvailable()
        {
            return _carForSaleRepository.GetCarsAvailable();
        }

        private CarForSaleModel GetCarForSaleModelFromCarForSale(CarForSale carForSale)
        {
            return new CarForSaleModel
            {
                IdCarForSale = carForSale.IdCarForSale,
                IdCar = carForSale.IdCar,
                DateOfAvailabilityForSale = carForSale.DateOfAvailabilityForSale,
                SellingPrice = this.GetSellingPrice(carForSale.IdCar),
                Description = carForSale.Description,
                ImagePath = carForSale.ImagePath,
                DateOfSale = carForSale.DateOfSale,
                Cars = this.GetCarsAvailable(),
                CarLabel = this.GetCarLabelForSale(carForSale.IdCar)
            };
        }

        public double GetSellingPrice(int idCar)
        {
            var car = this._carRepository.GetById(idCar);
            return car != null ? car.Price + car.RepairCost + 500d : 0d;
        }

        private CarForSale GetCarForSaleFromCarForSaleModel(CarForSaleModel carForSaleModel)
        {
            return new CarForSale
            {
                IdCarForSale = carForSaleModel.IdCarForSale,
                IdCar = carForSaleModel.IdCar,
                DateOfAvailabilityForSale = carForSaleModel.DateOfAvailabilityForSale,
                SellingPrice = carForSaleModel.SellingPrice,
                Description = carForSaleModel.Description,
                ImagePath = carForSaleModel.ImagePath,
                DateOfSale = carForSaleModel.DateOfSale
            };
        }

        private string GetCarLabelForSale(int? idCar)
        {
            if (idCar == null)
            {
                return "";
            }

            var car = this._carRepository.GetById(idCar.Value);
            return car != null ? car.Brand + " " + car.Model + " " + car.Finish + " " + car.VIN : "";
        }
    }
}
