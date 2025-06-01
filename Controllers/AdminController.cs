using ASP.NET_Projekt_Wypozyczalnia.Services;
using ASP.NET_Projekt_Wypozyczalnia.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Projekt_Wypozyczalnia.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ICarService _carService;
        private readonly IClientService _clientService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(
            ICarService carService,
            IClientService clientService,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _carService = carService;
            _clientService = clientService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Dashboard()
        {
            var cars = await _carService.GetAllCarsAsync();
            var (clients, _) = await _clientService.GetAllClientsAsync(1, int.MaxValue);

            var users = _userManager.Users.ToList();
            var roles = _roleManager.Roles.Select(r => r.Name).ToList();

             var userRoles = new Dictionary<string, List<string>>();

             foreach (var user in users)
            {
                var userRoleList = await _userManager.GetRolesAsync(user);
                userRoles[user.Id] = userRoleList.ToList();
                Debug.WriteLine($"Użytkownik: {user.Email}, Role: {string.Join(", ", userRoleList)}");
            }

            var model = new DashboardViewModel
            {
                CarCount = cars.Count(),
                ClientCount = clients.Count(),
                Users = users,
                Roles = roles,
                UserRoles = userRoles
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(DashboardViewModel model)
        {
            if (!string.IsNullOrEmpty(model.NewUserEmail) && !string.IsNullOrEmpty(model.NewUserPassword))
            {
                var user = new IdentityUser { UserName = model.NewUserEmail, Email = model.NewUserEmail };
                var result = await _userManager.CreateAsync(user, model.NewUserPassword);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Dashboard));
            }

            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(DashboardViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.SelectedUserId);

            if (user != null && !string.IsNullOrEmpty(model.SelectedRole))
            {
                // 1. Pobierz wszystkie role użytkownika
                var currentRoles = await _userManager.GetRolesAsync(user);

                // 2. Usuń je wszystkie
                if (currentRoles.Any())
                {
                    var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    if (!removeResult.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, "Nie udało się usunąć aktualnych ról.");
                        return RedirectToAction(nameof(Dashboard));
                    }
                }

                // 3. Dodaj nową rolę
                var addResult = await _userManager.AddToRoleAsync(user, model.SelectedRole);
                if (!addResult.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Nie udało się przypisać nowej roli.");
                }
            }

            return RedirectToAction(nameof(Dashboard));
}
    }
}
