using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Projekt_Wypozyczalnia.Models
{
    public enum RentalStatus
    {
        [Display(Name = "Wypożyczony")]
        Rented,
        [Display(Name = "Zwrócony")]
        Returned,
    }

    public class RentalItems
    {
        [Key]
        public int RentalID { get; set; }
    
        [ForeignKey("Client")]
        public int ClientID { get; set; }
        public Client Client { get; set; }
    
        [ForeignKey("Car")]
        public int CarID { get; set; }
        public Car Car { get; set; }
    
        [Required]
        [Display(Name = "Data Wypożyczenia")]
        public DateTime RentalDate { get; set; }
    
        [Display(Name = "Data Zwrotu")]
        public DateTime? ReturnDate { get; set; }
    
        [Required]
        [Display(Name = "Data Przewidywana Zwrotu")]
        public DateTime ExpectedReturnDate { get; set; }
    
        [Required]
        [Display(Name = "Status Wynajmu")]
        public RentalStatus RentalStatus { get; set; }
    }
}
