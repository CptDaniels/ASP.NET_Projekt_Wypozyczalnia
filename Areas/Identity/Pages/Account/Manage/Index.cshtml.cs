// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ASP.NET_Projekt_Wypozyczalnia.Data;
using ASP.NET_Projekt_Wypozyczalnia.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace test.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IValidator<Client> _clientValidator;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext context,
            IValidator<Client> clientValidator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _clientValidator = clientValidator;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Phone]
            [Display(Name = "Numer telefonu")]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "Imie")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Nazwisko")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Adres Zamieszkania")]
            public string Address { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);

            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.Email == user.Email);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = client?.PhoneNumber,
                FirstName = client?.FirstName,
                LastName = client?.LastName,
                Address = client?.Address
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            
            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.Email == user.Email);

            var validationResult = await _clientValidator.ValidateAsync(client);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }

                await LoadAsync(user);
                return Page();
            }
            if (client != null)
            {
                client.FirstName = Input.FirstName;
                client.LastName = Input.LastName;
                client.PhoneNumber = Input.PhoneNumber;
                client.Address = Input.Address;

                _context.Clients.Update(client);
                await _context.SaveChangesAsync();
            }
            else
            {
                client = new Client
                {
                    Email = user.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    PhoneNumber = Input.PhoneNumber,
                    Address = Input.Address,
                    DocumentNumber = "Nie podano",
                    DocumentType = DocumentType.ID
                };
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Profil zaktualizowany";
            return RedirectToPage();
        }
    }
}
