using Microsoft.AspNetCore.Mvc;
using ASP.NET_Projekt_Wypozyczalnia.Models;

namespace ASP.NET_Projekt_Wypozyczalnia.Controllers
{
    public class KlientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Klient klient)
        {
            return View();
        }
    }
}
