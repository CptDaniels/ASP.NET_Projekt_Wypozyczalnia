using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Projekt_Wypozyczalnia.Models
{
    public enum DocumentType
    {
        [Display(Name = "Prawo jazdy")]
        DriverLicence,
        [Display(Name = "Dowód osobisty")]
        ID,
    }

    public class Client
    {
        public Client()
        {
            RentalItems = new List<RentalItems>();
        }
        [Key]
        public int ClientID { get; set; }

        [Required]
        [Display(Name = "Imie")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Numer dokumentu")]
        public string DocumentNumber { get; set; }

        [Required]
        [Display(Name = "Typ dokumentu")]
        public DocumentType DocumentType { get; set; }

        [Required]
        [Display(Name = "Adres Zamieszkania")]
        public string Address { get; set; }

        // Nawigacja
        public ICollection<RentalItems> RentalItems { get; set; }
    }

}
