using Microsoft.AspNetCore.Mvc;
using ASP.NET_Projekt_Wypozyczalnia.Models;
using ASP.NET_Projekt_Wypozyczalnia.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ASP.NET_Projekt_Wypozyczalnia.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        // GET Odczyt
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var cars = await _carService.GetAllCarsAsync();
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
                await _carService.AddCarAsync(car);
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

            var car = await _carService.GetCarByIdAsync(id.Value);
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
                await _carService.UpdateCarAsync(car);
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

            var car = await _carService.GetCarByIdAsync(id.Value);
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
            await _carService.DeleteCarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}