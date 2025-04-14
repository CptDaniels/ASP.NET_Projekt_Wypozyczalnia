using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Projekt_Wypozyczalnia.ViewModels
{
    public class CarViewModel
    {
        [Display(Name = "ID Samochodu")]
        public int CarID { get; set; }

        [Required(ErrorMessage = "Marka jest wymagana")]
        [Display(Name = "Marka")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Model jest wymagany")]
        [Display(Name = "Model")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Rok jest wymagany")]
        [Range(1900, 2100, ErrorMessage = "Rok musi być między 1900 a 2100")]
        [Display(Name = "Rok")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Numer rejestracyjny jest wymagany")]
        [Display(Name = "Numer Rejestracyjny")]
        public string RegistrationNumber { get; set; }

        [Required(ErrorMessage = "Status samochodu jest wymagany")]
        [Display(Name = "Status Samochodu")]
        public CarStatus CarStatus { get; set; }

        [Required(ErrorMessage = "Przebieg jest wymagany")]
        [Range(0, int.MaxValue, ErrorMessage = "Przebieg nie może być ujemny")]
        [Display(Name = "Przebieg")]
        public int Mileage { get; set; }

        [Display(Name = "Termin Przeglądu")]
        public DateTime InspectionDate { get; set; }

        [Required(ErrorMessage = "Termin ubezpieczenia jest wymagany")]
        [Display(Name = "Termin Ubezpieczenia")]
        public DateTime InsuranceDate { get; set; }

        [Required(ErrorMessage = "Pojemność silnika jest wymagana")]
        [Range(1, int.MaxValue, ErrorMessage = "Pojemność silnika musi być większa od 0")]
        [Display(Name = "Pojemność Silnika")]
        public int EngineCapacity { get; set; }

        [Required(ErrorMessage = "Rodzaj paliwa jest wymagany")]
        [Display(Name = "Rodzaj Paliwa")]
        public FuelType FuelType { get; set; }

        [Display(Name = "Zdjęcie Samochodu")]
        public string CarPicture { get; set; }

        [Display(Name = "Liczba Wypożyczeń")]
        public int RentalCount { get; set; }

    }
    public enum CarStatus
    {
        [Display(Name = "Sprawny")]
        Operational,

        [Display(Name = "W Naprawie")]
        UnderRepair,

        [Display(Name = "Uszkodzony")]
        Damaged,

        [Display(Name = "Niesprawny")]
        OutOfService
    }

    public enum FuelType
    {
        [Display(Name = "Olej napędowy")]
        Diesel,

        [Display(Name = "Benzyna")]
        Gasoline,

        [Display(Name = "Gaz LPG")]
        LPG,

        [Display(Name = "Elektryczny")]
        Electric,

        [Display(Name = "Gaz CNG")]
        CNG
    }
}