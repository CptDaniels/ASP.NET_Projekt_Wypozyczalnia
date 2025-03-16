using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Projekt_Wypozyczalnia.Models
{
    public class Klient
    {
        [Key]
        public int IDKlienta { get; set; }
        [Required]
        [Display(Name ="Imie")]
        public string Imie { get; set; }
        [Required]
        [Display(Name = "Nazwisko")]
        public string Nazwisko { get; set; }
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Numer telefonu")]
        public string Telefon { get; set; }
        [Required]
        [Display(Name = "Numer dokumentu")]
        public string NumerDokumentu { get; set; }
        [Required]
        [Display(Name = "Typ dokumentu")]
        public string TypDokumentu { get; set; }
        [Required]
        [Display(Name = "Adres Zamieszkania")]
        public string AdresZamieszkania { get; set; }
        //Nawigacja
        public ICollection<PozycjaWypozyczenia> PozycjeWypozyczenia { get; set; }
    }

}
