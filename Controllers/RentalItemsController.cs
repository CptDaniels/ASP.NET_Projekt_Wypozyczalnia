using Microsoft.AspNetCore.Mvc;
using ASP.NET_Projekt_Wypozyczalnia.Models;
using ASP.NET_Projekt_Wypozyczalnia.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP.NET_Projekt_Wypozyczalnia.Controllers
{
    public class RentalItemsController : Controller
    {
        private readonly IRentalItemsRepository _rentalItemsRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ICarRepository _carRepository;

        public RentalItemsController(IRentalItemsRepository rentalItemsRepository, IClientRepository clientRepository, ICarRepository carRepository)
        {
            _rentalItemsRepository = rentalItemsRepository;
            _clientRepository = clientRepository;
            _carRepository = carRepository;
        }

        // GET Odczyt
        public async Task<IActionResult> Index()
        {
            var rentalItems = await _rentalItemsRepository.GetAllAsync();
            return View(rentalItems);
        }

        // GET Tworzenie
        public async Task<IActionResult> Create()
        {
            ViewBag.ClientID = new SelectList(await _clientRepository.GetAllAsync(), "ClientID", "FirstName");
            ViewBag.CarID = new SelectList(await _carRepository.GetAllAsync(), "CarID", "Make");
            return View();
        }

        // POST Tworzenie
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentalID,ClientID,CarID,RentalDate,ReturnDate,ExpectedReturnDate,RentalStatus")] RentalItems rentalItem)
        {
            if (ModelState.IsValid)
            {
                await _rentalItemsRepository.AddAsync(rentalItem);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ClientID = new SelectList(await _clientRepository.GetAllAsync(), "ClientID", "FirstName", rentalItem.ClientID);
            ViewBag.CarID = new SelectList(await _carRepository.GetAllAsync(), "CarID", "Make", rentalItem.CarID);
            return View(rentalItem);
        }

        // GET Edycja
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentalItem = await _rentalItemsRepository.GetByIdAsync(id.Value);
            if (rentalItem == null)
            {
                return NotFound();
            }

            ViewBag.ClientID = new SelectList(await _clientRepository.GetAllAsync(), "ClientID", "FirstName", rentalItem.ClientID);
            ViewBag.CarID = new SelectList(await _carRepository.GetAllAsync(), "CarID", "Make", rentalItem.CarID);
            return View(rentalItem);
        }

        // POST Edycja
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentalID,ClientID,CarID,RentalDate,ReturnDate,ExpectedReturnDate,RentalStatus")] RentalItems rentalItem)
        {
            if (id != rentalItem.RentalID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _rentalItemsRepository.UpdateAsync(rentalItem);
                return RedirectToAction(nameof(Index));
            }
            //To jest do listy rozwijanej w widoku dać @Html.DropDownListFor
            ViewBag.ClientID = new SelectList(await _clientRepository.GetAllAsync(), "ClientID", "FirstName", rentalItem.ClientID);
            ViewBag.CarID = new SelectList(await _carRepository.GetAllAsync(), "CarID", "Make", rentalItem.CarID);
            return View(rentalItem);
        }

        // GET Usuwanie
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentalItem = await _rentalItemsRepository.GetByIdAsync(id.Value);
            if (rentalItem == null)
            {
                return NotFound();
            }
            return View(rentalItem);
        }

        // POST Usuwanie
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _rentalItemsRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}