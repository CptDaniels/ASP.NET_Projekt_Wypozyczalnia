using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Projekt_Wypozyczalnia.Models
{
    public class PozycjaWypozyczenia
    {
        [Key]
        public int IDWypozyczenia { get; set; }

        [ForeignKey("Klient")]
        public int IDKlienta { get; set; }
        public Klient Klient { get; set; }

        [ForeignKey("Samochod")]
        public int IDSamochodu { get; set; }
        public Samochod Samochod { get; set; }
        [Required]
        public DateTime DataWypozyczenia { get; set; }
        public DateTime? DataZwrotu { get; set; }
        [Required]
        public DateTime DataPrzewidywanaZwrotu { get; set; }
        [Required]
        public string StatusWynajmu { get; set; }
    }
}
