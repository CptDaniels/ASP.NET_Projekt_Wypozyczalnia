using ASP.NET_Projekt_Wypozyczalnia.Models;
using ASP.NET_Projekt_Wypozyczalnia.Services;
using ASP.NET_Projekt_Wypozyczalnia.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
            var allCars = await _carService.GetAllCarsAsync();

            var fullViewModels = allCars.Select(car => new CarViewModel
            {
                CarID = car.CarID,
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                RegistrationNumber = car.RegistrationNumber,
                CarStatus = car.CarStatus,
                Mileage = car.Mileage,
                InspectionDate = car.InspectionDate,
                InsuranceDate = car.InsuranceDate,
                EngineCapacity = car.EngineCapacity,
                FuelType = car.FuelType
            }).ToList();

            var publicViewModels = allCars.Select(car => new CarPublicViewModel
            {
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                CarStatus = car.CarStatus,
                FuelType = car.FuelType,
                Mileage = car.Mileage
            }).ToList();

            var viewModel = new CarIndexViewModel
            {
                AllCars = fullViewModels,
                PublicCars = publicViewModels
            };

            return View(viewModel);
        }


        // GET Tworzenie
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST Tworzenie
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("CarID,Make,Model,Year,RegistrationNumber,CarStatus,Mileage,InspectionDate,InsuranceDate,EngineCapacity,FuelType")] Car car)
        {
            if (ModelState.IsValid)
            {
                //if (car.ImageFile != null && car.ImageFile.Length > 0)
                //{
                //    var fileName = Path.GetFileName(car.ImageFile.FileName);
                //    var filePath = Path.Combine("wwwroot/images", fileName);

                //    using var stream = new FileStream(filePath, FileMode.Create);
                //    await car.ImageFile.CopyToAsync(stream);

                //    car.ImagePath = "/images/" + fileName;
                //}
                await _carService.AddCarAsync(car);
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET Edycja
        [Authorize(Roles = "Admin,Manager")]
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
        [Authorize(Roles = "Admin,Manager")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _carService.DeleteCarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}