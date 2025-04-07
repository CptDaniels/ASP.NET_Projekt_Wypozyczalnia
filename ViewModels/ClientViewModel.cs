using ASP.NET_Projekt_Wypozyczalnia.Models;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Projekt_Wypozyczalnia.ViewModels
{
    public enum DocumentType
    {
        [Display(Name = "Prawo jazdy")]
        DriverLicence,
        [Display(Name = "Dowód osobisty")]
        ID,
    }
    public class ClientViewModel
    {
        public int ClientID { get; set; }

        [Display(Name = "Imię i Nazwisko")]
        public string FullName { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Telefon")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Rodzaj Dokumentu")]
        public DocumentType DocumentType { get; set; }

        [Display(Name = "Numer Dokumentu")]
        public string DocumentNumber { get; set; }

        [Display(Name = "Rodzaj Dokumentu (Pełna Nazwa)")]
        public string DocumentTypeDisplay => DocumentType switch
        {
            DocumentType.DriverLicence => "Prawo jazdy",
            DocumentType.ID => "Dowód osobisty",
            _ => "Nieznany"
        };

        [Display(Name = "Dokument")]
        public string DocumentInfo => $"{DocumentTypeDisplay} - {DocumentNumber}";

        [Display(Name = "Adres")]
        public string Address { get; set; }

    }
   
}