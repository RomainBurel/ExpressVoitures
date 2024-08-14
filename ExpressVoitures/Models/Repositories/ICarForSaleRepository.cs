using ExpressVoitures.Models.Entities;

namespace ExpressVoitures.Models.Repositories
{
    public interface ICarForSaleRepository : IGenericRepository<CarForSale>
    {
        public List<Car> GetCarsAvailable();
    }
}
