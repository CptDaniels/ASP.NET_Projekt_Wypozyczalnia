using Microsoft.AspNetCore.Mvc;
using ASP.NET_Projekt_Wypozyczalnia.Models;
using ASP.NET_Projekt_Wypozyczalnia.Data;
using Microsoft.EntityFrameworkCore;
using ASP.NET_Projekt_Wypozyczalnia.Services;
using ASP.NET_Projekt_Wypozyczalnia.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ASP.NET_Projekt_Wypozyczalnia.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }
        //GET z paginacją
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            // Ograniczenie na pierwszej stronie
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            var clients = await _clientService.GetAllClientsAsync(pageNumber, pageSize);

            var viewModels = clients.Select(client => new ClientViewModel
            {
                ClientID = client.ClientID,
                FullName = $"{client.FirstName} {client.LastName}",
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                DocumentInfo = $"{GetDocumentName(client.DocumentType)} - {client.DocumentNumber}",
                Address = client.Address
            }).ToList();

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;

            return View(viewModels);
        }
        private static string GetDocumentName(DocumentType documentType)
        {
            return documentType switch
            {
                DocumentType.ID => "Dowód osobisty",
                DocumentType.DriverLicence => "Prawo jazdy",
                _ => documentType.ToString()
            };
        }
        //GET
        public IActionResult Create()
        {
            return View(new ClientViewModel());
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