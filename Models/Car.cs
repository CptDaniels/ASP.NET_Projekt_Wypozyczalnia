using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Projekt_Wypozyczalnia.Models
{
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
    public class Car
    {
        [Key]
        public int CarID { get; set; }
    
        [Required]
        [Display(Name = "Marka")]
        public string Make { get; set; }
    
        [Required]
        [Display(Name = "Model")]
        public string Model { get; set; }
    
        [Required]
        [Display(Name = "Rok")]
        public int Year { get; set; }
    
        [Required]
        [Display(Name = "Numer Rejestracyjny")]
        public string RegistrationNumber { get; set; }
    
        [Required]
        [Display(Name = "Status Samochodu")]
        public CarStatus CarStatus { get; set; }
    
        [Required]
        [Display(Name = "Przebieg")]
        public int Mileage { get; set; }
    
        [Display(Name = "Termin Przeglądu")]
        public DateTime InspectionDate { get; set; }
    
        [Required]
        [Display(Name = "Termin Ubezpieczenia")]
        public DateTime InsuranceDate { get; set; }
    
        [Required]
        [Display(Name = "Pojemność Silnika")]
        public int EngineCapacity { get; set; }
    
        [Required]
        [Display(Name = "Rodzaj Paliwa")]
        public FuelType FuelType { get; set; }
        [Display(Name = "Zdjęcie Samochodu")]
        public string CarPicture { get; set; }
        //Nawigacja
    
        [Display(Name = "Pozycje Wypożyczenia")]
        public ICollection<RentalItems> RentalItems { get; set; }
    }

}
