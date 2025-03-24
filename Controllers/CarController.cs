using Microsoft.AspNetCore.Mvc;
using ASP.NET_Projekt_Wypozyczalnia.Models;
using ASP.NET_Projekt_Wypozyczalnia.Repositories;

namespace ASP.NET_Projekt_Wypozyczalnia.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;

        public CarController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        // GET Odczyt
        public async Task<IActionResult> Index()
        {
            var cars = await _carRepository.GetAllAsync();
            return View(cars);
        }

        // GET Tworzenie
        public IActionResult Create()
        {
            return View();
        }

        // POST Tworzenie
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarID,Make,Model,Year,RegistrationNumber,CarStatus,Mileage,InspectionDate,InsuranceDate,EngineCapacity,FuelType,CarPicture")] Car car)
        {
            if (ModelState.IsValid)
            {
                await _carRepository.AddAsync(car);
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET Edycja
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _carRepository.GetByIdAsync(id.Value);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST Edycja
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarID,Make,Model,Year,RegistrationNumber,CarStatus,Mileage,InspectionDate,InsuranceDate,EngineCapacity,FuelType,CarPicture")] Car car)
        {
            if (id != car.CarID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _carRepository.UpdateAsync(car);
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET Usuwanie
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _carRepository.GetByIdAsync(id.Value);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST Usuwanie
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _carRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}