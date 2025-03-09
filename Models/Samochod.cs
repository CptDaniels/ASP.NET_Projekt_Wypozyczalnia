using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Projekt_Wypozyczalnia.Models
{
    public class Samochod
    {
        [Key]
        public int IDSamochodu { get; set; }
        [Required]
        public string Marka { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int Rok { get; set; }
        [Required]
        public string NumerRejestracyjny { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public int Przebieg { get; set; }
        public DateTime TerminPrzegladu { get; set; }
        [Required]
        public DateTime TerminUbezpieczenia { get; set; }
        [Required]
        public int PojemnoscSilnika { get; set; }
        [Required]
        public string RodzajPaliwa { get; set; }

        public ICollection<PozycjaWypozyczenia> PozycjeWypozyczenia { get; set; }
    }

}
