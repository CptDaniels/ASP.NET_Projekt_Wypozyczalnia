using Microsoft.AspNetCore.Mvc;
using ASP.NET_Projekt_Wypozyczalnia.Models;
using ASP.NET_Projekt_Wypozyczalnia.Data;
using Microsoft.EntityFrameworkCore;
using ASP.NET_Projekt_Wypozyczalnia.Services;
using ASP.NET_Projekt_Wypozyczalnia.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using FluentValidation;
using ASP.NET_Projekt_Wypozyczalnia.Validators;
using Mapster;
using Microsoft.AspNetCore.Authorization;

namespace ASP.NET_Projekt_Wypozyczalnia.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IValidator<Client> _clientValidator;
        private readonly ApplicationDbContext _context;

        public ClientController(IClientService clientService, IValidator<Client> clientValidator, ApplicationDbContext context)
        {
            _clientService = clientService;
            _clientValidator = clientValidator;
            _context = context;
        }
        //GET z paginacją
        [AllowAnonymous]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            var (clients, totalCount) = await _clientService.GetAllClientsAsync(pageNumber, pageSize);

            //var viewModels = clients.Select(ClientViewModel.FromClient).ToList();
            var viewModels = clients.Adapt<List<ClientViewModel>>();

            var model = new ClientListViewModel
            {
                Clients = viewModels,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return View(model);
        }
        //GET
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new Client());
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Client client)
        {

            var validationResult = await _clientValidator.ValidateAsync(client);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(client);
            }

            await _clientService.AddClientAsync(client);
            return RedirectToAction(nameof(Index));
        }
        //GET
        [Authorize(Roles = "Admin,Manager")]
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
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int id, [Bind("ClientID,FirstName,LastName,Email,PhoneNumber,DocumentNumber,DocumentType,Address")] Client client)
        {
            if (id != client.ClientID)
            {
                return NotFound();
            }
    

            var validationResult = await _clientValidator.ValidateAsync(client);
    
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(client);
            }

            await _clientService.UpdateClientAsync(client);
            return RedirectToAction(nameof(Index));
        }
        //GET
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _clientService.DeleteClientAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}