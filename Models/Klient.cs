using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Projekt_Wypozyczalnia.Models
{
    public class Klient
    {
        [Key]
        public int IDKlienta { get; set; }
        [Required]
        public string Imie { get; set; }
        [Required]
        public string Nazwisko { get; set; }
        public string Email { get; set; }
        [Required]
        public string Telefon { get; set; }
        [Required]
        public string NumerDokumentu { get; set; }
        [Required]
        public string TypDokumentu { get; set; }
        [Required]
        public string AdresZamieszkania { get; set; }
        //Nawigacja
        public ICollection<PozycjaWypozyczenia> PozycjeWypozyczenia { get; set; }
    }

}
