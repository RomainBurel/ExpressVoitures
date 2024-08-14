using ExpressVoitures.Models.ViewModels;

namespace ExpressVoitures.Services
{
    public interface ICarService
    {
        public CarModel GetById(int id);

        public IEnumerable<CarModel> GetAll();

        public void Add(CarModel carModel);

        public void Update(CarModel carModel);

        public void Delete(CarModel carModel);

        public void SaveChanges();
    }
}
