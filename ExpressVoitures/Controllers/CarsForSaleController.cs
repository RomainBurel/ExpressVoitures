using ExpressVoitures.Models.ViewModels;
using ExpressVoitures.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitures.Controllers
{
    public class CarsForSaleController : Controller
    {
        private readonly ICarForSaleService _carForSaleService;

        public CarsForSaleController(ICarForSaleService carForSaleService)
        {
            this._carForSaleService = carForSaleService;
        }

        // GET: CarsForSale
        public IActionResult Index()
        {
            return View(this._carForSaleService.GetAll());
        }

        // GET: CarsForSale/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carForSale = this._carForSaleService.GetById(id.Value);
            if (carForSale == null)
            {
                return NotFound();
            }

            return View(carForSale);
        }

        // GET: CarsForSale/Create
        public IActionResult Create()
        {
            var viewModel = new CarForSaleModel()
            {
                DateOfAvailabilityForSale = DateTime.Today,
                Cars = _carForSaleService.GetCarsAvailable()
            };

            return View(viewModel);
        }

        // POST: CarsForSale/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CarForSaleModel carForSaleModel, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                this._carForSaleService.Add(carForSaleModel, image);
                this._carForSaleService.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(carForSaleModel);
        }

        // GET: CarsForSale/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carForSaleModel = this._carForSaleService.GetById(id.Value);
            if (carForSaleModel == null)
            {
                return NotFound();
            }
            return View(carForSaleModel);
        }

        // POST: CarsForSale/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CarForSaleModel carForSaleModel)
        {
            if (id != carForSaleModel.IdCarForSale)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this._carForSaleService.Update(carForSaleModel);
                    this._carForSaleService.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarForSaleExists(carForSaleModel.IdCarForSale))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(carForSaleModel);
        }

        // GET: CarsForSale/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carForSale = this._carForSaleService.GetById(id.Value);
            if (carForSale == null)
            {
                return NotFound();
            }
            return View(carForSale);
        }

        // POST: CarsForSale/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, CarForSaleModel carForSaleModel)
        {
            if (id != carForSaleModel.IdCarForSale)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this._carForSaleService.Delete(carForSaleModel);
                    this._carForSaleService.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarForSaleExists(carForSaleModel.IdCarForSale))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        private bool CarForSaleExists(int id)
        {
            return this._carForSaleService.GetById(id) != null;
        }
    }
}
