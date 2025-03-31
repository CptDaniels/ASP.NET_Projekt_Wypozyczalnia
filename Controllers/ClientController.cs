using Microsoft.AspNetCore.Mvc;
using ASP.NET_Projekt_Wypozyczalnia.Models;
using ASP.NET_Projekt_Wypozyczalnia.Data;
using Microsoft.EntityFrameworkCore;
using ASP.NET_Projekt_Wypozyczalnia.Services;

namespace ASP.NET_Projekt_Wypozyczalnia.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }
        //GET
        public async Task<IActionResult> Index()
        {
            var clients = await _clientService.GetAllClientsAsync();
            return View(clients);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientID,FirstName,LastName,Email,PhoneNumber,DocumentNumber,DocumentType,Address")] Client client)
        {
            if (ModelState.IsValid)
            {
                await _clientService.AddClientAsync(client);
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }
        //GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = await _clientService.GetClientByIdAsync(id.Value);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientID,FirstName,LastName,Email,PhoneNumber,DocumentNumber,DocumentType,Address")] Client client)
        {
            if (id != client.ClientID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _clientService.UpdateClientAsync(client);
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }
        //GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = await _clientService.GetClientByIdAsync(id.Value);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _clientService.DeleteClientAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}