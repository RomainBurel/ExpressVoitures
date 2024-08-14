using ExpressVoitures.Models.ViewModels;
using ExpressVoitures.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitures.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            this._carService = carService;
        }

        // GET: Cars
        public ViewResult Index()
        {
            return View(this._carService.GetAll());
        }

        // GET: Cars/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = this._carService.GetById(id.Value);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            var carModel = new CarModel { Year = 2016, DateOfBuy = DateTime.Today };
            return View(carModel);
        }

        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CarModel carModel)
        {
            if (ModelState.IsValid)
            {
                this._carService.Add(carModel);
                this._carService.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(carModel);
        }

        // GET: Cars/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = this._carService.GetById(id.Value);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CarModel carModel)
        {
            if (id != carModel.IdCar)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this._carService.Update(carModel);
                    this._carService.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(carModel.IdCar))
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
            return View(carModel);
        }

        // GET: Cars/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = this._carService.GetById(id.Value);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, CarModel carModel)
        {
            if (id != carModel.IdCar)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this._carService.Delete(carModel);
                    this._carService.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(carModel.IdCar))
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

        private bool CarExists(int id)
        {
            return this._carService.GetById(id) != null;
        }
    }
}
