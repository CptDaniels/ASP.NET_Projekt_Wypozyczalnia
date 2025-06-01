using Microsoft.AspNetCore.Mvc;
using ASP.NET_Projekt_Wypozyczalnia.Models;
using ASP.NET_Projekt_Wypozyczalnia.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace ASP.NET_Projekt_Wypozyczalnia.Controllers
{
    [Authorize]
    public class RentalItemsController : Controller
    {
        private readonly IRentalItemsService _rentalItemsservice;
        private readonly ICarService _carService;
        private readonly IClientService _clientService;

        public RentalItemsController(
            IRentalItemsService rentalItemsService,
            IClientService clientService,
            ICarService carService)
        {
            _clientService = clientService;
            _carService = carService;
            _rentalItemsservice = rentalItemsService;
        }

        // GET: List
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Index()
        {
            var rentalItems = await _rentalItemsservice.GetAllRentalItemsAsync();
            return View(rentalItems);
        }

        // GET: Create
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Create(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;

            var (clients, _) = await _clientService.GetAllClientsAsync(pageNumber, pageSize);
            ViewBag.ClientID = new SelectList(clients, "ClientID", "FirstName");
            ViewBag.CarID = new SelectList(await _carService.GetAllCarsAsync(), "CarID", "Make");

            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Create([Bind("RentalID,ClientID,CarID,RentalDate,ReturnDate,ExpectedReturnDate,RentalStatus")] RentalItems rentalItem)
        {
            if (ModelState.IsValid)
            {
                await _rentalItemsservice.AddRentalItemAsync(rentalItem);
                return RedirectToAction(nameof(Index));
            }

            var (clients, _) = await _clientService.GetAllClientsAsync(1, 10);
            ViewBag.ClientID = new SelectList(clients, "ClientID", "FirstName", rentalItem.ClientID);
            ViewBag.CarID = new SelectList(await _carService.GetAllCarsAsync(), "CarID", "Make", rentalItem.CarID);

            return View(rentalItem);
        }

        // GET: Edit
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0) return NotFound();

            var rentalItem = await _rentalItemsservice.GetRentalItemByIdAsync(id);
            if (rentalItem == null) return NotFound();

            var (clients, _) = await _clientService.GetAllClientsAsync(1, 10);
            ViewBag.ClientID = new SelectList(clients, "ClientID", "FirstName", rentalItem.ClientID);
            ViewBag.CarID = new SelectList(await _carService.GetAllCarsAsync(), "CarID", "Make", rentalItem.CarID);

            return View(rentalItem);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int id, [Bind("RentalID,ClientID,CarID,RentalDate,ReturnDate,ExpectedReturnDate,RentalStatus")] RentalItems rentalItem)
        {
            if (id != rentalItem.RentalID) return NotFound();

            if (ModelState.IsValid)
            {
                await _rentalItemsservice.UpdateRentalItemAsync(rentalItem);
                return RedirectToAction(nameof(Index));
            }

            var (clients, _) = await _clientService.GetAllClientsAsync(1, 10);
            ViewBag.ClientID = new SelectList(clients, "ClientID", "FirstName", rentalItem.ClientID);
            ViewBag.CarID = new SelectList(await _carService.GetAllCarsAsync(), "CarID", "Make", rentalItem.CarID);

            return View(rentalItem);
        }

        // GET: Delete
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return NotFound();

            var rentalItem = await _rentalItemsservice.GetRentalItemByIdAsync(id);
            if (rentalItem == null) return NotFound();

            return View(rentalItem);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _rentalItemsservice.DeleteRentalItemAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}