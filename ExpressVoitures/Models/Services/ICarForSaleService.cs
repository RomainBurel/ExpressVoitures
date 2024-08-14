using ExpressVoitures.Models.Entities;
using ExpressVoitures.ViewModels;

namespace ExpressVoitures.Services
{
    public interface ICarForSaleService
    {
        public CarForSaleModel GetById(int id);

        public IEnumerable<CarForSaleModel> GetAll();

        public void Add(CarForSaleModel carForSaleModel, IFormFile image);

        public void Update(CarForSaleModel carForSaleModel);

        public void Delete(CarForSaleModel carForSaleModel);

        public void SaveChanges();

        public List<Car> GetCarsAvailable();
    }
}
