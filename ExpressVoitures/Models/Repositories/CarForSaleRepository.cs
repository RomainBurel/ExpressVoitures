using ExpressVoitures.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitures.Models.Repositories
{
    public class CarForSaleRepository : GenericRepository<CarForSale>, ICarForSaleRepository
    {
        private DbSet<Car> _carsDbSet;

        public CarForSaleRepository(DbContext context) : base(context)
        {
            this._carsDbSet = this._context.Set<Car>();
        }

        public List<Car> GetCarsAvailable()
        {
            return this._carsDbSet.Where(c => !c.NoMoreAvailable && !this._dbSet.Any(s => s.IdCar == c.IdCar)).ToList();
        }
    }
}
