using ASP.NET_Projekt_Wypozyczalnia.Models;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Projekt_Wypozyczalnia.ViewModels
{
    public class ClientViewModel
    {
        public int ClientID { get; set; }

        [Display(Name = "Imię i Nazwisko")]
        public string FullName { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Telefon")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Dokument")]
        public string DocumentInfo { get; set; }

        [Display(Name = "Adres")]
        public string Address { get; set; }
    }
}