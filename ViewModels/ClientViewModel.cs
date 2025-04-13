using ASP.NET_Projekt_Wypozyczalnia.Models;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Projekt_Wypozyczalnia.ViewModels
{
    public class ClientViewModel
    {
        public int ClientID { get; set; }

        [Required(ErrorMessage = "Imię i nazwisko jest wymagane.")]
        [StringLength(100, ErrorMessage = "Imię i nazwisko nie może przekraczać 100 znaków.")]
        [Display(Name = "Imię i Nazwisko")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Adres e-mail jest wymagany.")]
        [EmailAddress(ErrorMessage = "Podaj poprawny adres e-mail.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Numer telefonu jest wymagany.")]
        [Phone(ErrorMessage = "Podaj poprawny numer telefonu.")]
        [Display(Name = "Telefon")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Informacje o dokumencie są wymagane.")]
        [StringLength(50, ErrorMessage = "Informacje o dokumencie nie mogą przekraczać 50 znaków.")]
        [Display(Name = "Dokument")]
        public string DocumentInfo { get; set; }

        [Required(ErrorMessage = "Adres jest wymagany.")]
        [StringLength(200, ErrorMessage = "Adres nie może przekraczać 200 znaków.")]
        [Display(Name = "Adres")]
        public string Address { get; set; }

        public static ClientViewModel FromClient(Client client)
        {
            return new ClientViewModel
            {
                ClientID = client.ClientID,
                FullName = $"{client.FirstName} {client.LastName}",
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                DocumentInfo = $"{GetDocumentName(client.DocumentType)} - {client.DocumentNumber}",
                Address = client.Address
            };
        }

        private static string GetDocumentName(DocumentType documentType)
        {
            return documentType switch
            {
                DocumentType.ID => "Dowód osobisty",
                DocumentType.DriverLicence => "Prawo jazdy",
                _ => documentType.ToString()
            };
        }
    }
}