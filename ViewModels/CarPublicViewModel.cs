using System.ComponentModel.DataAnnotations;
using ASP.NET_Projekt_Wypozyczalnia.Models;

namespace ASP.NET_Projekt_Wypozyczalnia.ViewModels
{
    public class CarPublicViewModel
    {
        [Display(Name = "Marka")]
        public string Make { get; set; }

        [Display(Name = "Model")]
        public string Model { get; set; }

        [Display(Name = "Rok")]
        public int Year { get; set; }

        [Display(Name = "Status")]
        public CarStatus CarStatus { get; set; }

        [Display(Name = "Paliwo")]
        public FuelType FuelType { get; set; }

        [Display(Name = "Przebieg")]
        public int Mileage { get; set; }
    }
}
